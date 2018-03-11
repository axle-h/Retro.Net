import {Component, OnDestroy, OnInit} from "@angular/core";
import {GameBoyService} from "../game-boy.service";
import * as Rx from "rxjs/Rx";
import { debounceTime } from "rxjs/operator/debounceTime";
import * as _ from "lodash";
import {
  GameBoyJoyPadButton,
  IGameBoyClientState,
  IGameBoyPublishedMessage,
  IGameBoyServerError
} from "../messages/messages";
import { GameBoyServerMessageType } from "../models/game-boy-server-message";
import {IGameBoyMetrics} from "../models/game-boy-metrics";
import {IGameBoyHealth} from "../models/game-boy-health";

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
  clientMessages: IGameBoyPublishedMessage[] = [];

  private ngUnsubscribe = new Rx.Subject();
  private errors = new Rx.Subject<string>();

  constructor(private service: GameBoyService) { }

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
          case GameBoyServerMessageType.Metrics:
            this.handleMetrics(message.metrics);
            break;

          case GameBoyServerMessageType.Message:
            this.handlePublishedMessage(message.message);
            break;

          case GameBoyServerMessageType.Error:
            this.handleError(message.error);
            break;

          case GameBoyServerMessageType.Health:
            this.handleHealthChange(message.health);
            break;

          case GameBoyServerMessageType.State:
            this.handleStateChange(message.state);
            break;
        }
    });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  pressA() {
    this.service.pressButton(GameBoyJoyPadButton.A);
  }

  pressB() {
    this.service.pressButton(GameBoyJoyPadButton.B);
  }

  pressStart() {
    this.service.pressButton(GameBoyJoyPadButton.START);
  }

  pressSelect() {
    this.service.pressButton(GameBoyJoyPadButton.SELECT);
  }

  pressRight() {
    this.service.pressButton(GameBoyJoyPadButton.RIGHT);
  }

  pressLeft() {
    this.service.pressButton(GameBoyJoyPadButton.LEFT);
  }

  pressUp() {
    this.service.pressButton(GameBoyJoyPadButton.UP);
  }

  pressDown() {
    this.service.pressButton(GameBoyJoyPadButton.DOWN);
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

  formatDate(date: any) {
    return new Date(date).toLocaleString();
  }

  private setDisplayName(displayName: string): void {
    this.displayNameSet = true;
    this.displayName = displayName;
  }

  private handleMetrics(metrics: IGameBoyMetrics): void {
    // Do nothing for now.
  }

  private handlePublishedMessage(message: IGameBoyPublishedMessage): void {
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

    this.clientMessages = _.orderBy(this.clientMessages, m => m.date, "desc");
  }

  private handleError(error: IGameBoyServerError): void {
    error.reasons.forEach(r => this.errors.next(r));
    this.errorMessages = error.reasons;
  }

  private handleHealthChange(health: IGameBoyHealth): void {
    this.healthy = health.healthy;
    if (!health.healthy) {
      this.clientMessages = [];
    }
  }

  private handleStateChange(state: IGameBoyClientState): void {
    if (state.displayName) {
      this.setDisplayName(state.displayName);
    }
  }
}
