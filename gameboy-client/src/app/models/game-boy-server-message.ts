import {IGameBoyClientState, IGameBoyPublishedMessage, IGameBoyServerError} from "../messages/messages";
import {IGameBoyMetrics} from "./game-boy-metrics";
import {IGameBoyHealth} from "./game-boy-health";

export interface IGameBoyServerMessage {
  type: GameBoyServerMessageType;
  error?: IGameBoyServerError;
  message?: IGameBoyPublishedMessage;
  metrics?: IGameBoyMetrics;
  state?: IGameBoyClientState;
  health?: IGameBoyHealth;
}

export enum GameBoyServerMessageType { State, Error, Message, Metrics, Health }

export class GameBoyServerMessage implements IGameBoyServerMessage {


  constructor(type: GameBoyServerMessageType,
              value: IGameBoyServerError | IGameBoyPublishedMessage | IGameBoyMetrics | IGameBoyClientState | IGameBoyHealth) {

    this.type = type;
    switch (type) {
      case GameBoyServerMessageType.Error:
        this.error = <IGameBoyServerError> value;
        break;

      case GameBoyServerMessageType.Message:
        this.message = <IGameBoyPublishedMessage> value;
        break;

      case GameBoyServerMessageType.Metrics:
        this.metrics = <IGameBoyMetrics> value;
        break;

      case GameBoyServerMessageType.State:
        this.state = <IGameBoyClientState> value;
        break;

      case GameBoyServerMessageType.Health:
        this.health = <IGameBoyHealth> value;
        break;
    }

  }

  public type: GameBoyServerMessageType;
  public error: IGameBoyServerError;
  public message: IGameBoyPublishedMessage;
  public metrics: IGameBoyMetrics;
  public state: IGameBoyClientState;
  public health?: IGameBoyHealth;

}
