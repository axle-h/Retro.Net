using System;
using Retro.Net.Config;
using Retro.Net.Exceptions;

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
        /// Reads a word from this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public ushort ReadWord(ushort address) => BitConverter.ToUInt16(_memory, address);

        /// <summary>
        /// Reads bytes from this address segment into a new buffer.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public byte[] ReadBytes(ushort address, int length)
        {
            var bytes = new byte[length];
            Array.Copy(_memory, address, bytes, 0, length);
            return bytes;
        }

        /// <summary>
        /// Reads bytes from this address segment into the specified buffer.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="buffer">The buffer.</param>
        public void ReadBytes(ushort address, byte[] buffer) => Array.Copy(_memory, address, buffer, 0, buffer.Length);

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Type}: 0x{Address:x4} - 0x{Address + Length - 1:x4}";
    }
}