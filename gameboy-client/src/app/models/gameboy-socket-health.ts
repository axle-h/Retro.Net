export class GameboySocketHealth {


  constructor(isHealthy: boolean) {
    this.isHealthy = isHealthy;
  }

  /**
   * Gets a value that determines the health of the socket.
   */
  isHealthy: boolean;
}
