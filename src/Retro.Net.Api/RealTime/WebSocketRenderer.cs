using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GameBoy.Net.Devices;
using GameBoy.Net.Devices.Graphics.Models;
using GameBoy.Net.Devices.Interfaces;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Retro.Net.Api.RealTime.Interfaces;
using Retro.Net.Api.RealTime.Messages.Command;
using Retro.Net.Api.RealTime.Messages.Event;
using Retro.Net.Api.Validation;
using Retro.Net.Util;
using Retro.Net.Z80.Core;

namespace Retro.Net.Api.RealTime
{
    public class WebSocketRenderer : IWebSocketRenderer, IDisposable
    {
        private static readonly TimeSpan MinimumHeartbeatInterval = TimeSpan.FromSeconds(30);
        private const int MaximumMessages = 20;
        private static readonly TimeSpan FrameLength = TimeSpan.FromMilliseconds(250);
        private static readonly TimeSpan ButtonPressLength = TimeSpan.FromMilliseconds(50);

        private readonly ISubject<GameBoyEvent> _eventsSubject;
        private readonly ISubject<GameBoyEvent> _frameSubject;
        private readonly ISubject<(string name, JoyPadButton button)> _joyPadSubject;

        private readonly IValidator<GameBoyCommand> _validator;
        private readonly IMessageBus _messageBus;
        private readonly IDisposable _joyPadSubscription;
        private readonly ISet<string> _displayNames;
        private readonly ILogger _logger;

        private GpuMetrics _lastGpuMetrics;

        private int _connectedSockets;

        public WebSocketRenderer(ILoggerFactory loggerFactory,
                                 IJoyPad joyPad,
                                 IMessageBus messageBus)
        {
            _messageBus = messageBus;
            _displayNames = new HashSet<string>();
            _logger = loggerFactory.CreateLogger<WebSocketRenderer>();

            _validator = new GameBoyCommandValidator(_displayNames);

            _eventsSubject = new ReplaySubject<GameBoyEvent>(MaximumMessages);
            _frameSubject = new ReplaySubject<GameBoyEvent>(1);
            _joyPadSubject = new Subject<(string name, JoyPadButton button)>();

            // Press the most requested button.
            _joyPadSubscription = _joyPadSubject
                                  .Buffer(FrameLength)
                                  .Where(x => x.Any())
                                  .ObserveOn(Scheduler.Default)
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
            var message = new GameBoyEvent
                          {
                              Frame = new GameBoyGpuFrame
                                      {
                                          Data = ByteString.CopyFrom(frame.Buffer), // TODO: find a way to avoid this copy.
                                          FramesPerSecond = _lastGpuMetrics.FramesPerSecond
                                      }
                          };
            _frameSubject.OnNext(message);
        }

        public void UpdateMetrics(GpuMetrics gpuMetrics) => _lastGpuMetrics = gpuMetrics;

        public async Task RenderToWebSocketAsync(WebSocket socket, CancellationToken token)
        {
            var id = Guid.NewGuid();
            var state = new GameBoyClientState();
            
            var connectedSockets = Interlocked.Increment(ref _connectedSockets);
            if (connectedSockets == 1)
            {
                _messageBus.FireAndForget(CpuCoreMessages.ResumeCpu);
                _logger.LogInformation("Resuming CPU");
            }
            _logger.LogInformation($"Added socket: {id}, Connected sockets: {connectedSockets}");

            try
            {
                using (var reactiveSocket = new ReactiveWebSocket(socket, token))
                using (var subscriptions = new CompositeDisposable())
                {
                    var observer = reactiveSocket.AsObserver();

                    // Forward server messages.
                    _eventsSubject.Subscribe(observer);

                    // Paint to the socket.
                    subscriptions.Add(_frameSubject.ObserveOn(Scheduler.Default).Subscribe(observer.OnNext));

                    // Listen for client messages.
                    var tcs = new TaskCompletionSource<bool>();
                    var subscription = reactiveSocket
                        .Timeout(MinimumHeartbeatInterval)
                        .Subscribe(m =>
                                   {
                                       var validation = _validator.Validate(m);
                                       if (!validation.IsValid)
                                       {
                                           var message = new GameBoyEvent { Error = new GameBoyServerError() };
                                           message.Error.Reasons.AddRange(validation.Errors.Select(e => e.ToString()));
                                           _logger.LogDebug($"[{id}] invalid message: {string.Join(", ", message.Error.Reasons)}");
                                           observer.OnNext(message);
                                           return;
                                       }

                                       switch (m.ValueCase)
                                       {
                                           case GameBoyCommand.ValueOneofCase.None:
                                           case GameBoyCommand.ValueOneofCase.HeartBeat:
                                               // Just a heartbeat to keep the connection alive.
                                               return;

                                           case GameBoyCommand.ValueOneofCase.SetState:
                                               // Update display name.
                                               if (!m.SetState.DisplayName.Equals(state.DisplayName, StringComparison.OrdinalIgnoreCase))
                                               {
                                                   state.DisplayName = m.SetState.DisplayName;
                                                   Publish(m.SetState.DisplayName, "Joined the game");
                                               }

                                               // Publish the new state.
                                               observer.OnNext(new GameBoyEvent { State = state });
                                               break;

                                           case GameBoyCommand.ValueOneofCase.PressButton:
                                               _joyPadSubject.OnNext((state.DisplayName, (JoyPadButton) (int) m.PressButton.Button));
                                               break;

                                           default:
                                               throw new ArgumentOutOfRangeException();
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
                    _messageBus.FireAndForget(CpuCoreMessages.PauseCpu);
                    _logger.LogInformation($"Removed socket: {id}, Connected sockets: 0. Pausing CPU.");
                }
                else
                {
                    _logger.LogInformation($"Removed socket: {id}, Connected sockets: {connectedSockets}");
                }
            }
        }

        private void Publish(string user, string messageBody)
        {
            _logger.LogDebug(messageBody);
            var message = new GameBoyEvent
                          {
                              PublishedMessage = new GameBoyPublishedMessage
                                                 {
                                                     Body = messageBody,
                                                     User = user,
                                                     Date = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                                                 }
                          };
            _eventsSubject.OnNext(message);
        }
        
        public void Dispose()
        {
            _joyPadSubscription?.Dispose();
        }
    }
}
