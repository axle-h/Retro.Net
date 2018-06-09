import {Long} from "protobufjs";
import * as $protobuf from "protobufjs";

/** Properties of an AddBreakpoint. */
export interface IAddBreakpoint {

    /** AddBreakpoint address */
    address?: (number|null);
}

/** Represents an AddBreakpoint. */
export class AddBreakpoint implements IAddBreakpoint {

    /**
     * Constructs a new AddBreakpoint.
     * @param [properties] Properties to set
     */
    constructor(properties?: IAddBreakpoint);

    /** AddBreakpoint address. */
    public address: number;

    /**
     * Creates a new AddBreakpoint instance using the specified properties.
     * @param [properties] Properties to set
     * @returns AddBreakpoint instance
     */
    public static create(properties?: IAddBreakpoint): AddBreakpoint;

    /**
     * Encodes the specified AddBreakpoint message. Does not implicitly {@link AddBreakpoint.verify|verify} messages.
     * @param message AddBreakpoint message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IAddBreakpoint, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified AddBreakpoint message, length delimited. Does not implicitly {@link AddBreakpoint.verify|verify} messages.
     * @param message AddBreakpoint message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IAddBreakpoint, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes an AddBreakpoint message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns AddBreakpoint
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): AddBreakpoint;

    /**
     * Decodes an AddBreakpoint message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns AddBreakpoint
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): AddBreakpoint;

    /**
     * Verifies an AddBreakpoint message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates an AddBreakpoint message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns AddBreakpoint
     */
    public static fromObject(object: { [k: string]: any }): AddBreakpoint;

    /**
     * Creates a plain object from an AddBreakpoint message. Also converts values to other types if specified.
     * @param message AddBreakpoint
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: AddBreakpoint, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this AddBreakpoint to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a RemoveBreakpoint. */
export interface IRemoveBreakpoint {

    /** RemoveBreakpoint address */
    address?: (number|null);
}

/** Represents a RemoveBreakpoint. */
export class RemoveBreakpoint implements IRemoveBreakpoint {

    /**
     * Constructs a new RemoveBreakpoint.
     * @param [properties] Properties to set
     */
    constructor(properties?: IRemoveBreakpoint);

    /** RemoveBreakpoint address. */
    public address: number;

    /**
     * Creates a new RemoveBreakpoint instance using the specified properties.
     * @param [properties] Properties to set
     * @returns RemoveBreakpoint instance
     */
    public static create(properties?: IRemoveBreakpoint): RemoveBreakpoint;

    /**
     * Encodes the specified RemoveBreakpoint message. Does not implicitly {@link RemoveBreakpoint.verify|verify} messages.
     * @param message RemoveBreakpoint message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IRemoveBreakpoint, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified RemoveBreakpoint message, length delimited. Does not implicitly {@link RemoveBreakpoint.verify|verify} messages.
     * @param message RemoveBreakpoint message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IRemoveBreakpoint, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a RemoveBreakpoint message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns RemoveBreakpoint
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): RemoveBreakpoint;

    /**
     * Decodes a RemoveBreakpoint message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns RemoveBreakpoint
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): RemoveBreakpoint;

    /**
     * Verifies a RemoveBreakpoint message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a RemoveBreakpoint message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns RemoveBreakpoint
     */
    public static fromObject(object: { [k: string]: any }): RemoveBreakpoint;

    /**
     * Creates a plain object from a RemoveBreakpoint message. Also converts values to other types if specified.
     * @param message RemoveBreakpoint
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: RemoveBreakpoint, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this RemoveBreakpoint to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GetBreakpoints. */
export interface IGetBreakpoints {

    /** GetBreakpoints breakpoints */
    breakpoints?: (number[]|null);
}

/** Represents a GetBreakpoints. */
export class GetBreakpoints implements IGetBreakpoints {

    /**
     * Constructs a new GetBreakpoints.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGetBreakpoints);

    /** GetBreakpoints breakpoints. */
    public breakpoints: number[];

    /**
     * Creates a new GetBreakpoints instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GetBreakpoints instance
     */
    public static create(properties?: IGetBreakpoints): GetBreakpoints;

    /**
     * Encodes the specified GetBreakpoints message. Does not implicitly {@link GetBreakpoints.verify|verify} messages.
     * @param message GetBreakpoints message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGetBreakpoints, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GetBreakpoints message, length delimited. Does not implicitly {@link GetBreakpoints.verify|verify} messages.
     * @param message GetBreakpoints message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGetBreakpoints, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GetBreakpoints message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GetBreakpoints
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GetBreakpoints;

    /**
     * Decodes a GetBreakpoints message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GetBreakpoints
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GetBreakpoints;

    /**
     * Verifies a GetBreakpoints message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GetBreakpoints message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GetBreakpoints
     */
    public static fromObject(object: { [k: string]: any }): GetBreakpoints;

