using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Retro.Net.Api.Infrastructure;
using Retro.Net.Api.RealTime;
using Retro.Net.Api.RealTime.Extensions;
using Retro.Net.Api.RealTime.Interfaces;
using Retro.Net.Api.Services.Interfaces;

namespace Retro.Net.Api.Controllers
{
    [WebSocketRoute]
    public class WebSocketController : Controller
    {
        private readonly IGameBoyContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public WebSocketController(IGameBoyContext context)
        {
            _context = context;
        }


        [HttpGet("gameboy")]
        public async Task<IActionResult> GameBoy()
        {
            var renderer = _context.GetRenderer() as IWebSocketRenderer ?? throw new InvalidOperationException("Cannot render to web sockets without the web socket renderer");
            return await HttpContext.WithWebSocketDo((ws, ct) => renderer.RenderToWebSocketAsync(ws, ct));
        }
    }
}