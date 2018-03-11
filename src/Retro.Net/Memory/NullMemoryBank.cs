using Retro.Net.Config;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Memory
{
    /// <summary>
    /// A readable, writable address segment with null behavior - always returns 0 and doesn't actually store anything.
    /// </summary>
    /// <seealso cref="IReadableAddressSegment" />
    /// <seealso cref="IWriteableAddressSegment" />
    public class NullMemoryBank : IReadWriteAddressSegment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullMemoryBank" /> class.
        /// </summary>
        /// <param name="memoryBankConfig">The memory bank configuration.</param>
        public NullMemoryBank(IMemoryBankConfig memoryBankConfig)
        {
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
        public byte ReadByte(ushort address) => 0x00;

        /// <summary>
        /// Reads the bytes.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public int ReadBytes(ushort address, byte[] buffer, int offset, int count) => count;
        
        /// <summary>
        /// Writes a byte to this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="value">The value.</param>
        public void WriteByte(ushort address, byte value)
        {
        }

        /// <summary>
        /// Writes the bytes.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int WriteBytes(ushort address, byte[] buffer, int offset, int count) => count;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Type}: 0x{Address:x4} - 0x{Address + Length - 1:x4}";
    }
}