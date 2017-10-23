using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Retro.Net.Api.RealTime;

namespace Retro.Net.Api.Controllers
{
    [Route(Startup.WebSocketRootPath)]
    public class WebSocketController : Controller
    {
        private readonly IWebSocketsRenderer _renderer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketController"/> class.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public WebSocketController(IWebSocketsRenderer renderer)
        {
            _renderer = renderer;
        }

        [Route("gameboy")]
        public async Task<IActionResult> GameBoy() =>
            await HttpContext.WithWebSocketDo((ws, ct) => _renderer.RenderToWebSocketAsync(ws, ct)).ConfigureAwait(false);
    }
}