    /**
     * Creates a plain object from a GetBreakpoints message. Also converts values to other types if specified.
     * @param message GetBreakpoints
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GetBreakpoints, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GetBreakpoints to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** DebuggerCommand enum. */
export enum DebuggerCommand {
    BREAK = 0,
    CONTINUE = 1,
    STEP_OVER = 2
}

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

/** Properties of a GameBoyDebuggerCommand. */
export interface IGameBoyDebuggerCommand {

    /** GameBoyDebuggerCommand addBreakpoint */
    addBreakpoint?: (IAddBreakpoint|null);

    /** GameBoyDebuggerCommand removeBreakpoint */
    removeBreakpoint?: (IRemoveBreakpoint|null);

    /** GameBoyDebuggerCommand getBreakpoints */
    getBreakpoints?: (IGetBreakpoints|null);

    /** GameBoyDebuggerCommand command */
    command?: (DebuggerCommand|null);
}

/** Represents a GameBoyDebuggerCommand. */
export class GameBoyDebuggerCommand implements IGameBoyDebuggerCommand {

    /**
     * Constructs a new GameBoyDebuggerCommand.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyDebuggerCommand);

    /** GameBoyDebuggerCommand addBreakpoint. */
    public addBreakpoint?: (IAddBreakpoint|null);

    /** GameBoyDebuggerCommand removeBreakpoint. */
    public removeBreakpoint?: (IRemoveBreakpoint|null);

    /** GameBoyDebuggerCommand getBreakpoints. */
    public getBreakpoints?: (IGetBreakpoints|null);

    /** GameBoyDebuggerCommand command. */
    public command: DebuggerCommand;

    /** GameBoyDebuggerCommand value. */
    public value?: ("addBreakpoint"|"removeBreakpoint"|"getBreakpoints"|"command");

    /**
     * Creates a new GameBoyDebuggerCommand instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyDebuggerCommand instance
     */
    public static create(properties?: IGameBoyDebuggerCommand): GameBoyDebuggerCommand;

    /**
     * Encodes the specified GameBoyDebuggerCommand message. Does not implicitly {@link GameBoyDebuggerCommand.verify|verify} messages.
     * @param message GameBoyDebuggerCommand message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyDebuggerCommand, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyDebuggerCommand message, length delimited. Does not implicitly {@link GameBoyDebuggerCommand.verify|verify} messages.
     * @param message GameBoyDebuggerCommand message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyDebuggerCommand, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyDebuggerCommand message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyDebuggerCommand
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyDebuggerCommand;

    /**
     * Decodes a GameBoyDebuggerCommand message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyDebuggerCommand
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyDebuggerCommand;

    /**
     * Verifies a GameBoyDebuggerCommand message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyDebuggerCommand message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyDebuggerCommand
     */
    public static fromObject(object: { [k: string]: any }): GameBoyDebuggerCommand;

    /**
     * Creates a plain object from a GameBoyDebuggerCommand message. Also converts values to other types if specified.
     * @param message GameBoyDebuggerCommand
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyDebuggerCommand, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyDebuggerCommand to JSON.
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

    /** GameBoyCommand debugger */
    "debugger"?: (IGameBoyDebuggerCommand|null);
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

    /** GameBoyCommand debugger. */
    public debugger?: (IGameBoyDebuggerCommand|null);

    /** GameBoyCommand value. */
    public value?: ("heartBeat"|"setState"|"pressButton"|"debugger");

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

/** Properties of a GameBoyInstructionBlock. */
export interface IGameBoyInstructionBlock {

    /** GameBoyInstructionBlock address */
    address?: (number|null);

    /** GameBoyInstructionBlock length */
    length?: (number|null);

    /** GameBoyInstructionBlock haltCpu */
    haltCpu?: (boolean|null);

    /** GameBoyInstructionBlock haltPeripherals */
    haltPeripherals?: (boolean|null);

    /** GameBoyInstructionBlock debugInfo */
    debugInfo?: (string|null);

    /** GameBoyInstructionBlock operations */
    operations?: (IGameBoyOperation[]|null);

    /** GameBoyInstructionBlock timings */
    timings?: (IGameBoyIntructionTimings|null);
}

/** Represents a GameBoyInstructionBlock. */
export class GameBoyInstructionBlock implements IGameBoyInstructionBlock {

