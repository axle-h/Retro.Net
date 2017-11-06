using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using GameBoy.Net.Devices;
using GameBoy.Net.Graphics;
using LZ4;
using Microsoft.Extensions.Logging;
using Retro.Net.Api.RealTime.Interfaces;
using Retro.Net.Api.RealTime.Models;
using Retro.Net.Api.RealTime.Util;
using Retro.Net.Api.Validation;
using Retro.Net.Util;

namespace Retro.Net.Api.RealTime
{
    public class WebSocketRenderer : IWebSocketRenderer, IDisposable
    {
        private const int MaximumMessages = 50;
        private static readonly TimeSpan FrameLength = TimeSpan.FromMilliseconds(250);
        private static readonly TimeSpan ButtonPressLength = TimeSpan.FromMilliseconds(50);

        private readonly ConcurrentDictionary<Guid, GameBoyClientContext> _contexts;
        private readonly IFramedMessageHandler<GameBoySocketMessage> _messageHandler;
        private readonly IJoyPad _joyPad;
        private readonly IMessageBus _messageBus;
        private readonly DropOutStack<GameBoyClientMessage> _messageCache;
        private readonly ISet<string> _displayNames;
        private readonly CancellationTokenSource _disposing;
        private readonly ILogger _logger;
        private readonly byte[] _buffer;
        private byte[] _lastFrame;
        

        public WebSocketRenderer(ILoggerFactory loggerFactory, IFramedMessageHandler<GameBoySocketMessage> messageHandler, IJoyPad joyPad, IMessageBus messageBus)
        {
            _messageHandler = messageHandler;
            _joyPad = joyPad;
            _messageBus = messageBus;
            _messageCache = new DropOutStack<GameBoyClientMessage>(MaximumMessages);
            _contexts = new ConcurrentDictionary<Guid, GameBoyClientContext>();
            _displayNames = new HashSet<string>();
            _disposing = new CancellationTokenSource();
            _logger = loggerFactory.CreateLogger<WebSocketRenderer>();
            _buffer = new byte[LZ4Codec.MaximumOutputLength(Gpu.LcdWidth * Gpu.LcdHeight)];

            Task.Run(ReceiveFrameAsync, _disposing.Token);
        }

