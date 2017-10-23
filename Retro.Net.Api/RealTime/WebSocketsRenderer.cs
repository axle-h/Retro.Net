using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameBoy.Net.Devices;
using GameBoy.Net.Graphics;
using LZ4;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.Extensions.Logging;

namespace Retro.Net.Api.RealTime
{
    public class WebSocketsRenderer : IWebSocketsRenderer, IDisposable
    {
        private static readonly byte[] FrameHeader = Encoding.UTF8.GetBytes("GPU");
        private static readonly byte[] MettricsHeader = Encoding.UTF8.GetBytes("MTC");

        private readonly ConcurrentDictionary<Guid, LockingWebSocket> _sockets;
        private readonly CancellationTokenSource _disposeSource;
        private readonly byte[] _buffer;

        private readonly ILogger _logger;

        public WebSocketsRenderer(ILoggerFactory loggerFactory)
        {
            _sockets = new ConcurrentDictionary<Guid, LockingWebSocket>();
            _disposeSource = new CancellationTokenSource();
            _buffer = new byte[LZ4Codec.MaximumOutputLength(Gpu.LcdWidth * Gpu.LcdHeight)];
            _logger = loggerFactory.CreateLogger<WebSocketsRenderer>();
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
                using (var safeSocket = new LockingWebSocket(socket, _logger, token))
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
        
        public void Dispose()
        {
            lock (_disposeSource)
            {
                if (_disposeSource.IsCancellationRequested)
                {
                    return;
                }
                _disposeSource.Cancel();
            }

            foreach (var socket in _sockets.Values)
            {
                socket.Dispose();
            }
        }
    }
}
