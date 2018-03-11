using System;

namespace Retro.Net.Memory.Interfaces
{
    /// <summary>
    /// An address segment.
    /// <see cref="IReadableAddressSegment" />
    /// <see cref="IWriteableAddressSegment" />
    /// </summary>
    public interface IAddressSegment
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        MemoryBankType Type { get; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        ushort Address { get; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        ushort Length { get; }
    }
}