        public void Paint(Frame frame)
        {
            _lastFrame = frame.Buffer;
            var message = WebSocketMessageFactory.GpuFrame(frame.Buffer, _buffer);
            var tasks = _contexts.Values.Select(s => s.Socket.SendAsync(message)).ToArray();
            Task.WhenAll(tasks).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void UpdateMetrics(GpuMetrics gpuMetrics)
        {
            var tasks = _contexts.Values.Where(x => x.State.MetricsEnabled)
                                 .Select(c =>
                                         {
                                             var metrics = new GameBoyMetrics
                                                           {
                                                               FramesPerSecond = gpuMetrics.FramesPerSecond,
                                                               SkippedFrames = gpuMetrics.SkippedFrames,
                                                               Messages = c.MessageQueue.TakeAll().SelectMany(x => x).ToList()
                                                           };
                                             var message = WebSocketMessageFactory.Metrics(metrics);
                                             return c.Socket.SendAsync(message);
                                         })
                                 .ToList();
            Task.WhenAll(tasks).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task RenderToWebSocketAsync(WebSocket socket, CancellationToken token)
        {
            
            var id = Guid.NewGuid();
            try
            {
                using (var safeSocket = new LockingWebSocket(id, socket, _messageHandler, _logger, token))
                {
                    // Paint the current frame before adding.
                    if(_lastFrame != null)
                    {
                        var message = WebSocketMessageFactory.GpuFrame(_lastFrame);
                        await safeSocket.SendAsync(message).ConfigureAwait(false);
                    }

                    lock (_contexts)
                    {
                        if (_contexts.Count == 0)
                        {
                            _messageBus.SendMessage(Message.ResumeCpu);
                            _logger.LogInformation("Resuming CPU");
                        }

                        var context = new GameBoyClientContext
                                      {
                                          Id = id,
                                          Socket = safeSocket,
                                          State = new GameBoySocketClientState(),
                                          MessageQueue = new ConcurrentQueue<ICollection<GameBoyClientMessage>>()
                                      };
                        _contexts.TryAdd(id, context);
                        _logger.LogInformation($"Added socket: {id}, Connected sockets: {_contexts.Count}");
                    }
                   
                    await safeSocket.LifetimeTask.ConfigureAwait(false);
                }
            }
            finally
            {
                lock (_contexts)
                {
                    if (_contexts.TryRemove(id, out var context))
                    {
                        if (!string.IsNullOrEmpty(context.State.DisplayName))
                        {
                            AddMessage(context.State.DisplayName, "Left the game");
                            _displayNames.Remove(context.State.DisplayName);
                        }

                        if (_contexts.Count == 0)
                        {
                            _messageBus.SendMessage(Message.PauseCpu);
                            _logger.LogInformation($"Removed socket: {id}, Connected sockts: 0. Pausing CPU.");
                        }
                        else
                        {
                            _logger.LogInformation($"Removed socket: {id}, Connected sockts: {_contexts.Count}");
                        }
                    }
                }
            }
        }

        private async Task SendErrorAsync(GameBoyClientContext context, ValidationResult validation)
        {
            var error = new ErrorMessage { Reasons = validation.Errors.Select(e => e.ToString()).ToList() };
            _logger.LogDebug($"[{context.Id}] invalid message: {string.Join(", ", error.Reasons)}");
            var message = WebSocketMessageFactory.Error(error);
            await context.Socket.SendAsync(message).ConfigureAwait(false);
        }

        private void AddMessage(string user, string message)
        {
            _logger.LogDebug(message);
            
            var clientMessage = new GameBoyClientMessage {User = user, Date = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), Message = message};
            lock (_messageCache)
            {
                _messageCache.Push(clientMessage);
            }

            foreach (var context in _contexts.Values)
            {
                if (context.State.MetricsEnabled)
                {
                    context.MessageQueue.Enqueue(new[] {clientMessage});
                }
            }
        }

        private async Task ReceiveFrameAsync()
        {
            var validator = new GameBoySocketMessageValidator(_displayNames);
            while (!_disposing.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(ButtonPressLength).ConfigureAwait(false);
                    _joyPad.ReleaseAll();

                    await Task.Delay(FrameLength - ButtonPressLength).ConfigureAwait(false);

                    var messages = _messageHandler.GetNextMessageFrame()
                                                  .Where(x => !x.message.IsHeartBeat())
                                                  .GroupBy(x => x.socketId)
                                                  .Select(grp => grp.Aggregate((m0, m1) => (socketId: m0.socketId, message: m0.message.Merge(m1.message))))
                                                  .Join(_contexts, m => m.socketId, s => s.Key,
                                                        (m, s) => (context: s.Value, body: m.message, validation: validator.Validate(m.message)))
                                                  .ToList();

                    // Inform of invalid messages
                    var invalidMessageTasks = messages
                        .Where(x => !x.validation.IsValid)
                        .Select(x => SendErrorAsync(x.context, x.validation))
                        .ToList();

                    var validMessages = messages.Where(x => x.validation.IsValid).Select(x => (context: x.context, body: x.body)).ToList();

                    // Update state.
                    ICollection<GameBoyClientMessage> seedClientMessages;
                    lock (_messageCache)
                    {
                        seedClientMessages = _messageCache.ToList();
                    }

                    foreach (var (context, body) in validMessages)
                    {
                        var stateChanged = false;
                        if (body.EnableMetrics.HasValue && body.EnableMetrics.Value != context.State.MetricsEnabled)
                        {
                            context.State.MetricsEnabled = body.EnableMetrics.Value;
                            stateChanged = true;
                            _logger.LogDebug($"[{context.Id}] set metrics: {body.EnableMetrics}");

                            if (context.State.MetricsEnabled)
                            {
                                context.MessageQueue.Enqueue(seedClientMessages);
                            }
                            else
                            {
                                context.MessageQueue.Clear();
                            }
                        }

                        if (!string.IsNullOrEmpty(body.SetDisplayName) && !body.SetDisplayName.Equals(context.State.DisplayName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            // Re-validate as the display name could conflict.
                            var reValidation = await validator.ValidateAsync(body);
                            if (reValidation.IsValid)
                            {
                                _displayNames.Add(body.SetDisplayName);
                                context.State.DisplayName = body.SetDisplayName;
                                stateChanged = true;
                                AddMessage(body.SetDisplayName, "Joined the game");
                            }
                            else
                            {
                                await SendErrorAsync(context, reValidation).ConfigureAwait(false);
                            }
                        }

                        if (stateChanged)
                        {
                            var message = WebSocketMessageFactory.StateUpdate(context.State);
                            await context.Socket.SendAsync(message).ConfigureAwait(false);
                        }
                    }

                    // Press the most requested button.
                    var (button, name) = validMessages
                        .Where(x => !string.IsNullOrEmpty(x.context.State.DisplayName) && x.body.Button.HasValue)
                        .Select(x => (name: x.context.State.DisplayName, button: (JoyPadButton) x.body.Button.Value))
                        .GroupBy(x => x.button)
                        .OrderByDescending(grp => grp.Count())
                        .Select(grp => (button: grp.Key, name: grp.Select(x => x.name).First()))
                        .FirstOrDefault();

                    if (!string.IsNullOrEmpty(name))
                    {
                        _joyPad.PressOne(button);
                        AddMessage(name, $"Pressed {button}");
                    }

                    await Task.WhenAll(invalidMessageTasks).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    _logger.LogError(0, e, "Failed to receive frame");
                }
            }
        }

        public void Dispose()
        {
            lock (_contexts)
            {
                if (_disposing.IsCancellationRequested)
                {
                    return;
                }
                _disposing.Cancel();
                _disposing.Dispose();
            }

            foreach (var context in _contexts.Values)
            {
                context.Socket.Dispose();
            }
        }
    }
}
