import {GameboySocketHealth} from "./gameboy-socket-health";
import {GameboySocketClientState} from "./gameboy-socket-client-state";
import {ErrorMessage} from "./error-message";
import {GameboyMetrics} from "./gameboy-metrics";
import {GameboySocketMessage} from "./gameboy-socket-message";
import {GameboyClientMessage} from "./gameboy-client-message";

export enum GameboyEventType {
  Metrics, ClientMessage, Error, State, Health
}

export class GameboyEvent {
  constructor(type: GameboyEventType,
              event?: GameboyMetrics | GameboyClientMessage | ErrorMessage | GameboySocketClientState | GameboySocketHealth) {
    this.type = type;
    this.event = event;
  }

  type: GameboyEventType;

  event: GameboyMetrics | GameboyClientMessage | ErrorMessage | GameboySocketClientState | GameboySocketHealth;

  public static state(state?: GameboySocketClientState): GameboyEvent {
    return new GameboyEvent(GameboyEventType.State, state);
  }

  public static health(isHealthy: boolean): GameboyEvent {
    return new GameboyEvent(GameboyEventType.Health, new GameboySocketHealth(isHealthy));
  }

  public static error(error: ErrorMessage): GameboyEvent {
    return new GameboyEvent(GameboyEventType.Error, error);
  }

  public static errorMessage(reason: string): GameboyEvent {
    const error = new ErrorMessage();
    error.reasons = [reason];
    return new GameboyEvent(GameboyEventType.Error, error);
  }

  public static metrics(metrics: GameboyMetrics): GameboyEvent {
    return new GameboyEvent(GameboyEventType.Metrics, metrics);
  }

  public static clientMessage(message: GameboyClientMessage): GameboyEvent {
    return new GameboyEvent(GameboyEventType.ClientMessage, message);
  }
}
