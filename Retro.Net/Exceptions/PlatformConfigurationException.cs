using System;

namespace Retro.Net.Exceptions
{
    /// <summary>
    /// Platform configuration exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class PlatformConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformConfigurationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PlatformConfigurationException(string message) : base(message)
        {
        }
    }
}