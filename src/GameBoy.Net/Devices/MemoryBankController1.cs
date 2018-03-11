using System;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;

namespace GameBoy.Net.Devices
{
    /// <summary>
    /// GameBoy MBC1 implementation.
    /// MBC1 (max 2MByte ROM and/or 32KByte RAM)
    /// This is the first MBC chip for the gameboy.
    /// Any newer MBC chips are working similar, so that is relative easy to upgrade a program from one MBC chip to another - or even to make it compatible to several different types of MBCs.
    /// Note that the memory in range 0000-7FFF is used for both reading from ROM, and for writing to the MBCs Control Registers.
    /// </summary>
    public class MemoryBankController1 : IMemoryBankController
    {
        private const ushort Address = 0x0000;
        private const ushort Length = 0x8000;

        private const ushort RamEnableAddress = 0x0000;
        private const ushort RomBankNumberAddress = 0x2000;
        private const ushort RamBankNumberAddress = 0x4000;
        private const ushort ModeSelectAddress = 0x6000;

        private bool _ramBankingMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryBankController1"/> class.
        /// </summary>
        public MemoryBankController1()
        {
            _ramBankingMode = false;
            RomBankNumber = 1;
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
        /// Writes a byte to this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="value">The value.</param>
        public void WriteByte(ushort address, byte value)
        {
            if (address < RomBankNumberAddress)
            {
                // RAM Enable
                RamEnable = (value & 0xf) == 0xa;
                OnMemoryBankControllerEvent(MemoryBankControllerEventTarget.RamEnable);
                return;
            }

            if (address < RamBankNumberAddress)
            {
                // ROM Bank Number
                RomBankNumber &= 0xe0; // Clear 0x17
                RomBankNumber |= GetRomBankNumber(value);

                OnMemoryBankControllerEvent(MemoryBankControllerEventTarget.RomBankSwitch);
                return;
            }

            if (address < ModeSelectAddress)
            {
                // RAM Bank Number
                // TODO this should select the low 2 bits of the ROM bank number when romBankingMode is false
                if (_ramBankingMode)
                {
                    RamBankNumber = (byte) (value & 0x3);
                    OnMemoryBankControllerEvent(MemoryBankControllerEventTarget.RamBankSwitch);
                }
                return;
            }

            // ROM / RAM Mode Select
            _ramBankingMode = (value & 0x1) == 0x1;
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
        public int WriteBytes(ushort address, byte[] buffer, int offset, int count)
        {
            count = Math.Min(count, Length - address);
            for (var i = 0; i < count; i++)
            {
                WriteByte((ushort) (address + i), buffer[i + offset]);
            }
            return count;
        }

        /// <summary>
        /// Gets a value indicating whether [ram enable].
        /// </summary>
        /// <value>
        /// <c>true</c> if [ram enable]; otherwise, <c>false</c>.
        /// </value>
        public bool RamEnable { get; private set; }

        /// <summary>
        /// Gets the rom bank number.
        /// </summary>
        /// <value>
        /// The rom bank number.
        /// </value>
        public byte RomBankNumber { get; private set; }

        /// <summary>
        /// Gets the ram bank number.
        /// </summary>
        /// <value>
        /// The ram bank number.
        /// </value>
        public byte RamBankNumber { get; private set; }

        /// <summary>
        /// Occurs when [memory bank switch].
        /// </summary>
        public event Action<MemoryBankControllerEventTarget> MemoryBankSwitch;

        /// <summary>
        /// Gets the ROM bank number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static byte GetRomBankNumber(byte value)
        {
            value = (byte) (value & 0x1f);
            switch (value)
            {
                case 0x00:
                    return 0x01;
                case 0x20:
                    return 0x21;
                case 0x40:
                    return 0x41;
                case 0x60:
                    return 0x61;
            }

            return value;
        }

        /// <summary>
        /// Called when [memory bank controller event].
        /// </summary>
        /// <param name="eventTarget">The event target.</param>
        protected void OnMemoryBankControllerEvent(MemoryBankControllerEventTarget eventTarget)
            => MemoryBankSwitch?.Invoke(eventTarget);
    }
}