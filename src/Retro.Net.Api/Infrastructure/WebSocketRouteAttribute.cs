using Microsoft.AspNetCore.Mvc;

namespace Retro.Net.Api.Infrastructure
{
    /// <summary>
    /// Defines a web socket route.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RouteAttribute" />
    public class WebSocketRouteAttribute : RouteAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketRouteAttribute"/> class.
        /// </summary>
        /// <param name="template">The route template.</param>
        public WebSocketRouteAttribute(string template = null) : base($"{RouteConstants.WebSocketRootPath}/{(template ?? string.Empty).TrimStart('/').TrimEnd('/')}")
        {
        }
    }
}
