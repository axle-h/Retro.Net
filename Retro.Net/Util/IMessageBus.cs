using System;

namespace Retro.Net.Util
{
    /// <summary>
    /// Simple messaging.
    /// </summary>
    public interface IMessageBus : IDisposable
    {
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendMessage(Message message);

        /// <summary>
        /// Registers a new message handler.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="handler">The handler.</param>
        void RegisterHandler(Message message, Action handler);
    }
}