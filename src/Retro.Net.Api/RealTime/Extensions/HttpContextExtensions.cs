using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Retro.Net.Api.RealTime.Extensions
{
    /// <summary>
    /// Extensions for the <see cref="HttpContext"/>.
    /// </summary>
    internal static class HttpContextExtensions
    {
        /// <summary>
        /// Sets up a websocket on the specified <see cref="HttpContext"/> and executes the speciifc function.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="f">The f.</param>
        /// <returns></returns>
        public static async Task<IActionResult> WithWebSocketDo(this HttpContext context, Func<WebSocket, CancellationToken, Task> f)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                return new BadRequestObjectResult("websockets only");
            }

            var token = context.RequestAborted;
            using (var socket = await context.WebSockets.AcceptWebSocketAsync())
            {
                await f(socket, token);
            }

            return new EmptyResult();
        }
    }
}