    /**
     * Constructs a new GameBoyInstructionBlock.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyInstructionBlock);

    /** GameBoyInstructionBlock address. */
    public address: number;

    /** GameBoyInstructionBlock length. */
    public length: number;

    /** GameBoyInstructionBlock haltCpu. */
    public haltCpu: boolean;

    /** GameBoyInstructionBlock haltPeripherals. */
    public haltPeripherals: boolean;

    /** GameBoyInstructionBlock debugInfo. */
    public debugInfo: string;

    /** GameBoyInstructionBlock operations. */
    public operations: IGameBoyOperation[];

    /** GameBoyInstructionBlock timings. */
    public timings?: (IGameBoyIntructionTimings|null);

    /**
     * Creates a new GameBoyInstructionBlock instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyInstructionBlock instance
     */
    public static create(properties?: IGameBoyInstructionBlock): GameBoyInstructionBlock;

    /**
     * Encodes the specified GameBoyInstructionBlock message. Does not implicitly {@link GameBoyInstructionBlock.verify|verify} messages.
     * @param message GameBoyInstructionBlock message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyInstructionBlock, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyInstructionBlock message, length delimited. Does not implicitly {@link GameBoyInstructionBlock.verify|verify} messages.
     * @param message GameBoyInstructionBlock message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyInstructionBlock, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyInstructionBlock message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyInstructionBlock
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyInstructionBlock;

    /**
     * Decodes a GameBoyInstructionBlock message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyInstructionBlock
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyInstructionBlock;

    /**
     * Verifies a GameBoyInstructionBlock message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyInstructionBlock message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyInstructionBlock
     */
    public static fromObject(object: { [k: string]: any }): GameBoyInstructionBlock;

    /**
     * Creates a plain object from a GameBoyInstructionBlock message. Also converts values to other types if specified.
     * @param message GameBoyInstructionBlock
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyInstructionBlock, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyInstructionBlock to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyOperation. */
export interface IGameBoyOperation {

    /** GameBoyOperation address */
    address?: (number|null);

    /** GameBoyOperation operation */
    operation?: (string|null);
}

/** Represents a GameBoyOperation. */
export class GameBoyOperation implements IGameBoyOperation {

    /**
     * Constructs a new GameBoyOperation.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyOperation);

    /** GameBoyOperation address. */
    public address: number;

    /** GameBoyOperation operation. */
    public operation: string;

    /**
     * Creates a new GameBoyOperation instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyOperation instance
     */
    public static create(properties?: IGameBoyOperation): GameBoyOperation;

    /**
     * Encodes the specified GameBoyOperation message. Does not implicitly {@link GameBoyOperation.verify|verify} messages.
     * @param message GameBoyOperation message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyOperation, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyOperation message, length delimited. Does not implicitly {@link GameBoyOperation.verify|verify} messages.
     * @param message GameBoyOperation message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyOperation, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyOperation message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyOperation
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyOperation;

    /**
     * Decodes a GameBoyOperation message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyOperation
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyOperation;

    /**
     * Verifies a GameBoyOperation message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyOperation message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyOperation
     */
    public static fromObject(object: { [k: string]: any }): GameBoyOperation;

    /**
     * Creates a plain object from a GameBoyOperation message. Also converts values to other types if specified.
     * @param message GameBoyOperation
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyOperation, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyOperation to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyIntructionTimings. */
export interface IGameBoyIntructionTimings {

    /** GameBoyIntructionTimings machineCycles */
    machineCycles?: (number|null);

    /** GameBoyIntructionTimings throttlingStates */
    throttlingStates?: (number|null);
}

/** Represents a GameBoyIntructionTimings. */
export class GameBoyIntructionTimings implements IGameBoyIntructionTimings {

    /**
     * Constructs a new GameBoyIntructionTimings.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyIntructionTimings);

    /** GameBoyIntructionTimings machineCycles. */
    public machineCycles: number;

    /** GameBoyIntructionTimings throttlingStates. */
    public throttlingStates: number;

    /**
     * Creates a new GameBoyIntructionTimings instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyIntructionTimings instance
     */
    public static create(properties?: IGameBoyIntructionTimings): GameBoyIntructionTimings;

