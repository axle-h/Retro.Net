using System;

namespace Retro.Net.Api.RealTime.Interfaces
{
    /// <summary>
    /// Handles websocket messages.
    /// </summary>
    public interface IWebSocketMessageHandler
    {
        /// <summary>
        /// Called when [the websocket receives a new message with a text body].
        /// </summary>
        /// <param name="socketId">The socket identifier.</param>
        /// <param name="message">The message.</param>
        void OnReceive(Guid socketId, string message);

        /// <summary>
        /// Called when [the websocket receives a new message with a binary body].
        /// </summary>
        /// <param name="socketId">The socket identifier.</param>
        /// <param name="message">The message.</param>
        void OnReceive(Guid socketId, ArraySegment<byte> message);
    }
}