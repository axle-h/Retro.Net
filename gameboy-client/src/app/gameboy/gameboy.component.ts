import {Component, OnDestroy, OnInit} from "@angular/core";
import {GameboyService} from "../gameboy.service";
import {GameboyButton} from "../models/gameboy-button";
import * as Rx from "rxjs/Rx";
import { debounceTime } from "rxjs/operator/debounceTime";
import {GameboyClientMessage} from "../models/gameboy-client-message";
import {GameboySocketHealth} from "../models/gameboy-socket-health";
import {ErrorMessage} from "../models/error-message";
import {GameboySocketClientState} from "../models/gameboy-socket-client-state";
import {GameboyMetrics} from "../models/gameboy-metrics";
import {GameboyEventType} from "../models/gameboy-event";

const maxClientMessages = 20;

@Component({
  selector: "gb-gameboy",
  templateUrl: "./gameboy.component.html",
  styleUrls: ["./gameboy.component.scss"]
})
export class GameboyComponent implements OnInit, OnDestroy {
  displayName: string;
  displayNameSet: boolean;
  healthy: boolean;
  errorMessages: string[] = [];
  clientMessages: GameboyClientMessage[] = [];

  private ngUnsubscribe = new Rx.Subject();
  private errors = new Rx.Subject<string>();

  constructor(private service: GameboyService) { }

  ngOnInit() {
    // Initial state.
    this.healthy = this.service.healthy;
    if (this.service.state.displayName) {
      this.setDisplayName(this.service.state.displayName);
    }

    // Errors.
    this.errors
      .takeUntil(this.ngUnsubscribe)
      .subscribe(err => this.errorMessages.push(err));
    debounceTime.call(this.errors, 5000).subscribe(err => this.closeErrorMessageAlert(err));

    // GameBoy socket subscription.
    this.service
      .stream()
      .takeUntil(this.ngUnsubscribe)
      .subscribe(message => {
        switch (message.type) {
          case GameboyEventType.Metrics:
            this.handleMetrics(<GameboyMetrics> message.event);
            break;

          case GameboyEventType.ClientMessage:
            this.handleClientMessage(<GameboyClientMessage> message.event);
            break;

          case GameboyEventType.Error:
            this.handleError(<ErrorMessage> message.event);
            break;

          case GameboyEventType.Health:
            this.handleHealthChange(<GameboySocketHealth> message.event);
            break;

          case GameboyEventType.State:
            this.handleStateChange(<GameboySocketClientState> message.event);
            break;
        }
    });

    this.service.setMetricsEnabled(true);
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
    this.service.setMetricsEnabled(false);
  }

  pressA() {
    this.service.pressButton(GameboyButton.A);
  }

  pressB() {
    this.service.pressButton(GameboyButton.B);
  }

  pressStart() {
    this.service.pressButton(GameboyButton.Start);
  }

  pressSelect() {
    this.service.pressButton(GameboyButton.Select);
  }

  pressRight() {
    this.service.pressButton(GameboyButton.Right);
  }

  pressLeft() {
    this.service.pressButton(GameboyButton.Left);
  }

  pressUp() {
    this.service.pressButton(GameboyButton.Up);
  }

  pressDown() {
    this.service.pressButton(GameboyButton.Down);
  }

  onSubmit() {
    this.service.trySetDisplayName(this.displayName);
  }

  closeErrorMessageAlert(message: string) {
    const index = this.errorMessages.indexOf(message);
    if (index > -1) {
      this.errorMessages.splice(index, 1);
    }
  }

  private setDisplayName(displayName: string): void {
    this.displayNameSet = true;
    this.displayName = displayName;
  }

  private handleMetrics(metrics: GameboyMetrics): void {
    // Do nothing for now.
  }

  private handleClientMessage(message: GameboyClientMessage): void {
    if (!message) {
      return;
    }

    if (this.clientMessages.length + 1 >= maxClientMessages) {
      this.clientMessages = this.clientMessages.slice(0, maxClientMessages - 1);
    }

    if (this.clientMessages.length > 0) {
      this.clientMessages.unshift(message);
    } else {
      this.clientMessages = [message];
    }
  }

  private handleError(error: ErrorMessage): void {
    error.reasons.forEach(r => this.errors.next(r));
    this.errorMessages = error.reasons;
  }

  private handleHealthChange(health: GameboySocketHealth): void {
    this.healthy = health.isHealthy;
    if (health.isHealthy && !this.service.state.metricsEnabled) {
      this.service.setMetricsEnabled(true);
    }

    if (!health.isHealthy) {
      this.clientMessages = [];
    }
  }

  private handleStateChange(state: GameboySocketClientState): void {
    if (state.displayName) {
      this.setDisplayName(state.displayName);
    }
  }
}
