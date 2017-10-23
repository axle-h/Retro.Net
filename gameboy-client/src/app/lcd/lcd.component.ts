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

@Component({
  selector: "gb-lcd",
  templateUrl: "./lcd.component.html"
})
export class LcdComponent implements OnInit {

  @ViewChild("lcd") lcdCanvas: ElementRef;
  @ViewChild("rawLcd") rawLcdCanvas: ElementRef;
  @Input() scale: number;

  private lcdInit = false;

  public lcdWidth: number;
  public lcdHeight: number;
  public width: number;
  public height: number;

  constructor(private service: GameboyService) {
    this.lcdWidth = service.lcdWidth;
    this.lcdHeight = service.lcdHeight;
  }

  ngOnInit() {
    this.width = 390 * this.scale;
    this.height = 303 * this.scale;
    this.render();
  }

  render = () => {
    const lcd: HTMLCanvasElement = this.lcdCanvas.nativeElement;
    const context: CanvasRenderingContext2D = lcd.getContext("2d");
    const lcdX = lcdXOffset * this.width, lcdY = lcdYOffset * this.height;
    const lcdW = this.lcdWidth * lcdScale * this.scale, lcdH = this.lcdHeight * lcdScale * this.scale;

    if (this.service.healthy && this.lcdInit) {
      const raw: HTMLCanvasElement = this.rawLcdCanvas.nativeElement;
      const rawContext: CanvasRenderingContext2D = raw.getContext("2d");
      const img = rawContext.createImageData(this.lcdWidth, this.lcdHeight);

      for (let y = 0; y < this.lcdHeight; y++) {
        for (let x = 0; x < this.lcdWidth; x++) {
          const index = y * this.lcdWidth + x;
          const imgIndex = index * 4;
          const colourIndex = this.service.frame[index];
          if (colourIndex < 0 || colourIndex >= colours.length){
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

      const lcdBorder = new Image();
      lcdBorder.src = "/assets/img/gameboy-lcd.svg";
      lcdBorder.onload = () => {
        context.drawImage(lcdBorder, 0, 0, lcd.width, lcd.height);

        context.fillStyle = borderColour.rgbString();
        context.fillRect(lcdX - borderWidth, lcdY - borderWidth, lcdW + 2 * borderWidth, lcdH + 2 * borderWidth);

        context.fillStyle = colour0.rgbString();
        context.fillRect(lcdX, lcdY, lcdW, lcdH);

        context.fillStyle = colour3.rgbString();
        context.font = "30px Arial";
        context.textAlign = "center";
        context.fillText("Connecting...", lcdX + lcdW / 2, lcdY + lcdH / 2);

        this.lcdInit = true;
      };
    }

    requestAnimationFrame(this.render);
  }
}
