using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Retro.Net.Api.RealTime.Interfaces;

namespace Retro.Net.Api.RealTime
{
    /// <inheritdoc />
    /// <summary>
    /// A websocket handler that collects received messages into frames.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public class FramedMessageHandler<TMessage> : IFramedMessageHandler<TMessage>
    {
        private readonly ConcurrentQueue<(Guid id, TMessage message)> _messages;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FramedMessageHandler{TMessage}"/> class.
        /// </summary>
        public FramedMessageHandler(ILoggerFactory loggerFactory)
        {
            _messages = new ConcurrentQueue<(Guid, TMessage)>();
            _logger = loggerFactory.CreateLogger<FramedMessageHandler<TMessage>>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Called when [the websocket receives a new message with a text body].
        /// </summary>
        /// <param name="socketId">The socket identifier.</param>
        /// <param name="message">The message.</param>
        public void OnReceive(Guid socketId, string message) => _messages.Enqueue((socketId, JsonConvert.DeserializeObject<TMessage>(message)));

        /// <inheritdoc />
        /// <summary>
        /// Called when [the websocket receives a new message with a binary body].
        /// </summary>
        /// <param name="socketId">The socket identifier.</param>
        /// <param name="message">The message.</param>
        public void OnReceive(Guid socketId, ArraySegment<byte> message) =>
            _messages.Enqueue((socketId, MessagePackSerializer.Deserialize<TMessage>(message)));

        /// <summary>
        /// Gets all messages received by the websocket since the last time this method was called.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<(Guid socketId, TMessage message)> GetNextMessageFrame()
        {
            var length = _messages.Count;
            if (length == 0)
            {
                yield break;
            }

            for (var i = 0; i < length; i++)
            {
                if (_messages.TryDequeue(out var msg))
                {
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug(msg.ToString());
                    }

                    if (!Equals(msg, default(TMessage)))
                    {
                        yield return msg;
                    }
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}