import {DOCUMENT} from "@angular/common";
import {Inject, Injectable, OnDestroy} from "@angular/core";
import * as lz4 from "lz4js/lz4.js";
import * as Rx from "rxjs/Rx";
import {environment} from "../environments/environment";
import {VisibilityService} from "./visibility.service";

import {
  GameBoyCommand,
  GameBoyEvent, GameBoyJoyPadButton,
  IGameBoyClientState,
  IGameBoyCommand,
  IGameBoyPublishedMessage,
  IGameBoyServerError
} from "./messages/messages";
import {IGameBoyHealth} from "./models/game-boy-health";
import {
  GameBoyServerMessage,
  GameBoyServerMessageType,
  IGameBoyServerMessage
} from "./models/game-boy-server-message";
import {IGameBoyMetrics} from "./models/game-boy-metrics";

const ServicePath = "ws/gameboy";
const lcdWidth = 160;
const lcdHeight = 144;

const reconnectInterval = 1000;
const heartbeatInterval = 5000;

@Injectable()
export class GameBoyService implements OnDestroy {
  public frame: Uint8Array;
  public lcdWidth = lcdWidth;
  public lcdHeight = lcdHeight;
  public healthy = false;
  public state: IGameBoyClientState;

  private ws: WebSocket;
  private readonly url: string;

  private readonly subject = new Rx.Subject<IGameBoyServerMessage>();
  private socketSend: Rx.Observer<IGameBoyCommand>;
  private messageSinceLastHeartbeat = false;
  private reconnectTimeout: Rx.Subscription;

  private readBuffer: Uint8Array;

  constructor(@Inject(DOCUMENT) document, private visibility: VisibilityService) {

    // TODO: save state in local storage and re-setup socket on connection.
    this.state = {};
    this.publishServerMessage(GameBoyServerMessageType.State, this.state);

    this.frame = lz4.makeBuffer(lcdWidth * lcdHeight);
    this.frame.fill(0);

    if (document.location) {
      const protocol = document.location.protocol.startsWith("https") ? "wss" : "ws";
      if (environment.serverUrl) {
        this.url = `${protocol}://${environment.serverUrl}/${ServicePath}`;
      } else {
        this.url = `${protocol}://${document.location.hostname}:${document.location.port}/${ServicePath}`;
      }
    } else {
      this.url = `$wss://${environment.serverUrl}/${ServicePath}`;
    }

    visibility.visibilityStream().subscribe(visible => {
      if (visible) {
        this.connect();
      } else {
        this.disconnect();
      }
    });

    this.readBuffer = lz4.makeBuffer(0);

    this.connect();
  }

  private static getLsbEncodedLength(buffer: Uint8Array): { offset: number, length: number } {
    let length = 0, shift = 0, offset = 0;
    let byte;

    do {
      byte = buffer[offset++];
      length |= (byte & 0x7F) << shift;
      shift += 7;
    }
    while (byte >= 0x80);

    return { offset, length };
  }

