using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using GameBoy.Net.Graphics;

namespace Retro.Net.Api.RealTime
{
    public interface IWebSocketsRenderer : IRenderer
    {
        Task RenderToWebSocketAsync(WebSocket socket, CancellationToken token);
    }
}