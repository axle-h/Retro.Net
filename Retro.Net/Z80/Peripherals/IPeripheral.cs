using System;

namespace Retro.Net.Z80.Peripherals
{
    /// <summary>
    /// A Z80 peripheral.
    /// </summary>
    public interface IPeripheral : IDisposable
    {
        /// <summary>
        /// Sends the specified signal to the peripheral.
        /// </summary>
        /// <param name="signal">The signal.</param>
        void Signal(ControlSignal signal);
    }
}