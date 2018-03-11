using System;
using Retro.Net.Config;
using Retro.Net.Exceptions;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Memory
{
    /// <summary>
    /// A read only memory bank (ROM).
    /// </summary>
    /// <seealso cref="IReadableAddressSegment" />
    public class ReadOnlyMemoryBank : IReadableAddressSegment
    {
        private readonly byte[] _memory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyMemoryBank" /> class.
        /// </summary>
        /// <param name="memoryBankConfig">The memory bank configuration.</param>
        /// <exception cref="MemoryConfigStateException"></exception>
        public ReadOnlyMemoryBank(IMemoryBankConfig memoryBankConfig)
        {
            if (memoryBankConfig.InitialState == null || memoryBankConfig.Length != memoryBankConfig.InitialState.Length)
            {
                throw new MemoryConfigStateException(memoryBankConfig.Address,
                                                     memoryBankConfig.Length,
                                                     memoryBankConfig.InitialState?.Length ?? 0);
            }

            _memory = new byte[memoryBankConfig.Length];
            Array.Copy(memoryBankConfig.InitialState, 0, _memory, 0, memoryBankConfig.InitialState.Length);

            Type = memoryBankConfig.Type;
            Address = memoryBankConfig.Address;
            Length = memoryBankConfig.Length;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public MemoryBankType Type { get; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address { get; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public ushort Length { get; }

        /// <summary>
        /// Reads a byte from this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public byte ReadByte(ushort address) => _memory[address];

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
        /// <exception cref="NotImplementedException"></exception>
        public int ReadBytes(ushort address, byte[] buffer, int offset, int count)
        {
            count = Math.Min(count, Length - address);
            Array.Copy(_memory, address, buffer, offset, count);
            return count;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Type}: 0x{Address:x4} - 0x{Address + Length - 1:x4}";
    }
}