    /**
     * Encodes the specified GameBoyIntructionTimings message. Does not implicitly {@link GameBoyIntructionTimings.verify|verify} messages.
     * @param message GameBoyIntructionTimings message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyIntructionTimings, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyIntructionTimings message, length delimited. Does not implicitly {@link GameBoyIntructionTimings.verify|verify} messages.
     * @param message GameBoyIntructionTimings message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyIntructionTimings, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyIntructionTimings message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyIntructionTimings
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyIntructionTimings;

    /**
     * Decodes a GameBoyIntructionTimings message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyIntructionTimings
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyIntructionTimings;

    /**
     * Verifies a GameBoyIntructionTimings message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyIntructionTimings message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyIntructionTimings
     */
    public static fromObject(object: { [k: string]: any }): GameBoyIntructionTimings;

    /**
     * Creates a plain object from a GameBoyIntructionTimings message. Also converts values to other types if specified.
     * @param message GameBoyIntructionTimings
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyIntructionTimings, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyIntructionTimings to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyGpuState. */
export interface IGameBoyGpuState {

    /** GameBoyGpuState renderSettings */
    renderSettings?: (IGameBoyRenderSettings|null);

    /** GameBoyGpuState backgroundTiles */
    backgroundTiles?: (IGameBoyTile[]|null);

    /** GameBoyGpuState spriteTiles */
    spriteTiles?: (IGameBoyTile[]|null);

    /** GameBoyGpuState sprites */
    sprites?: (IGameBoySprite[]|null);
}

/** Represents a GameBoyGpuState. */
export class GameBoyGpuState implements IGameBoyGpuState {

    /**
     * Constructs a new GameBoyGpuState.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyGpuState);

    /** GameBoyGpuState renderSettings. */
    public renderSettings?: (IGameBoyRenderSettings|null);

    /** GameBoyGpuState backgroundTiles. */
    public backgroundTiles: IGameBoyTile[];

    /** GameBoyGpuState spriteTiles. */
    public spriteTiles: IGameBoyTile[];

    /** GameBoyGpuState sprites. */
    public sprites: IGameBoySprite[];

    /**
     * Creates a new GameBoyGpuState instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyGpuState instance
     */
    public static create(properties?: IGameBoyGpuState): GameBoyGpuState;

    /**
     * Encodes the specified GameBoyGpuState message. Does not implicitly {@link GameBoyGpuState.verify|verify} messages.
     * @param message GameBoyGpuState message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyGpuState, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyGpuState message, length delimited. Does not implicitly {@link GameBoyGpuState.verify|verify} messages.
     * @param message GameBoyGpuState message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyGpuState, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyGpuState message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyGpuState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyGpuState;

    /**
     * Decodes a GameBoyGpuState message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyGpuState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyGpuState;

    /**
     * Verifies a GameBoyGpuState message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyGpuState message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyGpuState
     */
    public static fromObject(object: { [k: string]: any }): GameBoyGpuState;

    /**
     * Creates a plain object from a GameBoyGpuState message. Also converts values to other types if specified.
     * @param message GameBoyGpuState
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyGpuState, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyGpuState to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyRenderSettings. */
export interface IGameBoyRenderSettings {

    /** GameBoyRenderSettings backgroundDisplay */
    backgroundDisplay?: (boolean|null);

    /** GameBoyRenderSettings windowEnabled */
    windowEnabled?: (boolean|null);

    /** GameBoyRenderSettings spritesEnabled */
    spritesEnabled?: (boolean|null);

    /** GameBoyRenderSettings largeSprites */
    largeSprites?: (boolean|null);

    /** GameBoyRenderSettings backgroundTileMapAddress */
    backgroundTileMapAddress?: (ITileMapAddress|null);

    /** GameBoyRenderSettings windowTileMapAddress */
    windowTileMapAddress?: (ITileMapAddress|null);

    /** GameBoyRenderSettings tileSetAddress */
    tileSetAddress?: (number|null);

    /** GameBoyRenderSettings spriteTileSetAddress */
    spriteTileSetAddress?: (number|null);

    /** GameBoyRenderSettings scroll */
    scroll?: (ICoordinates|null);

    /** GameBoyRenderSettings windowPosition */
    windowPosition?: (ICoordinates|null);
}

/** Represents a GameBoyRenderSettings. */
export class GameBoyRenderSettings implements IGameBoyRenderSettings {

    /**
     * Constructs a new GameBoyRenderSettings.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyRenderSettings);

    /** GameBoyRenderSettings backgroundDisplay. */
    public backgroundDisplay: boolean;

    /** GameBoyRenderSettings windowEnabled. */
    public windowEnabled: boolean;

    /** GameBoyRenderSettings spritesEnabled. */
    public spritesEnabled: boolean;

    /** GameBoyRenderSettings largeSprites. */
    public largeSprites: boolean;

