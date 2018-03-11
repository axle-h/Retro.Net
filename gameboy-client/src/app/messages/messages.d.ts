import {Long} from "protobufjs";
import * as $protobuf from "protobufjs";

/** Properties of a HeartBeat. */
export interface IHeartBeat {

    /** HeartBeat date */
    date?: (string|null);
}

/** Represents a HeartBeat. */
export class HeartBeat implements IHeartBeat {

    /**
     * Constructs a new HeartBeat.
     * @param [properties] Properties to set
     */
    constructor(properties?: IHeartBeat);

    /** HeartBeat date. */
    public date: string;

    /**
     * Creates a new HeartBeat instance using the specified properties.
     * @param [properties] Properties to set
     * @returns HeartBeat instance
     */
    public static create(properties?: IHeartBeat): HeartBeat;

    /**
     * Encodes the specified HeartBeat message. Does not implicitly {@link HeartBeat.verify|verify} messages.
     * @param message HeartBeat message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IHeartBeat, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified HeartBeat message, length delimited. Does not implicitly {@link HeartBeat.verify|verify} messages.
     * @param message HeartBeat message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IHeartBeat, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a HeartBeat message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns HeartBeat
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): HeartBeat;

    /**
     * Decodes a HeartBeat message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns HeartBeat
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): HeartBeat;

    /**
     * Verifies a HeartBeat message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a HeartBeat message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns HeartBeat
     */
    public static fromObject(object: { [k: string]: any }): HeartBeat;

    /**
     * Creates a plain object from a HeartBeat message. Also converts values to other types if specified.
     * @param message HeartBeat
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: HeartBeat, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this HeartBeat to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a SetGameBoyClientState. */
export interface ISetGameBoyClientState {

    /** SetGameBoyClientState displayName */
    displayName?: (string|null);
}

/** Represents a SetGameBoyClientState. */
export class SetGameBoyClientState implements ISetGameBoyClientState {

    /**
     * Constructs a new SetGameBoyClientState.
     * @param [properties] Properties to set
     */
    constructor(properties?: ISetGameBoyClientState);

    /** SetGameBoyClientState displayName. */
    public displayName: string;

    /**
     * Creates a new SetGameBoyClientState instance using the specified properties.
     * @param [properties] Properties to set
     * @returns SetGameBoyClientState instance
     */
    public static create(properties?: ISetGameBoyClientState): SetGameBoyClientState;

    /**
     * Encodes the specified SetGameBoyClientState message. Does not implicitly {@link SetGameBoyClientState.verify|verify} messages.
     * @param message SetGameBoyClientState message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: ISetGameBoyClientState, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified SetGameBoyClientState message, length delimited. Does not implicitly {@link SetGameBoyClientState.verify|verify} messages.
     * @param message SetGameBoyClientState message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: ISetGameBoyClientState, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a SetGameBoyClientState message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns SetGameBoyClientState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): SetGameBoyClientState;

    /**
     * Decodes a SetGameBoyClientState message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns SetGameBoyClientState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): SetGameBoyClientState;

    /**
     * Verifies a SetGameBoyClientState message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a SetGameBoyClientState message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns SetGameBoyClientState
     */
    public static fromObject(object: { [k: string]: any }): SetGameBoyClientState;

    /**
     * Creates a plain object from a SetGameBoyClientState message. Also converts values to other types if specified.
     * @param message SetGameBoyClientState
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: SetGameBoyClientState, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this SetGameBoyClientState to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** GameBoyJoyPadButton enum. */
export enum GameBoyJoyPadButton {
    NONE = 0,
    UP = 1,
    DOWN = 2,
    LEFT = 3,
    RIGHT = 4,
    A = 5,
    B = 6,
    START = 7,
    SELECT = 8
}

/** Properties of a RequestGameBoyJoyPadButtonPress. */
export interface IRequestGameBoyJoyPadButtonPress {

    /** RequestGameBoyJoyPadButtonPress button */
    button?: (GameBoyJoyPadButton|null);
}

/** Represents a RequestGameBoyJoyPadButtonPress. */
export class RequestGameBoyJoyPadButtonPress implements IRequestGameBoyJoyPadButtonPress {

