using System;

namespace Retro.Net.Z80.Peripherals
{
    /// <summary>
    /// IO peripheral on port 1 that determines whether a key is being pressed on the system console.
    /// </summary>
    /// <seealso cref="IIoPeripheral" />
    public class SystemConsoleStatus : IIoPeripheral
    {
        /// <summary>
        /// The IO port of this peripheral
        /// </summary>
        public byte Port => 1;

        /// <summary>
        /// Read the next byte from this IO device
        /// </summary>
        /// <param name="addressMsb">The most significant byte of the address bus (the LSB is used as the IO port)</param>
        /// <returns></returns>
        public byte ReadByte(byte addressMsb)
        {
            return (byte) (Console.KeyAvailable ? 1 : 0);
        }

        /// <summary>
        /// Write a byte to this device
        /// </summary>
        /// <param name="addressMsb">The most significant byte of the address bus (the LSB is used as the IO port)</param>
        /// <param name="value">The byte to write</param>
        public void WriteByte(byte addressMsb, byte value)
        {
            // Nothing to write
        }

        /// <summary>
        /// Sends the specified signal to the peripheral.
        /// </summary>
        /// <param name="signal">The signal.</param>
        public void Signal(ControlSignal signal)
        {
            // Don't listen.
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}