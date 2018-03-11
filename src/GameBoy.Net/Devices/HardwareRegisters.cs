using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameBoy.Net.Devices.Interfaces;
using GameBoy.Net.Registers.Interfaces;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;

namespace GameBoy.Net.Devices
{
    /// <summary>
    /// GameBoy hardware registers.
    /// </summary>
    /// <seealso cref="IHardwareRegisters" />
    public class HardwareRegisters : IHardwareRegisters
    {
        private const ushort Address = 0xff00;
        private const ushort Length = 0x80;
        private readonly IDictionary<ushort, IRegister> _registers;
        private readonly HashSet<ushort> _missingRegisters;

        /// <summary>
        /// Initializes a new instance of the <see cref="HardwareRegisters"/> class.
        /// </summary>
        /// <param name="registers">The registers.</param>
        /// <param name="joyPad">The joy pad.</param>
        /// <param name="serialPort">The serial port.</param>
        /// <param name="gpuRegisters">The gpu registers.</param>
        /// <param name="timerRegisters">The timer registers.</param>
        public HardwareRegisters(IEnumerable<IRegister> registers,
            IJoyPadRegister joyPad,
            ISerialPortRegister serialPort,
            IGpuRegisters gpuRegisters,
            ITimerRegisters timerRegisters)
        {
            JoyPad = joyPad;
            SerialPort = serialPort;
            _registers =
                registers.Concat(new[]
                                 {
                                     joyPad, serialPort, serialPort.SerialData, gpuRegisters.ScrollXRegister,
                                     gpuRegisters.ScrollYRegister, gpuRegisters.CurrentScanlineRegister,
                                     gpuRegisters.LcdControlRegister, gpuRegisters.LcdMonochromePaletteRegister,
                                     gpuRegisters.LcdStatusRegister, gpuRegisters.WindowXPositionRegister,
                                     gpuRegisters.WindowYPositionRegister, timerRegisters.TimerControlRegister,
                                     timerRegisters.TimerCounterRegister, timerRegisters.TimerModuloRegister
                                 })
                         .ToDictionary(x => (ushort) (x.Address - Address));
            _missingRegisters = new HashSet<ushort>();
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public MemoryBankType Type => MemoryBankType.Peripheral;

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        ushort IAddressSegment.Address => Address;

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        ushort IAddressSegment.Length => Length;

        /// <summary>
        /// Reads a byte from this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public byte ReadByte(ushort address)
        {
            // TODO: remove check once all registers implemented.
            if (_registers.ContainsKey(address))
            {
                var value = _registers[address].Register;
                //Debug.WriteLine($"Read 0x{address + Address:x4} = 0x{value:x2}");
                return value;
            }

            if (!_missingRegisters.Contains(address))
            {
                Debug.WriteLine("Missing Hardware Register: 0x" + (address + Address).ToString("x4"));
                _missingRegisters.Add(address);
            }
            
            return 0x00;
        }

        /// <summary>
        /// Reads bytes from this address segment into the specified buffer.
        /// This does not wrap the segment.
        /// i.e. if address + count is larger than the segment length then this will return a value less than count.
        /// </summary>
        /// <param name="address">The segment address to start reading from.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset to start writing to in the buffer.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>
        /// The number of bytes read into the buffer.
        /// </returns>
        public int ReadBytes(ushort address, byte[] buffer, int offset, int count)
        {
            count = Math.Min(count, Length - address);
            for (var i = 0; i < count; i++)
            {
                buffer[i + offset] = ReadByte((ushort) (address + i));
            }
            return count;
        }
        
        /// <summary>
        /// Writes a byte to this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="value">The value.</param>
        public void WriteByte(ushort address, byte value)
        {
            //Debug.WriteLine($"Write 0x{address + Address:x4} = 0x{value:x2}");

            // TODO: remove check once all registers implemented.
            if (_registers.ContainsKey(address))
            {
                _registers[address].Register = value;
            }
            else if (!_missingRegisters.Contains(address))
            {
                Debug.WriteLine("Missing Hardware Register: 0x" + (address + Address).ToString("x4"));
                _missingRegisters.Add(address);
            }
        }
        
        /// <summary>
        /// Writes the bytes in the specified buffer to this address segment.
        /// This does not wrap the segment.
        /// i.e. if address + count is larger than the segment length then this will return a value less than count.
        /// </summary>
        /// <param name="address">The segment address to start writing to.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset to start reading from in the buffer.</param>
        /// <param name="count">The number of bytes to write.</param>
        /// <returns>
        /// The number of bytes written into this segment.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public int WriteBytes(ushort address, byte[] buffer, int offset, int count)
        {
            count = Math.Min(count, Length - address);
            for (var i = 0; i < count; i++)
            {
                WriteByte((ushort)(address + i), buffer[i + offset]);
            }
            return count;
        }

        /// <summary>
        /// Gets the joy pad.
        /// </summary>
        /// <value>
        /// The joy pad.
        /// </value>
        public IJoyPad JoyPad { get; }

        /// <summary>
        /// Gets the serial port.
        /// </summary>
        /// <value>
        /// The serial port.
        /// </value>
        public ISerialPort SerialPort { get; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Type}: 0x{Address:x4} - 0x{Address + Length - 1:x4}";
    }
}