    /**
     * Constructs a new RequestGameBoyJoyPadButtonPress.
     * @param [properties] Properties to set
     */
    constructor(properties?: IRequestGameBoyJoyPadButtonPress);

    /** RequestGameBoyJoyPadButtonPress button. */
    public button: GameBoyJoyPadButton;

    /**
     * Creates a new RequestGameBoyJoyPadButtonPress instance using the specified properties.
     * @param [properties] Properties to set
     * @returns RequestGameBoyJoyPadButtonPress instance
     */
    public static create(properties?: IRequestGameBoyJoyPadButtonPress): RequestGameBoyJoyPadButtonPress;

    /**
     * Encodes the specified RequestGameBoyJoyPadButtonPress message. Does not implicitly {@link RequestGameBoyJoyPadButtonPress.verify|verify} messages.
     * @param message RequestGameBoyJoyPadButtonPress message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IRequestGameBoyJoyPadButtonPress, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified RequestGameBoyJoyPadButtonPress message, length delimited. Does not implicitly {@link RequestGameBoyJoyPadButtonPress.verify|verify} messages.
     * @param message RequestGameBoyJoyPadButtonPress message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IRequestGameBoyJoyPadButtonPress, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a RequestGameBoyJoyPadButtonPress message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns RequestGameBoyJoyPadButtonPress
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): RequestGameBoyJoyPadButtonPress;

    /**
     * Decodes a RequestGameBoyJoyPadButtonPress message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns RequestGameBoyJoyPadButtonPress
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): RequestGameBoyJoyPadButtonPress;

    /**
     * Verifies a RequestGameBoyJoyPadButtonPress message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a RequestGameBoyJoyPadButtonPress message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns RequestGameBoyJoyPadButtonPress
     */
    public static fromObject(object: { [k: string]: any }): RequestGameBoyJoyPadButtonPress;

    /**
     * Creates a plain object from a RequestGameBoyJoyPadButtonPress message. Also converts values to other types if specified.
     * @param message RequestGameBoyJoyPadButtonPress
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: RequestGameBoyJoyPadButtonPress, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this RequestGameBoyJoyPadButtonPress to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyCommand. */
export interface IGameBoyCommand {

    /** GameBoyCommand heartBeat */
    heartBeat?: (IHeartBeat|null);

    /** GameBoyCommand setState */
    setState?: (ISetGameBoyClientState|null);

    /** GameBoyCommand pressButton */
    pressButton?: (IRequestGameBoyJoyPadButtonPress|null);
}

/** Represents a GameBoyCommand. */
export class GameBoyCommand implements IGameBoyCommand {

    /**
     * Constructs a new GameBoyCommand.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyCommand);

    /** GameBoyCommand heartBeat. */
    public heartBeat?: (IHeartBeat|null);

    /** GameBoyCommand setState. */
    public setState?: (ISetGameBoyClientState|null);

    /** GameBoyCommand pressButton. */
    public pressButton?: (IRequestGameBoyJoyPadButtonPress|null);

    /** GameBoyCommand value. */
    public value?: ("heartBeat"|"setState"|"pressButton");

    /**
     * Creates a new GameBoyCommand instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyCommand instance
     */
    public static create(properties?: IGameBoyCommand): GameBoyCommand;

    /**
     * Encodes the specified GameBoyCommand message. Does not implicitly {@link GameBoyCommand.verify|verify} messages.
     * @param message GameBoyCommand message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyCommand, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyCommand message, length delimited. Does not implicitly {@link GameBoyCommand.verify|verify} messages.
     * @param message GameBoyCommand message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyCommand, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyCommand message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyCommand
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyCommand;

    /**
     * Decodes a GameBoyCommand message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyCommand
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyCommand;

    /**
     * Verifies a GameBoyCommand message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyCommand message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyCommand
     */
    public static fromObject(object: { [k: string]: any }): GameBoyCommand;

    /**
     * Creates a plain object from a GameBoyCommand message. Also converts values to other types if specified.
     * @param message GameBoyCommand
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyCommand, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyCommand to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyGpuFrame. */
export interface IGameBoyGpuFrame {

