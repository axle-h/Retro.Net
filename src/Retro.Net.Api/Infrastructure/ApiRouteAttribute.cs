using Microsoft.AspNetCore.Mvc;

namespace Retro.Net.Api.Infrastructure
{
    /// <summary>
    /// Defines an API route.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RouteAttribute" />
    public class ApiRouteAttribute : RouteAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiRouteAttribute"/> class.
        /// </summary>
        /// <param name="template">The route template.</param>
        public ApiRouteAttribute(string template = null) : base($"{RouteConstants.ApiRootPath}/{(template ?? string.Empty).TrimStart('/')}")
        {
        }
    }
}
