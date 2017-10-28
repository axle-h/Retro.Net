import {DOCUMENT} from "@angular/common";
import {Inject, Injectable} from "@angular/core";
import * as msgpack from "msgpack-lite";
import * as lz4 from "lz4js/lz4.js";
import * as Rx from "rxjs/Rx";
import {GpuMetrics} from "./models/gpu-metrics";
import {environment} from "../environments/environment";
import {GameboyButton} from "./models/gameboy-button";

const ServicePath = "ws/gameboy";
const lcdWidth = 160;
const lcdHeight = 144;

interface GameboyMessage {
  button?: GameboyButton;
}

@Injectable()
export class GameboyService {

  public frame: Uint8Array;
  public lcdWidth = lcdWidth;
  public lcdHeight = lcdHeight;
  public healthy = false;


  private url: string;
  private reconnectInterval = 1000;
  private heartbeatInterval = 5000;
  private metricsSubject: Rx.Subject<GpuMetrics>;
  private messageObserver: Rx.Observer<GameboyMessage>;
  private heartbeat: Rx.Subscription;
  private messageSinceLastHeartbeat = false;

  constructor(@Inject(DOCUMENT) private document) {
    this.metricsSubject = Rx.Subject.create();
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

    this.messageObserver = <Rx.Observer<GameboyMessage>> {
      next: (message: GameboyMessage) => {
        if (ws.readyState === WebSocket.OPEN) {
          const packed = msgpack.encode(message);
          const b64encoded = btoa(String.fromCharCode.apply(null, packed));
          console.log(b64encoded);
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
        this.healthy = true;
        const data = new Uint8Array(<ArrayBuffer> msg.data);
        const header = data.slice(0, 3).reduce((s, x) => s + String.fromCharCode(x), "");
        const body = data.slice(3);

        if (header === "MTC") {
          this.metricsSubject.next(<GpuMetrics> msgpack.decode(body));
        } else if (header === "GPU") {
          lz4.decompressBlock(body, this.frame, 0, lcdWidth * lcdHeight, 0);
        }
      }, e => this.reconnect(), () => this.reconnect());
  }

  private reconnect(): void {
    this.healthy = false;
    this.heartbeat.unsubscribe();
    setTimeout(() => this.connect(), this.reconnectInterval);
  }

  public metrics(): Rx.Observable<GpuMetrics> {
    return this.metricsSubject.asObservable();
  }

  public pressButton(button: GameboyButton) {
    this.messageSinceLastHeartbeat = true;
    this.messageObserver.next({button});
  }
}

