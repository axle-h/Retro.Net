using Retro.Net.Memory;

namespace GameBoy.Net.Devices
{
    /// <summary>
    /// GameBoy hardware registers.
    /// </summary>
    /// <seealso cref="Retro.Net.Memory.IReadableAddressSegment" />
    /// <seealso cref="Retro.Net.Memory.IWriteableAddressSegment" />
    public interface IHardwareRegisters : IReadWriteAddressSegment
    {
        /// <summary>
        /// Gets the joy pad.
        /// </summary>
        /// <value>
        /// The joy pad.
        /// </value>
        IJoyPad JoyPad { get; }

        /// <summary>
        /// Gets the serial port.
        /// </summary>
        /// <value>
        /// The serial port.
        /// </value>
        ISerialPort SerialPort { get; }
    }
}