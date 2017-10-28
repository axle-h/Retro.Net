using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameBoy.Net.Devices;
using GameBoy.Net.Graphics;
using GameBoy.Net.Peripherals;
using LZ4;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.Extensions.Logging;
using Retro.Net.Api.RealTime.Interfaces;

namespace Retro.Net.Api.RealTime
{
    public class WebSocketRenderer : IWebSocketRenderer, IDisposable
    {
        private static readonly TimeSpan FrameLength = TimeSpan.FromMilliseconds(250);
        private static readonly TimeSpan ButtonPressLength = TimeSpan.FromMilliseconds(50);

        private static readonly byte[] FrameHeader = Encoding.UTF8.GetBytes("GPU");
        private static readonly byte[] MettricsHeader = Encoding.UTF8.GetBytes("MTC");

        private readonly ConcurrentDictionary<Guid, LockingWebSocket> _sockets;
        private readonly IFramedMessageHandler<GameBoySocketMessage> _messageHandler;
        private readonly IJoyPad _joyPad;
        private readonly CancellationTokenSource _disposing;
        private readonly byte[] _buffer;

        private readonly ILogger _logger;

        public WebSocketRenderer(ILoggerFactory loggerFactory, IFramedMessageHandler<GameBoySocketMessage> messageHandler, IJoyPad joyPad)
        {
            _messageHandler = messageHandler;
            _joyPad = joyPad;
            _sockets = new ConcurrentDictionary<Guid, LockingWebSocket>();
            _disposing = new CancellationTokenSource();
            _buffer = new byte[LZ4Codec.MaximumOutputLength(Gpu.LcdWidth * Gpu.LcdHeight)];
            _logger = loggerFactory.CreateLogger<WebSocketRenderer>();

            Task.Run(ReceiveFrameAsync, _disposing.Token);
        }

        public void Paint(Frame frame)
        {
            var length = LZ4Codec.Encode(frame.Buffer, 0, frame.Buffer.Length, _buffer, 0, _buffer.Length);
            var result = new ArraySegment<byte>(_buffer, 0, length);
            var tasks = _sockets.Values.Select(s => s.SendAsync(FrameHeader, result)).ToArray();
            Task.WhenAll(tasks).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void UpdateMetrics(GpuMetrics metrics)
        {
            var message = MessagePackSerializer.Serialize(metrics, ContractlessStandardResolver.Instance);
            var tasks = _sockets.Values.Select(s => s.SendAsync(MettricsHeader, message)).ToArray();
            Task.WhenAll(tasks).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task RenderToWebSocketAsync(WebSocket socket, CancellationToken token)
        {
            var id = Guid.NewGuid();
            try
            {
                using (var safeSocket = new LockingWebSocket(id, socket, _messageHandler, _logger, token))
                {
                    // Paint the current frame before adding.
                    await safeSocket.SendAsync(FrameHeader, _buffer).ConfigureAwait(false);

                    _sockets.TryAdd(id, safeSocket);
                    _logger.LogInformation($"Added socket: {id}, Connected sockts: {_sockets.Count}");

                    await safeSocket.LifetimeTask.ConfigureAwait(false);
                }
            }
            finally
            {
                if (_sockets.TryRemove(id, out _))
                {
                    _logger.LogInformation($"Removed socket: {id}, Connected sockts: {_sockets.Count}");
                }
            }
        }

        private async Task ReceiveFrameAsync()
        {
            while (!_disposing.IsCancellationRequested)
            {
                await Task.Delay(ButtonPressLength).ConfigureAwait(false);
                _joyPad.ReleaseAll();

                await Task.Delay(FrameLength - ButtonPressLength).ConfigureAwait(false);

                // Press the most requested button.
                var (button, ids) = _messageHandler.GetNextMessageFrame()
                                                   .Where(x => x.message != null && Enum.IsDefined(typeof(JoyPadButton), x.message.Button))
                                                   .Select(x => (id: x.socketId, button: (JoyPadButton) x.message.Button))
                                                   .GroupBy(x => x.id)
                                                   .Select(grp => (id: grp.Last().id, button: grp.Last().button))
                                                   .GroupBy(x => x.button)
                                                   .OrderByDescending(grp => grp.Count())
                                                   .Select(grp => (button: grp.Key, ids: grp.Select(x => x.id).ToList()))
                                                   .FirstOrDefault();
                if (ids != null)
                {
                    _joyPad.PressOne(button);
                    _logger.LogInformation($"[{string.Join(", ", ids)}] pressed {button}");
                }
            }
        }

        public void Dispose()
        {
            lock (_sockets)
            {
                if (_disposing.IsCancellationRequested)
                {
                    return;
                }
                _disposing.Cancel();
                _disposing.Dispose();
            }

            foreach (var socket in _sockets.Values)
            {
                socket.Dispose();
            }
        }

        
    }
}
