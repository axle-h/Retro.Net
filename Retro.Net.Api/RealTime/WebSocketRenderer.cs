using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GameBoy.Net.Devices;
using GameBoy.Net.Graphics;
using LZ4;
using Microsoft.Extensions.Logging;
using Retro.Net.Api.RealTime.Interfaces;
using Retro.Net.Api.RealTime.Models;
using Retro.Net.Api.Validation;
using Retro.Net.Util;

namespace Retro.Net.Api.RealTime
{
    public class WebSocketRenderer : IWebSocketRenderer, IDisposable
    {
        private static readonly TimeSpan MinimumHeartbeatInterval = TimeSpan.FromSeconds(30);
        private const int MaximumMessages = 20;
        private static readonly TimeSpan FrameLength = TimeSpan.FromMilliseconds(250);
        private static readonly TimeSpan ButtonPressLength = TimeSpan.FromMilliseconds(50);

        private readonly ISubject<IWebSocketMessage> _metricsSubject;
        private readonly ISubject<IWebSocketMessage> _eventsSubject;
        private readonly ISubject<IWebSocketMessage> _frameSubject;
        private readonly ISubject<(string name, JoyPadButton button)> _joyPadSubject;

        private readonly IValidator<GameBoySocketMessage> _validator;
        private readonly IWebSocketMessageSerializer _serializer;
        private readonly IMessageBus _messageBus;
        private readonly IDisposable _joyPadSubscription;
        private readonly ISet<string> _displayNames;
        private readonly ILogger _logger;
        private readonly byte[] _buffer;

        private int _connectedSockets;

        public WebSocketRenderer(ILoggerFactory loggerFactory, IJoyPad joyPad, IMessageBus messageBus, IWebSocketMessageSerializer serializer)
        {
            _messageBus = messageBus;
            _serializer = serializer;
            _displayNames = new HashSet<string>();
            _logger = loggerFactory.CreateLogger<WebSocketRenderer>();
            _buffer = new byte[LZ4Codec.MaximumOutputLength(Gpu.LcdWidth * Gpu.LcdHeight)];

            _validator = new GameBoySocketMessageValidator(_displayNames);

            _metricsSubject = new ReplaySubject<IWebSocketMessage>(1);
            _eventsSubject = new ReplaySubject<IWebSocketMessage>(MaximumMessages);
            _frameSubject = new ReplaySubject<IWebSocketMessage>(1);
            _joyPadSubject = new Subject<(string name, JoyPadButton button)>();

            // Press the most requested button.
            _joyPadSubscription = _joyPadSubject
                .Buffer(FrameLength)
                .Where(x => x.Any())
                .Subscribe(presses =>
                           {
                               var (button, name) = presses
                                   .Where(x => !string.IsNullOrEmpty(x.name))
                                   .GroupBy(x => x.button)
                                   .OrderByDescending(grp => grp.Count())
                                   .Select(grp => (button: grp.Key, name: grp.Select(x => x.name).First()))
                                   .FirstOrDefault();
                               joyPad.PressOne(button);
                               Publish(name, $"Pressed {button}");

                               Thread.Sleep(ButtonPressLength);
                               joyPad.ReleaseAll();
                           });
        }

        public void Paint(Frame frame)
        {
            var message = WebSocketMessageFactory.GpuFrame(frame.Buffer, _buffer);
            _frameSubject.OnNext(message);
        }

        public void UpdateMetrics(GpuMetrics gpuMetrics)
        {
            var metrics = new GameBoyMetrics
                          {
                              FramesPerSecond = gpuMetrics.FramesPerSecond,
                              SkippedFrames = gpuMetrics.SkippedFrames
                          };
            var message = WebSocketMessageFactory.Metrics(metrics);
            _metricsSubject.OnNext(message);
        }
        
        public async Task RenderToWebSocketAsync(WebSocket socket, CancellationToken token)
        {
            var id = Guid.NewGuid();
            var state = new GameBoySocketClientState();

            var connectedSockets = Interlocked.Increment(ref _connectedSockets);
            if (connectedSockets == 1)
            {
                _messageBus.SendMessage(Message.ResumeCpu);
                _logger.LogInformation("Resuming CPU");
            }
            _logger.LogInformation($"Added socket: {id}, Connected sockets: {connectedSockets}");

            try
            {
                using (var reactiveSocket = new ReactiveWebSocket(socket, _serializer, token))
                using (var subscriptions = new CompositeDisposable())
                {
                    var observer = reactiveSocket.AsObserver();
                    
                    // Paint to the socket.
                    subscriptions.Add(_frameSubject.Subscribe(observer.OnNext));

                    // Listen for client messages.
                    IDisposable metricsSubscription = null;
                    var tcs = new TaskCompletionSource<bool>();
                    var subscription = reactiveSocket
                        .Timeout(MinimumHeartbeatInterval)
                        .Subscribe(m =>
                                   {
                                       if (m.IsHeartBeat())
                                       {
                                           return;
                                       }

                                       var validation = _validator.Validate(m);
                                       if (!validation.IsValid)
                                       {
                                           var error = new ErrorMessage { Reasons = validation.Errors.Select(e => e.ToString()).ToList() };
                                           _logger.LogDebug($"[{id}] invalid message: {string.Join(", ", error.Reasons)}");
                                           var message = WebSocketMessageFactory.Error(error);
                                           observer.OnNext(message);
                                           return;
                                       }

                                       if (state.StateChanged(m))
                                       {
                                           if (state.MetricsEnabledChanged(m))
                                           {
                                               if (m.EnableMetrics.GetValueOrDefault())
                                               {
                                                   metricsSubscription = new CompositeDisposable(_metricsSubject.Subscribe(observer.OnNext),
                                                                                                 _eventsSubject.Subscribe(observer.OnNext));
                                                   subscriptions.Add(metricsSubscription);
                                               }
                                               else if (metricsSubscription != null)
                                               {
                                                   subscriptions.Remove(metricsSubscription);
                                               }
                                           }

                                           state.Update(m);
                                           var stateMessage = WebSocketMessageFactory.StateUpdate(state);
                                           observer.OnNext(stateMessage);
                                       }

                                       if (!string.IsNullOrEmpty(state.DisplayName) && m.Button.HasValue)
                                       {
                                           _joyPadSubject.OnNext((state.DisplayName, (JoyPadButton) m.Button.Value));
                                       }
                                   }, e => tcs.SetException(e), () => tcs.SetResult(true));
                    
                    subscriptions.Add(subscription);
                    await tcs.Task;
                }
            }
            finally
            {
                connectedSockets = Interlocked.Decrement(ref _connectedSockets);
                if (!string.IsNullOrEmpty(state.DisplayName))
                {
                    Publish(state.DisplayName, "Left the game");
                    _displayNames.Remove(state.DisplayName);
                }

                if (connectedSockets == 0)
                {
                    _messageBus.SendMessage(Message.PauseCpu);
                    _logger.LogInformation($"Removed socket: {id}, Connected sockets: 0. Pausing CPU.");
                }
                else
                {
                    _logger.LogInformation($"Removed socket: {id}, Connected sockets: {connectedSockets}");
                }
            }
        }

        private void Publish(string user, string message)
        {
            _logger.LogDebug(message);
            var clientMessage = new GameBoyClientMessage {User = user, Date = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), Message = message};
            var webSocketMessage = WebSocketMessageFactory.ClientMessage(clientMessage);
            _eventsSubject.OnNext(webSocketMessage);
        }
        
        public void Dispose()
        {
            _joyPadSubscription?.Dispose();
        }
    }
}
