using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Retro.Net.Util
{
    /// <inheritdoc />
    /// <summary>
    /// Simple messaging.
    /// </summary>
    /// <seealso cref="T:Retro.Net.Util.IMessageBus" />
    public class MessageBus : IMessageBus
    {
        private readonly ConcurrentDictionary<Message, List<Action>> _handlers;
        private readonly BlockingCollection<Message> _messages;
        private readonly CancellationTokenSource _diposing;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBus"/> class.
        /// </summary>
        public MessageBus()
        {
            _handlers = new ConcurrentDictionary<Message, List<Action>>();
            _messages = new BlockingCollection<Message>();
            _diposing = new CancellationTokenSource();

            Task.Run(ReceiveAsync, _diposing.Token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(Message message)
        {
            if (!_handlers.TryGetValue(message, out var handlers))
            {
                return;
            }

            foreach (var handler in handlers)
            {
                handler();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Registers a new message handler.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="handler">The handler.</param>
        public void RegisterHandler(Message message, Action handler) =>
            _handlers.AddOrUpdate(message, m => new List<Action> { handler },
                                  (m, actions) =>
                                  {
                                      actions.Add(handler);
                                      return actions;
                                  });

        private async Task ReceiveAsync()
        {
            while (!_diposing.IsCancellationRequested)
            {
                try
                {
                    var message = _messages.Take(_diposing.Token);
                    if (!_handlers.TryGetValue(message, out var handlers))
                    {
                        continue;
                    }

                    await Task.WhenAll(handlers.Select(Task.Run));
                }
                catch (Exception)
                {
                    // Soak this up.
                }
            }
        }

        public void Dispose()
        {
            lock (_handlers)
            {
                if (_disposed)
                {
                    return;
                }

                _disposed = true;
            }

            _diposing.Cancel();
            _diposing.Dispose();
            _messages.Dispose();
        }
    }
}
