using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Retro.Net.Api.Infrastructure
{
    /// <summary>
    /// Middleware to catch 404's that are not on the API or Websocket paths and redirect to index.html so that Angular can sort out a route.
    /// </summary>
    public class AngularRoutesMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="AngularRoutesMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public AngularRoutesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes this middleware.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 404
                && !context.Request.Path.Value.StartsWith(RouteConstants.ApiRootPath, StringComparison.OrdinalIgnoreCase)
                && !context.Request.Path.Value.StartsWith(RouteConstants.WebSocketRootPath, StringComparison.OrdinalIgnoreCase))
            {
                context.Request.Path = "/index.html";
                context.Response.StatusCode = 200;

                // Re-run the fixed request.
                await _next(context);
            }
        }
    }
}
