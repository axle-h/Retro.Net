import {DOCUMENT} from "@angular/common";
import {Inject, Injectable} from "@angular/core";
import * as msgpack from "msgpack-lite";
import * as lz4 from "lz4js/lz4.js";
import * as Rx from "rxjs/Rx";
import {GpuMetrics} from "./models/gpu-metrics";
import { environment } from "../environments/environment";

const ServicePath = "ws/gameboy";
const heartbeatMessage = "heartbeat";
const lcdWidth = 160;
const lcdHeight = 144;

@Injectable()
export class GameboyService {

  public frame: Uint8Array;
  public lcdWidth = lcdWidth;
  public lcdHeight = lcdHeight;

  private url: string;
  private reconnectInterval = 1000;
  private heartbeatInterval = 1000;
  private metricsSubject: Rx.Subject<GpuMetrics>;
  private heartbeat: Rx.Subscription;
  public healthy = false;

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

    const observer = {
      next: (data: Object) => {
        if (ws.readyState === WebSocket.OPEN) {
          if (typeof(data) === "string") {
            ws.send(data);
          } else {
            ws.send(JSON.stringify(data));
          }
        }
      }
    };

    this.heartbeat = Rx.Observable.interval(this.heartbeatInterval)
      .subscribe(() => observer.next(heartbeatMessage));

    Rx.Subject.create(observer, observable)
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

}