    /** GameBoyGpuFrame data */
    data?: (Uint8Array|null);

    /** GameBoyGpuFrame framesPerSecond */
    framesPerSecond?: (number|null);
}

/** Represents a GameBoyGpuFrame. */
export class GameBoyGpuFrame implements IGameBoyGpuFrame {

    /**
     * Constructs a new GameBoyGpuFrame.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyGpuFrame);

    /** GameBoyGpuFrame data. */
    public data: Uint8Array;

    /** GameBoyGpuFrame framesPerSecond. */
    public framesPerSecond: number;

    /**
     * Creates a new GameBoyGpuFrame instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyGpuFrame instance
     */
    public static create(properties?: IGameBoyGpuFrame): GameBoyGpuFrame;

    /**
     * Encodes the specified GameBoyGpuFrame message. Does not implicitly {@link GameBoyGpuFrame.verify|verify} messages.
     * @param message GameBoyGpuFrame message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyGpuFrame, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyGpuFrame message, length delimited. Does not implicitly {@link GameBoyGpuFrame.verify|verify} messages.
     * @param message GameBoyGpuFrame message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyGpuFrame, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyGpuFrame message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyGpuFrame
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyGpuFrame;

    /**
     * Decodes a GameBoyGpuFrame message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyGpuFrame
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyGpuFrame;

    /**
     * Verifies a GameBoyGpuFrame message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyGpuFrame message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyGpuFrame
     */
    public static fromObject(object: { [k: string]: any }): GameBoyGpuFrame;

    /**
     * Creates a plain object from a GameBoyGpuFrame message. Also converts values to other types if specified.
     * @param message GameBoyGpuFrame
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyGpuFrame, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyGpuFrame to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyPublishedMessage. */
export interface IGameBoyPublishedMessage {

    /** GameBoyPublishedMessage user */
    user?: (string|null);

    /** GameBoyPublishedMessage date */
    date?: (number|Long|null);

    /** GameBoyPublishedMessage body */
    body?: (string|null);
}

/** Represents a GameBoyPublishedMessage. */
export class GameBoyPublishedMessage implements IGameBoyPublishedMessage {

    /**
     * Constructs a new GameBoyPublishedMessage.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyPublishedMessage);

    /** GameBoyPublishedMessage user. */
    public user: string;

    /** GameBoyPublishedMessage date. */
    public date: (number|Long);

    /** GameBoyPublishedMessage body. */
    public body: string;

    /**
     * Creates a new GameBoyPublishedMessage instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyPublishedMessage instance
     */
    public static create(properties?: IGameBoyPublishedMessage): GameBoyPublishedMessage;

    /**
     * Encodes the specified GameBoyPublishedMessage message. Does not implicitly {@link GameBoyPublishedMessage.verify|verify} messages.
     * @param message GameBoyPublishedMessage message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyPublishedMessage, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyPublishedMessage message, length delimited. Does not implicitly {@link GameBoyPublishedMessage.verify|verify} messages.
     * @param message GameBoyPublishedMessage message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyPublishedMessage, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyPublishedMessage message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyPublishedMessage
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyPublishedMessage;

    /**
     * Decodes a GameBoyPublishedMessage message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyPublishedMessage
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyPublishedMessage;

    /**
     * Verifies a GameBoyPublishedMessage message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyPublishedMessage message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyPublishedMessage
     */
    public static fromObject(object: { [k: string]: any }): GameBoyPublishedMessage;

    /**
     * Creates a plain object from a GameBoyPublishedMessage message. Also converts values to other types if specified.
     * @param message GameBoyPublishedMessage
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyPublishedMessage, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyPublishedMessage to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyServerError. */
export interface IGameBoyServerError {

    /** GameBoyServerError reasons */
    reasons?: (string[]|null);
}

/** Represents a GameBoyServerError. */
export class GameBoyServerError implements IGameBoyServerError {

    /**
     * Constructs a new GameBoyServerError.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyServerError);

    /** GameBoyServerError reasons. */
    public reasons: string[];

    /**
     * Creates a new GameBoyServerError instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyServerError instance
     */
    public static create(properties?: IGameBoyServerError): GameBoyServerError;