    /** GameBoyRenderSettings backgroundTileMapAddress. */
    public backgroundTileMapAddress?: (ITileMapAddress|null);

    /** GameBoyRenderSettings windowTileMapAddress. */
    public windowTileMapAddress?: (ITileMapAddress|null);

    /** GameBoyRenderSettings tileSetAddress. */
    public tileSetAddress: number;

    /** GameBoyRenderSettings spriteTileSetAddress. */
    public spriteTileSetAddress: number;

    /** GameBoyRenderSettings scroll. */
    public scroll?: (ICoordinates|null);

    /** GameBoyRenderSettings windowPosition. */
    public windowPosition?: (ICoordinates|null);

    /**
     * Creates a new GameBoyRenderSettings instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyRenderSettings instance
     */
    public static create(properties?: IGameBoyRenderSettings): GameBoyRenderSettings;

    /**
     * Encodes the specified GameBoyRenderSettings message. Does not implicitly {@link GameBoyRenderSettings.verify|verify} messages.
     * @param message GameBoyRenderSettings message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyRenderSettings, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyRenderSettings message, length delimited. Does not implicitly {@link GameBoyRenderSettings.verify|verify} messages.
     * @param message GameBoyRenderSettings message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyRenderSettings, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyRenderSettings message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyRenderSettings
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyRenderSettings;

    /**
     * Decodes a GameBoyRenderSettings message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyRenderSettings
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyRenderSettings;

    /**
     * Verifies a GameBoyRenderSettings message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyRenderSettings message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyRenderSettings
     */
    public static fromObject(object: { [k: string]: any }): GameBoyRenderSettings;

    /**
     * Creates a plain object from a GameBoyRenderSettings message. Also converts values to other types if specified.
     * @param message GameBoyRenderSettings
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyRenderSettings, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyRenderSettings to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a TileMapAddress. */
export interface ITileMapAddress {

    /** TileMapAddress address */
    address?: (number|null);

    /** TileMapAddress isSigned */
    isSigned?: (boolean|null);
}

/** Represents a TileMapAddress. */
export class TileMapAddress implements ITileMapAddress {

    /**
     * Constructs a new TileMapAddress.
     * @param [properties] Properties to set
     */
    constructor(properties?: ITileMapAddress);

    /** TileMapAddress address. */
    public address: number;

    /** TileMapAddress isSigned. */
    public isSigned: boolean;

    /**
     * Creates a new TileMapAddress instance using the specified properties.
     * @param [properties] Properties to set
     * @returns TileMapAddress instance
     */
    public static create(properties?: ITileMapAddress): TileMapAddress;

    /**
     * Encodes the specified TileMapAddress message. Does not implicitly {@link TileMapAddress.verify|verify} messages.
     * @param message TileMapAddress message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: ITileMapAddress, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified TileMapAddress message, length delimited. Does not implicitly {@link TileMapAddress.verify|verify} messages.
     * @param message TileMapAddress message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: ITileMapAddress, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a TileMapAddress message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns TileMapAddress
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): TileMapAddress;

    /**
     * Decodes a TileMapAddress message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns TileMapAddress
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): TileMapAddress;

    /**
     * Verifies a TileMapAddress message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a TileMapAddress message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns TileMapAddress
     */
    public static fromObject(object: { [k: string]: any }): TileMapAddress;

    /**
     * Creates a plain object from a TileMapAddress message. Also converts values to other types if specified.
     * @param message TileMapAddress
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: TileMapAddress, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this TileMapAddress to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a Coordinates. */
export interface ICoordinates {

    /** Coordinates x */
    x?: (number|null);

    /** Coordinates y */
    y?: (number|null);
}

/** Represents a Coordinates. */
export class Coordinates implements ICoordinates {

    /**
     * Constructs a new Coordinates.
     * @param [properties] Properties to set
     */
    constructor(properties?: ICoordinates);

    /** Coordinates x. */
    public x: number;

    /** Coordinates y. */
    public y: number;

    /**
     * Creates a new Coordinates instance using the specified properties.
     * @param [properties] Properties to set
     * @returns Coordinates instance
     */
    public static create(properties?: ICoordinates): Coordinates;

    /**
     * Encodes the specified Coordinates message. Does not implicitly {@link Coordinates.verify|verify} messages.
     * @param message Coordinates message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: ICoordinates, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified Coordinates message, length delimited. Does not implicitly {@link Coordinates.verify|verify} messages.
     * @param message Coordinates message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: ICoordinates, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a Coordinates message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns Coordinates
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): Coordinates;

    /**
     * Decodes a Coordinates message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns Coordinates
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): Coordinates;

    /**
     * Verifies a Coordinates message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a Coordinates message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns Coordinates
     */
    public static fromObject(object: { [k: string]: any }): Coordinates;

