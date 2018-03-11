using System;
using Retro.Net.Config;
using Retro.Net.Exceptions;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Memory
{
    /// <summary>
    /// A simple managed array backed memory segment.
    /// </summary>
    /// <seealso cref="IReadWriteAddressSegment" />
    public class ArrayBackedMemoryBank : IReadWriteAddressSegment
    {
        private readonly byte[] _memory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayBackedMemoryBank"/> class.
        /// </summary>
        /// <param name="memoryBankConfig">The memory bank configuration.</param>
        /// <exception cref="MemoryConfigStateException"></exception>
        public ArrayBackedMemoryBank(IMemoryBankConfig memoryBankConfig)
        {
            _memory = new byte[memoryBankConfig.Length];
            Type = memoryBankConfig.Type;
            Address = memoryBankConfig.Address;
            Length = memoryBankConfig.Length;

            if (memoryBankConfig.InitialState == null)
            {
                return;
            }

            if (memoryBankConfig.Length != memoryBankConfig.InitialState.Length)
            {
                throw new MemoryConfigStateException(memoryBankConfig.Address,
                                                     memoryBankConfig.Length,
                                                     memoryBankConfig.InitialState.Length);
            }
            Array.Copy(memoryBankConfig.InitialState, 0, _memory, 0, memoryBankConfig.InitialState.Length);
        }

        public MemoryBankType Type { get; }

        public ushort Address { get; }

        public ushort Length { get; }

        public byte ReadByte(ushort address) => _memory[address];

        public int ReadBytes(ushort address, byte[] buffer, int offset, int count)
        {
            count = Math.Min(count, Length - address);
            Array.Copy(_memory, address, buffer, offset, count);
            return count;
        }

        public void WriteByte(ushort address, byte value) => _memory[address] = value;

        public int WriteBytes(ushort address, byte[] buffer, int offset, int count)
        {
            count = Math.Min(count, Length - address);
            Array.Copy(buffer, offset, _memory, address, count);
            return count;
        }

        public override string ToString() => $"{Type}: 0x{Address:x4} - 0x{Address + Length - 1:x4}";

        public ArraySegment<byte> Segment(ushort address, int length) => new ArraySegment<byte>(_memory, address, length);
    }
}