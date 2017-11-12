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

const ServicePath = "ws/gameboy";
const lcdWidth = 160;
const lcdHeight = 144;
const frameHeader = "GPU";
const metricsHeader = "MTC";
const stateUpdateHeader = "STU";
const errorHeader = "ERR";

@Injectable()
export class GameboyService implements OnDestroy {
  public frame: Uint8Array;
  public lcdWidth = lcdWidth;
  public lcdHeight = lcdHeight;
  public healthy = false;
  public state = new GameboySocketClientState();

  private url: string;
  private reconnectInterval = 1000;
  private heartbeatInterval = 5000;

  private metricsSubject = new Rx.Subject<GameboyMetrics>();
  private errorSubject = new Rx.Subject<ErrorMessage>();
  private stateSubject = new Rx.Subject<GameboySocketClientState>();
  private healthSubject = new Rx.Subject<boolean>();

  private messageObserver: Rx.Observer<GameboySocketMessage>;
  private heartbeat: Rx.Subscription;
  private messageSinceLastHeartbeat = false;
  private reconnectTimeout: NodeJS.Timer;

  constructor(@Inject(DOCUMENT) private document) {
    // TODO: save state in local storage and re-setup socket on connection.
    this.stateSubject.next(new GameboySocketClientState());

    this.frame = lz4.makeBuffer(lcdWidth * lcdHeight);
    this.frame.fill(0);

    const protocol = document.location.protocol.startsWith("https") ? "wss" : "ws";
    if (environment.serverUrl) {
      this.url = `${protocol}://${environment.serverUrl}/${ServicePath}`;
    } else {
      this.url = `${protocol}://${document.location.hostname}:${document.location.port}/${ServicePath}`;
    }
    this.connect();
  }

  private connect(): void {
    const ws = new WebSocket(this.url);
    ws.binaryType = "arraybuffer";

    const observable = Rx.Observable.create(
      (obs: Rx.Observer<MessageEvent>) => {
        ws.onmessage = obs.next.bind(obs);
        ws.onerror = obs.error.bind(obs);
        ws.onclose = obs.complete.bind(obs);
        return ws.close.bind(ws);
      });

    this.messageObserver = <Rx.Observer<GameboySocketMessage>> {
      next: (message: GameboySocketMessage) => {
        if (ws.readyState === WebSocket.OPEN) {
          const packed = msgpack.encode(message);
          ws.send(packed);
        }
      }
    };

    this.heartbeat = Rx.Observable.interval(this.heartbeatInterval)
      .subscribe(() => {
        if (this.messageSinceLastHeartbeat) {
          this.messageSinceLastHeartbeat = false;
        } else {
          this.messageObserver.next({});
        }
      });

    Rx.Subject.create(this.messageObserver, observable)
      .subscribe(msg => {
        if (!this.healthy) {
          this.healthy = true;
          this.healthSubject.next(true);
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
            this.metricsSubject.next(<GameboyMetrics> msgpack.decode(body));
            break;

          case stateUpdateHeader:
            const state = <GameboySocketClientState> msgpack.decode(body);
            this.stateSubject.next(state);
            this.state = state;
            break;

          case errorHeader:
            const error = <ErrorMessage> msgpack.decode(body);
            console.error("Received gameboy server error: " + JSON.stringify(error));
            this.errorSubject.next(error);
            break;
        }
      }, e => this.reconnect(), () => this.reconnect());
  }

  private reconnect(): void {
    if (this.reconnectTimeout) {
      clearTimeout(this.reconnectTimeout);
    }

    if (this.healthy) {
      this.healthy = false;
      this.healthSubject.next(false);
      this.errorSubject.next(<ErrorMessage> { reasons: ["Lost server connection"] });
    }

    this.heartbeat.unsubscribe();
    this.reconnectTimeout = setTimeout(() => this.connect(), this.reconnectInterval);
  }

  public metricsStream(): Rx.Observable<GameboyMetrics> {
    return this.metricsSubject.asObservable();
  }

  public stateStream(): Rx.Observable<GameboySocketClientState> {
    return this.stateSubject.asObservable();
  }

  public errorStream(): Rx.Observable<ErrorMessage> {
    return this.errorSubject.asObservable();
  }

  public healthStream(): Rx.Observable<boolean> {
    return this.healthSubject.asObservable();
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
    this.messageObserver.next(message);
  }

  ngOnDestroy(): void {
    if (this.reconnectTimeout) {
      clearTimeout(this.reconnectTimeout);
    }
  }
}

