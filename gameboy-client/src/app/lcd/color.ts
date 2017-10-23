export class Color {
  constructor(red: number, green: number, blue: number) {
    this.red = red;
    this.green = green;
    this.blue = blue;
  }
  red: number;
  green: number;
  blue: number;

  public rgbString(): string {
    return `rgb(${this.red},${this.green},${this.blue})`;
  }
}
