import {Component, ElementRef, Input, OnInit, ViewChild} from "@angular/core";
import {Color} from "./color";
import {GameboyService} from "../gameboy.service";

const colour0 = new Color(0x9b, 0xbc, 0x0f);
const colour1 = new Color(0x8b, 0xac, 0x0f);
const colour2 = new Color(0x30, 0x62, 0x30);
const colour3 = new Color(0x0f, 0x38, 0x0f);
const borderColour = new Color(0x07, 0x1C, 0x07);
const colours: Color[] = [colour0, colour1, colour2, colour3];

const lcdXOffset = 0.22;
const lcdYOffset = 0.15;
const lcdScale = 1.45;
const borderWidth = 3;
const minScale = 0.5;

@Component({
  selector: "gb-lcd",
  templateUrl: "./lcd.component.html",
  styleUrls: ["./lcd.component.scss"]
})
export class LcdComponent implements OnInit {

  @ViewChild("lcd") lcdCanvas: ElementRef;
  @ViewChild("rawLcd") rawLcdCanvas: ElementRef;
  @Input() maxScale: number;

  maxWidth: number;
  maxHeight: number;
  minWidth: number;
  minHeight: number;

  lcdWidth: number;
  lcdHeight: number;

  private lcdInit = false;
  private lcdBorder: HTMLImageElement;

  constructor(private service: GameboyService) {
    this.lcdWidth = service.lcdWidth;
    this.lcdHeight = service.lcdHeight;
  }

  ngOnInit() {
    this.maxScale = Math.max(this.maxScale, minScale);

    this.lcdBorder = new Image();
    this.lcdBorder.src = "/assets/img/gameboy-lcd.svg";
    this.lcdBorder.onload = () => {
      this.maxWidth = this.maxScale * this.lcdBorder.width;
      this.maxHeight = this.maxScale * this.lcdBorder.height;
      this.minWidth = minScale * this.lcdBorder.width;
      this.minHeight = minScale * this.lcdBorder.height;
      this.render();
    };
  }

  render = () => {
    const lcd: HTMLCanvasElement = this.lcdCanvas.nativeElement;
    const context: CanvasRenderingContext2D = lcd.getContext("2d");
    const lcdX = lcdXOffset * this.maxWidth, lcdY = lcdYOffset * this.maxHeight;
    const lcdW = this.lcdWidth * lcdScale * this.maxScale, lcdH = this.lcdHeight * lcdScale * this.maxScale;

    if (this.service.healthy && this.lcdInit) {
      const raw: HTMLCanvasElement = this.rawLcdCanvas.nativeElement;
      const rawContext: CanvasRenderingContext2D = raw.getContext("2d");
      const img = rawContext.createImageData(this.lcdWidth, this.lcdHeight);

      for (let y = 0; y < this.lcdHeight; y++) {
        for (let x = 0; x < this.lcdWidth; x++) {
          const index = y * this.lcdWidth + x;
          const imgIndex = index * 4;
          const colourIndex = this.service.frame[index];
          if (colourIndex < 0 || colourIndex >= colours.length) {
            throw new Error("Unknown colour: " + colourIndex);
          }

          const colour = colours[colourIndex];

          img.data[imgIndex] = colour.red;
          img.data[imgIndex + 1] = colour.green;
          img.data[imgIndex + 2] = colour.blue;
          img.data[imgIndex + 3] = 255;
        }
      }
      rawContext.putImageData(img, 0, 0);

      context.drawImage(raw, lcdX, lcdY, lcdW, lcdH);
    } else {
      lcd.width = this.maxWidth;
      lcd.height = this.maxHeight;

      context.drawImage(this.lcdBorder, 0, 0, lcd.width, lcd.height);

      context.fillStyle = borderColour.rgbString();
      context.fillRect(lcdX - borderWidth, lcdY - borderWidth, lcdW + 2 * borderWidth, lcdH + 2 * borderWidth);

      context.fillStyle = colour0.rgbString();
      context.fillRect(lcdX, lcdY, lcdW, lcdH);

      context.fillStyle = colour3.rgbString();
      const fontSize = 2 * this.maxScale;
      context.font = fontSize + "em Arial";
      context.textAlign = "center";
      context.fillText("Connecting...", lcdX + lcdW / 2, lcdY + lcdH / 2);

      this.lcdInit = true;
    }

    requestAnimationFrame(this.render);
  }
}
