using Retro.Net.Memory.Interfaces;

namespace GameBoy.Net.Devices.Interfaces
{
    /// <summary>
    /// GameBoy hardware registers.
    /// </summary>
    /// <seealso cref="IReadableAddressSegment" />
    /// <seealso cref="IWriteableAddressSegment" />
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