    /**
     * Encodes the specified GameBoyServerError message. Does not implicitly {@link GameBoyServerError.verify|verify} messages.
     * @param message GameBoyServerError message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyServerError, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyServerError message, length delimited. Does not implicitly {@link GameBoyServerError.verify|verify} messages.
     * @param message GameBoyServerError message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyServerError, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyServerError message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyServerError
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyServerError;

    /**
     * Decodes a GameBoyServerError message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyServerError
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyServerError;

    /**
     * Verifies a GameBoyServerError message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyServerError message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyServerError
     */
    public static fromObject(object: { [k: string]: any }): GameBoyServerError;

    /**
     * Creates a plain object from a GameBoyServerError message. Also converts values to other types if specified.
     * @param message GameBoyServerError
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyServerError, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyServerError to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyClientState. */
export interface IGameBoyClientState {

    /** GameBoyClientState displayName */
    displayName?: (string|null);
}

/** Represents a GameBoyClientState. */
export class GameBoyClientState implements IGameBoyClientState {

    /**
     * Constructs a new GameBoyClientState.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyClientState);

    /** GameBoyClientState displayName. */
    public displayName: string;

    /**
     * Creates a new GameBoyClientState instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyClientState instance
     */
    public static create(properties?: IGameBoyClientState): GameBoyClientState;

    /**
     * Encodes the specified GameBoyClientState message. Does not implicitly {@link GameBoyClientState.verify|verify} messages.
     * @param message GameBoyClientState message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyClientState, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyClientState message, length delimited. Does not implicitly {@link GameBoyClientState.verify|verify} messages.
     * @param message GameBoyClientState message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyClientState, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyClientState message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyClientState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyClientState;

    /**
     * Decodes a GameBoyClientState message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyClientState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyClientState;

    /**
     * Verifies a GameBoyClientState message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyClientState message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyClientState
     */
    public static fromObject(object: { [k: string]: any }): GameBoyClientState;

    /**
     * Creates a plain object from a GameBoyClientState message. Also converts values to other types if specified.
     * @param message GameBoyClientState
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyClientState, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyClientState to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyEvent. */
export interface IGameBoyEvent {

    /** GameBoyEvent frame */
    frame?: (IGameBoyGpuFrame|null);

    /** GameBoyEvent publishedMessage */
    publishedMessage?: (IGameBoyPublishedMessage|null);

    /** GameBoyEvent error */
    error?: (IGameBoyServerError|null);

    /** GameBoyEvent state */
    state?: (IGameBoyClientState|null);
}

/** Represents a GameBoyEvent. */
export class GameBoyEvent implements IGameBoyEvent {

    /**
     * Constructs a new GameBoyEvent.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyEvent);

    /** GameBoyEvent frame. */
    public frame?: (IGameBoyGpuFrame|null);

    /** GameBoyEvent publishedMessage. */
    public publishedMessage?: (IGameBoyPublishedMessage|null);

    /** GameBoyEvent error. */
    public error?: (IGameBoyServerError|null);

    /** GameBoyEvent state. */
    public state?: (IGameBoyClientState|null);

    /** GameBoyEvent value. */
    public value?: ("frame"|"publishedMessage"|"error"|"state");

    /**
     * Creates a new GameBoyEvent instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyEvent instance
     */
    public static create(properties?: IGameBoyEvent): GameBoyEvent;

    /**
     * Encodes the specified GameBoyEvent message. Does not implicitly {@link GameBoyEvent.verify|verify} messages.
     * @param message GameBoyEvent message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyEvent, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyEvent message, length delimited. Does not implicitly {@link GameBoyEvent.verify|verify} messages.
     * @param message GameBoyEvent message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyEvent, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyEvent message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyEvent
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyEvent;

    /**
     * Decodes a GameBoyEvent message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyEvent
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyEvent;

    /**
     * Verifies a GameBoyEvent message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyEvent message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyEvent
     */
    public static fromObject(object: { [k: string]: any }): GameBoyEvent;

    /**
     * Creates a plain object from a GameBoyEvent message. Also converts values to other types if specified.
     * @param message GameBoyEvent
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyEvent, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyEvent to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}
