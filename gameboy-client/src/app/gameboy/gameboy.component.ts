import {Component, OnDestroy, OnInit} from "@angular/core";
import {GameboyService} from "../gameboy.service";
import {GameboyButton} from "../models/gameboy-button";
import * as Rx from "rxjs/Rx";
import { debounceTime } from "rxjs/operator/debounceTime";
import {GameboyClientMessage} from "../models/gameboy-client-message";

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

  private errors = new Rx.Subject<string>();
  private subscriptions: [Rx.Subscription];

  constructor(private service: GameboyService) {
  }

  ngOnInit() {
    this.healthy = this.service.healthy;
    if (this.service.state.displayName) {
      this.setDisplayName(this.service.state.displayName);
    }

    const errorMessages = this.errors.subscribe(err => this.errorMessages.push(err));
    debounceTime.call(this.errors, 5000).subscribe(err => this.closeErrorMessageAlert(err));

    const health = this.service.healthStream().subscribe(h => {
      this.healthy = h;
      if (this.healthy && !this.service.state.metricsEnabled) {
        this.service.setMetricsEnabled(true);
      }

      if (!this.healthy) {
        this.clientMessages = [];
      }
    });

    const errors = this.service.errorStream().subscribe(err => {
      if (err.reasons.find(x => x.includes("Display name"))) {
        // TODO: devise a better method of working this out.
        this.displayNameSet = false;
      }

      err.reasons.forEach(r => this.addErrorMessageAlert(r));
      this.errorMessages = err.reasons;
    });

    const states = this.service.stateStream()
      .filter(state => !!state.displayName)
      .subscribe(state => this.setDisplayName(state.displayName));

    const metrics = this.service.metricsStream()
      .filter(mt => mt.messages.length > 0)
      .subscribe(mt => {
        if (this.clientMessages.length + mt.messages.length >= maxClientMessages) {
          this.clientMessages = this.clientMessages.slice(0, maxClientMessages - mt.messages.length);
        }

        if (this.clientMessages.length > 0) {
          mt.messages.forEach(m => this.clientMessages.unshift(m));
        } else {
          this.clientMessages = mt.messages.reverse();
        }
      });

    this.subscriptions = [health, errors, states, errorMessages, metrics];

    this.service.setMetricsEnabled(true);
  }

  ngOnDestroy(): void {
    if (this.subscriptions) {
      this.subscriptions.forEach(s => s.unsubscribe());
    }

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

  addErrorMessageAlert(message: string) {
    this.errors.next(message);
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
}
