/**
 * The state of a gameboy socket connection from the perspective of the server.
 */
export class GameboySocketClientState {

  /**
   * The current display displayName associated with this socket.
   */
  displayName: string;

  /**
   * Whether metrics are enabled on this socket.
   */
  metricsEnabled: boolean;
}
