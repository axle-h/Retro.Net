namespace GameBoy.Net.Devices.Interfaces
{
    /// <summary>
    /// Serial Data Transfer (Link Cable).
    /// </summary>
    public interface ISerialPort
    {
        /// <summary>
        /// Connect to external serial port.
        /// </summary>
        /// <param name="serialPort"></param>
        void Connect(ISerialPort serialPort);

        /// <summary>
        /// Disconnect from external serial port.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Transfer a byte to external serial port and read a byte from external serial port.
        /// Only the serial port running the GB internal clock should be calling this method.
        /// </summary>
        /// <param name="value">The value to write to this port.</param>
        /// <returns>The value to transfer to the connected port.</returns>
        byte Transfer(byte value);
    }
}