/*eslint-disable block-scoped-var, no-redeclare, no-control-regex, no-prototype-builtins*/
"use strict";

var $protobuf = require("protobufjs/minimal");

// Common aliases
var $Reader = $protobuf.Reader, $Writer = $protobuf.Writer, $util = $protobuf.util;

// Exported root namespace
var $root = $protobuf.roots["default"] || ($protobuf.roots["default"] = {});

$root.HeartBeat = (function() {

    /**
     * Properties of a HeartBeat.
     * @exports IHeartBeat
     * @interface IHeartBeat
     * @property {string|null} [date] HeartBeat date
     */

    /**
     * Constructs a new HeartBeat.
     * @exports HeartBeat
     * @classdesc Represents a HeartBeat.
     * @implements IHeartBeat
     * @constructor
     * @param {IHeartBeat=} [properties] Properties to set
     */
    function HeartBeat(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * HeartBeat date.
     * @member {string} date
     * @memberof HeartBeat
     * @instance
     */
    HeartBeat.prototype.date = "";

    /**
     * Creates a new HeartBeat instance using the specified properties.
     * @function create
     * @memberof HeartBeat
     * @static
     * @param {IHeartBeat=} [properties] Properties to set
     * @returns {HeartBeat} HeartBeat instance
     */
    HeartBeat.create = function create(properties) {
        return new HeartBeat(properties);
    };

    /**
     * Encodes the specified HeartBeat message. Does not implicitly {@link HeartBeat.verify|verify} messages.
     * @function encode
     * @memberof HeartBeat
     * @static
     * @param {IHeartBeat} message HeartBeat message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    HeartBeat.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.date != null && message.hasOwnProperty("date"))
            writer.uint32(/* id 1, wireType 2 =*/10).string(message.date);
        return writer;
    };

    /**
     * Encodes the specified HeartBeat message, length delimited. Does not implicitly {@link HeartBeat.verify|verify} messages.
     * @function encodeDelimited
     * @memberof HeartBeat
     * @static
     * @param {IHeartBeat} message HeartBeat message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    HeartBeat.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a HeartBeat message from the specified reader or buffer.
     * @function decode
     * @memberof HeartBeat
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {HeartBeat} HeartBeat
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    HeartBeat.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.HeartBeat();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.date = reader.string();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a HeartBeat message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof HeartBeat
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {HeartBeat} HeartBeat
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    HeartBeat.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a HeartBeat message.
     * @function verify
     * @memberof HeartBeat
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    HeartBeat.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.date != null && message.hasOwnProperty("date"))
            if (!$util.isString(message.date))
                return "date: string expected";
        return null;
    };

    /**
     * Creates a HeartBeat message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof HeartBeat
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {HeartBeat} HeartBeat
     */
    HeartBeat.fromObject = function fromObject(object) {
        if (object instanceof $root.HeartBeat)
            return object;
        var message = new $root.HeartBeat();
        if (object.date != null)
            message.date = String(object.date);
        return message;
    };

    /**
     * Creates a plain object from a HeartBeat message. Also converts values to other types if specified.
     * @function toObject
     * @memberof HeartBeat
     * @static
     * @param {HeartBeat} message HeartBeat
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    HeartBeat.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults)
            object.date = "";
        if (message.date != null && message.hasOwnProperty("date"))
            object.date = message.date;
        return object;
    };

    /**
     * Converts this HeartBeat to JSON.
     * @function toJSON
     * @memberof HeartBeat
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    HeartBeat.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return HeartBeat;
})();

$root.SetGameBoyClientState = (function() {

    /**
     * Properties of a SetGameBoyClientState.
     * @exports ISetGameBoyClientState
     * @interface ISetGameBoyClientState
     * @property {string|null} [displayName] SetGameBoyClientState displayName
     */

    /**
     * Constructs a new SetGameBoyClientState.
     * @exports SetGameBoyClientState
     * @classdesc Represents a SetGameBoyClientState.
     * @implements ISetGameBoyClientState
     * @constructor
     * @param {ISetGameBoyClientState=} [properties] Properties to set
     */
    function SetGameBoyClientState(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * SetGameBoyClientState displayName.
     * @member {string} displayName
     * @memberof SetGameBoyClientState
     * @instance
     */
    SetGameBoyClientState.prototype.displayName = "";

    /**
     * Creates a new SetGameBoyClientState instance using the specified properties.
     * @function create
     * @memberof SetGameBoyClientState
     * @static
     * @param {ISetGameBoyClientState=} [properties] Properties to set
     * @returns {SetGameBoyClientState} SetGameBoyClientState instance
     */
    SetGameBoyClientState.create = function create(properties) {
        return new SetGameBoyClientState(properties);
    };

    /**
     * Encodes the specified SetGameBoyClientState message. Does not implicitly {@link SetGameBoyClientState.verify|verify} messages.
     * @function encode
     * @memberof SetGameBoyClientState
     * @static
     * @param {ISetGameBoyClientState} message SetGameBoyClientState message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    SetGameBoyClientState.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.displayName != null && message.hasOwnProperty("displayName"))
            writer.uint32(/* id 1, wireType 2 =*/10).string(message.displayName);
        return writer;
    };

    /**
     * Encodes the specified SetGameBoyClientState message, length delimited. Does not implicitly {@link SetGameBoyClientState.verify|verify} messages.
     * @function encodeDelimited
     * @memberof SetGameBoyClientState
     * @static
     * @param {ISetGameBoyClientState} message SetGameBoyClientState message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    SetGameBoyClientState.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a SetGameBoyClientState message from the specified reader or buffer.
     * @function decode
     * @memberof SetGameBoyClientState
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {SetGameBoyClientState} SetGameBoyClientState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    SetGameBoyClientState.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.SetGameBoyClientState();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.displayName = reader.string();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a SetGameBoyClientState message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof SetGameBoyClientState
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {SetGameBoyClientState} SetGameBoyClientState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    SetGameBoyClientState.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a SetGameBoyClientState message.
     * @function verify
     * @memberof SetGameBoyClientState
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    SetGameBoyClientState.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.displayName != null && message.hasOwnProperty("displayName"))
            if (!$util.isString(message.displayName))
                return "displayName: string expected";
        return null;
    };

    /**
     * Creates a SetGameBoyClientState message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof SetGameBoyClientState
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {SetGameBoyClientState} SetGameBoyClientState
     */
    SetGameBoyClientState.fromObject = function fromObject(object) {
        if (object instanceof $root.SetGameBoyClientState)
            return object;
        var message = new $root.SetGameBoyClientState();
        if (object.displayName != null)
            message.displayName = String(object.displayName);
        return message;
    };

    /**
     * Creates a plain object from a SetGameBoyClientState message. Also converts values to other types if specified.
     * @function toObject
     * @memberof SetGameBoyClientState
     * @static
     * @param {SetGameBoyClientState} message SetGameBoyClientState
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    SetGameBoyClientState.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults)
            object.displayName = "";
        if (message.displayName != null && message.hasOwnProperty("displayName"))
            object.displayName = message.displayName;
        return object;
    };

    /**
     * Converts this SetGameBoyClientState to JSON.
     * @function toJSON
     * @memberof SetGameBoyClientState
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    SetGameBoyClientState.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return SetGameBoyClientState;
})();

/**
 * GameBoyJoyPadButton enum.
 * @exports GameBoyJoyPadButton
 * @enum {string}
 * @property {number} NONE=0 NONE value
 * @property {number} UP=1 UP value
 * @property {number} DOWN=2 DOWN value
 * @property {number} LEFT=3 LEFT value
 * @property {number} RIGHT=4 RIGHT value
 * @property {number} A=5 A value
 * @property {number} B=6 B value
 * @property {number} START=7 START value
 * @property {number} SELECT=8 SELECT value
 */