    /**
     * Creates a plain object from a Coordinates message. Also converts values to other types if specified.
     * @param message Coordinates
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: Coordinates, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this Coordinates to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoySprite. */
export interface IGameBoySprite {

    /** GameBoySprite x */
    x?: (number|null);

    /** GameBoySprite y */
    y?: (number|null);

    /** GameBoySprite tileNumber */
    tileNumber?: (number|null);

    /** GameBoySprite backgroundPriority */
    backgroundPriority?: (boolean|null);

    /** GameBoySprite yFlip */
    yFlip?: (boolean|null);

    /** GameBoySprite xFlip */
    xFlip?: (boolean|null);

    /** GameBoySprite usePalette1 */
    usePalette1?: (boolean|null);
}

/** Represents a GameBoySprite. */
export class GameBoySprite implements IGameBoySprite {

    /**
     * Constructs a new GameBoySprite.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoySprite);

    /** GameBoySprite x. */
    public x: number;

    /** GameBoySprite y. */
    public y: number;

    /** GameBoySprite tileNumber. */
    public tileNumber: number;

    /** GameBoySprite backgroundPriority. */
    public backgroundPriority: boolean;

    /** GameBoySprite yFlip. */
    public yFlip: boolean;

    /** GameBoySprite xFlip. */
    public xFlip: boolean;

    /** GameBoySprite usePalette1. */
    public usePalette1: boolean;

    /**
     * Creates a new GameBoySprite instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoySprite instance
     */
    public static create(properties?: IGameBoySprite): GameBoySprite;

    /**
     * Encodes the specified GameBoySprite message. Does not implicitly {@link GameBoySprite.verify|verify} messages.
     * @param message GameBoySprite message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoySprite, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoySprite message, length delimited. Does not implicitly {@link GameBoySprite.verify|verify} messages.
     * @param message GameBoySprite message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoySprite, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoySprite message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoySprite
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoySprite;

    /**
     * Decodes a GameBoySprite message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoySprite
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoySprite;

    /**
     * Verifies a GameBoySprite message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoySprite message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoySprite
     */
    public static fromObject(object: { [k: string]: any }): GameBoySprite;

    /**
     * Creates a plain object from a GameBoySprite message. Also converts values to other types if specified.
     * @param message GameBoySprite
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoySprite, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoySprite to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyTile. */
export interface IGameBoyTile {

    /** GameBoyTile tileNumber */
    tileNumber?: (number|null);

    /** GameBoyTile palette */
    palette?: (Uint8Array|null);
}

/** Represents a GameBoyTile. */
export class GameBoyTile implements IGameBoyTile {

    /**
     * Constructs a new GameBoyTile.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyTile);

    /** GameBoyTile tileNumber. */
    public tileNumber: number;

    /** GameBoyTile palette. */
    public palette: Uint8Array;

    /**
     * Creates a new GameBoyTile instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyTile instance
     */
    public static create(properties?: IGameBoyTile): GameBoyTile;

    /**
     * Encodes the specified GameBoyTile message. Does not implicitly {@link GameBoyTile.verify|verify} messages.
     * @param message GameBoyTile message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyTile, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyTile message, length delimited. Does not implicitly {@link GameBoyTile.verify|verify} messages.
     * @param message GameBoyTile message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyTile, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyTile message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyTile
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyTile;

    /**
     * Decodes a GameBoyTile message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyTile
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyTile;

    /**
     * Verifies a GameBoyTile message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyTile message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyTile
     */
    public static fromObject(object: { [k: string]: any }): GameBoyTile;

    /**
     * Creates a plain object from a GameBoyTile message. Also converts values to other types if specified.
     * @param message GameBoyTile
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyTile, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyTile to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyRegisters. */
export interface IGameBoyRegisters {

    /** GameBoyRegisters A */
    A?: (number|null);

    /** GameBoyRegisters B */
    B?: (number|null);

    /** GameBoyRegisters C */
    C?: (number|null);

    /** GameBoyRegisters D */
    D?: (number|null);

    /** GameBoyRegisters E */
    E?: (number|null);

    /** GameBoyRegisters H */
    H?: (number|null);

    /** GameBoyRegisters L */
    L?: (number|null);

    /** GameBoyRegisters Flags */
    Flags?: (IGameBoyFlagsRegister|null);

