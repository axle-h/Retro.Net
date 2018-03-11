using System;
using System.Net.WebSockets;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Api.RealTime.Extensions;
using Retro.Net.Api.RealTime.Messages.Command;
using Retro.Net.Api.RealTime.Messages.Event;

namespace Retro.Net.Api.RealTime
{
    public class ReactiveWebSocket : ISubject<GameBoyEvent, GameBoyCommand>, IDisposable
    {
        private const int InitialBufferSize = 256;
        private const int BufferSizeIncrement = 64;

        private readonly SemaphoreSlim _semaphore;
        private readonly CancellationTokenSource _cancellation;
        private readonly WebSocket _socket;
        private bool _hasSubscriber;
        private bool _disposed;

        // We need 2 serializers as this impl is not thread safe.
        private readonly ProtoMessageSerializer _deSerializer;
        private readonly ProtoMessageSerializer _serializer;
        private byte[] _outputBuffer;

        public ReactiveWebSocket(WebSocket socket, CancellationToken token)
        {
            _socket = socket;
            _semaphore = new SemaphoreSlim(1, 1);

            _deSerializer = new ProtoMessageSerializer();
            _serializer = new ProtoMessageSerializer();
            _cancellation = CancellationTokenSource.CreateLinkedTokenSource(token);
        }

        public void OnCompleted() => Task.Run(CloseAsync);

        public void OnError(Exception error) => Task.Run(CloseAsync);

        public void OnNext(GameBoyEvent value) => Task.Run(() => OnNextAsync(value));

        public IDisposable Subscribe(IObserver<GameBoyCommand> observer)
        {
            lock (_cancellation)
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

            Task.Run(() => ReceiveAsync(observer), _cancellation.Token);
            return this;
        }

        private async Task ReceiveAsync(IObserver<GameBoyCommand> observer)
        {
            var buffer = new byte[InitialBufferSize];
            while (!_cancellation.IsCancellationRequested)
            {
                try
                {
                    var result = await _socket.ReceiveAsync(buffer, _cancellation.Token);
                    var length = result.Count;
                    while (!result.EndOfMessage)
                    {
                        if (length >= buffer.Length)
                        {
                            Array.Resize(ref buffer, buffer.Length + BufferSizeIncrement);
                        }

                        var segment = buffer.EndSegment(length);
                        result = await _socket.ReceiveAsync(segment, _cancellation.Token);
                        length += result.Count;
                    }

                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Binary:
                            var proto = _deSerializer.FromArraySegment(buffer.Segment(length), GameBoyCommand.Parser);
                            observer.OnNext(proto);
                            break;

                        case WebSocketMessageType.Text:
                            observer.OnNext(GameBoyCommand.Parser.ParseJson(Encoding.UTF8.GetString(buffer, 0, length)));
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

            await _semaphore.WaitAsync(_cancellation.Token);
            try
            {
                await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task OnNextAsync(GameBoyEvent value)
        {
            if (_cancellation.IsCancellationRequested)
            {
                // Do not throw.
                return;
            }

            await _semaphore.WaitAsync(_cancellation.Token);
            try
            {
                if (_socket.State == WebSocketState.Open)
                {
                    var message = _serializer.ToCompressedArraySegment(value, ref _outputBuffer);
                    await _socket.SendAsync(message, WebSocketMessageType.Binary, true, _cancellation.Token);
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
            lock (_cancellation)
            {
                if (_disposed)
                {
                    return;
                }
                _disposed = true;
            }

            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}