$root.GameBoyJoyPadButton = (function() {
    var valuesById = {}, values = Object.create(valuesById);
    values[valuesById[0] = "NONE"] = 0;
    values[valuesById[1] = "UP"] = 1;
    values[valuesById[2] = "DOWN"] = 2;
    values[valuesById[3] = "LEFT"] = 3;
    values[valuesById[4] = "RIGHT"] = 4;
    values[valuesById[5] = "A"] = 5;
    values[valuesById[6] = "B"] = 6;
    values[valuesById[7] = "START"] = 7;
    values[valuesById[8] = "SELECT"] = 8;
    return values;
})();

$root.RequestGameBoyJoyPadButtonPress = (function() {

    /**
     * Properties of a RequestGameBoyJoyPadButtonPress.
     * @exports IRequestGameBoyJoyPadButtonPress
     * @interface IRequestGameBoyJoyPadButtonPress
     * @property {GameBoyJoyPadButton|null} [button] RequestGameBoyJoyPadButtonPress button
     */

    /**
     * Constructs a new RequestGameBoyJoyPadButtonPress.
     * @exports RequestGameBoyJoyPadButtonPress
     * @classdesc Represents a RequestGameBoyJoyPadButtonPress.
     * @implements IRequestGameBoyJoyPadButtonPress
     * @constructor
     * @param {IRequestGameBoyJoyPadButtonPress=} [properties] Properties to set
     */
    function RequestGameBoyJoyPadButtonPress(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * RequestGameBoyJoyPadButtonPress button.
     * @member {GameBoyJoyPadButton} button
     * @memberof RequestGameBoyJoyPadButtonPress
     * @instance
     */
    RequestGameBoyJoyPadButtonPress.prototype.button = 0;

    /**
     * Creates a new RequestGameBoyJoyPadButtonPress instance using the specified properties.
     * @function create
     * @memberof RequestGameBoyJoyPadButtonPress
     * @static
     * @param {IRequestGameBoyJoyPadButtonPress=} [properties] Properties to set
     * @returns {RequestGameBoyJoyPadButtonPress} RequestGameBoyJoyPadButtonPress instance
     */
    RequestGameBoyJoyPadButtonPress.create = function create(properties) {
        return new RequestGameBoyJoyPadButtonPress(properties);
    };

    /**
     * Encodes the specified RequestGameBoyJoyPadButtonPress message. Does not implicitly {@link RequestGameBoyJoyPadButtonPress.verify|verify} messages.
     * @function encode
     * @memberof RequestGameBoyJoyPadButtonPress
     * @static
     * @param {IRequestGameBoyJoyPadButtonPress} message RequestGameBoyJoyPadButtonPress message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    RequestGameBoyJoyPadButtonPress.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.button != null && message.hasOwnProperty("button"))
            writer.uint32(/* id 1, wireType 0 =*/8).int32(message.button);
        return writer;
    };

    /**
     * Encodes the specified RequestGameBoyJoyPadButtonPress message, length delimited. Does not implicitly {@link RequestGameBoyJoyPadButtonPress.verify|verify} messages.
     * @function encodeDelimited
     * @memberof RequestGameBoyJoyPadButtonPress
     * @static
     * @param {IRequestGameBoyJoyPadButtonPress} message RequestGameBoyJoyPadButtonPress message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    RequestGameBoyJoyPadButtonPress.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a RequestGameBoyJoyPadButtonPress message from the specified reader or buffer.
     * @function decode
     * @memberof RequestGameBoyJoyPadButtonPress
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {RequestGameBoyJoyPadButtonPress} RequestGameBoyJoyPadButtonPress
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    RequestGameBoyJoyPadButtonPress.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.RequestGameBoyJoyPadButtonPress();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.button = reader.int32();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a RequestGameBoyJoyPadButtonPress message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof RequestGameBoyJoyPadButtonPress
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {RequestGameBoyJoyPadButtonPress} RequestGameBoyJoyPadButtonPress
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    RequestGameBoyJoyPadButtonPress.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a RequestGameBoyJoyPadButtonPress message.
     * @function verify
     * @memberof RequestGameBoyJoyPadButtonPress
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    RequestGameBoyJoyPadButtonPress.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.button != null && message.hasOwnProperty("button"))
            switch (message.button) {
            default:
                return "button: enum value expected";
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                break;
            }
        return null;
    };

    /**
     * Creates a RequestGameBoyJoyPadButtonPress message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof RequestGameBoyJoyPadButtonPress
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {RequestGameBoyJoyPadButtonPress} RequestGameBoyJoyPadButtonPress
     */
    RequestGameBoyJoyPadButtonPress.fromObject = function fromObject(object) {
        if (object instanceof $root.RequestGameBoyJoyPadButtonPress)
            return object;
        var message = new $root.RequestGameBoyJoyPadButtonPress();
        switch (object.button) {
        case "NONE":
        case 0:
            message.button = 0;
            break;
        case "UP":
        case 1:
            message.button = 1;
            break;
        case "DOWN":
        case 2:
            message.button = 2;
            break;
        case "LEFT":
        case 3:
            message.button = 3;
            break;
        case "RIGHT":
        case 4:
            message.button = 4;
            break;
        case "A":
        case 5:
            message.button = 5;
            break;
        case "B":
        case 6:
            message.button = 6;
            break;
        case "START":
        case 7:
            message.button = 7;
            break;
        case "SELECT":
        case 8:
            message.button = 8;
            break;
        }
        return message;
    };

    /**
     * Creates a plain object from a RequestGameBoyJoyPadButtonPress message. Also converts values to other types if specified.
     * @function toObject
     * @memberof RequestGameBoyJoyPadButtonPress
     * @static
     * @param {RequestGameBoyJoyPadButtonPress} message RequestGameBoyJoyPadButtonPress
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    RequestGameBoyJoyPadButtonPress.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults)
            object.button = options.enums === String ? "NONE" : 0;
        if (message.button != null && message.hasOwnProperty("button"))
            object.button = options.enums === String ? $root.GameBoyJoyPadButton[message.button] : message.button;
        return object;
    };

    /**
     * Converts this RequestGameBoyJoyPadButtonPress to JSON.
     * @function toJSON
     * @memberof RequestGameBoyJoyPadButtonPress
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    RequestGameBoyJoyPadButtonPress.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return RequestGameBoyJoyPadButtonPress;
})();

$root.GameBoyCommand = (function() {

    /**
     * Properties of a GameBoyCommand.
     * @exports IGameBoyCommand
     * @interface IGameBoyCommand
     * @property {IHeartBeat|null} [heartBeat] GameBoyCommand heartBeat
     * @property {ISetGameBoyClientState|null} [setState] GameBoyCommand setState
     * @property {IRequestGameBoyJoyPadButtonPress|null} [pressButton] GameBoyCommand pressButton
     */

    /**
     * Constructs a new GameBoyCommand.
     * @exports GameBoyCommand
     * @classdesc Represents a GameBoyCommand.
     * @implements IGameBoyCommand
     * @constructor
     * @param {IGameBoyCommand=} [properties] Properties to set
     */
    function GameBoyCommand(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyCommand heartBeat.
     * @member {IHeartBeat|null|undefined} heartBeat
     * @memberof GameBoyCommand
     * @instance
     */
    GameBoyCommand.prototype.heartBeat = null;

    /**
     * GameBoyCommand setState.
     * @member {ISetGameBoyClientState|null|undefined} setState
     * @memberof GameBoyCommand
     * @instance
     */
    GameBoyCommand.prototype.setState = null;

    /**
     * GameBoyCommand pressButton.
     * @member {IRequestGameBoyJoyPadButtonPress|null|undefined} pressButton
     * @memberof GameBoyCommand
     * @instance
     */
    GameBoyCommand.prototype.pressButton = null;

    // OneOf field names bound to virtual getters and setters
    var $oneOfFields;

    /**
     * GameBoyCommand value.
     * @member {"heartBeat"|"setState"|"pressButton"|undefined} value
     * @memberof GameBoyCommand
     * @instance
     */
    Object.defineProperty(GameBoyCommand.prototype, "value", {
        get: $util.oneOfGetter($oneOfFields = ["heartBeat", "setState", "pressButton"]),
        set: $util.oneOfSetter($oneOfFields)
    });

    /**
     * Creates a new GameBoyCommand instance using the specified properties.
     * @function create
     * @memberof GameBoyCommand
     * @static
     * @param {IGameBoyCommand=} [properties] Properties to set
     * @returns {GameBoyCommand} GameBoyCommand instance
     */
    GameBoyCommand.create = function create(properties) {
        return new GameBoyCommand(properties);
    };

    /**
     * Encodes the specified GameBoyCommand message. Does not implicitly {@link GameBoyCommand.verify|verify} messages.
     * @function encode
     * @memberof GameBoyCommand
     * @static
     * @param {IGameBoyCommand} message GameBoyCommand message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyCommand.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.heartBeat != null && message.hasOwnProperty("heartBeat"))
            $root.HeartBeat.encode(message.heartBeat, writer.uint32(/* id 1, wireType 2 =*/10).fork()).ldelim();
        if (message.setState != null && message.hasOwnProperty("setState"))
            $root.SetGameBoyClientState.encode(message.setState, writer.uint32(/* id 2, wireType 2 =*/18).fork()).ldelim();
        if (message.pressButton != null && message.hasOwnProperty("pressButton"))
            $root.RequestGameBoyJoyPadButtonPress.encode(message.pressButton, writer.uint32(/* id 3, wireType 2 =*/26).fork()).ldelim();
        return writer;
    };

    /**
     * Encodes the specified GameBoyCommand message, length delimited. Does not implicitly {@link GameBoyCommand.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyCommand
     * @static
     * @param {IGameBoyCommand} message GameBoyCommand message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyCommand.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyCommand message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyCommand
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyCommand} GameBoyCommand
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyCommand.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyCommand();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.heartBeat = $root.HeartBeat.decode(reader, reader.uint32());
                break;
            case 2:
                message.setState = $root.SetGameBoyClientState.decode(reader, reader.uint32());
                break;
            case 3:
                message.pressButton = $root.RequestGameBoyJoyPadButtonPress.decode(reader, reader.uint32());
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyCommand message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyCommand
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyCommand} GameBoyCommand
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyCommand.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyCommand message.
     * @function verify
     * @memberof GameBoyCommand
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyCommand.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        var properties = {};
        if (message.heartBeat != null && message.hasOwnProperty("heartBeat")) {
            properties.value = 1;
            {
                var error = $root.HeartBeat.verify(message.heartBeat);
                if (error)
                    return "heartBeat." + error;
            }
        }
        if (message.setState != null && message.hasOwnProperty("setState")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            {
                var error = $root.SetGameBoyClientState.verify(message.setState);
                if (error)
                    return "setState." + error;
            }
        }
        if (message.pressButton != null && message.hasOwnProperty("pressButton")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            {
                var error = $root.RequestGameBoyJoyPadButtonPress.verify(message.pressButton);
                if (error)
                    return "pressButton." + error;
            }
        }
        return null;
    };

    /**
     * Creates a GameBoyCommand message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyCommand
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyCommand} GameBoyCommand
     */
    GameBoyCommand.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyCommand)
            return object;
        var message = new $root.GameBoyCommand();
        if (object.heartBeat != null) {
            if (typeof object.heartBeat !== "object")
                throw TypeError(".GameBoyCommand.heartBeat: object expected");
            message.heartBeat = $root.HeartBeat.fromObject(object.heartBeat);
        }
        if (object.setState != null) {
            if (typeof object.setState !== "object")
                throw TypeError(".GameBoyCommand.setState: object expected");
            message.setState = $root.SetGameBoyClientState.fromObject(object.setState);
        }
        if (object.pressButton != null) {
            if (typeof object.pressButton !== "object")
                throw TypeError(".GameBoyCommand.pressButton: object expected");
            message.pressButton = $root.RequestGameBoyJoyPadButtonPress.fromObject(object.pressButton);
        }
        return message;
    };

    /**
     * Creates a plain object from a GameBoyCommand message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyCommand
     * @static
     * @param {GameBoyCommand} message GameBoyCommand
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyCommand.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (message.heartBeat != null && message.hasOwnProperty("heartBeat")) {
            object.heartBeat = $root.HeartBeat.toObject(message.heartBeat, options);
            if (options.oneofs)
                object.value = "heartBeat";
        }
        if (message.setState != null && message.hasOwnProperty("setState")) {
            object.setState = $root.SetGameBoyClientState.toObject(message.setState, options);
            if (options.oneofs)
                object.value = "setState";
        }
        if (message.pressButton != null && message.hasOwnProperty("pressButton")) {
            object.pressButton = $root.RequestGameBoyJoyPadButtonPress.toObject(message.pressButton, options);
            if (options.oneofs)
                object.value = "pressButton";
        }
        return object;
    };

    /**
     * Converts this GameBoyCommand to JSON.
     * @function toJSON
     * @memberof GameBoyCommand
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyCommand.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyCommand;
})();

$root.GameBoyGpuFrame = (function() {

    /**
     * Properties of a GameBoyGpuFrame.
     * @exports IGameBoyGpuFrame
     * @interface IGameBoyGpuFrame
     * @property {Uint8Array|null} [data] GameBoyGpuFrame data
     * @property {number|null} [framesPerSecond] GameBoyGpuFrame framesPerSecond
     */

    /**
     * Constructs a new GameBoyGpuFrame.
     * @exports GameBoyGpuFrame
     * @classdesc Represents a GameBoyGpuFrame.
     * @implements IGameBoyGpuFrame
     * @constructor
     * @param {IGameBoyGpuFrame=} [properties] Properties to set
     */
    function GameBoyGpuFrame(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyGpuFrame data.
     * @member {Uint8Array} data
     * @memberof GameBoyGpuFrame
     * @instance
     */
    GameBoyGpuFrame.prototype.data = $util.newBuffer([]);

    /**
     * GameBoyGpuFrame framesPerSecond.
     * @member {number} framesPerSecond
     * @memberof GameBoyGpuFrame
     * @instance
     */
    GameBoyGpuFrame.prototype.framesPerSecond = 0;

    /**
     * Creates a new GameBoyGpuFrame instance using the specified properties.
     * @function create
     * @memberof GameBoyGpuFrame
     * @static
     * @param {IGameBoyGpuFrame=} [properties] Properties to set
     * @returns {GameBoyGpuFrame} GameBoyGpuFrame instance
     */
    GameBoyGpuFrame.create = function create(properties) {
        return new GameBoyGpuFrame(properties);
    };

    /**
     * Encodes the specified GameBoyGpuFrame message. Does not implicitly {@link GameBoyGpuFrame.verify|verify} messages.
     * @function encode
     * @memberof GameBoyGpuFrame
     * @static
     * @param {IGameBoyGpuFrame} message GameBoyGpuFrame message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyGpuFrame.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.data != null && message.hasOwnProperty("data"))
            writer.uint32(/* id 1, wireType 2 =*/10).bytes(message.data);
        if (message.framesPerSecond != null && message.hasOwnProperty("framesPerSecond"))
            writer.uint32(/* id 2, wireType 0 =*/16).int32(message.framesPerSecond);
        return writer;
    };

    /**
     * Encodes the specified GameBoyGpuFrame message, length delimited. Does not implicitly {@link GameBoyGpuFrame.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyGpuFrame
     * @static
     * @param {IGameBoyGpuFrame} message GameBoyGpuFrame message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyGpuFrame.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyGpuFrame message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyGpuFrame
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyGpuFrame} GameBoyGpuFrame
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyGpuFrame.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyGpuFrame();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.data = reader.bytes();
                break;
            case 2:
                message.framesPerSecond = reader.int32();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyGpuFrame message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyGpuFrame
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyGpuFrame} GameBoyGpuFrame
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyGpuFrame.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyGpuFrame message.
     * @function verify
     * @memberof GameBoyGpuFrame
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyGpuFrame.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.data != null && message.hasOwnProperty("data"))
            if (!(message.data && typeof message.data.length === "number" || $util.isString(message.data)))
                return "data: buffer expected";
        if (message.framesPerSecond != null && message.hasOwnProperty("framesPerSecond"))
            if (!$util.isInteger(message.framesPerSecond))
                return "framesPerSecond: integer expected";
        return null;
    };

    /**
     * Creates a GameBoyGpuFrame message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyGpuFrame
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyGpuFrame} GameBoyGpuFrame
     */
    GameBoyGpuFrame.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyGpuFrame)
            return object;
        var message = new $root.GameBoyGpuFrame();
        if (object.data != null)
            if (typeof object.data === "string")
                $util.base64.decode(object.data, message.data = $util.newBuffer($util.base64.length(object.data)), 0);
            else if (object.data.length)
                message.data = object.data;
        if (object.framesPerSecond != null)
            message.framesPerSecond = object.framesPerSecond | 0;
        return message;
    };

    /**
     * Creates a plain object from a GameBoyGpuFrame message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyGpuFrame
     * @static
     * @param {GameBoyGpuFrame} message GameBoyGpuFrame
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyGpuFrame.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.data = options.bytes === String ? "" : [];
            object.framesPerSecond = 0;
        }
        if (message.data != null && message.hasOwnProperty("data"))
            object.data = options.bytes === String ? $util.base64.encode(message.data, 0, message.data.length) : options.bytes === Array ? Array.prototype.slice.call(message.data) : message.data;
        if (message.framesPerSecond != null && message.hasOwnProperty("framesPerSecond"))
            object.framesPerSecond = message.framesPerSecond;
        return object;
    };

    /**
     * Converts this GameBoyGpuFrame to JSON.
     * @function toJSON
     * @memberof GameBoyGpuFrame
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyGpuFrame.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyGpuFrame;
})();

$root.GameBoyPublishedMessage = (function() {

    /**
     * Properties of a GameBoyPublishedMessage.
     * @exports IGameBoyPublishedMessage
     * @interface IGameBoyPublishedMessage
     * @property {string|null} [user] GameBoyPublishedMessage user
     * @property {number|Long|null} [date] GameBoyPublishedMessage date
     * @property {string|null} [body] GameBoyPublishedMessage body
     */

    /**
     * Constructs a new GameBoyPublishedMessage.
     * @exports GameBoyPublishedMessage
     * @classdesc Represents a GameBoyPublishedMessage.
     * @implements IGameBoyPublishedMessage
     * @constructor
     * @param {IGameBoyPublishedMessage=} [properties] Properties to set
     */
    function GameBoyPublishedMessage(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyPublishedMessage user.
     * @member {string} user
     * @memberof GameBoyPublishedMessage
     * @instance
     */
    GameBoyPublishedMessage.prototype.user = "";

    /**
     * GameBoyPublishedMessage date.
     * @member {number|Long} date
     * @memberof GameBoyPublishedMessage
     * @instance
     */
    GameBoyPublishedMessage.prototype.date = $util.Long ? $util.Long.fromBits(0,0,false) : 0;

    /**
     * GameBoyPublishedMessage body.
     * @member {string} body
     * @memberof GameBoyPublishedMessage
     * @instance
     */
    GameBoyPublishedMessage.prototype.body = "";

    /**
     * Creates a new GameBoyPublishedMessage instance using the specified properties.
     * @function create
     * @memberof GameBoyPublishedMessage
     * @static
     * @param {IGameBoyPublishedMessage=} [properties] Properties to set
     * @returns {GameBoyPublishedMessage} GameBoyPublishedMessage instance
     */
    GameBoyPublishedMessage.create = function create(properties) {
        return new GameBoyPublishedMessage(properties);
    };

    /**
     * Encodes the specified GameBoyPublishedMessage message. Does not implicitly {@link GameBoyPublishedMessage.verify|verify} messages.
     * @function encode
     * @memberof GameBoyPublishedMessage
     * @static
     * @param {IGameBoyPublishedMessage} message GameBoyPublishedMessage message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyPublishedMessage.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.user != null && message.hasOwnProperty("user"))
            writer.uint32(/* id 1, wireType 2 =*/10).string(message.user);
        if (message.date != null && message.hasOwnProperty("date"))
            writer.uint32(/* id 2, wireType 0 =*/16).int64(message.date);
        if (message.body != null && message.hasOwnProperty("body"))
            writer.uint32(/* id 3, wireType 2 =*/26).string(message.body);
        return writer;
    };

    /**
     * Encodes the specified GameBoyPublishedMessage message, length delimited. Does not implicitly {@link GameBoyPublishedMessage.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyPublishedMessage
     * @static
     * @param {IGameBoyPublishedMessage} message GameBoyPublishedMessage message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyPublishedMessage.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyPublishedMessage message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyPublishedMessage
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyPublishedMessage} GameBoyPublishedMessage
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyPublishedMessage.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyPublishedMessage();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.user = reader.string();
                break;
            case 2:
                message.date = reader.int64();
                break;
            case 3:
                message.body = reader.string();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyPublishedMessage message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyPublishedMessage
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyPublishedMessage} GameBoyPublishedMessage
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyPublishedMessage.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyPublishedMessage message.
     * @function verify
     * @memberof GameBoyPublishedMessage
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyPublishedMessage.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.user != null && message.hasOwnProperty("user"))
            if (!$util.isString(message.user))
                return "user: string expected";
        if (message.date != null && message.hasOwnProperty("date"))
            if (!$util.isInteger(message.date) && !(message.date && $util.isInteger(message.date.low) && $util.isInteger(message.date.high)))
                return "date: integer|Long expected";
        if (message.body != null && message.hasOwnProperty("body"))
            if (!$util.isString(message.body))
                return "body: string expected";
        return null;
    };

    /**
     * Creates a GameBoyPublishedMessage message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyPublishedMessage
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyPublishedMessage} GameBoyPublishedMessage
     */
    GameBoyPublishedMessage.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyPublishedMessage)
            return object;
        var message = new $root.GameBoyPublishedMessage();
        if (object.user != null)
            message.user = String(object.user);
        if (object.date != null)
            if ($util.Long)
                (message.date = $util.Long.fromValue(object.date)).unsigned = false;
            else if (typeof object.date === "string")
                message.date = parseInt(object.date, 10);
            else if (typeof object.date === "number")
                message.date = object.date;
            else if (typeof object.date === "object")
                message.date = new $util.LongBits(object.date.low >>> 0, object.date.high >>> 0).toNumber();
        if (object.body != null)
            message.body = String(object.body);
        return message;
    };

    /**
     * Creates a plain object from a GameBoyPublishedMessage message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyPublishedMessage
     * @static
     * @param {GameBoyPublishedMessage} message GameBoyPublishedMessage
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyPublishedMessage.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.user = "";
            if ($util.Long) {
                var long = new $util.Long(0, 0, false);
                object.date = options.longs === String ? long.toString() : options.longs === Number ? long.toNumber() : long;
            } else
                object.date = options.longs === String ? "0" : 0;
            object.body = "";
        }
        if (message.user != null && message.hasOwnProperty("user"))
            object.user = message.user;
        if (message.date != null && message.hasOwnProperty("date"))
            if (typeof message.date === "number")
                object.date = options.longs === String ? String(message.date) : message.date;
            else
                object.date = options.longs === String ? $util.Long.prototype.toString.call(message.date) : options.longs === Number ? new $util.LongBits(message.date.low >>> 0, message.date.high >>> 0).toNumber() : message.date;
        if (message.body != null && message.hasOwnProperty("body"))
            object.body = message.body;
        return object;
    };

    /**
     * Converts this GameBoyPublishedMessage to JSON.
     * @function toJSON
     * @memberof GameBoyPublishedMessage
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyPublishedMessage.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyPublishedMessage;
})();

$root.GameBoyServerError = (function() {

    /**
     * Properties of a GameBoyServerError.
     * @exports IGameBoyServerError
     * @interface IGameBoyServerError
     * @property {Array.<string>|null} [reasons] GameBoyServerError reasons
     */

    /**
     * Constructs a new GameBoyServerError.
     * @exports GameBoyServerError
     * @classdesc Represents a GameBoyServerError.
     * @implements IGameBoyServerError
     * @constructor
     * @param {IGameBoyServerError=} [properties] Properties to set
     */
    function GameBoyServerError(properties) {
        this.reasons = [];
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyServerError reasons.
     * @member {Array.<string>} reasons
     * @memberof GameBoyServerError
     * @instance
     */
    GameBoyServerError.prototype.reasons = $util.emptyArray;

    /**
     * Creates a new GameBoyServerError instance using the specified properties.
     * @function create
     * @memberof GameBoyServerError
     * @static
     * @param {IGameBoyServerError=} [properties] Properties to set
     * @returns {GameBoyServerError} GameBoyServerError instance
     */
    GameBoyServerError.create = function create(properties) {
        return new GameBoyServerError(properties);
    };

    /**
     * Encodes the specified GameBoyServerError message. Does not implicitly {@link GameBoyServerError.verify|verify} messages.
     * @function encode
     * @memberof GameBoyServerError
     * @static
     * @param {IGameBoyServerError} message GameBoyServerError message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyServerError.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.reasons != null && message.reasons.length)
            for (var i = 0; i < message.reasons.length; ++i)
                writer.uint32(/* id 1, wireType 2 =*/10).string(message.reasons[i]);
        return writer;
    };

    /**
     * Encodes the specified GameBoyServerError message, length delimited. Does not implicitly {@link GameBoyServerError.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyServerError
     * @static
     * @param {IGameBoyServerError} message GameBoyServerError message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyServerError.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyServerError message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyServerError
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyServerError} GameBoyServerError
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyServerError.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyServerError();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                if (!(message.reasons && message.reasons.length))
                    message.reasons = [];
                message.reasons.push(reader.string());
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyServerError message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyServerError
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyServerError} GameBoyServerError
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyServerError.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyServerError message.
     * @function verify
     * @memberof GameBoyServerError
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyServerError.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.reasons != null && message.hasOwnProperty("reasons")) {
            if (!Array.isArray(message.reasons))
                return "reasons: array expected";
            for (var i = 0; i < message.reasons.length; ++i)
                if (!$util.isString(message.reasons[i]))
                    return "reasons: string[] expected";
        }
        return null;
    };

    /**
     * Creates a GameBoyServerError message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyServerError
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyServerError} GameBoyServerError
     */
    GameBoyServerError.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyServerError)
            return object;
        var message = new $root.GameBoyServerError();
        if (object.reasons) {
            if (!Array.isArray(object.reasons))
                throw TypeError(".GameBoyServerError.reasons: array expected");
            message.reasons = [];
            for (var i = 0; i < object.reasons.length; ++i)
                message.reasons[i] = String(object.reasons[i]);
        }
        return message;
    };

    /**
     * Creates a plain object from a GameBoyServerError message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyServerError
     * @static
     * @param {GameBoyServerError} message GameBoyServerError
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyServerError.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.arrays || options.defaults)
            object.reasons = [];
        if (message.reasons && message.reasons.length) {
            object.reasons = [];
            for (var j = 0; j < message.reasons.length; ++j)
                object.reasons[j] = message.reasons[j];
        }
        return object;
    };

    /**
     * Converts this GameBoyServerError to JSON.
     * @function toJSON
     * @memberof GameBoyServerError
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyServerError.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyServerError;
})();

$root.GameBoyClientState = (function() {

    /**
     * Properties of a GameBoyClientState.
     * @exports IGameBoyClientState
     * @interface IGameBoyClientState
     * @property {string|null} [displayName] GameBoyClientState displayName
     */

    /**
     * Constructs a new GameBoyClientState.
     * @exports GameBoyClientState
     * @classdesc Represents a GameBoyClientState.
     * @implements IGameBoyClientState
     * @constructor
     * @param {IGameBoyClientState=} [properties] Properties to set
     */
    function GameBoyClientState(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyClientState displayName.
     * @member {string} displayName
     * @memberof GameBoyClientState
     * @instance
     */
    GameBoyClientState.prototype.displayName = "";

    /**
     * Creates a new GameBoyClientState instance using the specified properties.
     * @function create
     * @memberof GameBoyClientState
     * @static
     * @param {IGameBoyClientState=} [properties] Properties to set
     * @returns {GameBoyClientState} GameBoyClientState instance
     */
    GameBoyClientState.create = function create(properties) {
        return new GameBoyClientState(properties);
    };

    /**
     * Encodes the specified GameBoyClientState message. Does not implicitly {@link GameBoyClientState.verify|verify} messages.
     * @function encode
     * @memberof GameBoyClientState
     * @static
     * @param {IGameBoyClientState} message GameBoyClientState message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyClientState.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.displayName != null && message.hasOwnProperty("displayName"))
            writer.uint32(/* id 1, wireType 2 =*/10).string(message.displayName);
        return writer;
    };

    /**
     * Encodes the specified GameBoyClientState message, length delimited. Does not implicitly {@link GameBoyClientState.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyClientState
     * @static
     * @param {IGameBoyClientState} message GameBoyClientState message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyClientState.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyClientState message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyClientState
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyClientState} GameBoyClientState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyClientState.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyClientState();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.displayName = reader.string();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyClientState message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyClientState
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyClientState} GameBoyClientState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyClientState.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyClientState message.
     * @function verify
     * @memberof GameBoyClientState
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyClientState.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.displayName != null && message.hasOwnProperty("displayName"))
            if (!$util.isString(message.displayName))
                return "displayName: string expected";
        return null;
    };

    /**
     * Creates a GameBoyClientState message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyClientState
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyClientState} GameBoyClientState
     */
    GameBoyClientState.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyClientState)
            return object;
        var message = new $root.GameBoyClientState();
        if (object.displayName != null)
            message.displayName = String(object.displayName);
        return message;
    };

    /**
     * Creates a plain object from a GameBoyClientState message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyClientState
     * @static
     * @param {GameBoyClientState} message GameBoyClientState
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyClientState.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults)
            object.displayName = "";
        if (message.displayName != null && message.hasOwnProperty("displayName"))
            object.displayName = message.displayName;
        return object;
    };

    /**
     * Converts this GameBoyClientState to JSON.
     * @function toJSON
     * @memberof GameBoyClientState
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyClientState.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyClientState;
})();

$root.GameBoyEvent = (function() {

    /**
     * Properties of a GameBoyEvent.
     * @exports IGameBoyEvent
     * @interface IGameBoyEvent
     * @property {IGameBoyGpuFrame|null} [frame] GameBoyEvent frame
     * @property {IGameBoyPublishedMessage|null} [publishedMessage] GameBoyEvent publishedMessage
     * @property {IGameBoyServerError|null} [error] GameBoyEvent error
     * @property {IGameBoyClientState|null} [state] GameBoyEvent state
     */

    /**
     * Constructs a new GameBoyEvent.
     * @exports GameBoyEvent
     * @classdesc Represents a GameBoyEvent.
     * @implements IGameBoyEvent
     * @constructor
     * @param {IGameBoyEvent=} [properties] Properties to set
     */
    function GameBoyEvent(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyEvent frame.
     * @member {IGameBoyGpuFrame|null|undefined} frame
     * @memberof GameBoyEvent
     * @instance
     */
    GameBoyEvent.prototype.frame = null;

    /**
     * GameBoyEvent publishedMessage.
     * @member {IGameBoyPublishedMessage|null|undefined} publishedMessage
     * @memberof GameBoyEvent
     * @instance
     */
    GameBoyEvent.prototype.publishedMessage = null;

    /**
     * GameBoyEvent error.
     * @member {IGameBoyServerError|null|undefined} error
     * @memberof GameBoyEvent
     * @instance
     */
    GameBoyEvent.prototype.error = null;

    /**
     * GameBoyEvent state.
     * @member {IGameBoyClientState|null|undefined} state
     * @memberof GameBoyEvent
     * @instance
     */
    GameBoyEvent.prototype.state = null;

    // OneOf field names bound to virtual getters and setters
    var $oneOfFields;

    /**
     * GameBoyEvent value.
     * @member {"frame"|"publishedMessage"|"error"|"state"|undefined} value
     * @memberof GameBoyEvent
     * @instance
     */
    Object.defineProperty(GameBoyEvent.prototype, "value", {
        get: $util.oneOfGetter($oneOfFields = ["frame", "publishedMessage", "error", "state"]),
        set: $util.oneOfSetter($oneOfFields)
    });

    /**
     * Creates a new GameBoyEvent instance using the specified properties.
     * @function create
     * @memberof GameBoyEvent
     * @static
     * @param {IGameBoyEvent=} [properties] Properties to set
     * @returns {GameBoyEvent} GameBoyEvent instance
     */
    GameBoyEvent.create = function create(properties) {
        return new GameBoyEvent(properties);
    };

    /**
     * Encodes the specified GameBoyEvent message. Does not implicitly {@link GameBoyEvent.verify|verify} messages.
     * @function encode
     * @memberof GameBoyEvent
     * @static
     * @param {IGameBoyEvent} message GameBoyEvent message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyEvent.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.frame != null && message.hasOwnProperty("frame"))
            $root.GameBoyGpuFrame.encode(message.frame, writer.uint32(/* id 1, wireType 2 =*/10).fork()).ldelim();
        if (message.publishedMessage != null && message.hasOwnProperty("publishedMessage"))
            $root.GameBoyPublishedMessage.encode(message.publishedMessage, writer.uint32(/* id 2, wireType 2 =*/18).fork()).ldelim();
        if (message.error != null && message.hasOwnProperty("error"))
            $root.GameBoyServerError.encode(message.error, writer.uint32(/* id 3, wireType 2 =*/26).fork()).ldelim();
        if (message.state != null && message.hasOwnProperty("state"))
            $root.GameBoyClientState.encode(message.state, writer.uint32(/* id 4, wireType 2 =*/34).fork()).ldelim();
        return writer;
    };

    /**
     * Encodes the specified GameBoyEvent message, length delimited. Does not implicitly {@link GameBoyEvent.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyEvent
     * @static
     * @param {IGameBoyEvent} message GameBoyEvent message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyEvent.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyEvent message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyEvent
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyEvent} GameBoyEvent
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyEvent.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyEvent();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.frame = $root.GameBoyGpuFrame.decode(reader, reader.uint32());
                break;
            case 2:
                message.publishedMessage = $root.GameBoyPublishedMessage.decode(reader, reader.uint32());
                break;
            case 3:
                message.error = $root.GameBoyServerError.decode(reader, reader.uint32());
                break;
            case 4:
                message.state = $root.GameBoyClientState.decode(reader, reader.uint32());
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyEvent message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyEvent
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyEvent} GameBoyEvent
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyEvent.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyEvent message.
     * @function verify
     * @memberof GameBoyEvent
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyEvent.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        var properties = {};
        if (message.frame != null && message.hasOwnProperty("frame")) {
            properties.value = 1;
            {
                var error = $root.GameBoyGpuFrame.verify(message.frame);
                if (error)
                    return "frame." + error;
            }
        }
        if (message.publishedMessage != null && message.hasOwnProperty("publishedMessage")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            {
                var error = $root.GameBoyPublishedMessage.verify(message.publishedMessage);
                if (error)
                    return "publishedMessage." + error;
            }
        }
        if (message.error != null && message.hasOwnProperty("error")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            {
                var error = $root.GameBoyServerError.verify(message.error);
                if (error)
                    return "error." + error;
            }
        }
        if (message.state != null && message.hasOwnProperty("state")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            {
                var error = $root.GameBoyClientState.verify(message.state);
                if (error)
                    return "state." + error;
            }
        }
        return null;
    };

    /**
     * Creates a GameBoyEvent message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyEvent
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyEvent} GameBoyEvent
     */
    GameBoyEvent.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyEvent)
            return object;
        var message = new $root.GameBoyEvent();
        if (object.frame != null) {
            if (typeof object.frame !== "object")
                throw TypeError(".GameBoyEvent.frame: object expected");
            message.frame = $root.GameBoyGpuFrame.fromObject(object.frame);
        }
        if (object.publishedMessage != null) {
            if (typeof object.publishedMessage !== "object")
                throw TypeError(".GameBoyEvent.publishedMessage: object expected");
            message.publishedMessage = $root.GameBoyPublishedMessage.fromObject(object.publishedMessage);
        }
        if (object.error != null) {
            if (typeof object.error !== "object")
                throw TypeError(".GameBoyEvent.error: object expected");
            message.error = $root.GameBoyServerError.fromObject(object.error);
        }
        if (object.state != null) {
            if (typeof object.state !== "object")
                throw TypeError(".GameBoyEvent.state: object expected");
            message.state = $root.GameBoyClientState.fromObject(object.state);
        }
        return message;
    };

    /**
     * Creates a plain object from a GameBoyEvent message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyEvent
     * @static
     * @param {GameBoyEvent} message GameBoyEvent
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyEvent.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (message.frame != null && message.hasOwnProperty("frame")) {
            object.frame = $root.GameBoyGpuFrame.toObject(message.frame, options);
            if (options.oneofs)
                object.value = "frame";
        }
        if (message.publishedMessage != null && message.hasOwnProperty("publishedMessage")) {
            object.publishedMessage = $root.GameBoyPublishedMessage.toObject(message.publishedMessage, options);
            if (options.oneofs)
                object.value = "publishedMessage";
        }
        if (message.error != null && message.hasOwnProperty("error")) {
            object.error = $root.GameBoyServerError.toObject(message.error, options);
            if (options.oneofs)
                object.value = "error";
        }
        if (message.state != null && message.hasOwnProperty("state")) {
            object.state = $root.GameBoyClientState.toObject(message.state, options);
            if (options.oneofs)
                object.value = "state";
        }
        return object;
    };

    /**
     * Converts this GameBoyEvent to JSON.
     * @function toJSON
     * @memberof GameBoyEvent
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyEvent.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyEvent;
})();

module.exports = $root;