    /** GameBoyRegisters programCounter */
    programCounter?: (number|null);

    /** GameBoyRegisters stackPointer */
    stackPointer?: (number|null);

    /** GameBoyRegisters interruptFlipFlop1 */
    interruptFlipFlop1?: (boolean|null);

    /** GameBoyRegisters interruptFlipFlop2 */
    interruptFlipFlop2?: (boolean|null);

    /** GameBoyRegisters interruptMode */
    interruptMode?: (number|null);
}

/** Represents a GameBoyRegisters. */
export class GameBoyRegisters implements IGameBoyRegisters {

    /**
     * Constructs a new GameBoyRegisters.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyRegisters);

    /** GameBoyRegisters A. */
    public A: number;

    /** GameBoyRegisters B. */
    public B: number;

    /** GameBoyRegisters C. */
    public C: number;

    /** GameBoyRegisters D. */
    public D: number;

    /** GameBoyRegisters E. */
    public E: number;

    /** GameBoyRegisters H. */
    public H: number;

    /** GameBoyRegisters L. */
    public L: number;

    /** GameBoyRegisters Flags. */
    public Flags?: (IGameBoyFlagsRegister|null);

    /** GameBoyRegisters programCounter. */
    public programCounter: number;

    /** GameBoyRegisters stackPointer. */
    public stackPointer: number;

    /** GameBoyRegisters interruptFlipFlop1. */
    public interruptFlipFlop1: boolean;

    /** GameBoyRegisters interruptFlipFlop2. */
    public interruptFlipFlop2: boolean;

    /** GameBoyRegisters interruptMode. */
    public interruptMode: number;

    /**
     * Creates a new GameBoyRegisters instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyRegisters instance
     */
    public static create(properties?: IGameBoyRegisters): GameBoyRegisters;

    /**
     * Encodes the specified GameBoyRegisters message. Does not implicitly {@link GameBoyRegisters.verify|verify} messages.
     * @param message GameBoyRegisters message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyRegisters, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyRegisters message, length delimited. Does not implicitly {@link GameBoyRegisters.verify|verify} messages.
     * @param message GameBoyRegisters message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyRegisters, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyRegisters message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyRegisters
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyRegisters;

    /**
     * Decodes a GameBoyRegisters message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyRegisters
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyRegisters;

    /**
     * Verifies a GameBoyRegisters message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyRegisters message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyRegisters
     */
    public static fromObject(object: { [k: string]: any }): GameBoyRegisters;

    /**
     * Creates a plain object from a GameBoyRegisters message. Also converts values to other types if specified.
     * @param message GameBoyRegisters
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyRegisters, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyRegisters to JSON.
     * @returns JSON object
     */
    public toJSON(): { [k: string]: any };
}

/** Properties of a GameBoyFlagsRegister. */
export interface IGameBoyFlagsRegister {

    /** GameBoyFlagsRegister zero */
    zero?: (boolean|null);

    /** GameBoyFlagsRegister halfCarry */
    halfCarry?: (boolean|null);

    /** GameBoyFlagsRegister subtract */
    subtract?: (boolean|null);

    /** GameBoyFlagsRegister carry */
    carry?: (boolean|null);
}

/** Represents a GameBoyFlagsRegister. */
export class GameBoyFlagsRegister implements IGameBoyFlagsRegister {

    /**
     * Constructs a new GameBoyFlagsRegister.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyFlagsRegister);

    /** GameBoyFlagsRegister zero. */
    public zero: boolean;

    /** GameBoyFlagsRegister halfCarry. */
    public halfCarry: boolean;

    /** GameBoyFlagsRegister subtract. */
    public subtract: boolean;

    /** GameBoyFlagsRegister carry. */
    public carry: boolean;

    /**
     * Creates a new GameBoyFlagsRegister instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyFlagsRegister instance
     */
    public static create(properties?: IGameBoyFlagsRegister): GameBoyFlagsRegister;

    /**
     * Encodes the specified GameBoyFlagsRegister message. Does not implicitly {@link GameBoyFlagsRegister.verify|verify} messages.
     * @param message GameBoyFlagsRegister message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyFlagsRegister, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyFlagsRegister message, length delimited. Does not implicitly {@link GameBoyFlagsRegister.verify|verify} messages.
     * @param message GameBoyFlagsRegister message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyFlagsRegister, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyFlagsRegister message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyFlagsRegister
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyFlagsRegister;

    /**
     * Decodes a GameBoyFlagsRegister message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyFlagsRegister
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyFlagsRegister;

    /**
     * Verifies a GameBoyFlagsRegister message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyFlagsRegister message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyFlagsRegister
     */
    public static fromObject(object: { [k: string]: any }): GameBoyFlagsRegister;