  private connect(): void {
    if (this.healthy || !this.visibility.isVisible()) {
      return;
    }

    if (this.ws) {
      this.ws.close();
    }

    this.ws = new WebSocket(this.url);
    this.ws.binaryType = "arraybuffer";

    // Create observer to send messages to the server.
    this.socketSend = <Rx.Observer<IGameBoyCommand>> {
      next: (command: IGameBoyCommand) => {
        if (this.ws && this.ws.readyState === WebSocket.OPEN) {
          const cmd = new GameBoyCommand(command);
          console.log(cmd);
          const proto = GameBoyCommand.encode(cmd).finish();
          this.ws.send(proto);
        }
      }
    };

    // Create heartbeat.
    const heartbeat = Rx.Observable.interval(heartbeatInterval)
      .subscribe(() => {
        if (this.messageSinceLastHeartbeat) {
          this.messageSinceLastHeartbeat = false;
        } else {
          this.socketSend.next({heartBeat: {date: new Date().toISOString()}});
        }
      });

    const observable = Rx.Observable.create(
      (obs: Rx.Observer<MessageEvent>) => {
        this.ws.onmessage = obs.next.bind(obs);
        this.ws.onerror = obs.error.bind(obs);
        this.ws.onclose = obs.complete.bind(obs);
        return this.ws.close.bind(this.ws);
      });

    observable
      .subscribe(msg => {
        if (!this.healthy) {
          this.healthChange(true);
          this.trySetDisplayName(this.state.displayName);
        }

        const data = new Uint8Array(<ArrayBuffer> msg.data);
        const params = GameBoyService.getLsbEncodedLength(data);

        if (this.readBuffer.length < params.length) {
          this.readBuffer = lz4.makeBuffer(params.length);
        }

        const protoLength = lz4.decompressBlock(data, this.readBuffer, params.offset, data.length - params.offset, 0);

        const event = GameBoyEvent.decode(this.readBuffer, protoLength);

        switch (event.value) {
          case "frame":

            this.frame.set(event.frame.data);
            const metrics = <IGameBoyMetrics> { framesPerSecond: event.frame.framesPerSecond };
            this.publishServerMessage(GameBoyServerMessageType.Metrics, metrics);
            break;

          case "publishedMessage":
            this.publishServerMessage(GameBoyServerMessageType.Message, event.publishedMessage);
            break;

          case "error":
            this.publishServerMessage(GameBoyServerMessageType.Error, event.error);
            break;

          case "state":
            this.state = event.state;
            this.publishServerMessage(GameBoyServerMessageType.State, event.state);
            break;

          default:
            console.error("Unknown message type: " + event.value);
        }
      }, () => {
        heartbeat.unsubscribe();
        this.reconnect();
      }, () => {
        heartbeat.unsubscribe();
        this.reconnect();
      });
  }

  private reconnect(): void {
    this.tryUnsubscribe();

    if (this.healthy) {
      this.healthChange(false);
    }

    if (!this.visibility.isVisible()) {
      return;
    }

    this.reconnectTimeout = Rx.Observable.timer(reconnectInterval).subscribe(() => this.connect());
  }

  private disconnect(): void {
    if (this.ws && this.ws.OPEN) {
      this.ws.close();
      this.ws = undefined;
    }
  }

  public stream(): Rx.Observable<IGameBoyServerMessage> {
    return this.subject.asObservable();
  }

  public pressButton(button: GameBoyJoyPadButton) {
    this.sendCommand({ pressButton: { button: button } });
  }

  public trySetDisplayName(name: string): void {
    if (name) {
      this.sendCommand({ setState: { displayName: name } });
    }
  }

  private sendCommand(command: IGameBoyCommand): void {
    if (!this.healthy) {
      return;
    }

    this.messageSinceLastHeartbeat = true;
    this.socketSend.next(command);
  }

  private healthChange(healthy: boolean) {
    this.healthy = healthy;

    this.publishServerMessage(GameBoyServerMessageType.Health, <IGameBoyHealth> {healthy: healthy});

    if (healthy) {
      // re-establish display name.
      this.trySetDisplayName(this.state.displayName);
    } else {
      this.publishError("Lost server connection");
    }
  }

  private publishError(error: string) {
    this.publishServerMessage(GameBoyServerMessageType.Error, <IGameBoyServerError> {reasons: [error]});
  }

  private publishServerMessage(type: GameBoyServerMessageType,
                               value: IGameBoyMetrics | IGameBoyPublishedMessage | IGameBoyServerError |
                                 IGameBoyClientState | IGameBoyHealth) {
    this.subject.next(new GameBoyServerMessage(type, value));
  }

  private tryUnsubscribe(): void {
    if (this.reconnectTimeout && !this.reconnectTimeout.closed) {
      this.reconnectTimeout.unsubscribe();
    }
  }

  ngOnDestroy(): void {
    this.tryUnsubscribe();
  }
}

