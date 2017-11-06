import {GameboyButton} from "./gameboy-button";

/**
 * A message sent to a connected Gameboy server.
 */
export interface GameboySocketMessage {

  /**
   * The requested joypad button.
   */
  button?: GameboyButton;

  /**
   * A request to enable or disable the sending of matrics.
   */
  enableMetrics?: boolean;

  /**
   * A request to set the display name associated with this socket.
   */
  setDisplayName?: string;
}