    /**
     * Creates a plain object from a GameBoyFlagsRegister message. Also converts values to other types if specified.
     * @param message GameBoyFlagsRegister
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyFlagsRegister, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyFlagsRegister to JSON.
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

/** Properties of a GameBoyDebuggerEvent. */
export interface IGameBoyDebuggerEvent {

    /** GameBoyDebuggerEvent registers */
    registers?: (IGameBoyRegisters|null);

    /** GameBoyDebuggerEvent memory */
    memory?: (Uint8Array|null);

    /** GameBoyDebuggerEvent gpuState */
    gpuState?: (IGameBoyGpuState|null);

    /** GameBoyDebuggerEvent instructionBlock */
    instructionBlock?: (IGameBoyInstructionBlock|null);
}

/** Represents a GameBoyDebuggerEvent. */
export class GameBoyDebuggerEvent implements IGameBoyDebuggerEvent {

    /**
     * Constructs a new GameBoyDebuggerEvent.
     * @param [properties] Properties to set
     */
    constructor(properties?: IGameBoyDebuggerEvent);

    /** GameBoyDebuggerEvent registers. */
    public registers?: (IGameBoyRegisters|null);

    /** GameBoyDebuggerEvent memory. */
    public memory: Uint8Array;

    /** GameBoyDebuggerEvent gpuState. */
    public gpuState?: (IGameBoyGpuState|null);

    /** GameBoyDebuggerEvent instructionBlock. */
    public instructionBlock?: (IGameBoyInstructionBlock|null);

    /**
     * Creates a new GameBoyDebuggerEvent instance using the specified properties.
     * @param [properties] Properties to set
     * @returns GameBoyDebuggerEvent instance
     */
    public static create(properties?: IGameBoyDebuggerEvent): GameBoyDebuggerEvent;

    /**
     * Encodes the specified GameBoyDebuggerEvent message. Does not implicitly {@link GameBoyDebuggerEvent.verify|verify} messages.
     * @param message GameBoyDebuggerEvent message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encode(message: IGameBoyDebuggerEvent, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Encodes the specified GameBoyDebuggerEvent message, length delimited. Does not implicitly {@link GameBoyDebuggerEvent.verify|verify} messages.
     * @param message GameBoyDebuggerEvent message or plain object to encode
     * @param [writer] Writer to encode to
     * @returns Writer
     */
    public static encodeDelimited(message: IGameBoyDebuggerEvent, writer?: $protobuf.Writer): $protobuf.Writer;

    /**
     * Decodes a GameBoyDebuggerEvent message from the specified reader or buffer.
     * @param reader Reader or buffer to decode from
     * @param [length] Message length if known beforehand
     * @returns GameBoyDebuggerEvent
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decode(reader: ($protobuf.Reader|Uint8Array), length?: number): GameBoyDebuggerEvent;

    /**
     * Decodes a GameBoyDebuggerEvent message from the specified reader or buffer, length delimited.
     * @param reader Reader or buffer to decode from
     * @returns GameBoyDebuggerEvent
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    public static decodeDelimited(reader: ($protobuf.Reader|Uint8Array)): GameBoyDebuggerEvent;

    /**
     * Verifies a GameBoyDebuggerEvent message.
     * @param message Plain object to verify
     * @returns `null` if valid, otherwise the reason why it is not
     */
    public static verify(message: { [k: string]: any }): (string|null);

    /**
     * Creates a GameBoyDebuggerEvent message from a plain object. Also converts values to their respective internal types.
     * @param object Plain object
     * @returns GameBoyDebuggerEvent
     */
    public static fromObject(object: { [k: string]: any }): GameBoyDebuggerEvent;

    /**
     * Creates a plain object from a GameBoyDebuggerEvent message. Also converts values to other types if specified.
     * @param message GameBoyDebuggerEvent
     * @param [options] Conversion options
     * @returns Plain object
     */
    public static toObject(message: GameBoyDebuggerEvent, options?: $protobuf.IConversionOptions): { [k: string]: any };

    /**
     * Converts this GameBoyDebuggerEvent to JSON.
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

    /** GameBoyEvent debugger */
    "debugger"?: (IGameBoyDebuggerEvent|null);
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

    /** GameBoyEvent debugger. */
    public debugger?: (IGameBoyDebuggerEvent|null);

    /** GameBoyEvent value. */
    public value?: ("frame"|"publishedMessage"|"error"|"state"|"debugger");

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
