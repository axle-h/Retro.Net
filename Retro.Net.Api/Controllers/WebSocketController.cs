using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Retro.Net.Api.RealTime;
using Retro.Net.Api.RealTime.Interfaces;

namespace Retro.Net.Api.Controllers
{
    [Route(Startup.WebSocketRootPath)]
    public class WebSocketController : Controller
    {
        private readonly IWebSocketContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public WebSocketController(IWebSocketContext context)
        {
            _context = context;
        }


        [Route("gameboy")]
        public async Task<IActionResult> GameBoy()
        {
            var renderer = _context.GetRenderer(Guid.Empty);
            return await HttpContext.WithWebSocketDo((ws, ct) => renderer.RenderToWebSocketAsync(ws, ct));
        }
    }
}