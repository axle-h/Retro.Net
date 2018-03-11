using System;
using System.Collections.Generic;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Z80.Peripherals
{
    /// <summary>
    /// The peripheral manager.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IPeripheralManager : IDisposable
    {
        /// <summary>
        /// Gets all memory mapped peripherals.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IAddressSegment> MemoryMap { get; }

        /// <summary>
        /// Read the next byte from the peripheral at IO port
        /// </summary>
        /// <param name="port">The port of the device to read from</param>
        /// <param name="addressMsb">The most significant byte of the address bus (the LSB is used as the IO port)</param>
        /// <returns></returns>
        byte ReadByteFromPort(byte port, byte addressMsb);

        /// <summary>
        /// Write a byte to the peripheral at IO port
        /// </summary>
        /// <param name="port">The port of the device to write to</param>
        /// <param name="addressMsb">The most significant byte of the address bus (the LSB is used as the IO port)</param>
        /// <param name="value">The byte to write</param>
        void WriteByteToPort(byte port, byte addressMsb, byte value);

        /// <summary>
        /// Signal all peripherals
        /// </summary>
        void Signal(ControlSignal signal);

        /// <summary>
        /// Retrieve peripheral of specified type.
        /// </summary>
        /// <typeparam name="TPeripheral"></typeparam>
        /// <returns></returns>
        TPeripheral PeripheralOfType<TPeripheral>() where TPeripheral : IPeripheral;
    }
}