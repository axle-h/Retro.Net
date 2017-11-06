/**
 * A message from a gameboy client connected to the server.
 */
export class GameboyClientMessage {
  /**
   * Gets the user associated with this message.
   */
  user: string;

  /**
   * The date that the message was created.
   */
  date: string;

  /**
   * The message body.
   */
  message: string;
}
