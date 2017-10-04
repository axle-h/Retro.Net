using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameBoy.Net.Registers.Interfaces;
using Retro.Net.Memory;

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
        /// Reads a word from this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public ushort ReadWord(ushort address)
        {
            var bytes = ReadBytes(address, 2);
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        /// Reads bytes from this address segment into a new buffer.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public byte[] ReadBytes(ushort address, int length)
        {
            var bytes = new byte[length];
            ReadBytes(address, bytes);
            return bytes;
        }

        /// <summary>
        /// Reads bytes from this address segment into the specified buffer.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="buffer">The buffer.</param>
        public void ReadBytes(ushort address, byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = ReadByte((ushort) (address + i));
            }
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
        /// Writes a word to this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="word">The word.</param>
        public void WriteWord(ushort address, ushort word)
        {
            var bytes = BitConverter.GetBytes(word);
            WriteBytes(address, bytes);
        }

        /// <summary>
        /// Writes bytes to this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="values">The values.</param>
        public void WriteBytes(ushort address, byte[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                WriteByte((ushort) (address + i), values[i]);
            }
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