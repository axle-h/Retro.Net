using System.Collections.Generic;
using System.Linq;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Z80.Peripherals
{
    /// <summary>
    /// The peripheral manager.
    /// </summary>
    /// <seealso cref="IPeripheralManager" />
    public class PeripheralManager : IPeripheralManager
    {
        private readonly IDictionary<byte, IIoPeripheral> _ioPeripherals;
        private readonly IMemoryMappedPeripheral[] _memoryMappedPeripherals;
        private readonly ICollection<IPeripheral> _peripherals;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeripheralManager"/> class.
        /// </summary>
        /// <param name="peripherals">The peripherals.</param>
        public PeripheralManager(ICollection<IPeripheral> peripherals)
        {
            _peripherals = peripherals;
            _ioPeripherals = peripherals.OfType<IIoPeripheral>().ToDictionary(x => x.Port);
            _memoryMappedPeripherals = peripherals.OfType<IMemoryMappedPeripheral>().ToArray();
        }

        /// <summary>
        /// Read the next byte from the peripheral at IO port
        /// </summary>
        /// <param name="port">The port of the device to read from</param>
        /// <param name="addressMsb">The most significant byte of the address bus (the LSB is used as the IO port)</param>
        /// <returns></returns>
        public byte ReadByteFromPort(byte port, byte addressMsb)
        {
            return _ioPeripherals.ContainsKey(port) ? _ioPeripherals[port].ReadByte(addressMsb) : (byte) 0;
        }

        /// <summary>
        /// Write a byte to the peripheral at IO port
        /// </summary>
        /// <param name="port">The port of the device to write to</param>
        /// <param name="addressMsb">The most significant byte of the address bus (the LSB is used as the IO port)</param>
        /// <param name="value">The byte to write</param>
        public void WriteByteToPort(byte port, byte addressMsb, byte value)
        {
            if (!_ioPeripherals.ContainsKey(port))
            {
                return;
            }

            _ioPeripherals[port].WriteByte(addressMsb, value);
        }

        /// <summary>
        /// Signal all peripherals
        /// </summary>
        /// <param name="signal"></param>
        public void Signal(ControlSignal signal)
        {
            foreach (var peripheral in _peripherals)
            {
                peripheral.Signal(signal);
            }
        }

        /// <summary>
        /// Gets all memory mapped peripherals.
        /// </summary>
        public IEnumerable<IAddressSegment> MemoryMap => _memoryMappedPeripherals.SelectMany(x => x.AddressSegments);

        /// <summary>
        /// Retrieve peripheral of specified type.
        /// </summary>
        /// <typeparam name="TPeripheral"></typeparam>
        /// <returns></returns>
        public TPeripheral PeripheralOfType<TPeripheral>() where TPeripheral : IPeripheral
            => _peripherals.OfType<TPeripheral>().FirstOrDefault();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (var peripheral in _peripherals)
            {
                peripheral.Dispose();
            }
        }
    }
}