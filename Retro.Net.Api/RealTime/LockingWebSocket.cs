using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Retro.Net.Api.RealTime.Interfaces;

namespace Retro.Net.Api.RealTime
{
    /// <inheritdoc />
    /// <summary>
    /// A wrapper for a websocket that handles:
    /// * Safe closing of the socket.
    /// * Send message locking.
    /// * Receive message events via the provided <see cref="IWebSocketMessageHandler"/>.
    /// * Manages a heartbat mechanism where if the client doesn't send a emssage for a pre-determined time then teh socket will be closed.
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    internal class LockingWebSocket : IDisposable
    {
        private static readonly TimeSpan MinimumHeartbeatInterval = TimeSpan.FromSeconds(30);
        private const int InitialBufferSize = 256;
        private const int BufferSizeIncrement = 64;

        private readonly SemaphoreSlim _semaphore;
        private readonly Guid _id;
        private readonly WebSocket _socket;
        private readonly IWebSocketMessageHandler _handler;
        private readonly CancellationTokenSource _disposing;
        private readonly Stopwatch _timeSinceLastMessage;
        private readonly TaskCompletionSource<bool> _lifetime;
        private readonly ILogger _logger;

        public LockingWebSocket(Guid id, WebSocket socket, IWebSocketMessageHandler handler, ILogger logger, CancellationToken token)
        {
            _id = id;
            _socket = socket;
            _handler = handler;
            _logger = logger;
            _semaphore = new SemaphoreSlim(1, 1);
            _timeSinceLastMessage = Stopwatch.StartNew();
            _disposing = new CancellationTokenSource();
            _lifetime = new TaskCompletionSource<bool>();

            Task.Run(ReceiveAsync, _disposing.Token);
            Task.Run(HeartbeatAsync, _disposing.Token);

            // this is a hack... linked token sources don't seem to work here. dunno if it's because the token is from MVC?
            token.Register(() => _lifetime.SetResult(true));
        }

        /// <summary>
        /// Gets a task that will complete when the socket has either closed or failed.
        /// You should run <see cref="Dispose"/> after this task has completed.
        /// </summary>
        /// <value>
        /// The lifetime task.
        /// </value>
        public Task LifetimeTask => _lifetime.Task;

        public bool IsClosed { get; private set; }

        public async Task SendAsync(params ArraySegment<byte>[] messages) =>
            await WithSocketAsync(async s =>
                                  {
                                      for (var i = 0; i < messages.Length; i++)
                                      {
                                          var isLast = i == messages.Length - 1;
                                          await s.SendAsync(messages[i], WebSocketMessageType.Binary, isLast, _disposing.Token).ConfigureAwait(false);
                                      }
                                  }).ConfigureAwait(false);

        private async Task HeartbeatAsync()
        {
            while (!_disposing.IsCancellationRequested)
            {
                await Task.Delay(MinimumHeartbeatInterval - _timeSinceLastMessage.Elapsed + TimeSpan.FromSeconds(1)).ConfigureAwait(false);

                if (_timeSinceLastMessage.Elapsed > MinimumHeartbeatInterval)
                {
                    _logger.LogInformation($"Received no message from client for {_timeSinceLastMessage.Elapsed}, closing websocket {_id}");
                    await CloseNowAsync().ConfigureAwait(false);
                    return;
                }
            }
        }

        private async Task ReceiveAsync()
        {
            var buffer = new byte[InitialBufferSize];
            while (!_disposing.IsCancellationRequested)
            {
                try
                {
                    var result = await _socket.ReceiveAsync(buffer, _disposing.Token);
                    var length = result.Count;
                    while (!result.EndOfMessage)
                    {
                        if (length >= buffer.Length)
                        {
                            Array.Resize(ref buffer, buffer.Length + BufferSizeIncrement);
                        }

                        var segment = buffer.EndSegment(length);
                        result = await _socket.ReceiveAsync(segment, _disposing.Token);
                        length += result.Count;
                    }

                    _timeSinceLastMessage.Reset();

                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Binary:
                            _handler.OnReceive(_id, buffer.Segment(length));
                            break;
                        case WebSocketMessageType.Text:
                            _handler.OnReceive(_id, Encoding.UTF8.GetString(buffer, 0, length));
                            break;
                        case WebSocketMessageType.Close:
                            await CloseNowAsync().ConfigureAwait(false);
                            return;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(0, e, "Receive message failed");
                }
            }
        }

        private async Task CloseNowAsync() => await WithSocketAsync(async s =>
                                                                    {
                                                                        await s.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, CancellationToken.None).ConfigureAwait(false);
                                                                        IsClosed = true;
                                                                    }).ConfigureAwait(false);
        
        private async Task WithSocketAsync(Func<WebSocket, Task> f)
        {
            if (_disposing.IsCancellationRequested)
            {
                // Do not throw.
                return;
            }

            await _semaphore.WaitAsync(_disposing.Token).ConfigureAwait(false);
            try
            {
                if (_socket.State != WebSocketState.Open)
                {
                    if (!IsClosed)
                    {
                        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, CancellationToken.None).ConfigureAwait(false);
                        IsClosed = true;
                    }
                }
                else
                {
                    await f(_socket).ConfigureAwait(false);
                }

            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Dispose()
        {
            // Need a lock here because we're diposing teh semaphore.
            lock (_semaphore)
            {
                if (_disposing.IsCancellationRequested)
                {
                    return;
                }

                DisposeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                _disposing.Cancel();
                _lifetime.TrySetResult(true);
            }
        }

        private async Task DisposeAsync()
        {
            await CloseNowAsync().ConfigureAwait(false);
            _socket.Dispose();
        }
    }
}