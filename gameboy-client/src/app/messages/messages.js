/*eslint-disable block-scoped-var, no-redeclare, no-control-regex, no-prototype-builtins*/
"use strict";

var $protobuf = require("protobufjs/minimal");

// Common aliases
var $Reader = $protobuf.Reader, $Writer = $protobuf.Writer, $util = $protobuf.util;

// Exported root namespace
var $root = $protobuf.roots["default"] || ($protobuf.roots["default"] = {});

$root.AddBreakpoint = (function() {

    /**
     * Properties of an AddBreakpoint.
     * @exports IAddBreakpoint
     * @interface IAddBreakpoint
     * @property {number|null} [address] AddBreakpoint address
     */

    /**
     * Constructs a new AddBreakpoint.
     * @exports AddBreakpoint
     * @classdesc Represents an AddBreakpoint.
     * @implements IAddBreakpoint
     * @constructor
     * @param {IAddBreakpoint=} [properties] Properties to set
     */
    function AddBreakpoint(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * AddBreakpoint address.
     * @member {number} address
     * @memberof AddBreakpoint
     * @instance
     */
    AddBreakpoint.prototype.address = 0;

    /**
     * Creates a new AddBreakpoint instance using the specified properties.
     * @function create
     * @memberof AddBreakpoint
     * @static
     * @param {IAddBreakpoint=} [properties] Properties to set
     * @returns {AddBreakpoint} AddBreakpoint instance
     */
    AddBreakpoint.create = function create(properties) {
        return new AddBreakpoint(properties);
    };

    /**
     * Encodes the specified AddBreakpoint message. Does not implicitly {@link AddBreakpoint.verify|verify} messages.
     * @function encode
     * @memberof AddBreakpoint
     * @static
     * @param {IAddBreakpoint} message AddBreakpoint message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    AddBreakpoint.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.address != null && message.hasOwnProperty("address"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.address);
        return writer;
    };

    /**
     * Encodes the specified AddBreakpoint message, length delimited. Does not implicitly {@link AddBreakpoint.verify|verify} messages.
     * @function encodeDelimited
     * @memberof AddBreakpoint
     * @static
     * @param {IAddBreakpoint} message AddBreakpoint message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    AddBreakpoint.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes an AddBreakpoint message from the specified reader or buffer.
     * @function decode
     * @memberof AddBreakpoint
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {AddBreakpoint} AddBreakpoint
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    AddBreakpoint.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.AddBreakpoint();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.address = reader.uint32();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes an AddBreakpoint message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof AddBreakpoint
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {AddBreakpoint} AddBreakpoint
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    AddBreakpoint.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies an AddBreakpoint message.
     * @function verify
     * @memberof AddBreakpoint
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    AddBreakpoint.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.address != null && message.hasOwnProperty("address"))
            if (!$util.isInteger(message.address))
                return "address: integer expected";
        return null;
    };

    /**
     * Creates an AddBreakpoint message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof AddBreakpoint
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {AddBreakpoint} AddBreakpoint
     */
    AddBreakpoint.fromObject = function fromObject(object) {
        if (object instanceof $root.AddBreakpoint)
            return object;
        var message = new $root.AddBreakpoint();
        if (object.address != null)
            message.address = object.address >>> 0;
        return message;
    };

    /**
     * Creates a plain object from an AddBreakpoint message. Also converts values to other types if specified.
     * @function toObject
     * @memberof AddBreakpoint
     * @static
     * @param {AddBreakpoint} message AddBreakpoint
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    AddBreakpoint.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults)
            object.address = 0;
        if (message.address != null && message.hasOwnProperty("address"))
            object.address = message.address;
        return object;
    };

    /**
     * Converts this AddBreakpoint to JSON.
     * @function toJSON
     * @memberof AddBreakpoint
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    AddBreakpoint.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return AddBreakpoint;
})();

$root.RemoveBreakpoint = (function() {

    /**
     * Properties of a RemoveBreakpoint.
     * @exports IRemoveBreakpoint
     * @interface IRemoveBreakpoint
     * @property {number|null} [address] RemoveBreakpoint address
     */

    /**
     * Constructs a new RemoveBreakpoint.
     * @exports RemoveBreakpoint
     * @classdesc Represents a RemoveBreakpoint.
     * @implements IRemoveBreakpoint
     * @constructor
     * @param {IRemoveBreakpoint=} [properties] Properties to set
     */
    function RemoveBreakpoint(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * RemoveBreakpoint address.
     * @member {number} address
     * @memberof RemoveBreakpoint
     * @instance
     */
    RemoveBreakpoint.prototype.address = 0;

    /**
     * Creates a new RemoveBreakpoint instance using the specified properties.
     * @function create
     * @memberof RemoveBreakpoint
     * @static
     * @param {IRemoveBreakpoint=} [properties] Properties to set
     * @returns {RemoveBreakpoint} RemoveBreakpoint instance
     */
    RemoveBreakpoint.create = function create(properties) {
        return new RemoveBreakpoint(properties);
    };

    /**
     * Encodes the specified RemoveBreakpoint message. Does not implicitly {@link RemoveBreakpoint.verify|verify} messages.
     * @function encode
     * @memberof RemoveBreakpoint
     * @static
     * @param {IRemoveBreakpoint} message RemoveBreakpoint message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    RemoveBreakpoint.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.address != null && message.hasOwnProperty("address"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.address);
        return writer;
    };

    /**
     * Encodes the specified RemoveBreakpoint message, length delimited. Does not implicitly {@link RemoveBreakpoint.verify|verify} messages.
     * @function encodeDelimited
     * @memberof RemoveBreakpoint
     * @static
     * @param {IRemoveBreakpoint} message RemoveBreakpoint message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    RemoveBreakpoint.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a RemoveBreakpoint message from the specified reader or buffer.
     * @function decode
     * @memberof RemoveBreakpoint
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {RemoveBreakpoint} RemoveBreakpoint
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    RemoveBreakpoint.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.RemoveBreakpoint();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.address = reader.uint32();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a RemoveBreakpoint message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof RemoveBreakpoint
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {RemoveBreakpoint} RemoveBreakpoint
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    RemoveBreakpoint.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a RemoveBreakpoint message.
     * @function verify
     * @memberof RemoveBreakpoint
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    RemoveBreakpoint.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.address != null && message.hasOwnProperty("address"))
            if (!$util.isInteger(message.address))
                return "address: integer expected";
        return null;
    };

    /**
     * Creates a RemoveBreakpoint message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof RemoveBreakpoint
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {RemoveBreakpoint} RemoveBreakpoint
     */
    RemoveBreakpoint.fromObject = function fromObject(object) {
        if (object instanceof $root.RemoveBreakpoint)
            return object;
        var message = new $root.RemoveBreakpoint();
        if (object.address != null)
            message.address = object.address >>> 0;
        return message;
    };

    /**
     * Creates a plain object from a RemoveBreakpoint message. Also converts values to other types if specified.
     * @function toObject
     * @memberof RemoveBreakpoint
     * @static
     * @param {RemoveBreakpoint} message RemoveBreakpoint
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    RemoveBreakpoint.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults)
            object.address = 0;
        if (message.address != null && message.hasOwnProperty("address"))
            object.address = message.address;
        return object;
    };

    /**
     * Converts this RemoveBreakpoint to JSON.
     * @function toJSON
     * @memberof RemoveBreakpoint
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    RemoveBreakpoint.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return RemoveBreakpoint;
})();

$root.GetBreakpoints = (function() {

    /**
     * Properties of a GetBreakpoints.
     * @exports IGetBreakpoints
     * @interface IGetBreakpoints
     * @property {Array.<number>|null} [breakpoints] GetBreakpoints breakpoints
     */

    /**
     * Constructs a new GetBreakpoints.
     * @exports GetBreakpoints
     * @classdesc Represents a GetBreakpoints.
     * @implements IGetBreakpoints
     * @constructor
     * @param {IGetBreakpoints=} [properties] Properties to set
     */
    function GetBreakpoints(properties) {
        this.breakpoints = [];
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GetBreakpoints breakpoints.
     * @member {Array.<number>} breakpoints
     * @memberof GetBreakpoints
     * @instance
     */
    GetBreakpoints.prototype.breakpoints = $util.emptyArray;

    /**
     * Creates a new GetBreakpoints instance using the specified properties.
     * @function create
     * @memberof GetBreakpoints
     * @static
     * @param {IGetBreakpoints=} [properties] Properties to set
     * @returns {GetBreakpoints} GetBreakpoints instance
     */
    GetBreakpoints.create = function create(properties) {
        return new GetBreakpoints(properties);
    };

    /**
     * Encodes the specified GetBreakpoints message. Does not implicitly {@link GetBreakpoints.verify|verify} messages.
     * @function encode
     * @memberof GetBreakpoints
     * @static
     * @param {IGetBreakpoints} message GetBreakpoints message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GetBreakpoints.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.breakpoints != null && message.breakpoints.length) {
            writer.uint32(/* id 1, wireType 2 =*/10).fork();
            for (var i = 0; i < message.breakpoints.length; ++i)
                writer.uint32(message.breakpoints[i]);
            writer.ldelim();
        }
        return writer;
    };

    /**
     * Encodes the specified GetBreakpoints message, length delimited. Does not implicitly {@link GetBreakpoints.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GetBreakpoints
     * @static
     * @param {IGetBreakpoints} message GetBreakpoints message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GetBreakpoints.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GetBreakpoints message from the specified reader or buffer.
     * @function decode
     * @memberof GetBreakpoints
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GetBreakpoints} GetBreakpoints
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GetBreakpoints.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GetBreakpoints();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                if (!(message.breakpoints && message.breakpoints.length))
                    message.breakpoints = [];
                if ((tag & 7) === 2) {
                    var end2 = reader.uint32() + reader.pos;
                    while (reader.pos < end2)
                        message.breakpoints.push(reader.uint32());
                } else
                    message.breakpoints.push(reader.uint32());
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GetBreakpoints message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GetBreakpoints
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GetBreakpoints} GetBreakpoints
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GetBreakpoints.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GetBreakpoints message.
     * @function verify
     * @memberof GetBreakpoints
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GetBreakpoints.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.breakpoints != null && message.hasOwnProperty("breakpoints")) {
            if (!Array.isArray(message.breakpoints))
                return "breakpoints: array expected";
            for (var i = 0; i < message.breakpoints.length; ++i)
                if (!$util.isInteger(message.breakpoints[i]))
                    return "breakpoints: integer[] expected";
        }
        return null;
    };

    /**
     * Creates a GetBreakpoints message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GetBreakpoints
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GetBreakpoints} GetBreakpoints
     */
    GetBreakpoints.fromObject = function fromObject(object) {
        if (object instanceof $root.GetBreakpoints)
            return object;
        var message = new $root.GetBreakpoints();
        if (object.breakpoints) {
            if (!Array.isArray(object.breakpoints))
                throw TypeError(".GetBreakpoints.breakpoints: array expected");
            message.breakpoints = [];
            for (var i = 0; i < object.breakpoints.length; ++i)
                message.breakpoints[i] = object.breakpoints[i] >>> 0;
        }
        return message;
    };

    /**
     * Creates a plain object from a GetBreakpoints message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GetBreakpoints
     * @static
     * @param {GetBreakpoints} message GetBreakpoints
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GetBreakpoints.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.arrays || options.defaults)
            object.breakpoints = [];
        if (message.breakpoints && message.breakpoints.length) {
            object.breakpoints = [];
            for (var j = 0; j < message.breakpoints.length; ++j)
                object.breakpoints[j] = message.breakpoints[j];
        }
        return object;
    };

    /**
     * Converts this GetBreakpoints to JSON.
     * @function toJSON
     * @memberof GetBreakpoints
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GetBreakpoints.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GetBreakpoints;
})();

/**
 * DebuggerCommand enum.
 * @exports DebuggerCommand
 * @enum {string}
 * @property {number} BREAK=0 BREAK value
 * @property {number} CONTINUE=1 CONTINUE value
 * @property {number} STEP_OVER=2 STEP_OVER value
 */
$root.DebuggerCommand = (function() {
    var valuesById = {}, values = Object.create(valuesById);
    values[valuesById[0] = "BREAK"] = 0;
    values[valuesById[1] = "CONTINUE"] = 1;
    values[valuesById[2] = "STEP_OVER"] = 2;
    return values;
})();

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

$root.GameBoyDebuggerCommand = (function() {

    /**
     * Properties of a GameBoyDebuggerCommand.
     * @exports IGameBoyDebuggerCommand
     * @interface IGameBoyDebuggerCommand
     * @property {IAddBreakpoint|null} [addBreakpoint] GameBoyDebuggerCommand addBreakpoint
     * @property {IRemoveBreakpoint|null} [removeBreakpoint] GameBoyDebuggerCommand removeBreakpoint
     * @property {IGetBreakpoints|null} [getBreakpoints] GameBoyDebuggerCommand getBreakpoints
     * @property {DebuggerCommand|null} [command] GameBoyDebuggerCommand command
     */

    /**
     * Constructs a new GameBoyDebuggerCommand.
     * @exports GameBoyDebuggerCommand
     * @classdesc Represents a GameBoyDebuggerCommand.
     * @implements IGameBoyDebuggerCommand
     * @constructor
     * @param {IGameBoyDebuggerCommand=} [properties] Properties to set
     */
    function GameBoyDebuggerCommand(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyDebuggerCommand addBreakpoint.
     * @member {IAddBreakpoint|null|undefined} addBreakpoint
     * @memberof GameBoyDebuggerCommand
     * @instance
     */
    GameBoyDebuggerCommand.prototype.addBreakpoint = null;

    /**
     * GameBoyDebuggerCommand removeBreakpoint.
     * @member {IRemoveBreakpoint|null|undefined} removeBreakpoint
     * @memberof GameBoyDebuggerCommand
     * @instance
     */
    GameBoyDebuggerCommand.prototype.removeBreakpoint = null;

    /**
     * GameBoyDebuggerCommand getBreakpoints.
     * @member {IGetBreakpoints|null|undefined} getBreakpoints
     * @memberof GameBoyDebuggerCommand
     * @instance
     */
    GameBoyDebuggerCommand.prototype.getBreakpoints = null;

    /**
     * GameBoyDebuggerCommand command.
     * @member {DebuggerCommand} command
     * @memberof GameBoyDebuggerCommand
     * @instance
     */
    GameBoyDebuggerCommand.prototype.command = 0;

    // OneOf field names bound to virtual getters and setters
    var $oneOfFields;

    /**
     * GameBoyDebuggerCommand value.
     * @member {"addBreakpoint"|"removeBreakpoint"|"getBreakpoints"|"command"|undefined} value
     * @memberof GameBoyDebuggerCommand
     * @instance
     */
    Object.defineProperty(GameBoyDebuggerCommand.prototype, "value", {
        get: $util.oneOfGetter($oneOfFields = ["addBreakpoint", "removeBreakpoint", "getBreakpoints", "command"]),
        set: $util.oneOfSetter($oneOfFields)
    });

    /**
     * Creates a new GameBoyDebuggerCommand instance using the specified properties.
     * @function create
     * @memberof GameBoyDebuggerCommand
     * @static
     * @param {IGameBoyDebuggerCommand=} [properties] Properties to set
     * @returns {GameBoyDebuggerCommand} GameBoyDebuggerCommand instance
     */
    GameBoyDebuggerCommand.create = function create(properties) {
        return new GameBoyDebuggerCommand(properties);
    };

    /**
     * Encodes the specified GameBoyDebuggerCommand message. Does not implicitly {@link GameBoyDebuggerCommand.verify|verify} messages.
     * @function encode
     * @memberof GameBoyDebuggerCommand
     * @static
     * @param {IGameBoyDebuggerCommand} message GameBoyDebuggerCommand message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyDebuggerCommand.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.addBreakpoint != null && message.hasOwnProperty("addBreakpoint"))
            $root.AddBreakpoint.encode(message.addBreakpoint, writer.uint32(/* id 1, wireType 2 =*/10).fork()).ldelim();
        if (message.removeBreakpoint != null && message.hasOwnProperty("removeBreakpoint"))
            $root.RemoveBreakpoint.encode(message.removeBreakpoint, writer.uint32(/* id 2, wireType 2 =*/18).fork()).ldelim();
        if (message.getBreakpoints != null && message.hasOwnProperty("getBreakpoints"))
            $root.GetBreakpoints.encode(message.getBreakpoints, writer.uint32(/* id 3, wireType 2 =*/26).fork()).ldelim();
        if (message.command != null && message.hasOwnProperty("command"))
            writer.uint32(/* id 4, wireType 0 =*/32).int32(message.command);
        return writer;
    };

    /**
     * Encodes the specified GameBoyDebuggerCommand message, length delimited. Does not implicitly {@link GameBoyDebuggerCommand.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyDebuggerCommand
     * @static
     * @param {IGameBoyDebuggerCommand} message GameBoyDebuggerCommand message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyDebuggerCommand.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyDebuggerCommand message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyDebuggerCommand
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyDebuggerCommand} GameBoyDebuggerCommand
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyDebuggerCommand.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyDebuggerCommand();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.addBreakpoint = $root.AddBreakpoint.decode(reader, reader.uint32());
                break;
            case 2:
                message.removeBreakpoint = $root.RemoveBreakpoint.decode(reader, reader.uint32());
                break;
            case 3:
                message.getBreakpoints = $root.GetBreakpoints.decode(reader, reader.uint32());
                break;
            case 4:
                message.command = reader.int32();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyDebuggerCommand message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyDebuggerCommand
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyDebuggerCommand} GameBoyDebuggerCommand
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyDebuggerCommand.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyDebuggerCommand message.
     * @function verify
     * @memberof GameBoyDebuggerCommand
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyDebuggerCommand.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        var properties = {};
        if (message.addBreakpoint != null && message.hasOwnProperty("addBreakpoint")) {
            properties.value = 1;
            {
                var error = $root.AddBreakpoint.verify(message.addBreakpoint);
                if (error)
                    return "addBreakpoint." + error;
            }
        }
        if (message.removeBreakpoint != null && message.hasOwnProperty("removeBreakpoint")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            {
                var error = $root.RemoveBreakpoint.verify(message.removeBreakpoint);
                if (error)
                    return "removeBreakpoint." + error;
            }
        }
        if (message.getBreakpoints != null && message.hasOwnProperty("getBreakpoints")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            {
                var error = $root.GetBreakpoints.verify(message.getBreakpoints);
                if (error)
                    return "getBreakpoints." + error;
            }
        }
        if (message.command != null && message.hasOwnProperty("command")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            switch (message.command) {
            default:
                return "command: enum value expected";
            case 0:
            case 1:
            case 2:
                break;
            }
        }
        return null;
    };

    /**
     * Creates a GameBoyDebuggerCommand message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyDebuggerCommand
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyDebuggerCommand} GameBoyDebuggerCommand
     */
    GameBoyDebuggerCommand.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyDebuggerCommand)
            return object;
        var message = new $root.GameBoyDebuggerCommand();
        if (object.addBreakpoint != null) {
            if (typeof object.addBreakpoint !== "object")
                throw TypeError(".GameBoyDebuggerCommand.addBreakpoint: object expected");
            message.addBreakpoint = $root.AddBreakpoint.fromObject(object.addBreakpoint);
        }
        if (object.removeBreakpoint != null) {
            if (typeof object.removeBreakpoint !== "object")
                throw TypeError(".GameBoyDebuggerCommand.removeBreakpoint: object expected");
            message.removeBreakpoint = $root.RemoveBreakpoint.fromObject(object.removeBreakpoint);
        }
        if (object.getBreakpoints != null) {
            if (typeof object.getBreakpoints !== "object")
                throw TypeError(".GameBoyDebuggerCommand.getBreakpoints: object expected");
            message.getBreakpoints = $root.GetBreakpoints.fromObject(object.getBreakpoints);
        }
        switch (object.command) {
        case "BREAK":
        case 0:
            message.command = 0;
            break;
        case "CONTINUE":
        case 1:
            message.command = 1;
            break;
        case "STEP_OVER":
        case 2:
            message.command = 2;
            break;
        }
        return message;
    };

    /**
     * Creates a plain object from a GameBoyDebuggerCommand message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyDebuggerCommand
     * @static
     * @param {GameBoyDebuggerCommand} message GameBoyDebuggerCommand
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyDebuggerCommand.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (message.addBreakpoint != null && message.hasOwnProperty("addBreakpoint")) {
            object.addBreakpoint = $root.AddBreakpoint.toObject(message.addBreakpoint, options);
            if (options.oneofs)
                object.value = "addBreakpoint";
        }
        if (message.removeBreakpoint != null && message.hasOwnProperty("removeBreakpoint")) {
            object.removeBreakpoint = $root.RemoveBreakpoint.toObject(message.removeBreakpoint, options);
            if (options.oneofs)
                object.value = "removeBreakpoint";
        }
        if (message.getBreakpoints != null && message.hasOwnProperty("getBreakpoints")) {
            object.getBreakpoints = $root.GetBreakpoints.toObject(message.getBreakpoints, options);
            if (options.oneofs)
                object.value = "getBreakpoints";
        }
        if (message.command != null && message.hasOwnProperty("command")) {
            object.command = options.enums === String ? $root.DebuggerCommand[message.command] : message.command;
            if (options.oneofs)
                object.value = "command";
        }
        return object;
    };

    /**
     * Converts this GameBoyDebuggerCommand to JSON.
     * @function toJSON
     * @memberof GameBoyDebuggerCommand
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyDebuggerCommand.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyDebuggerCommand;
})();

$root.GameBoyCommand = (function() {

    /**
     * Properties of a GameBoyCommand.
     * @exports IGameBoyCommand
     * @interface IGameBoyCommand
     * @property {IHeartBeat|null} [heartBeat] GameBoyCommand heartBeat
     * @property {ISetGameBoyClientState|null} [setState] GameBoyCommand setState
     * @property {IRequestGameBoyJoyPadButtonPress|null} [pressButton] GameBoyCommand pressButton
     * @property {IGameBoyDebuggerCommand|null} ["debugger"] GameBoyCommand debugger
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

    /**
     * GameBoyCommand debugger.
     * @member {IGameBoyDebuggerCommand|null|undefined} debugger
     * @memberof GameBoyCommand
     * @instance
     */
    GameBoyCommand.prototype["debugger"] = null;

    // OneOf field names bound to virtual getters and setters
    var $oneOfFields;

    /**
     * GameBoyCommand value.
     * @member {"heartBeat"|"setState"|"pressButton"|"debugger"|undefined} value
     * @memberof GameBoyCommand
     * @instance
     */
    Object.defineProperty(GameBoyCommand.prototype, "value", {
        get: $util.oneOfGetter($oneOfFields = ["heartBeat", "setState", "pressButton", "debugger"]),
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
        if (message["debugger"] != null && message.hasOwnProperty("debugger"))
            $root.GameBoyDebuggerCommand.encode(message["debugger"], writer.uint32(/* id 4, wireType 2 =*/34).fork()).ldelim();
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
            case 4:
                message["debugger"] = $root.GameBoyDebuggerCommand.decode(reader, reader.uint32());
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
        if (message["debugger"] != null && message.hasOwnProperty("debugger")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            {
                var error = $root.GameBoyDebuggerCommand.verify(message["debugger"]);
                if (error)
                    return "debugger." + error;
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
        if (object["debugger"] != null) {
            if (typeof object["debugger"] !== "object")
                throw TypeError(".GameBoyCommand.debugger: object expected");
            message["debugger"] = $root.GameBoyDebuggerCommand.fromObject(object["debugger"]);
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
        if (message["debugger"] != null && message.hasOwnProperty("debugger")) {
            object["debugger"] = $root.GameBoyDebuggerCommand.toObject(message["debugger"], options);
            if (options.oneofs)
                object.value = "debugger";
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

$root.GameBoyInstructionBlock = (function() {

    /**
     * Properties of a GameBoyInstructionBlock.
     * @exports IGameBoyInstructionBlock
     * @interface IGameBoyInstructionBlock
     * @property {number|null} [address] GameBoyInstructionBlock address
     * @property {number|null} [length] GameBoyInstructionBlock length
     * @property {boolean|null} [haltCpu] GameBoyInstructionBlock haltCpu
     * @property {boolean|null} [haltPeripherals] GameBoyInstructionBlock haltPeripherals
     * @property {string|null} [debugInfo] GameBoyInstructionBlock debugInfo
     * @property {Array.<IGameBoyOperation>|null} [operations] GameBoyInstructionBlock operations
     * @property {IGameBoyIntructionTimings|null} [timings] GameBoyInstructionBlock timings
     */

    /**
     * Constructs a new GameBoyInstructionBlock.
     * @exports GameBoyInstructionBlock
     * @classdesc Represents a GameBoyInstructionBlock.
     * @implements IGameBoyInstructionBlock
     * @constructor
     * @param {IGameBoyInstructionBlock=} [properties] Properties to set
     */
    function GameBoyInstructionBlock(properties) {
        this.operations = [];
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyInstructionBlock address.
     * @member {number} address
     * @memberof GameBoyInstructionBlock
     * @instance
     */
    GameBoyInstructionBlock.prototype.address = 0;

    /**
     * GameBoyInstructionBlock length.
     * @member {number} length
     * @memberof GameBoyInstructionBlock
     * @instance
     */
    GameBoyInstructionBlock.prototype.length = 0;

    /**
     * GameBoyInstructionBlock haltCpu.
     * @member {boolean} haltCpu
     * @memberof GameBoyInstructionBlock
     * @instance
     */
    GameBoyInstructionBlock.prototype.haltCpu = false;

    /**
     * GameBoyInstructionBlock haltPeripherals.
     * @member {boolean} haltPeripherals
     * @memberof GameBoyInstructionBlock
     * @instance
     */
    GameBoyInstructionBlock.prototype.haltPeripherals = false;

    /**
     * GameBoyInstructionBlock debugInfo.
     * @member {string} debugInfo
     * @memberof GameBoyInstructionBlock
     * @instance
     */
    GameBoyInstructionBlock.prototype.debugInfo = "";

    /**
     * GameBoyInstructionBlock operations.
     * @member {Array.<IGameBoyOperation>} operations
     * @memberof GameBoyInstructionBlock
     * @instance
     */
    GameBoyInstructionBlock.prototype.operations = $util.emptyArray;

    /**
     * GameBoyInstructionBlock timings.
     * @member {IGameBoyIntructionTimings|null|undefined} timings
     * @memberof GameBoyInstructionBlock
     * @instance
     */
    GameBoyInstructionBlock.prototype.timings = null;

    /**
     * Creates a new GameBoyInstructionBlock instance using the specified properties.
     * @function create
     * @memberof GameBoyInstructionBlock
     * @static
     * @param {IGameBoyInstructionBlock=} [properties] Properties to set
     * @returns {GameBoyInstructionBlock} GameBoyInstructionBlock instance
     */
    GameBoyInstructionBlock.create = function create(properties) {
        return new GameBoyInstructionBlock(properties);
    };

    /**
     * Encodes the specified GameBoyInstructionBlock message. Does not implicitly {@link GameBoyInstructionBlock.verify|verify} messages.
     * @function encode
     * @memberof GameBoyInstructionBlock
     * @static
     * @param {IGameBoyInstructionBlock} message GameBoyInstructionBlock message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyInstructionBlock.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.address != null && message.hasOwnProperty("address"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.address);
        if (message.length != null && message.hasOwnProperty("length"))
            writer.uint32(/* id 2, wireType 0 =*/16).uint32(message.length);
        if (message.haltCpu != null && message.hasOwnProperty("haltCpu"))
            writer.uint32(/* id 3, wireType 0 =*/24).bool(message.haltCpu);
        if (message.haltPeripherals != null && message.hasOwnProperty("haltPeripherals"))
            writer.uint32(/* id 4, wireType 0 =*/32).bool(message.haltPeripherals);
        if (message.debugInfo != null && message.hasOwnProperty("debugInfo"))
            writer.uint32(/* id 5, wireType 2 =*/42).string(message.debugInfo);
        if (message.operations != null && message.operations.length)
            for (var i = 0; i < message.operations.length; ++i)
                $root.GameBoyOperation.encode(message.operations[i], writer.uint32(/* id 6, wireType 2 =*/50).fork()).ldelim();
        if (message.timings != null && message.hasOwnProperty("timings"))
            $root.GameBoyIntructionTimings.encode(message.timings, writer.uint32(/* id 7, wireType 2 =*/58).fork()).ldelim();
        return writer;
    };

    /**
     * Encodes the specified GameBoyInstructionBlock message, length delimited. Does not implicitly {@link GameBoyInstructionBlock.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyInstructionBlock
     * @static
     * @param {IGameBoyInstructionBlock} message GameBoyInstructionBlock message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyInstructionBlock.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyInstructionBlock message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyInstructionBlock
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyInstructionBlock} GameBoyInstructionBlock
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyInstructionBlock.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyInstructionBlock();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.address = reader.uint32();
                break;
            case 2:
                message.length = reader.uint32();
                break;
            case 3:
                message.haltCpu = reader.bool();
                break;
            case 4:
                message.haltPeripherals = reader.bool();
                break;
            case 5:
                message.debugInfo = reader.string();
                break;
            case 6:
                if (!(message.operations && message.operations.length))
                    message.operations = [];
                message.operations.push($root.GameBoyOperation.decode(reader, reader.uint32()));
                break;
            case 7:
                message.timings = $root.GameBoyIntructionTimings.decode(reader, reader.uint32());
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyInstructionBlock message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyInstructionBlock
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyInstructionBlock} GameBoyInstructionBlock
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyInstructionBlock.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyInstructionBlock message.
     * @function verify
     * @memberof GameBoyInstructionBlock
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyInstructionBlock.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.address != null && message.hasOwnProperty("address"))
            if (!$util.isInteger(message.address))
                return "address: integer expected";
        if (message.length != null && message.hasOwnProperty("length"))
            if (!$util.isInteger(message.length))
                return "length: integer expected";
        if (message.haltCpu != null && message.hasOwnProperty("haltCpu"))
            if (typeof message.haltCpu !== "boolean")
                return "haltCpu: boolean expected";
        if (message.haltPeripherals != null && message.hasOwnProperty("haltPeripherals"))
            if (typeof message.haltPeripherals !== "boolean")
                return "haltPeripherals: boolean expected";
        if (message.debugInfo != null && message.hasOwnProperty("debugInfo"))
            if (!$util.isString(message.debugInfo))
                return "debugInfo: string expected";
        if (message.operations != null && message.hasOwnProperty("operations")) {
            if (!Array.isArray(message.operations))
                return "operations: array expected";
            for (var i = 0; i < message.operations.length; ++i) {
                var error = $root.GameBoyOperation.verify(message.operations[i]);
                if (error)
                    return "operations." + error;
            }
        }
        if (message.timings != null && message.hasOwnProperty("timings")) {
            var error = $root.GameBoyIntructionTimings.verify(message.timings);
            if (error)
                return "timings." + error;
        }
        return null;
    };

    /**
     * Creates a GameBoyInstructionBlock message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyInstructionBlock
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyInstructionBlock} GameBoyInstructionBlock
     */
    GameBoyInstructionBlock.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyInstructionBlock)
            return object;
        var message = new $root.GameBoyInstructionBlock();
        if (object.address != null)
            message.address = object.address >>> 0;
        if (object.length != null)
            message.length = object.length >>> 0;
        if (object.haltCpu != null)
            message.haltCpu = Boolean(object.haltCpu);
        if (object.haltPeripherals != null)
            message.haltPeripherals = Boolean(object.haltPeripherals);
        if (object.debugInfo != null)
            message.debugInfo = String(object.debugInfo);
        if (object.operations) {
            if (!Array.isArray(object.operations))
                throw TypeError(".GameBoyInstructionBlock.operations: array expected");
            message.operations = [];
            for (var i = 0; i < object.operations.length; ++i) {
                if (typeof object.operations[i] !== "object")
                    throw TypeError(".GameBoyInstructionBlock.operations: object expected");
                message.operations[i] = $root.GameBoyOperation.fromObject(object.operations[i]);
            }
        }
        if (object.timings != null) {
            if (typeof object.timings !== "object")
                throw TypeError(".GameBoyInstructionBlock.timings: object expected");
            message.timings = $root.GameBoyIntructionTimings.fromObject(object.timings);
        }
        return message;
    };

    /**
     * Creates a plain object from a GameBoyInstructionBlock message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyInstructionBlock
     * @static
     * @param {GameBoyInstructionBlock} message GameBoyInstructionBlock
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyInstructionBlock.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.arrays || options.defaults)
            object.operations = [];
        if (options.defaults) {
            object.address = 0;
            object.length = 0;
            object.haltCpu = false;
            object.haltPeripherals = false;
            object.debugInfo = "";
            object.timings = null;
        }
        if (message.address != null && message.hasOwnProperty("address"))
            object.address = message.address;
        if (message.length != null && message.hasOwnProperty("length"))
            object.length = message.length;
        if (message.haltCpu != null && message.hasOwnProperty("haltCpu"))
            object.haltCpu = message.haltCpu;
        if (message.haltPeripherals != null && message.hasOwnProperty("haltPeripherals"))
            object.haltPeripherals = message.haltPeripherals;
        if (message.debugInfo != null && message.hasOwnProperty("debugInfo"))
            object.debugInfo = message.debugInfo;
        if (message.operations && message.operations.length) {
            object.operations = [];
            for (var j = 0; j < message.operations.length; ++j)
                object.operations[j] = $root.GameBoyOperation.toObject(message.operations[j], options);
        }
        if (message.timings != null && message.hasOwnProperty("timings"))
            object.timings = $root.GameBoyIntructionTimings.toObject(message.timings, options);
        return object;
    };

    /**
     * Converts this GameBoyInstructionBlock to JSON.
     * @function toJSON
     * @memberof GameBoyInstructionBlock
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyInstructionBlock.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyInstructionBlock;
})();

$root.GameBoyOperation = (function() {

    /**
     * Properties of a GameBoyOperation.
     * @exports IGameBoyOperation
     * @interface IGameBoyOperation
     * @property {number|null} [address] GameBoyOperation address
     * @property {string|null} [operation] GameBoyOperation operation
     */

    /**
     * Constructs a new GameBoyOperation.
     * @exports GameBoyOperation
     * @classdesc Represents a GameBoyOperation.
     * @implements IGameBoyOperation
     * @constructor
     * @param {IGameBoyOperation=} [properties] Properties to set
     */
    function GameBoyOperation(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyOperation address.
     * @member {number} address
     * @memberof GameBoyOperation
     * @instance
     */
    GameBoyOperation.prototype.address = 0;

    /**
     * GameBoyOperation operation.
     * @member {string} operation
     * @memberof GameBoyOperation
     * @instance
     */
    GameBoyOperation.prototype.operation = "";

    /**
     * Creates a new GameBoyOperation instance using the specified properties.
     * @function create
     * @memberof GameBoyOperation
     * @static
     * @param {IGameBoyOperation=} [properties] Properties to set
     * @returns {GameBoyOperation} GameBoyOperation instance
     */
    GameBoyOperation.create = function create(properties) {
        return new GameBoyOperation(properties);
    };

    /**
     * Encodes the specified GameBoyOperation message. Does not implicitly {@link GameBoyOperation.verify|verify} messages.
     * @function encode
     * @memberof GameBoyOperation
     * @static
     * @param {IGameBoyOperation} message GameBoyOperation message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyOperation.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.address != null && message.hasOwnProperty("address"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.address);
        if (message.operation != null && message.hasOwnProperty("operation"))
            writer.uint32(/* id 2, wireType 2 =*/18).string(message.operation);
        return writer;
    };

    /**
     * Encodes the specified GameBoyOperation message, length delimited. Does not implicitly {@link GameBoyOperation.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyOperation
     * @static
     * @param {IGameBoyOperation} message GameBoyOperation message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyOperation.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyOperation message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyOperation
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyOperation} GameBoyOperation
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyOperation.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyOperation();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.address = reader.uint32();
                break;
            case 2:
                message.operation = reader.string();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyOperation message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyOperation
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyOperation} GameBoyOperation
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyOperation.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyOperation message.
     * @function verify
     * @memberof GameBoyOperation
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyOperation.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.address != null && message.hasOwnProperty("address"))
            if (!$util.isInteger(message.address))
                return "address: integer expected";
        if (message.operation != null && message.hasOwnProperty("operation"))
            if (!$util.isString(message.operation))
                return "operation: string expected";
        return null;
    };

    /**
     * Creates a GameBoyOperation message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyOperation
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyOperation} GameBoyOperation
     */
    GameBoyOperation.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyOperation)
            return object;
        var message = new $root.GameBoyOperation();
        if (object.address != null)
            message.address = object.address >>> 0;
        if (object.operation != null)
            message.operation = String(object.operation);
        return message;
    };

    /**
     * Creates a plain object from a GameBoyOperation message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyOperation
     * @static
     * @param {GameBoyOperation} message GameBoyOperation
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyOperation.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.address = 0;
            object.operation = "";
        }
        if (message.address != null && message.hasOwnProperty("address"))
            object.address = message.address;
        if (message.operation != null && message.hasOwnProperty("operation"))
            object.operation = message.operation;
        return object;
    };

    /**
     * Converts this GameBoyOperation to JSON.
     * @function toJSON
     * @memberof GameBoyOperation
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyOperation.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyOperation;
})();

$root.GameBoyIntructionTimings = (function() {

    /**
     * Properties of a GameBoyIntructionTimings.
     * @exports IGameBoyIntructionTimings
     * @interface IGameBoyIntructionTimings
     * @property {number|null} [machineCycles] GameBoyIntructionTimings machineCycles
     * @property {number|null} [throttlingStates] GameBoyIntructionTimings throttlingStates
     */

    /**
     * Constructs a new GameBoyIntructionTimings.
     * @exports GameBoyIntructionTimings
     * @classdesc Represents a GameBoyIntructionTimings.
     * @implements IGameBoyIntructionTimings
     * @constructor
     * @param {IGameBoyIntructionTimings=} [properties] Properties to set
     */
    function GameBoyIntructionTimings(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyIntructionTimings machineCycles.
     * @member {number} machineCycles
     * @memberof GameBoyIntructionTimings
     * @instance
     */
    GameBoyIntructionTimings.prototype.machineCycles = 0;

    /**
     * GameBoyIntructionTimings throttlingStates.
     * @member {number} throttlingStates
     * @memberof GameBoyIntructionTimings
     * @instance
     */
    GameBoyIntructionTimings.prototype.throttlingStates = 0;

    /**
     * Creates a new GameBoyIntructionTimings instance using the specified properties.
     * @function create
     * @memberof GameBoyIntructionTimings
     * @static
     * @param {IGameBoyIntructionTimings=} [properties] Properties to set
     * @returns {GameBoyIntructionTimings} GameBoyIntructionTimings instance
     */
    GameBoyIntructionTimings.create = function create(properties) {
        return new GameBoyIntructionTimings(properties);
    };

    /**
     * Encodes the specified GameBoyIntructionTimings message. Does not implicitly {@link GameBoyIntructionTimings.verify|verify} messages.
     * @function encode
     * @memberof GameBoyIntructionTimings
     * @static
     * @param {IGameBoyIntructionTimings} message GameBoyIntructionTimings message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyIntructionTimings.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.machineCycles != null && message.hasOwnProperty("machineCycles"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.machineCycles);
        if (message.throttlingStates != null && message.hasOwnProperty("throttlingStates"))
            writer.uint32(/* id 2, wireType 0 =*/16).uint32(message.throttlingStates);
        return writer;
    };

    /**
     * Encodes the specified GameBoyIntructionTimings message, length delimited. Does not implicitly {@link GameBoyIntructionTimings.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyIntructionTimings
     * @static
     * @param {IGameBoyIntructionTimings} message GameBoyIntructionTimings message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyIntructionTimings.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyIntructionTimings message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyIntructionTimings
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyIntructionTimings} GameBoyIntructionTimings
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyIntructionTimings.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyIntructionTimings();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.machineCycles = reader.uint32();
                break;
            case 2:
                message.throttlingStates = reader.uint32();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyIntructionTimings message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyIntructionTimings
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyIntructionTimings} GameBoyIntructionTimings
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyIntructionTimings.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyIntructionTimings message.
     * @function verify
     * @memberof GameBoyIntructionTimings
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyIntructionTimings.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.machineCycles != null && message.hasOwnProperty("machineCycles"))
            if (!$util.isInteger(message.machineCycles))
                return "machineCycles: integer expected";
        if (message.throttlingStates != null && message.hasOwnProperty("throttlingStates"))
            if (!$util.isInteger(message.throttlingStates))
                return "throttlingStates: integer expected";
        return null;
    };

    /**
     * Creates a GameBoyIntructionTimings message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyIntructionTimings
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyIntructionTimings} GameBoyIntructionTimings
     */
    GameBoyIntructionTimings.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyIntructionTimings)
            return object;
        var message = new $root.GameBoyIntructionTimings();
        if (object.machineCycles != null)
            message.machineCycles = object.machineCycles >>> 0;
        if (object.throttlingStates != null)
            message.throttlingStates = object.throttlingStates >>> 0;
        return message;
    };

    /**
     * Creates a plain object from a GameBoyIntructionTimings message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyIntructionTimings
     * @static
     * @param {GameBoyIntructionTimings} message GameBoyIntructionTimings
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyIntructionTimings.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.machineCycles = 0;
            object.throttlingStates = 0;
        }
        if (message.machineCycles != null && message.hasOwnProperty("machineCycles"))
            object.machineCycles = message.machineCycles;
        if (message.throttlingStates != null && message.hasOwnProperty("throttlingStates"))
            object.throttlingStates = message.throttlingStates;
        return object;
    };

    /**
     * Converts this GameBoyIntructionTimings to JSON.
     * @function toJSON
     * @memberof GameBoyIntructionTimings
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyIntructionTimings.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyIntructionTimings;
})();

$root.GameBoyGpuState = (function() {

    /**
     * Properties of a GameBoyGpuState.
     * @exports IGameBoyGpuState
     * @interface IGameBoyGpuState
     * @property {IGameBoyRenderSettings|null} [renderSettings] GameBoyGpuState renderSettings
     * @property {Array.<IGameBoyTile>|null} [backgroundTiles] GameBoyGpuState backgroundTiles
     * @property {Array.<IGameBoyTile>|null} [spriteTiles] GameBoyGpuState spriteTiles
     * @property {Array.<IGameBoySprite>|null} [sprites] GameBoyGpuState sprites
     */

    /**
     * Constructs a new GameBoyGpuState.
     * @exports GameBoyGpuState
     * @classdesc Represents a GameBoyGpuState.
     * @implements IGameBoyGpuState
     * @constructor
     * @param {IGameBoyGpuState=} [properties] Properties to set
     */
    function GameBoyGpuState(properties) {
        this.backgroundTiles = [];
        this.spriteTiles = [];
        this.sprites = [];
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyGpuState renderSettings.
     * @member {IGameBoyRenderSettings|null|undefined} renderSettings
     * @memberof GameBoyGpuState
     * @instance
     */
    GameBoyGpuState.prototype.renderSettings = null;

    /**
     * GameBoyGpuState backgroundTiles.
     * @member {Array.<IGameBoyTile>} backgroundTiles
     * @memberof GameBoyGpuState
     * @instance
     */
    GameBoyGpuState.prototype.backgroundTiles = $util.emptyArray;

    /**
     * GameBoyGpuState spriteTiles.
     * @member {Array.<IGameBoyTile>} spriteTiles
     * @memberof GameBoyGpuState
     * @instance
     */
    GameBoyGpuState.prototype.spriteTiles = $util.emptyArray;

    /**
     * GameBoyGpuState sprites.
     * @member {Array.<IGameBoySprite>} sprites
     * @memberof GameBoyGpuState
     * @instance
     */
    GameBoyGpuState.prototype.sprites = $util.emptyArray;

    /**
     * Creates a new GameBoyGpuState instance using the specified properties.
     * @function create
     * @memberof GameBoyGpuState
     * @static
     * @param {IGameBoyGpuState=} [properties] Properties to set
     * @returns {GameBoyGpuState} GameBoyGpuState instance
     */
    GameBoyGpuState.create = function create(properties) {
        return new GameBoyGpuState(properties);
    };

    /**
     * Encodes the specified GameBoyGpuState message. Does not implicitly {@link GameBoyGpuState.verify|verify} messages.
     * @function encode
     * @memberof GameBoyGpuState
     * @static
     * @param {IGameBoyGpuState} message GameBoyGpuState message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyGpuState.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.renderSettings != null && message.hasOwnProperty("renderSettings"))
            $root.GameBoyRenderSettings.encode(message.renderSettings, writer.uint32(/* id 1, wireType 2 =*/10).fork()).ldelim();
        if (message.backgroundTiles != null && message.backgroundTiles.length)
            for (var i = 0; i < message.backgroundTiles.length; ++i)
                $root.GameBoyTile.encode(message.backgroundTiles[i], writer.uint32(/* id 2, wireType 2 =*/18).fork()).ldelim();
        if (message.spriteTiles != null && message.spriteTiles.length)
            for (var i = 0; i < message.spriteTiles.length; ++i)
                $root.GameBoyTile.encode(message.spriteTiles[i], writer.uint32(/* id 3, wireType 2 =*/26).fork()).ldelim();
        if (message.sprites != null && message.sprites.length)
            for (var i = 0; i < message.sprites.length; ++i)
                $root.GameBoySprite.encode(message.sprites[i], writer.uint32(/* id 4, wireType 2 =*/34).fork()).ldelim();
        return writer;
    };

    /**
     * Encodes the specified GameBoyGpuState message, length delimited. Does not implicitly {@link GameBoyGpuState.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyGpuState
     * @static
     * @param {IGameBoyGpuState} message GameBoyGpuState message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyGpuState.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyGpuState message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyGpuState
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyGpuState} GameBoyGpuState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyGpuState.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyGpuState();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.renderSettings = $root.GameBoyRenderSettings.decode(reader, reader.uint32());
                break;
            case 2:
                if (!(message.backgroundTiles && message.backgroundTiles.length))
                    message.backgroundTiles = [];
                message.backgroundTiles.push($root.GameBoyTile.decode(reader, reader.uint32()));
                break;
            case 3:
                if (!(message.spriteTiles && message.spriteTiles.length))
                    message.spriteTiles = [];
                message.spriteTiles.push($root.GameBoyTile.decode(reader, reader.uint32()));
                break;
            case 4:
                if (!(message.sprites && message.sprites.length))
                    message.sprites = [];
                message.sprites.push($root.GameBoySprite.decode(reader, reader.uint32()));
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyGpuState message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyGpuState
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyGpuState} GameBoyGpuState
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyGpuState.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyGpuState message.
     * @function verify
     * @memberof GameBoyGpuState
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyGpuState.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.renderSettings != null && message.hasOwnProperty("renderSettings")) {
            var error = $root.GameBoyRenderSettings.verify(message.renderSettings);
            if (error)
                return "renderSettings." + error;
        }
        if (message.backgroundTiles != null && message.hasOwnProperty("backgroundTiles")) {
            if (!Array.isArray(message.backgroundTiles))
                return "backgroundTiles: array expected";
            for (var i = 0; i < message.backgroundTiles.length; ++i) {
                var error = $root.GameBoyTile.verify(message.backgroundTiles[i]);
                if (error)
                    return "backgroundTiles." + error;
            }
        }
        if (message.spriteTiles != null && message.hasOwnProperty("spriteTiles")) {
            if (!Array.isArray(message.spriteTiles))
                return "spriteTiles: array expected";
            for (var i = 0; i < message.spriteTiles.length; ++i) {
                var error = $root.GameBoyTile.verify(message.spriteTiles[i]);
                if (error)
                    return "spriteTiles." + error;
            }
        }
        if (message.sprites != null && message.hasOwnProperty("sprites")) {
            if (!Array.isArray(message.sprites))
                return "sprites: array expected";
            for (var i = 0; i < message.sprites.length; ++i) {
                var error = $root.GameBoySprite.verify(message.sprites[i]);
                if (error)
                    return "sprites." + error;
            }
        }
        return null;
    };

    /**
     * Creates a GameBoyGpuState message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyGpuState
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyGpuState} GameBoyGpuState
     */
    GameBoyGpuState.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyGpuState)
            return object;
        var message = new $root.GameBoyGpuState();
        if (object.renderSettings != null) {
            if (typeof object.renderSettings !== "object")
                throw TypeError(".GameBoyGpuState.renderSettings: object expected");
            message.renderSettings = $root.GameBoyRenderSettings.fromObject(object.renderSettings);
        }
        if (object.backgroundTiles) {
            if (!Array.isArray(object.backgroundTiles))
                throw TypeError(".GameBoyGpuState.backgroundTiles: array expected");
            message.backgroundTiles = [];
            for (var i = 0; i < object.backgroundTiles.length; ++i) {
                if (typeof object.backgroundTiles[i] !== "object")
                    throw TypeError(".GameBoyGpuState.backgroundTiles: object expected");
                message.backgroundTiles[i] = $root.GameBoyTile.fromObject(object.backgroundTiles[i]);
            }
        }
        if (object.spriteTiles) {
            if (!Array.isArray(object.spriteTiles))
                throw TypeError(".GameBoyGpuState.spriteTiles: array expected");
            message.spriteTiles = [];
            for (var i = 0; i < object.spriteTiles.length; ++i) {
                if (typeof object.spriteTiles[i] !== "object")
                    throw TypeError(".GameBoyGpuState.spriteTiles: object expected");
                message.spriteTiles[i] = $root.GameBoyTile.fromObject(object.spriteTiles[i]);
            }
        }
        if (object.sprites) {
            if (!Array.isArray(object.sprites))
                throw TypeError(".GameBoyGpuState.sprites: array expected");
            message.sprites = [];
            for (var i = 0; i < object.sprites.length; ++i) {
                if (typeof object.sprites[i] !== "object")
                    throw TypeError(".GameBoyGpuState.sprites: object expected");
                message.sprites[i] = $root.GameBoySprite.fromObject(object.sprites[i]);
            }
        }
        return message;
    };

    /**
     * Creates a plain object from a GameBoyGpuState message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyGpuState
     * @static
     * @param {GameBoyGpuState} message GameBoyGpuState
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyGpuState.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.arrays || options.defaults) {
            object.backgroundTiles = [];
            object.spriteTiles = [];
            object.sprites = [];
        }
        if (options.defaults)
            object.renderSettings = null;
        if (message.renderSettings != null && message.hasOwnProperty("renderSettings"))
            object.renderSettings = $root.GameBoyRenderSettings.toObject(message.renderSettings, options);
        if (message.backgroundTiles && message.backgroundTiles.length) {
            object.backgroundTiles = [];
            for (var j = 0; j < message.backgroundTiles.length; ++j)
                object.backgroundTiles[j] = $root.GameBoyTile.toObject(message.backgroundTiles[j], options);
        }
        if (message.spriteTiles && message.spriteTiles.length) {
            object.spriteTiles = [];
            for (var j = 0; j < message.spriteTiles.length; ++j)
                object.spriteTiles[j] = $root.GameBoyTile.toObject(message.spriteTiles[j], options);
        }
        if (message.sprites && message.sprites.length) {
            object.sprites = [];
            for (var j = 0; j < message.sprites.length; ++j)
                object.sprites[j] = $root.GameBoySprite.toObject(message.sprites[j], options);
        }
        return object;
    };

    /**
     * Converts this GameBoyGpuState to JSON.
     * @function toJSON
     * @memberof GameBoyGpuState
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyGpuState.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyGpuState;
})();

$root.GameBoyRenderSettings = (function() {

    /**
     * Properties of a GameBoyRenderSettings.
     * @exports IGameBoyRenderSettings
     * @interface IGameBoyRenderSettings
     * @property {boolean|null} [backgroundDisplay] GameBoyRenderSettings backgroundDisplay
     * @property {boolean|null} [windowEnabled] GameBoyRenderSettings windowEnabled
     * @property {boolean|null} [spritesEnabled] GameBoyRenderSettings spritesEnabled
     * @property {boolean|null} [largeSprites] GameBoyRenderSettings largeSprites
     * @property {ITileMapAddress|null} [backgroundTileMapAddress] GameBoyRenderSettings backgroundTileMapAddress
     * @property {ITileMapAddress|null} [windowTileMapAddress] GameBoyRenderSettings windowTileMapAddress
     * @property {number|null} [tileSetAddress] GameBoyRenderSettings tileSetAddress
     * @property {number|null} [spriteTileSetAddress] GameBoyRenderSettings spriteTileSetAddress
     * @property {ICoordinates|null} [scroll] GameBoyRenderSettings scroll
     * @property {ICoordinates|null} [windowPosition] GameBoyRenderSettings windowPosition
     */

    /**
     * Constructs a new GameBoyRenderSettings.
     * @exports GameBoyRenderSettings
     * @classdesc Represents a GameBoyRenderSettings.
     * @implements IGameBoyRenderSettings
     * @constructor
     * @param {IGameBoyRenderSettings=} [properties] Properties to set
     */
    function GameBoyRenderSettings(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyRenderSettings backgroundDisplay.
     * @member {boolean} backgroundDisplay
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.backgroundDisplay = false;

    /**
     * GameBoyRenderSettings windowEnabled.
     * @member {boolean} windowEnabled
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.windowEnabled = false;

    /**
     * GameBoyRenderSettings spritesEnabled.
     * @member {boolean} spritesEnabled
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.spritesEnabled = false;

    /**
     * GameBoyRenderSettings largeSprites.
     * @member {boolean} largeSprites
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.largeSprites = false;

    /**
     * GameBoyRenderSettings backgroundTileMapAddress.
     * @member {ITileMapAddress|null|undefined} backgroundTileMapAddress
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.backgroundTileMapAddress = null;

    /**
     * GameBoyRenderSettings windowTileMapAddress.
     * @member {ITileMapAddress|null|undefined} windowTileMapAddress
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.windowTileMapAddress = null;

    /**
     * GameBoyRenderSettings tileSetAddress.
     * @member {number} tileSetAddress
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.tileSetAddress = 0;

    /**
     * GameBoyRenderSettings spriteTileSetAddress.
     * @member {number} spriteTileSetAddress
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.spriteTileSetAddress = 0;

    /**
     * GameBoyRenderSettings scroll.
     * @member {ICoordinates|null|undefined} scroll
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.scroll = null;

    /**
     * GameBoyRenderSettings windowPosition.
     * @member {ICoordinates|null|undefined} windowPosition
     * @memberof GameBoyRenderSettings
     * @instance
     */
    GameBoyRenderSettings.prototype.windowPosition = null;

    /**
     * Creates a new GameBoyRenderSettings instance using the specified properties.
     * @function create
     * @memberof GameBoyRenderSettings
     * @static
     * @param {IGameBoyRenderSettings=} [properties] Properties to set
     * @returns {GameBoyRenderSettings} GameBoyRenderSettings instance
     */
    GameBoyRenderSettings.create = function create(properties) {
        return new GameBoyRenderSettings(properties);
    };

    /**
     * Encodes the specified GameBoyRenderSettings message. Does not implicitly {@link GameBoyRenderSettings.verify|verify} messages.
     * @function encode
     * @memberof GameBoyRenderSettings
     * @static
     * @param {IGameBoyRenderSettings} message GameBoyRenderSettings message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyRenderSettings.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.backgroundDisplay != null && message.hasOwnProperty("backgroundDisplay"))
            writer.uint32(/* id 1, wireType 0 =*/8).bool(message.backgroundDisplay);
        if (message.windowEnabled != null && message.hasOwnProperty("windowEnabled"))
            writer.uint32(/* id 2, wireType 0 =*/16).bool(message.windowEnabled);
        if (message.spritesEnabled != null && message.hasOwnProperty("spritesEnabled"))
            writer.uint32(/* id 3, wireType 0 =*/24).bool(message.spritesEnabled);
        if (message.largeSprites != null && message.hasOwnProperty("largeSprites"))
            writer.uint32(/* id 4, wireType 0 =*/32).bool(message.largeSprites);
        if (message.backgroundTileMapAddress != null && message.hasOwnProperty("backgroundTileMapAddress"))
            $root.TileMapAddress.encode(message.backgroundTileMapAddress, writer.uint32(/* id 5, wireType 2 =*/42).fork()).ldelim();
        if (message.windowTileMapAddress != null && message.hasOwnProperty("windowTileMapAddress"))
            $root.TileMapAddress.encode(message.windowTileMapAddress, writer.uint32(/* id 6, wireType 2 =*/50).fork()).ldelim();
        if (message.tileSetAddress != null && message.hasOwnProperty("tileSetAddress"))
            writer.uint32(/* id 7, wireType 0 =*/56).uint32(message.tileSetAddress);
        if (message.spriteTileSetAddress != null && message.hasOwnProperty("spriteTileSetAddress"))
            writer.uint32(/* id 8, wireType 0 =*/64).uint32(message.spriteTileSetAddress);
        if (message.scroll != null && message.hasOwnProperty("scroll"))
            $root.Coordinates.encode(message.scroll, writer.uint32(/* id 9, wireType 2 =*/74).fork()).ldelim();
        if (message.windowPosition != null && message.hasOwnProperty("windowPosition"))
            $root.Coordinates.encode(message.windowPosition, writer.uint32(/* id 10, wireType 2 =*/82).fork()).ldelim();
        return writer;
    };

    /**
     * Encodes the specified GameBoyRenderSettings message, length delimited. Does not implicitly {@link GameBoyRenderSettings.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyRenderSettings
     * @static
     * @param {IGameBoyRenderSettings} message GameBoyRenderSettings message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyRenderSettings.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyRenderSettings message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyRenderSettings
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyRenderSettings} GameBoyRenderSettings
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyRenderSettings.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyRenderSettings();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.backgroundDisplay = reader.bool();
                break;
            case 2:
                message.windowEnabled = reader.bool();
                break;
            case 3:
                message.spritesEnabled = reader.bool();
                break;
            case 4:
                message.largeSprites = reader.bool();
                break;
            case 5:
                message.backgroundTileMapAddress = $root.TileMapAddress.decode(reader, reader.uint32());
                break;
            case 6:
                message.windowTileMapAddress = $root.TileMapAddress.decode(reader, reader.uint32());
                break;
            case 7:
                message.tileSetAddress = reader.uint32();
                break;
            case 8:
                message.spriteTileSetAddress = reader.uint32();
                break;
            case 9:
                message.scroll = $root.Coordinates.decode(reader, reader.uint32());
                break;
            case 10:
                message.windowPosition = $root.Coordinates.decode(reader, reader.uint32());
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyRenderSettings message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyRenderSettings
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyRenderSettings} GameBoyRenderSettings
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyRenderSettings.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyRenderSettings message.
     * @function verify
     * @memberof GameBoyRenderSettings
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyRenderSettings.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.backgroundDisplay != null && message.hasOwnProperty("backgroundDisplay"))
            if (typeof message.backgroundDisplay !== "boolean")
                return "backgroundDisplay: boolean expected";
        if (message.windowEnabled != null && message.hasOwnProperty("windowEnabled"))
            if (typeof message.windowEnabled !== "boolean")
                return "windowEnabled: boolean expected";
        if (message.spritesEnabled != null && message.hasOwnProperty("spritesEnabled"))
            if (typeof message.spritesEnabled !== "boolean")
                return "spritesEnabled: boolean expected";
        if (message.largeSprites != null && message.hasOwnProperty("largeSprites"))
            if (typeof message.largeSprites !== "boolean")
                return "largeSprites: boolean expected";
        if (message.backgroundTileMapAddress != null && message.hasOwnProperty("backgroundTileMapAddress")) {
            var error = $root.TileMapAddress.verify(message.backgroundTileMapAddress);
            if (error)
                return "backgroundTileMapAddress." + error;
        }
        if (message.windowTileMapAddress != null && message.hasOwnProperty("windowTileMapAddress")) {
            var error = $root.TileMapAddress.verify(message.windowTileMapAddress);
            if (error)
                return "windowTileMapAddress." + error;
        }
        if (message.tileSetAddress != null && message.hasOwnProperty("tileSetAddress"))
            if (!$util.isInteger(message.tileSetAddress))
                return "tileSetAddress: integer expected";
        if (message.spriteTileSetAddress != null && message.hasOwnProperty("spriteTileSetAddress"))
            if (!$util.isInteger(message.spriteTileSetAddress))
                return "spriteTileSetAddress: integer expected";
        if (message.scroll != null && message.hasOwnProperty("scroll")) {
            var error = $root.Coordinates.verify(message.scroll);
            if (error)
                return "scroll." + error;
        }
        if (message.windowPosition != null && message.hasOwnProperty("windowPosition")) {
            var error = $root.Coordinates.verify(message.windowPosition);
            if (error)
                return "windowPosition." + error;
        }
        return null;
    };

    /**
     * Creates a GameBoyRenderSettings message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyRenderSettings
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyRenderSettings} GameBoyRenderSettings
     */
    GameBoyRenderSettings.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyRenderSettings)
            return object;
        var message = new $root.GameBoyRenderSettings();
        if (object.backgroundDisplay != null)
            message.backgroundDisplay = Boolean(object.backgroundDisplay);
        if (object.windowEnabled != null)
            message.windowEnabled = Boolean(object.windowEnabled);
        if (object.spritesEnabled != null)
            message.spritesEnabled = Boolean(object.spritesEnabled);
        if (object.largeSprites != null)
            message.largeSprites = Boolean(object.largeSprites);
        if (object.backgroundTileMapAddress != null) {
            if (typeof object.backgroundTileMapAddress !== "object")
                throw TypeError(".GameBoyRenderSettings.backgroundTileMapAddress: object expected");
            message.backgroundTileMapAddress = $root.TileMapAddress.fromObject(object.backgroundTileMapAddress);
        }
        if (object.windowTileMapAddress != null) {
            if (typeof object.windowTileMapAddress !== "object")
                throw TypeError(".GameBoyRenderSettings.windowTileMapAddress: object expected");
            message.windowTileMapAddress = $root.TileMapAddress.fromObject(object.windowTileMapAddress);
        }
        if (object.tileSetAddress != null)
            message.tileSetAddress = object.tileSetAddress >>> 0;
        if (object.spriteTileSetAddress != null)
            message.spriteTileSetAddress = object.spriteTileSetAddress >>> 0;
        if (object.scroll != null) {
            if (typeof object.scroll !== "object")
                throw TypeError(".GameBoyRenderSettings.scroll: object expected");
            message.scroll = $root.Coordinates.fromObject(object.scroll);
        }
        if (object.windowPosition != null) {
            if (typeof object.windowPosition !== "object")
                throw TypeError(".GameBoyRenderSettings.windowPosition: object expected");
            message.windowPosition = $root.Coordinates.fromObject(object.windowPosition);
        }
        return message;
    };

    /**
     * Creates a plain object from a GameBoyRenderSettings message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyRenderSettings
     * @static
     * @param {GameBoyRenderSettings} message GameBoyRenderSettings
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyRenderSettings.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.backgroundDisplay = false;
            object.windowEnabled = false;
            object.spritesEnabled = false;
            object.largeSprites = false;
            object.backgroundTileMapAddress = null;
            object.windowTileMapAddress = null;
            object.tileSetAddress = 0;
            object.spriteTileSetAddress = 0;
            object.scroll = null;
            object.windowPosition = null;
        }
        if (message.backgroundDisplay != null && message.hasOwnProperty("backgroundDisplay"))
            object.backgroundDisplay = message.backgroundDisplay;
        if (message.windowEnabled != null && message.hasOwnProperty("windowEnabled"))
            object.windowEnabled = message.windowEnabled;
        if (message.spritesEnabled != null && message.hasOwnProperty("spritesEnabled"))
            object.spritesEnabled = message.spritesEnabled;
        if (message.largeSprites != null && message.hasOwnProperty("largeSprites"))
            object.largeSprites = message.largeSprites;
        if (message.backgroundTileMapAddress != null && message.hasOwnProperty("backgroundTileMapAddress"))
            object.backgroundTileMapAddress = $root.TileMapAddress.toObject(message.backgroundTileMapAddress, options);
        if (message.windowTileMapAddress != null && message.hasOwnProperty("windowTileMapAddress"))
            object.windowTileMapAddress = $root.TileMapAddress.toObject(message.windowTileMapAddress, options);
        if (message.tileSetAddress != null && message.hasOwnProperty("tileSetAddress"))
            object.tileSetAddress = message.tileSetAddress;
        if (message.spriteTileSetAddress != null && message.hasOwnProperty("spriteTileSetAddress"))
            object.spriteTileSetAddress = message.spriteTileSetAddress;
        if (message.scroll != null && message.hasOwnProperty("scroll"))
            object.scroll = $root.Coordinates.toObject(message.scroll, options);
        if (message.windowPosition != null && message.hasOwnProperty("windowPosition"))
            object.windowPosition = $root.Coordinates.toObject(message.windowPosition, options);
        return object;
    };

    /**
     * Converts this GameBoyRenderSettings to JSON.
     * @function toJSON
     * @memberof GameBoyRenderSettings
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyRenderSettings.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyRenderSettings;
})();

$root.TileMapAddress = (function() {

    /**
     * Properties of a TileMapAddress.
     * @exports ITileMapAddress
     * @interface ITileMapAddress
     * @property {number|null} [address] TileMapAddress address
     * @property {boolean|null} [isSigned] TileMapAddress isSigned
     */

    /**
     * Constructs a new TileMapAddress.
     * @exports TileMapAddress
     * @classdesc Represents a TileMapAddress.
     * @implements ITileMapAddress
     * @constructor
     * @param {ITileMapAddress=} [properties] Properties to set
     */
    function TileMapAddress(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * TileMapAddress address.
     * @member {number} address
     * @memberof TileMapAddress
     * @instance
     */
    TileMapAddress.prototype.address = 0;

    /**
     * TileMapAddress isSigned.
     * @member {boolean} isSigned
     * @memberof TileMapAddress
     * @instance
     */
    TileMapAddress.prototype.isSigned = false;

    /**
     * Creates a new TileMapAddress instance using the specified properties.
     * @function create
     * @memberof TileMapAddress
     * @static
     * @param {ITileMapAddress=} [properties] Properties to set
     * @returns {TileMapAddress} TileMapAddress instance
     */
    TileMapAddress.create = function create(properties) {
        return new TileMapAddress(properties);
    };

    /**
     * Encodes the specified TileMapAddress message. Does not implicitly {@link TileMapAddress.verify|verify} messages.
     * @function encode
     * @memberof TileMapAddress
     * @static
     * @param {ITileMapAddress} message TileMapAddress message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    TileMapAddress.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.address != null && message.hasOwnProperty("address"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.address);
        if (message.isSigned != null && message.hasOwnProperty("isSigned"))
            writer.uint32(/* id 2, wireType 0 =*/16).bool(message.isSigned);
        return writer;
    };

    /**
     * Encodes the specified TileMapAddress message, length delimited. Does not implicitly {@link TileMapAddress.verify|verify} messages.
     * @function encodeDelimited
     * @memberof TileMapAddress
     * @static
     * @param {ITileMapAddress} message TileMapAddress message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    TileMapAddress.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a TileMapAddress message from the specified reader or buffer.
     * @function decode
     * @memberof TileMapAddress
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {TileMapAddress} TileMapAddress
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    TileMapAddress.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.TileMapAddress();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.address = reader.uint32();
                break;
            case 2:
                message.isSigned = reader.bool();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a TileMapAddress message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof TileMapAddress
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {TileMapAddress} TileMapAddress
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    TileMapAddress.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a TileMapAddress message.
     * @function verify
     * @memberof TileMapAddress
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    TileMapAddress.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.address != null && message.hasOwnProperty("address"))
            if (!$util.isInteger(message.address))
                return "address: integer expected";
        if (message.isSigned != null && message.hasOwnProperty("isSigned"))
            if (typeof message.isSigned !== "boolean")
                return "isSigned: boolean expected";
        return null;
    };

    /**
     * Creates a TileMapAddress message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof TileMapAddress
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {TileMapAddress} TileMapAddress
     */
    TileMapAddress.fromObject = function fromObject(object) {
        if (object instanceof $root.TileMapAddress)
            return object;
        var message = new $root.TileMapAddress();
        if (object.address != null)
            message.address = object.address >>> 0;
        if (object.isSigned != null)
            message.isSigned = Boolean(object.isSigned);
        return message;
    };

    /**
     * Creates a plain object from a TileMapAddress message. Also converts values to other types if specified.
     * @function toObject
     * @memberof TileMapAddress
     * @static
     * @param {TileMapAddress} message TileMapAddress
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    TileMapAddress.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.address = 0;
            object.isSigned = false;
        }
        if (message.address != null && message.hasOwnProperty("address"))
            object.address = message.address;
        if (message.isSigned != null && message.hasOwnProperty("isSigned"))
            object.isSigned = message.isSigned;
        return object;
    };

    /**
     * Converts this TileMapAddress to JSON.
     * @function toJSON
     * @memberof TileMapAddress
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    TileMapAddress.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return TileMapAddress;
})();

$root.Coordinates = (function() {

    /**
     * Properties of a Coordinates.
     * @exports ICoordinates
     * @interface ICoordinates
     * @property {number|null} [x] Coordinates x
     * @property {number|null} [y] Coordinates y
     */

    /**
     * Constructs a new Coordinates.
     * @exports Coordinates
     * @classdesc Represents a Coordinates.
     * @implements ICoordinates
     * @constructor
     * @param {ICoordinates=} [properties] Properties to set
     */
    function Coordinates(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * Coordinates x.
     * @member {number} x
     * @memberof Coordinates
     * @instance
     */
    Coordinates.prototype.x = 0;

    /**
     * Coordinates y.
     * @member {number} y
     * @memberof Coordinates
     * @instance
     */
    Coordinates.prototype.y = 0;

    /**
     * Creates a new Coordinates instance using the specified properties.
     * @function create
     * @memberof Coordinates
     * @static
     * @param {ICoordinates=} [properties] Properties to set
     * @returns {Coordinates} Coordinates instance
     */
    Coordinates.create = function create(properties) {
        return new Coordinates(properties);
    };

    /**
     * Encodes the specified Coordinates message. Does not implicitly {@link Coordinates.verify|verify} messages.
     * @function encode
     * @memberof Coordinates
     * @static
     * @param {ICoordinates} message Coordinates message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    Coordinates.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.x != null && message.hasOwnProperty("x"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.x);
        if (message.y != null && message.hasOwnProperty("y"))
            writer.uint32(/* id 2, wireType 0 =*/16).uint32(message.y);
        return writer;
    };

    /**
     * Encodes the specified Coordinates message, length delimited. Does not implicitly {@link Coordinates.verify|verify} messages.
     * @function encodeDelimited
     * @memberof Coordinates
     * @static
     * @param {ICoordinates} message Coordinates message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    Coordinates.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a Coordinates message from the specified reader or buffer.
     * @function decode
     * @memberof Coordinates
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {Coordinates} Coordinates
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    Coordinates.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.Coordinates();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.x = reader.uint32();
                break;
            case 2:
                message.y = reader.uint32();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a Coordinates message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof Coordinates
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {Coordinates} Coordinates
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    Coordinates.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a Coordinates message.
     * @function verify
     * @memberof Coordinates
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    Coordinates.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.x != null && message.hasOwnProperty("x"))
            if (!$util.isInteger(message.x))
                return "x: integer expected";
        if (message.y != null && message.hasOwnProperty("y"))
            if (!$util.isInteger(message.y))
                return "y: integer expected";
        return null;
    };

    /**
     * Creates a Coordinates message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof Coordinates
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {Coordinates} Coordinates
     */
    Coordinates.fromObject = function fromObject(object) {
        if (object instanceof $root.Coordinates)
            return object;
        var message = new $root.Coordinates();
        if (object.x != null)
            message.x = object.x >>> 0;
        if (object.y != null)
            message.y = object.y >>> 0;
        return message;
    };

    /**
     * Creates a plain object from a Coordinates message. Also converts values to other types if specified.
     * @function toObject
     * @memberof Coordinates
     * @static
     * @param {Coordinates} message Coordinates
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    Coordinates.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.x = 0;
            object.y = 0;
        }
        if (message.x != null && message.hasOwnProperty("x"))
            object.x = message.x;
        if (message.y != null && message.hasOwnProperty("y"))
            object.y = message.y;
        return object;
    };

    /**
     * Converts this Coordinates to JSON.
     * @function toJSON
     * @memberof Coordinates
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    Coordinates.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return Coordinates;
})();

$root.GameBoySprite = (function() {

    /**
     * Properties of a GameBoySprite.
     * @exports IGameBoySprite
     * @interface IGameBoySprite
     * @property {number|null} [x] GameBoySprite x
     * @property {number|null} [y] GameBoySprite y
     * @property {number|null} [tileNumber] GameBoySprite tileNumber
     * @property {boolean|null} [backgroundPriority] GameBoySprite backgroundPriority
     * @property {boolean|null} [yFlip] GameBoySprite yFlip
     * @property {boolean|null} [xFlip] GameBoySprite xFlip
     * @property {boolean|null} [usePalette1] GameBoySprite usePalette1
     */

    /**
     * Constructs a new GameBoySprite.
     * @exports GameBoySprite
     * @classdesc Represents a GameBoySprite.
     * @implements IGameBoySprite
     * @constructor
     * @param {IGameBoySprite=} [properties] Properties to set
     */
    function GameBoySprite(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoySprite x.
     * @member {number} x
     * @memberof GameBoySprite
     * @instance
     */
    GameBoySprite.prototype.x = 0;

    /**
     * GameBoySprite y.
     * @member {number} y
     * @memberof GameBoySprite
     * @instance
     */
    GameBoySprite.prototype.y = 0;

    /**
     * GameBoySprite tileNumber.
     * @member {number} tileNumber
     * @memberof GameBoySprite
     * @instance
     */
    GameBoySprite.prototype.tileNumber = 0;

    /**
     * GameBoySprite backgroundPriority.
     * @member {boolean} backgroundPriority
     * @memberof GameBoySprite
     * @instance
     */
    GameBoySprite.prototype.backgroundPriority = false;

    /**
     * GameBoySprite yFlip.
     * @member {boolean} yFlip
     * @memberof GameBoySprite
     * @instance
     */
    GameBoySprite.prototype.yFlip = false;

    /**
     * GameBoySprite xFlip.
     * @member {boolean} xFlip
     * @memberof GameBoySprite
     * @instance
     */
    GameBoySprite.prototype.xFlip = false;

    /**
     * GameBoySprite usePalette1.
     * @member {boolean} usePalette1
     * @memberof GameBoySprite
     * @instance
     */
    GameBoySprite.prototype.usePalette1 = false;

    /**
     * Creates a new GameBoySprite instance using the specified properties.
     * @function create
     * @memberof GameBoySprite
     * @static
     * @param {IGameBoySprite=} [properties] Properties to set
     * @returns {GameBoySprite} GameBoySprite instance
     */
    GameBoySprite.create = function create(properties) {
        return new GameBoySprite(properties);
    };

    /**
     * Encodes the specified GameBoySprite message. Does not implicitly {@link GameBoySprite.verify|verify} messages.
     * @function encode
     * @memberof GameBoySprite
     * @static
     * @param {IGameBoySprite} message GameBoySprite message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoySprite.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.x != null && message.hasOwnProperty("x"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.x);
        if (message.y != null && message.hasOwnProperty("y"))
            writer.uint32(/* id 2, wireType 0 =*/16).uint32(message.y);
        if (message.tileNumber != null && message.hasOwnProperty("tileNumber"))
            writer.uint32(/* id 3, wireType 0 =*/24).uint32(message.tileNumber);
        if (message.backgroundPriority != null && message.hasOwnProperty("backgroundPriority"))
            writer.uint32(/* id 4, wireType 0 =*/32).bool(message.backgroundPriority);
        if (message.yFlip != null && message.hasOwnProperty("yFlip"))
            writer.uint32(/* id 5, wireType 0 =*/40).bool(message.yFlip);
        if (message.xFlip != null && message.hasOwnProperty("xFlip"))
            writer.uint32(/* id 6, wireType 0 =*/48).bool(message.xFlip);
        if (message.usePalette1 != null && message.hasOwnProperty("usePalette1"))
            writer.uint32(/* id 7, wireType 0 =*/56).bool(message.usePalette1);
        return writer;
    };

    /**
     * Encodes the specified GameBoySprite message, length delimited. Does not implicitly {@link GameBoySprite.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoySprite
     * @static
     * @param {IGameBoySprite} message GameBoySprite message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoySprite.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoySprite message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoySprite
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoySprite} GameBoySprite
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoySprite.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoySprite();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.x = reader.uint32();
                break;
            case 2:
                message.y = reader.uint32();
                break;
            case 3:
                message.tileNumber = reader.uint32();
                break;
            case 4:
                message.backgroundPriority = reader.bool();
                break;
            case 5:
                message.yFlip = reader.bool();
                break;
            case 6:
                message.xFlip = reader.bool();
                break;
            case 7:
                message.usePalette1 = reader.bool();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoySprite message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoySprite
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoySprite} GameBoySprite
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoySprite.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoySprite message.
     * @function verify
     * @memberof GameBoySprite
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoySprite.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.x != null && message.hasOwnProperty("x"))
            if (!$util.isInteger(message.x))
                return "x: integer expected";
        if (message.y != null && message.hasOwnProperty("y"))
            if (!$util.isInteger(message.y))
                return "y: integer expected";
        if (message.tileNumber != null && message.hasOwnProperty("tileNumber"))
            if (!$util.isInteger(message.tileNumber))
                return "tileNumber: integer expected";
        if (message.backgroundPriority != null && message.hasOwnProperty("backgroundPriority"))
            if (typeof message.backgroundPriority !== "boolean")
                return "backgroundPriority: boolean expected";
        if (message.yFlip != null && message.hasOwnProperty("yFlip"))
            if (typeof message.yFlip !== "boolean")
                return "yFlip: boolean expected";
        if (message.xFlip != null && message.hasOwnProperty("xFlip"))
            if (typeof message.xFlip !== "boolean")
                return "xFlip: boolean expected";
        if (message.usePalette1 != null && message.hasOwnProperty("usePalette1"))
            if (typeof message.usePalette1 !== "boolean")
                return "usePalette1: boolean expected";
        return null;
    };

    /**
     * Creates a GameBoySprite message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoySprite
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoySprite} GameBoySprite
     */
    GameBoySprite.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoySprite)
            return object;
        var message = new $root.GameBoySprite();
        if (object.x != null)
            message.x = object.x >>> 0;
        if (object.y != null)
            message.y = object.y >>> 0;
        if (object.tileNumber != null)
            message.tileNumber = object.tileNumber >>> 0;
        if (object.backgroundPriority != null)
            message.backgroundPriority = Boolean(object.backgroundPriority);
        if (object.yFlip != null)
            message.yFlip = Boolean(object.yFlip);
        if (object.xFlip != null)
            message.xFlip = Boolean(object.xFlip);
        if (object.usePalette1 != null)
            message.usePalette1 = Boolean(object.usePalette1);
        return message;
    };

    /**
     * Creates a plain object from a GameBoySprite message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoySprite
     * @static
     * @param {GameBoySprite} message GameBoySprite
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoySprite.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.x = 0;
            object.y = 0;
            object.tileNumber = 0;
            object.backgroundPriority = false;
            object.yFlip = false;
            object.xFlip = false;
            object.usePalette1 = false;
        }
        if (message.x != null && message.hasOwnProperty("x"))
            object.x = message.x;
        if (message.y != null && message.hasOwnProperty("y"))
            object.y = message.y;
        if (message.tileNumber != null && message.hasOwnProperty("tileNumber"))
            object.tileNumber = message.tileNumber;
        if (message.backgroundPriority != null && message.hasOwnProperty("backgroundPriority"))
            object.backgroundPriority = message.backgroundPriority;
        if (message.yFlip != null && message.hasOwnProperty("yFlip"))
            object.yFlip = message.yFlip;
        if (message.xFlip != null && message.hasOwnProperty("xFlip"))
            object.xFlip = message.xFlip;
        if (message.usePalette1 != null && message.hasOwnProperty("usePalette1"))
            object.usePalette1 = message.usePalette1;
        return object;
    };

    /**
     * Converts this GameBoySprite to JSON.
     * @function toJSON
     * @memberof GameBoySprite
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoySprite.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoySprite;
})();

$root.GameBoyTile = (function() {

    /**
     * Properties of a GameBoyTile.
     * @exports IGameBoyTile
     * @interface IGameBoyTile
     * @property {number|null} [tileNumber] GameBoyTile tileNumber
     * @property {Uint8Array|null} [palette] GameBoyTile palette
     */

    /**
     * Constructs a new GameBoyTile.
     * @exports GameBoyTile
     * @classdesc Represents a GameBoyTile.
     * @implements IGameBoyTile
     * @constructor
     * @param {IGameBoyTile=} [properties] Properties to set
     */
    function GameBoyTile(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyTile tileNumber.
     * @member {number} tileNumber
     * @memberof GameBoyTile
     * @instance
     */
    GameBoyTile.prototype.tileNumber = 0;

    /**
     * GameBoyTile palette.
     * @member {Uint8Array} palette
     * @memberof GameBoyTile
     * @instance
     */
    GameBoyTile.prototype.palette = $util.newBuffer([]);

    /**
     * Creates a new GameBoyTile instance using the specified properties.
     * @function create
     * @memberof GameBoyTile
     * @static
     * @param {IGameBoyTile=} [properties] Properties to set
     * @returns {GameBoyTile} GameBoyTile instance
     */
    GameBoyTile.create = function create(properties) {
        return new GameBoyTile(properties);
    };

    /**
     * Encodes the specified GameBoyTile message. Does not implicitly {@link GameBoyTile.verify|verify} messages.
     * @function encode
     * @memberof GameBoyTile
     * @static
     * @param {IGameBoyTile} message GameBoyTile message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyTile.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.tileNumber != null && message.hasOwnProperty("tileNumber"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.tileNumber);
        if (message.palette != null && message.hasOwnProperty("palette"))
            writer.uint32(/* id 2, wireType 2 =*/18).bytes(message.palette);
        return writer;
    };

    /**
     * Encodes the specified GameBoyTile message, length delimited. Does not implicitly {@link GameBoyTile.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyTile
     * @static
     * @param {IGameBoyTile} message GameBoyTile message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyTile.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyTile message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyTile
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyTile} GameBoyTile
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyTile.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyTile();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.tileNumber = reader.uint32();
                break;
            case 2:
                message.palette = reader.bytes();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyTile message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyTile
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyTile} GameBoyTile
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyTile.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyTile message.
     * @function verify
     * @memberof GameBoyTile
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyTile.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.tileNumber != null && message.hasOwnProperty("tileNumber"))
            if (!$util.isInteger(message.tileNumber))
                return "tileNumber: integer expected";
        if (message.palette != null && message.hasOwnProperty("palette"))
            if (!(message.palette && typeof message.palette.length === "number" || $util.isString(message.palette)))
                return "palette: buffer expected";
        return null;
    };

    /**
     * Creates a GameBoyTile message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyTile
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyTile} GameBoyTile
     */
    GameBoyTile.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyTile)
            return object;
        var message = new $root.GameBoyTile();
        if (object.tileNumber != null)
            message.tileNumber = object.tileNumber >>> 0;
        if (object.palette != null)
            if (typeof object.palette === "string")
                $util.base64.decode(object.palette, message.palette = $util.newBuffer($util.base64.length(object.palette)), 0);
            else if (object.palette.length)
                message.palette = object.palette;
        return message;
    };

    /**
     * Creates a plain object from a GameBoyTile message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyTile
     * @static
     * @param {GameBoyTile} message GameBoyTile
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyTile.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.tileNumber = 0;
            object.palette = options.bytes === String ? "" : [];
        }
        if (message.tileNumber != null && message.hasOwnProperty("tileNumber"))
            object.tileNumber = message.tileNumber;
        if (message.palette != null && message.hasOwnProperty("palette"))
            object.palette = options.bytes === String ? $util.base64.encode(message.palette, 0, message.palette.length) : options.bytes === Array ? Array.prototype.slice.call(message.palette) : message.palette;
        return object;
    };

    /**
     * Converts this GameBoyTile to JSON.
     * @function toJSON
     * @memberof GameBoyTile
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyTile.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyTile;
})();

$root.GameBoyRegisters = (function() {

    /**
     * Properties of a GameBoyRegisters.
     * @exports IGameBoyRegisters
     * @interface IGameBoyRegisters
     * @property {number|null} [A] GameBoyRegisters A
     * @property {number|null} [B] GameBoyRegisters B
     * @property {number|null} [C] GameBoyRegisters C
     * @property {number|null} [D] GameBoyRegisters D
     * @property {number|null} [E] GameBoyRegisters E
     * @property {number|null} [H] GameBoyRegisters H
     * @property {number|null} [L] GameBoyRegisters L
     * @property {IGameBoyFlagsRegister|null} [Flags] GameBoyRegisters Flags
     * @property {number|null} [programCounter] GameBoyRegisters programCounter
     * @property {number|null} [stackPointer] GameBoyRegisters stackPointer
     * @property {boolean|null} [interruptFlipFlop1] GameBoyRegisters interruptFlipFlop1
     * @property {boolean|null} [interruptFlipFlop2] GameBoyRegisters interruptFlipFlop2
     * @property {number|null} [interruptMode] GameBoyRegisters interruptMode
     */

    /**
     * Constructs a new GameBoyRegisters.
     * @exports GameBoyRegisters
     * @classdesc Represents a GameBoyRegisters.
     * @implements IGameBoyRegisters
     * @constructor
     * @param {IGameBoyRegisters=} [properties] Properties to set
     */
    function GameBoyRegisters(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyRegisters A.
     * @member {number} A
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.A = 0;

    /**
     * GameBoyRegisters B.
     * @member {number} B
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.B = 0;

    /**
     * GameBoyRegisters C.
     * @member {number} C
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.C = 0;

    /**
     * GameBoyRegisters D.
     * @member {number} D
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.D = 0;

    /**
     * GameBoyRegisters E.
     * @member {number} E
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.E = 0;

    /**
     * GameBoyRegisters H.
     * @member {number} H
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.H = 0;

    /**
     * GameBoyRegisters L.
     * @member {number} L
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.L = 0;

    /**
     * GameBoyRegisters Flags.
     * @member {IGameBoyFlagsRegister|null|undefined} Flags
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.Flags = null;

    /**
     * GameBoyRegisters programCounter.
     * @member {number} programCounter
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.programCounter = 0;

    /**
     * GameBoyRegisters stackPointer.
     * @member {number} stackPointer
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.stackPointer = 0;

    /**
     * GameBoyRegisters interruptFlipFlop1.
     * @member {boolean} interruptFlipFlop1
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.interruptFlipFlop1 = false;

    /**
     * GameBoyRegisters interruptFlipFlop2.
     * @member {boolean} interruptFlipFlop2
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.interruptFlipFlop2 = false;

    /**
     * GameBoyRegisters interruptMode.
     * @member {number} interruptMode
     * @memberof GameBoyRegisters
     * @instance
     */
    GameBoyRegisters.prototype.interruptMode = 0;

    /**
     * Creates a new GameBoyRegisters instance using the specified properties.
     * @function create
     * @memberof GameBoyRegisters
     * @static
     * @param {IGameBoyRegisters=} [properties] Properties to set
     * @returns {GameBoyRegisters} GameBoyRegisters instance
     */
    GameBoyRegisters.create = function create(properties) {
        return new GameBoyRegisters(properties);
    };

    /**
     * Encodes the specified GameBoyRegisters message. Does not implicitly {@link GameBoyRegisters.verify|verify} messages.
     * @function encode
     * @memberof GameBoyRegisters
     * @static
     * @param {IGameBoyRegisters} message GameBoyRegisters message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyRegisters.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.A != null && message.hasOwnProperty("A"))
            writer.uint32(/* id 1, wireType 0 =*/8).uint32(message.A);
        if (message.B != null && message.hasOwnProperty("B"))
            writer.uint32(/* id 2, wireType 0 =*/16).uint32(message.B);
        if (message.C != null && message.hasOwnProperty("C"))
            writer.uint32(/* id 3, wireType 0 =*/24).uint32(message.C);
        if (message.D != null && message.hasOwnProperty("D"))
            writer.uint32(/* id 4, wireType 0 =*/32).uint32(message.D);
        if (message.E != null && message.hasOwnProperty("E"))
            writer.uint32(/* id 5, wireType 0 =*/40).uint32(message.E);
        if (message.H != null && message.hasOwnProperty("H"))
            writer.uint32(/* id 6, wireType 0 =*/48).uint32(message.H);
        if (message.L != null && message.hasOwnProperty("L"))
            writer.uint32(/* id 7, wireType 0 =*/56).uint32(message.L);
        if (message.Flags != null && message.hasOwnProperty("Flags"))
            $root.GameBoyFlagsRegister.encode(message.Flags, writer.uint32(/* id 8, wireType 2 =*/66).fork()).ldelim();
        if (message.programCounter != null && message.hasOwnProperty("programCounter"))
            writer.uint32(/* id 9, wireType 0 =*/72).uint32(message.programCounter);
        if (message.stackPointer != null && message.hasOwnProperty("stackPointer"))
            writer.uint32(/* id 10, wireType 0 =*/80).uint32(message.stackPointer);
        if (message.interruptFlipFlop1 != null && message.hasOwnProperty("interruptFlipFlop1"))
            writer.uint32(/* id 11, wireType 0 =*/88).bool(message.interruptFlipFlop1);
        if (message.interruptFlipFlop2 != null && message.hasOwnProperty("interruptFlipFlop2"))
            writer.uint32(/* id 12, wireType 0 =*/96).bool(message.interruptFlipFlop2);
        if (message.interruptMode != null && message.hasOwnProperty("interruptMode"))
            writer.uint32(/* id 13, wireType 0 =*/104).uint32(message.interruptMode);
        return writer;
    };

    /**
     * Encodes the specified GameBoyRegisters message, length delimited. Does not implicitly {@link GameBoyRegisters.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyRegisters
     * @static
     * @param {IGameBoyRegisters} message GameBoyRegisters message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyRegisters.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyRegisters message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyRegisters
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyRegisters} GameBoyRegisters
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyRegisters.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyRegisters();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.A = reader.uint32();
                break;
            case 2:
                message.B = reader.uint32();
                break;
            case 3:
                message.C = reader.uint32();
                break;
            case 4:
                message.D = reader.uint32();
                break;
            case 5:
                message.E = reader.uint32();
                break;
            case 6:
                message.H = reader.uint32();
                break;
            case 7:
                message.L = reader.uint32();
                break;
            case 8:
                message.Flags = $root.GameBoyFlagsRegister.decode(reader, reader.uint32());
                break;
            case 9:
                message.programCounter = reader.uint32();
                break;
            case 10:
                message.stackPointer = reader.uint32();
                break;
            case 11:
                message.interruptFlipFlop1 = reader.bool();
                break;
            case 12:
                message.interruptFlipFlop2 = reader.bool();
                break;
            case 13:
                message.interruptMode = reader.uint32();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyRegisters message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyRegisters
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyRegisters} GameBoyRegisters
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyRegisters.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyRegisters message.
     * @function verify
     * @memberof GameBoyRegisters
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyRegisters.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.A != null && message.hasOwnProperty("A"))
            if (!$util.isInteger(message.A))
                return "A: integer expected";
        if (message.B != null && message.hasOwnProperty("B"))
            if (!$util.isInteger(message.B))
                return "B: integer expected";
        if (message.C != null && message.hasOwnProperty("C"))
            if (!$util.isInteger(message.C))
                return "C: integer expected";
        if (message.D != null && message.hasOwnProperty("D"))
            if (!$util.isInteger(message.D))
                return "D: integer expected";
        if (message.E != null && message.hasOwnProperty("E"))
            if (!$util.isInteger(message.E))
                return "E: integer expected";
        if (message.H != null && message.hasOwnProperty("H"))
            if (!$util.isInteger(message.H))
                return "H: integer expected";
        if (message.L != null && message.hasOwnProperty("L"))
            if (!$util.isInteger(message.L))
                return "L: integer expected";
        if (message.Flags != null && message.hasOwnProperty("Flags")) {
            var error = $root.GameBoyFlagsRegister.verify(message.Flags);
            if (error)
                return "Flags." + error;
        }
        if (message.programCounter != null && message.hasOwnProperty("programCounter"))
            if (!$util.isInteger(message.programCounter))
                return "programCounter: integer expected";
        if (message.stackPointer != null && message.hasOwnProperty("stackPointer"))
            if (!$util.isInteger(message.stackPointer))
                return "stackPointer: integer expected";
        if (message.interruptFlipFlop1 != null && message.hasOwnProperty("interruptFlipFlop1"))
            if (typeof message.interruptFlipFlop1 !== "boolean")
                return "interruptFlipFlop1: boolean expected";
        if (message.interruptFlipFlop2 != null && message.hasOwnProperty("interruptFlipFlop2"))
            if (typeof message.interruptFlipFlop2 !== "boolean")
                return "interruptFlipFlop2: boolean expected";
        if (message.interruptMode != null && message.hasOwnProperty("interruptMode"))
            if (!$util.isInteger(message.interruptMode))
                return "interruptMode: integer expected";
        return null;
    };

    /**
     * Creates a GameBoyRegisters message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyRegisters
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyRegisters} GameBoyRegisters
     */
    GameBoyRegisters.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyRegisters)
            return object;
        var message = new $root.GameBoyRegisters();
        if (object.A != null)
            message.A = object.A >>> 0;
        if (object.B != null)
            message.B = object.B >>> 0;
        if (object.C != null)
            message.C = object.C >>> 0;
        if (object.D != null)
            message.D = object.D >>> 0;
        if (object.E != null)
            message.E = object.E >>> 0;
        if (object.H != null)
            message.H = object.H >>> 0;
        if (object.L != null)
            message.L = object.L >>> 0;
        if (object.Flags != null) {
            if (typeof object.Flags !== "object")
                throw TypeError(".GameBoyRegisters.Flags: object expected");
            message.Flags = $root.GameBoyFlagsRegister.fromObject(object.Flags);
        }
        if (object.programCounter != null)
            message.programCounter = object.programCounter >>> 0;
        if (object.stackPointer != null)
            message.stackPointer = object.stackPointer >>> 0;
        if (object.interruptFlipFlop1 != null)
            message.interruptFlipFlop1 = Boolean(object.interruptFlipFlop1);
        if (object.interruptFlipFlop2 != null)
            message.interruptFlipFlop2 = Boolean(object.interruptFlipFlop2);
        if (object.interruptMode != null)
            message.interruptMode = object.interruptMode >>> 0;
        return message;
    };

    /**
     * Creates a plain object from a GameBoyRegisters message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyRegisters
     * @static
     * @param {GameBoyRegisters} message GameBoyRegisters
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyRegisters.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.A = 0;
            object.B = 0;
            object.C = 0;
            object.D = 0;
            object.E = 0;
            object.H = 0;
            object.L = 0;
            object.Flags = null;
            object.programCounter = 0;
            object.stackPointer = 0;
            object.interruptFlipFlop1 = false;
            object.interruptFlipFlop2 = false;
            object.interruptMode = 0;
        }
        if (message.A != null && message.hasOwnProperty("A"))
            object.A = message.A;
        if (message.B != null && message.hasOwnProperty("B"))
            object.B = message.B;
        if (message.C != null && message.hasOwnProperty("C"))
            object.C = message.C;
        if (message.D != null && message.hasOwnProperty("D"))
            object.D = message.D;
        if (message.E != null && message.hasOwnProperty("E"))
            object.E = message.E;
        if (message.H != null && message.hasOwnProperty("H"))
            object.H = message.H;
        if (message.L != null && message.hasOwnProperty("L"))
            object.L = message.L;
        if (message.Flags != null && message.hasOwnProperty("Flags"))
            object.Flags = $root.GameBoyFlagsRegister.toObject(message.Flags, options);
        if (message.programCounter != null && message.hasOwnProperty("programCounter"))
            object.programCounter = message.programCounter;
        if (message.stackPointer != null && message.hasOwnProperty("stackPointer"))
            object.stackPointer = message.stackPointer;
        if (message.interruptFlipFlop1 != null && message.hasOwnProperty("interruptFlipFlop1"))
            object.interruptFlipFlop1 = message.interruptFlipFlop1;
        if (message.interruptFlipFlop2 != null && message.hasOwnProperty("interruptFlipFlop2"))
            object.interruptFlipFlop2 = message.interruptFlipFlop2;
        if (message.interruptMode != null && message.hasOwnProperty("interruptMode"))
            object.interruptMode = message.interruptMode;
        return object;
    };

    /**
     * Converts this GameBoyRegisters to JSON.
     * @function toJSON
     * @memberof GameBoyRegisters
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyRegisters.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyRegisters;
})();

$root.GameBoyFlagsRegister = (function() {

    /**
     * Properties of a GameBoyFlagsRegister.
     * @exports IGameBoyFlagsRegister
     * @interface IGameBoyFlagsRegister
     * @property {boolean|null} [zero] GameBoyFlagsRegister zero
     * @property {boolean|null} [halfCarry] GameBoyFlagsRegister halfCarry
     * @property {boolean|null} [subtract] GameBoyFlagsRegister subtract
     * @property {boolean|null} [carry] GameBoyFlagsRegister carry
     */

    /**
     * Constructs a new GameBoyFlagsRegister.
     * @exports GameBoyFlagsRegister
     * @classdesc Represents a GameBoyFlagsRegister.
     * @implements IGameBoyFlagsRegister
     * @constructor
     * @param {IGameBoyFlagsRegister=} [properties] Properties to set
     */
    function GameBoyFlagsRegister(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyFlagsRegister zero.
     * @member {boolean} zero
     * @memberof GameBoyFlagsRegister
     * @instance
     */
    GameBoyFlagsRegister.prototype.zero = false;

    /**
     * GameBoyFlagsRegister halfCarry.
     * @member {boolean} halfCarry
     * @memberof GameBoyFlagsRegister
     * @instance
     */
    GameBoyFlagsRegister.prototype.halfCarry = false;

    /**
     * GameBoyFlagsRegister subtract.
     * @member {boolean} subtract
     * @memberof GameBoyFlagsRegister
     * @instance
     */
    GameBoyFlagsRegister.prototype.subtract = false;

    /**
     * GameBoyFlagsRegister carry.
     * @member {boolean} carry
     * @memberof GameBoyFlagsRegister
     * @instance
     */
    GameBoyFlagsRegister.prototype.carry = false;

    /**
     * Creates a new GameBoyFlagsRegister instance using the specified properties.
     * @function create
     * @memberof GameBoyFlagsRegister
     * @static
     * @param {IGameBoyFlagsRegister=} [properties] Properties to set
     * @returns {GameBoyFlagsRegister} GameBoyFlagsRegister instance
     */
    GameBoyFlagsRegister.create = function create(properties) {
        return new GameBoyFlagsRegister(properties);
    };

    /**
     * Encodes the specified GameBoyFlagsRegister message. Does not implicitly {@link GameBoyFlagsRegister.verify|verify} messages.
     * @function encode
     * @memberof GameBoyFlagsRegister
     * @static
     * @param {IGameBoyFlagsRegister} message GameBoyFlagsRegister message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyFlagsRegister.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.zero != null && message.hasOwnProperty("zero"))
            writer.uint32(/* id 1, wireType 0 =*/8).bool(message.zero);
        if (message.halfCarry != null && message.hasOwnProperty("halfCarry"))
            writer.uint32(/* id 2, wireType 0 =*/16).bool(message.halfCarry);
        if (message.subtract != null && message.hasOwnProperty("subtract"))
            writer.uint32(/* id 3, wireType 0 =*/24).bool(message.subtract);
        if (message.carry != null && message.hasOwnProperty("carry"))
            writer.uint32(/* id 4, wireType 0 =*/32).bool(message.carry);
        return writer;
    };

    /**
     * Encodes the specified GameBoyFlagsRegister message, length delimited. Does not implicitly {@link GameBoyFlagsRegister.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyFlagsRegister
     * @static
     * @param {IGameBoyFlagsRegister} message GameBoyFlagsRegister message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyFlagsRegister.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyFlagsRegister message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyFlagsRegister
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyFlagsRegister} GameBoyFlagsRegister
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyFlagsRegister.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyFlagsRegister();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.zero = reader.bool();
                break;
            case 2:
                message.halfCarry = reader.bool();
                break;
            case 3:
                message.subtract = reader.bool();
                break;
            case 4:
                message.carry = reader.bool();
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyFlagsRegister message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyFlagsRegister
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyFlagsRegister} GameBoyFlagsRegister
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyFlagsRegister.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyFlagsRegister message.
     * @function verify
     * @memberof GameBoyFlagsRegister
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyFlagsRegister.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.zero != null && message.hasOwnProperty("zero"))
            if (typeof message.zero !== "boolean")
                return "zero: boolean expected";
        if (message.halfCarry != null && message.hasOwnProperty("halfCarry"))
            if (typeof message.halfCarry !== "boolean")
                return "halfCarry: boolean expected";
        if (message.subtract != null && message.hasOwnProperty("subtract"))
            if (typeof message.subtract !== "boolean")
                return "subtract: boolean expected";
        if (message.carry != null && message.hasOwnProperty("carry"))
            if (typeof message.carry !== "boolean")
                return "carry: boolean expected";
        return null;
    };

    /**
     * Creates a GameBoyFlagsRegister message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyFlagsRegister
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyFlagsRegister} GameBoyFlagsRegister
     */
    GameBoyFlagsRegister.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyFlagsRegister)
            return object;
        var message = new $root.GameBoyFlagsRegister();
        if (object.zero != null)
            message.zero = Boolean(object.zero);
        if (object.halfCarry != null)
            message.halfCarry = Boolean(object.halfCarry);
        if (object.subtract != null)
            message.subtract = Boolean(object.subtract);
        if (object.carry != null)
            message.carry = Boolean(object.carry);
        return message;
    };

    /**
     * Creates a plain object from a GameBoyFlagsRegister message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyFlagsRegister
     * @static
     * @param {GameBoyFlagsRegister} message GameBoyFlagsRegister
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyFlagsRegister.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.zero = false;
            object.halfCarry = false;
            object.subtract = false;
            object.carry = false;
        }
        if (message.zero != null && message.hasOwnProperty("zero"))
            object.zero = message.zero;
        if (message.halfCarry != null && message.hasOwnProperty("halfCarry"))
            object.halfCarry = message.halfCarry;
        if (message.subtract != null && message.hasOwnProperty("subtract"))
            object.subtract = message.subtract;
        if (message.carry != null && message.hasOwnProperty("carry"))
            object.carry = message.carry;
        return object;
    };

    /**
     * Converts this GameBoyFlagsRegister to JSON.
     * @function toJSON
     * @memberof GameBoyFlagsRegister
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyFlagsRegister.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyFlagsRegister;
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

$root.GameBoyDebuggerEvent = (function() {

    /**
     * Properties of a GameBoyDebuggerEvent.
     * @exports IGameBoyDebuggerEvent
     * @interface IGameBoyDebuggerEvent
     * @property {IGameBoyRegisters|null} [registers] GameBoyDebuggerEvent registers
     * @property {Uint8Array|null} [memory] GameBoyDebuggerEvent memory
     * @property {IGameBoyGpuState|null} [gpuState] GameBoyDebuggerEvent gpuState
     * @property {IGameBoyInstructionBlock|null} [instructionBlock] GameBoyDebuggerEvent instructionBlock
     */

    /**
     * Constructs a new GameBoyDebuggerEvent.
     * @exports GameBoyDebuggerEvent
     * @classdesc Represents a GameBoyDebuggerEvent.
     * @implements IGameBoyDebuggerEvent
     * @constructor
     * @param {IGameBoyDebuggerEvent=} [properties] Properties to set
     */
    function GameBoyDebuggerEvent(properties) {
        if (properties)
            for (var keys = Object.keys(properties), i = 0; i < keys.length; ++i)
                if (properties[keys[i]] != null)
                    this[keys[i]] = properties[keys[i]];
    }

    /**
     * GameBoyDebuggerEvent registers.
     * @member {IGameBoyRegisters|null|undefined} registers
     * @memberof GameBoyDebuggerEvent
     * @instance
     */
    GameBoyDebuggerEvent.prototype.registers = null;

    /**
     * GameBoyDebuggerEvent memory.
     * @member {Uint8Array} memory
     * @memberof GameBoyDebuggerEvent
     * @instance
     */
    GameBoyDebuggerEvent.prototype.memory = $util.newBuffer([]);

    /**
     * GameBoyDebuggerEvent gpuState.
     * @member {IGameBoyGpuState|null|undefined} gpuState
     * @memberof GameBoyDebuggerEvent
     * @instance
     */
    GameBoyDebuggerEvent.prototype.gpuState = null;

    /**
     * GameBoyDebuggerEvent instructionBlock.
     * @member {IGameBoyInstructionBlock|null|undefined} instructionBlock
     * @memberof GameBoyDebuggerEvent
     * @instance
     */
    GameBoyDebuggerEvent.prototype.instructionBlock = null;

    /**
     * Creates a new GameBoyDebuggerEvent instance using the specified properties.
     * @function create
     * @memberof GameBoyDebuggerEvent
     * @static
     * @param {IGameBoyDebuggerEvent=} [properties] Properties to set
     * @returns {GameBoyDebuggerEvent} GameBoyDebuggerEvent instance
     */
    GameBoyDebuggerEvent.create = function create(properties) {
        return new GameBoyDebuggerEvent(properties);
    };

    /**
     * Encodes the specified GameBoyDebuggerEvent message. Does not implicitly {@link GameBoyDebuggerEvent.verify|verify} messages.
     * @function encode
     * @memberof GameBoyDebuggerEvent
     * @static
     * @param {IGameBoyDebuggerEvent} message GameBoyDebuggerEvent message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyDebuggerEvent.encode = function encode(message, writer) {
        if (!writer)
            writer = $Writer.create();
        if (message.registers != null && message.hasOwnProperty("registers"))
            $root.GameBoyRegisters.encode(message.registers, writer.uint32(/* id 1, wireType 2 =*/10).fork()).ldelim();
        if (message.memory != null && message.hasOwnProperty("memory"))
            writer.uint32(/* id 2, wireType 2 =*/18).bytes(message.memory);
        if (message.gpuState != null && message.hasOwnProperty("gpuState"))
            $root.GameBoyGpuState.encode(message.gpuState, writer.uint32(/* id 3, wireType 2 =*/26).fork()).ldelim();
        if (message.instructionBlock != null && message.hasOwnProperty("instructionBlock"))
            $root.GameBoyInstructionBlock.encode(message.instructionBlock, writer.uint32(/* id 4, wireType 2 =*/34).fork()).ldelim();
        return writer;
    };

    /**
     * Encodes the specified GameBoyDebuggerEvent message, length delimited. Does not implicitly {@link GameBoyDebuggerEvent.verify|verify} messages.
     * @function encodeDelimited
     * @memberof GameBoyDebuggerEvent
     * @static
     * @param {IGameBoyDebuggerEvent} message GameBoyDebuggerEvent message or plain object to encode
     * @param {$protobuf.Writer} [writer] Writer to encode to
     * @returns {$protobuf.Writer} Writer
     */
    GameBoyDebuggerEvent.encodeDelimited = function encodeDelimited(message, writer) {
        return this.encode(message, writer).ldelim();
    };

    /**
     * Decodes a GameBoyDebuggerEvent message from the specified reader or buffer.
     * @function decode
     * @memberof GameBoyDebuggerEvent
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @param {number} [length] Message length if known beforehand
     * @returns {GameBoyDebuggerEvent} GameBoyDebuggerEvent
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyDebuggerEvent.decode = function decode(reader, length) {
        if (!(reader instanceof $Reader))
            reader = $Reader.create(reader);
        var end = length === undefined ? reader.len : reader.pos + length, message = new $root.GameBoyDebuggerEvent();
        while (reader.pos < end) {
            var tag = reader.uint32();
            switch (tag >>> 3) {
            case 1:
                message.registers = $root.GameBoyRegisters.decode(reader, reader.uint32());
                break;
            case 2:
                message.memory = reader.bytes();
                break;
            case 3:
                message.gpuState = $root.GameBoyGpuState.decode(reader, reader.uint32());
                break;
            case 4:
                message.instructionBlock = $root.GameBoyInstructionBlock.decode(reader, reader.uint32());
                break;
            default:
                reader.skipType(tag & 7);
                break;
            }
        }
        return message;
    };

    /**
     * Decodes a GameBoyDebuggerEvent message from the specified reader or buffer, length delimited.
     * @function decodeDelimited
     * @memberof GameBoyDebuggerEvent
     * @static
     * @param {$protobuf.Reader|Uint8Array} reader Reader or buffer to decode from
     * @returns {GameBoyDebuggerEvent} GameBoyDebuggerEvent
     * @throws {Error} If the payload is not a reader or valid buffer
     * @throws {$protobuf.util.ProtocolError} If required fields are missing
     */
    GameBoyDebuggerEvent.decodeDelimited = function decodeDelimited(reader) {
        if (!(reader instanceof $Reader))
            reader = new $Reader(reader);
        return this.decode(reader, reader.uint32());
    };

    /**
     * Verifies a GameBoyDebuggerEvent message.
     * @function verify
     * @memberof GameBoyDebuggerEvent
     * @static
     * @param {Object.<string,*>} message Plain object to verify
     * @returns {string|null} `null` if valid, otherwise the reason why it is not
     */
    GameBoyDebuggerEvent.verify = function verify(message) {
        if (typeof message !== "object" || message === null)
            return "object expected";
        if (message.registers != null && message.hasOwnProperty("registers")) {
            var error = $root.GameBoyRegisters.verify(message.registers);
            if (error)
                return "registers." + error;
        }
        if (message.memory != null && message.hasOwnProperty("memory"))
            if (!(message.memory && typeof message.memory.length === "number" || $util.isString(message.memory)))
                return "memory: buffer expected";
        if (message.gpuState != null && message.hasOwnProperty("gpuState")) {
            var error = $root.GameBoyGpuState.verify(message.gpuState);
            if (error)
                return "gpuState." + error;
        }
        if (message.instructionBlock != null && message.hasOwnProperty("instructionBlock")) {
            var error = $root.GameBoyInstructionBlock.verify(message.instructionBlock);
            if (error)
                return "instructionBlock." + error;
        }
        return null;
    };

    /**
     * Creates a GameBoyDebuggerEvent message from a plain object. Also converts values to their respective internal types.
     * @function fromObject
     * @memberof GameBoyDebuggerEvent
     * @static
     * @param {Object.<string,*>} object Plain object
     * @returns {GameBoyDebuggerEvent} GameBoyDebuggerEvent
     */
    GameBoyDebuggerEvent.fromObject = function fromObject(object) {
        if (object instanceof $root.GameBoyDebuggerEvent)
            return object;
        var message = new $root.GameBoyDebuggerEvent();
        if (object.registers != null) {
            if (typeof object.registers !== "object")
                throw TypeError(".GameBoyDebuggerEvent.registers: object expected");
            message.registers = $root.GameBoyRegisters.fromObject(object.registers);
        }
        if (object.memory != null)
            if (typeof object.memory === "string")
                $util.base64.decode(object.memory, message.memory = $util.newBuffer($util.base64.length(object.memory)), 0);
            else if (object.memory.length)
                message.memory = object.memory;
        if (object.gpuState != null) {
            if (typeof object.gpuState !== "object")
                throw TypeError(".GameBoyDebuggerEvent.gpuState: object expected");
            message.gpuState = $root.GameBoyGpuState.fromObject(object.gpuState);
        }
        if (object.instructionBlock != null) {
            if (typeof object.instructionBlock !== "object")
                throw TypeError(".GameBoyDebuggerEvent.instructionBlock: object expected");
            message.instructionBlock = $root.GameBoyInstructionBlock.fromObject(object.instructionBlock);
        }
        return message;
    };

    /**
     * Creates a plain object from a GameBoyDebuggerEvent message. Also converts values to other types if specified.
     * @function toObject
     * @memberof GameBoyDebuggerEvent
     * @static
     * @param {GameBoyDebuggerEvent} message GameBoyDebuggerEvent
     * @param {$protobuf.IConversionOptions} [options] Conversion options
     * @returns {Object.<string,*>} Plain object
     */
    GameBoyDebuggerEvent.toObject = function toObject(message, options) {
        if (!options)
            options = {};
        var object = {};
        if (options.defaults) {
            object.registers = null;
            object.memory = options.bytes === String ? "" : [];
            object.gpuState = null;
            object.instructionBlock = null;
        }
        if (message.registers != null && message.hasOwnProperty("registers"))
            object.registers = $root.GameBoyRegisters.toObject(message.registers, options);
        if (message.memory != null && message.hasOwnProperty("memory"))
            object.memory = options.bytes === String ? $util.base64.encode(message.memory, 0, message.memory.length) : options.bytes === Array ? Array.prototype.slice.call(message.memory) : message.memory;
        if (message.gpuState != null && message.hasOwnProperty("gpuState"))
            object.gpuState = $root.GameBoyGpuState.toObject(message.gpuState, options);
        if (message.instructionBlock != null && message.hasOwnProperty("instructionBlock"))
            object.instructionBlock = $root.GameBoyInstructionBlock.toObject(message.instructionBlock, options);
        return object;
    };

    /**
     * Converts this GameBoyDebuggerEvent to JSON.
     * @function toJSON
     * @memberof GameBoyDebuggerEvent
     * @instance
     * @returns {Object.<string,*>} JSON object
     */
    GameBoyDebuggerEvent.prototype.toJSON = function toJSON() {
        return this.constructor.toObject(this, $protobuf.util.toJSONOptions);
    };

    return GameBoyDebuggerEvent;
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
     * @property {IGameBoyDebuggerEvent|null} ["debugger"] GameBoyEvent debugger
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

    /**
     * GameBoyEvent debugger.
     * @member {IGameBoyDebuggerEvent|null|undefined} debugger
     * @memberof GameBoyEvent
     * @instance
     */
    GameBoyEvent.prototype["debugger"] = null;

    // OneOf field names bound to virtual getters and setters
    var $oneOfFields;

    /**
     * GameBoyEvent value.
     * @member {"frame"|"publishedMessage"|"error"|"state"|"debugger"|undefined} value
     * @memberof GameBoyEvent
     * @instance
     */
    Object.defineProperty(GameBoyEvent.prototype, "value", {
        get: $util.oneOfGetter($oneOfFields = ["frame", "publishedMessage", "error", "state", "debugger"]),
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
        if (message["debugger"] != null && message.hasOwnProperty("debugger"))
            $root.GameBoyDebuggerEvent.encode(message["debugger"], writer.uint32(/* id 5, wireType 2 =*/42).fork()).ldelim();
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
            case 5:
                message["debugger"] = $root.GameBoyDebuggerEvent.decode(reader, reader.uint32());
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
        if (message["debugger"] != null && message.hasOwnProperty("debugger")) {
            if (properties.value === 1)
                return "value: multiple values";
            properties.value = 1;
            {
                var error = $root.GameBoyDebuggerEvent.verify(message["debugger"]);
                if (error)
                    return "debugger." + error;
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
        if (object["debugger"] != null) {
            if (typeof object["debugger"] !== "object")
                throw TypeError(".GameBoyEvent.debugger: object expected");
            message["debugger"] = $root.GameBoyDebuggerEvent.fromObject(object["debugger"]);
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
        if (message["debugger"] != null && message.hasOwnProperty("debugger")) {
            object["debugger"] = $root.GameBoyDebuggerEvent.toObject(message["debugger"], options);
            if (options.oneofs)
                object.value = "debugger";
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
