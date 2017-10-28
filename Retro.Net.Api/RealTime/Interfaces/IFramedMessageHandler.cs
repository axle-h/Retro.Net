using System;
using System.Collections.Generic;

namespace Retro.Net.Api.RealTime.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// A websocket handler that collects received messages into frames.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <seealso cref="T:Retro.Net.Api.RealTime.Interfaces.IWebSocketMessageHandler" />
    public interface IFramedMessageHandler<TMessage> : IWebSocketMessageHandler
    {
        /// <summary>
        /// Gets all messages received by the websocket since the last time this method was called.
        /// </summary>
        /// <returns></returns>
        IEnumerable<(Guid socketId, TMessage message)> GetNextMessageFrame();
    }
}