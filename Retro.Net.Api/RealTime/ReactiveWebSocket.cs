using System;
using System.Linq;
using System.Net.WebSockets;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Api.RealTime.Interfaces;
using Retro.Net.Api.RealTime.Models;

namespace Retro.Net.Api.RealTime
{
    public class ReactiveWebSocket : ISubject<IWebSocketMessage, GameBoySocketMessage>, IDisposable
    {
        private const int InitialBufferSize = 256;
        private const int BufferSizeIncrement = 64;

        private readonly SemaphoreSlim _semaphore;
        private readonly CancellationTokenSource _disposing;
        private readonly WebSocket _socket;
        private readonly IWebSocketMessageSerializer _serializer;
        private bool _hasSubscriber;
        private bool _disposed;

        public ReactiveWebSocket(WebSocket socket, IWebSocketMessageSerializer serializer, CancellationToken token)
        {
            _socket = socket;
            _serializer = serializer;
            _disposing = new CancellationTokenSource();
            _semaphore = new SemaphoreSlim(1, 1);

            // this is a hack... linked token sources don't seem to work here. dunno if it's because the token is from MVC?
            token.Register(_disposing.Cancel);
        }

        public void OnCompleted() => Task.Run(CloseAsync);

        public void OnError(Exception error) => Task.Run(CloseAsync);

        public void OnNext(IWebSocketMessage value) => Task.Run(() => OnNextAsync(value));

        public IDisposable Subscribe(IObserver<GameBoySocketMessage> observer)
        {
            lock (_disposing)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(ReactiveWebSocket));
                }

                if (_hasSubscriber)
                {
                    throw new InvalidOperationException("Only a single subscriber supported per websocket");
                }
                _hasSubscriber = true;
            }

            Task.Run(() => ReceiveAsync(observer), _disposing.Token);
            return this;
        }

        private async Task ReceiveAsync(IObserver<GameBoySocketMessage> observer)
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

                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Binary:
                            observer.OnNext(_serializer.DeSerialize(buffer.Segment(length)));
                            break;

                        case WebSocketMessageType.Text:
                            observer.OnNext(_serializer.DeSerialize(Encoding.UTF8.GetString(buffer, 0, length)));
                            break;

                        case WebSocketMessageType.Close:
                            await CloseAsync();
                            observer.OnCompleted();
                            return;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (OperationCanceledException)
                {
                    observer.OnCompleted();
                    break;
                }
                catch (Exception e)
                {
                    observer.OnError(e);
                    break;
                }
            }
        }
        
        private async Task CloseAsync()
        {
            if (_socket.State == WebSocketState.Closed || _socket.State == WebSocketState.CloseSent)
            {
                return;
            }

            await _semaphore.WaitAsync(_disposing.Token);
            try
            {
                await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task OnNextAsync(IWebSocketMessage value)
        {
            var messages = value.Serialize().ToArray();
            if (_disposing.IsCancellationRequested)
            {
                // Do not throw.
                return;
            }

            await _semaphore.WaitAsync(_disposing.Token);
            try
            {
                if (_socket.State == WebSocketState.Open)
                {
                    for (var i = 0; i < messages.Length; i++)
                    {
                        var isLast = i == messages.Length - 1;
                        await _socket.SendAsync(messages[i], WebSocketMessageType.Binary, isLast, _disposing.Token);
                    }
                }
                else if (_socket.State == WebSocketState.CloseReceived)
                {
                    await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Dispose()
        {
            lock (_disposing)
            {
                if (_disposed)
                {
                    return;
                }
                _disposed = true;
            }

            _disposing.Cancel();
            _disposing.Dispose();
        }
    }
}