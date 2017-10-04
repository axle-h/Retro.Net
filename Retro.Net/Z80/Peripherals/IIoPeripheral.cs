namespace Retro.Net.Z80.Peripherals
{
    /// <summary>
    /// A peripheral called through the 8080's IO interface.
    /// IO ports on the 8080 are produced form the LSB of the address bus. There are 256 possible IO ports.
    /// The data bus on the 8080 is 8 bits wide so all communication is done byte-wise
    /// </summary>
    public interface IIoPeripheral : IPeripheral
    {
        /// <summary>
        /// The IO port of this peripheral
        /// </summary>
        byte Port { get; }

        /// <summary>
        /// Read the next byte from this IO device
        /// </summary>
        /// <param name="addressMsb">The most significant byte of the address bus (the LSB is used as the IO port)</param>
        /// <returns></returns>
        byte ReadByte(byte addressMsb);

        /// <summary>
        /// Write a byte to this device
        /// </summary>
        /// <param name="addressMsb">The most significant byte of the address bus (the LSB is used as the IO port)</param>
        /// <param name="value">The byte to write</param>
        void WriteByte(byte addressMsb, byte value);
    }
}