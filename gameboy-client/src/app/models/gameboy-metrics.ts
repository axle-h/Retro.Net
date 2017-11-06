/**
 * Metrics sent from a GameBoy socket server.
 */
import {GameboyClientMessage} from "./gameboy-client-message";

export class GameboyMetrics {

  /**
   * The average number of frames that were rendered on the server in the alst second.
   */
  framesPerSecond: number;

  /**
   * The total number of frames that had to be skipped due to the GPU falling behind the CPU.
   */
  skippedFrames: number;

  /**
   * Client messages that should be displayed.
   */
  messages: GameboyClientMessage[];
}
