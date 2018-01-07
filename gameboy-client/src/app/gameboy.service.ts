import {DOCUMENT} from "@angular/common";
import {Inject, Injectable, OnDestroy} from "@angular/core";
import * as msgpack from "msgpack-lite";
import * as lz4 from "lz4js/lz4.js";
import * as Rx from "rxjs/Rx";
import {environment} from "../environments/environment";
import {GameboyButton} from "./models/gameboy-button";
import {GameboyMetrics} from "./models/gameboy-metrics";
import {GameboySocketMessage} from "./models/gameboy-socket-message";
import {GameboySocketClientState} from "./models/gameboy-socket-client-state";
import {ErrorMessage} from "./models/error-message";
import {VisibilityService} from "./visibility.service";
import {GameboyEvent} from "./models/gameboy-event";
import {GameboyClientMessage} from "./models/gameboy-client-message";

const ServicePath = "ws/gameboy";
const lcdWidth = 160;
const lcdHeight = 144;
const frameHeader = "GPU";
const metricsHeader = "MTC";
const clientMessageHeader = "MSG";
const stateUpdateHeader = "STU";
const errorHeader = "ERR";

const reconnectInterval = 1000;
const heartbeatInterval = 5000;

@Injectable()
export class GameboyService implements OnDestroy {
  public frame: Uint8Array;
  public lcdWidth = lcdWidth;
  public lcdHeight = lcdHeight;
  public healthy = false;
  public state = new GameboySocketClientState();

  private ws: WebSocket;
  private url: string;

  private subject = new Rx.Subject<GameboyEvent>();
  private socketSend: Rx.Observer<GameboySocketMessage>;
  private messageSinceLastHeartbeat = false;
  private reconnectTimeout: NodeJS.Timer;

  constructor(@Inject(DOCUMENT) document, private visibility: VisibilityService) {
    // TODO: save state in local storage and re-setup socket on connection.
    this.subject.next(GameboyEvent.state());

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

    this.connect();
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

    const observable = Rx.Observable.create(
      (obs: Rx.Observer<MessageEvent>) => {
        this.ws.onmessage = obs.next.bind(obs);
        this.ws.onerror = obs.error.bind(obs);
        this.ws.onclose = obs.complete.bind(obs);
        return this.ws.close.bind(this.ws);
      });

    // Create observer to send messages to the server.
    this.socketSend = <Rx.Observer<GameboySocketMessage>> {
      next: (message: GameboySocketMessage) => {
        if (this.ws.readyState === WebSocket.OPEN) {
          const packed = msgpack.encode(message);
          this.ws.send(packed);
        }
      }
    };

    // Create heartbeat.
    const heartbeat = Rx.Observable.interval(heartbeatInterval)
      .subscribe(() => {
        if (this.messageSinceLastHeartbeat) {
          this.messageSinceLastHeartbeat = false;
        } else {
          this.socketSend.next({});
        }
      });

    Rx.Subject.create(this.socketSend, observable)
      .subscribe(msg => {
        if (!this.healthy) {
          this.healthy = true;
          this.subject.next(GameboyEvent.health(true));
          this.sendMessage({enableMetrics: this.state.metricsEnabled, setDisplayName: this.state.displayName});
        }

        const data = new Uint8Array(<ArrayBuffer> msg.data);
        const header = data.slice(0, 3).reduce((s, x) => s + String.fromCharCode(x), "");
        const body = data.slice(3);

        switch (header) {
          case frameHeader:
            lz4.decompressBlock(body, this.frame, 0, lcdWidth * lcdHeight, 0);
            break;

          case metricsHeader:
            this.subject.next(GameboyEvent.metrics(<GameboyMetrics> msgpack.decode(body)));
            break;

          case clientMessageHeader:
            this.subject.next(GameboyEvent.clientMessage(<GameboyClientMessage> msgpack.decode(body)));
            break;

          case stateUpdateHeader:
            this.state = <GameboySocketClientState> msgpack.decode(body);
            this.subject.next(GameboyEvent.state(this.state));
            break;

          case errorHeader:
            this.subject.next(GameboyEvent.error(<ErrorMessage> msgpack.decode(body)));
            break;
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
    if (this.reconnectTimeout) {
      clearTimeout(this.reconnectTimeout);
    }

    if (this.healthy) {
      this.healthy = false;
      this.subject.next(GameboyEvent.health(false));
      this.subject.next(GameboyEvent.errorMessage("Lost server connection"));
    }

    if (!this.visibility.isVisible()) {
      return;
    }

    this.reconnectTimeout = setTimeout(() => this.connect(), reconnectInterval);
  }

  private disconnect(): void {
    if (this.ws && this.ws.OPEN) {
      this.ws.close();
      this.ws = undefined;
    }
  }

  public stream(): Rx.Observable<GameboyEvent> {
    return this.subject.asObservable();
  }

  public pressButton(button: GameboyButton) {
    this.sendMessage({button});
  }

  public trySetDisplayName(name: string): void {
    this.sendMessage({setDisplayName: name});
  }

  public setMetricsEnabled(value: boolean) {
    this.state.metricsEnabled = value; // set for intent.
    this.sendMessage({enableMetrics: value});
  }

  private sendMessage(message: GameboySocketMessage): void {
    if (!this.healthy) {
      return;
    }

    this.messageSinceLastHeartbeat = true;
    this.socketSend.next(message);
  }

  ngOnDestroy(): void {
    if (this.reconnectTimeout) {
      clearTimeout(this.reconnectTimeout);
    }
  }
}

