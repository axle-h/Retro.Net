using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Retro.Net.Api.RealTime.Models
{
    internal class GameBoyClientContext
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the socket.
        /// </summary>
        /// <value>
        /// The socket.
        /// </value>
        public LockingWebSocket Socket { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public GameBoySocketClientState State { get; set; }

        /// <summary>
        /// Gets or sets the message queue.
        /// </summary>
        /// <value>
        /// The message queue.
        /// </value>
        public ConcurrentQueue<ICollection<GameBoyClientMessage>> MessageQueue { get; set; }
    }
}