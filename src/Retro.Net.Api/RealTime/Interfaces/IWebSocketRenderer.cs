using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using GameBoy.Net.Devices.Interfaces;

namespace Retro.Net.Api.RealTime.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// A Gameboy GPU renderer that renders frames out to connected websockets.
    /// This sends binary encoded frames and metrics to the client and receives joypad button presses.
    /// Messages are received in either msgpack when binary or JSON when in text.
    /// </summary>
    /// <seealso cref="T:GameBoy.Net.Graphics.IRenderer" />
    public interface IWebSocketRenderer : IRenderer
    {
        /// <summary>
        /// Adds a websocket to the renderer and blocks until either the specified cancellation token is cancelled or the client clsoes the websocket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task RenderToWebSocketAsync(WebSocket socket, CancellationToken token);
    }
}