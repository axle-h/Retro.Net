import { Component, OnInit } from "@angular/core";
import {GameboyService} from "../gameboy.service";
import {GameboyButton} from "../models/gameboy-button";

@Component({
  selector: "gb-gameboy",
  templateUrl: "./gameboy.component.html",
  styleUrls: ["./gameboy.component.scss"]
})
export class GameboyComponent implements OnInit {

  constructor(private service: GameboyService) { }

  ngOnInit() {
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
}
