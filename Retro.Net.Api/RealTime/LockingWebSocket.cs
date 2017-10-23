using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Retro.Net.Api.RealTime
{
    internal class LockingWebSocket : IDisposable
    {
        private static readonly TimeSpan MinimumHeartbeatInterval = TimeSpan.FromSeconds(30);
        private const string HeartbeatMessage = "heartbeat";
        private const int InitialBufferSize = 256;
        private const int BufferSizeIncrement = 64;

        private readonly SemaphoreSlim _semaphore;
        private readonly WebSocket _socket;
        private readonly CancellationTokenSource _disposing;
        private readonly Stopwatch _timeSinceLastHeartbeat;
        private readonly TaskCompletionSource<bool> _lifetime;
        private readonly ILogger _logger;

        public LockingWebSocket(WebSocket socket, ILogger logger, CancellationToken token)
        {
            _socket = socket;
            _logger = logger;
            _semaphore = new SemaphoreSlim(1, 1);
            _timeSinceLastHeartbeat = Stopwatch.StartNew();
            _disposing = new CancellationTokenSource();
            Task.Run(ReceiveAsync, _disposing.Token);
            Task.Run(async () =>
                     {
                         while (!_disposing.Token.IsCancellationRequested)
                         {
                             await Task.Delay(MinimumHeartbeatInterval - _timeSinceLastHeartbeat.Elapsed + TimeSpan.FromSeconds(1)).ConfigureAwait(false);

                             if (_timeSinceLastHeartbeat.Elapsed > MinimumHeartbeatInterval)
                             {
                                 _logger.LogInformation($"Received no heartbeat for {_timeSinceLastHeartbeat.Elapsed}, closing websocket");
                                 await CloseNowAsync().ConfigureAwait(false);
                                 return;
                             }
                         }
                     }, _disposing.Token);

            _lifetime = new TaskCompletionSource<bool>();
            token.Register(() => _lifetime.TrySetResult(true));
        }

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
            
        private async Task ReceiveAsync()
        {
            var buffer = new byte[InitialBufferSize];
            while (!_disposing.Token.IsCancellationRequested)
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

                        var segment = new ArraySegment<byte>(buffer, length, buffer.Length - length);
                        result = await _socket.ReceiveAsync(segment, _disposing.Token);
                        length += result.Count;
                    }

                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Binary:
                            throw new NotSupportedException("TODO: Binary messages");
                        case WebSocketMessageType.Close:
                            await CloseNowAsync().ConfigureAwait(false);
                            return;
                        case WebSocketMessageType.Text:
                            HandleTextMessage(buffer, length);
                            break;
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

        private void HandleTextMessage(byte[] buffer, int length)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, length);
            if (message.Trim().Equals(HeartbeatMessage, StringComparison.OrdinalIgnoreCase))
            {
                _timeSinceLastHeartbeat.Reset();
            }
            else
            {
                throw new NotSupportedException("Unknown message: " + message);
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