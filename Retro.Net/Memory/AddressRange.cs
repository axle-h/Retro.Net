using System;
using System.Collections.Generic;

namespace Retro.Net.Memory
{
    /// <summary>
    /// A normal address range.
    /// I.e. min > max due to mod 2^16
    /// </summary>
    public struct AddressRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRange" /> structure.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="maxAddress">The maximum address.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public AddressRange(ushort address, ushort maxAddress) : this()
        {
            if (address > maxAddress)
            {
                throw new ArgumentException($"Cannot create normal range: {maxAddress} > {address}");
            }
            Address = address;
            MaxAddress = maxAddress;
            
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address { get; }

        /// <summary>
        /// Gets the maximum address.
        /// </summary>
        /// <value>
        /// The maximum address.
        /// </value>
        public ushort MaxAddress { get; }

        /// <summary>
        /// Determines whether the specified address range intersects with this one.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public bool Intersects(AddressRange range)
            => Math.Max(range.Address, Address) <= Math.Min(range.MaxAddress, MaxAddress);

        /// <summary>
        /// Determines whether the specified address range intersects or is adjacent to this one.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public bool IntersectsOrAdjacent(AddressRange range)
            => Math.Max(range.Address, Address) <= Math.Min(range.MaxAddress, MaxAddress) + 1;

        /// <summary>
        /// Merges the specified range with this one.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public AddressRange Merge(AddressRange range)
            => new AddressRange(Math.Min(Address, range.Address), Math.Max(MaxAddress, range.MaxAddress));

        /// <summary>
        /// Gets all address ranges required to satisfy the specified address, length pair.
        /// If the range overflows an unsigned 16-bit number then two ranges will be returned.
        /// </summary>
        /// <param name="address">The start address of the range.</param>
        /// <param name="length">The address range length.</param>
        /// <returns></returns>
        public static IEnumerable<AddressRange> GetRanges(ushort address, int length)
        {
            var maxAddress = (ushort) (address + length - 1);
            if (maxAddress >= address)
            {
                yield return new AddressRange(address, maxAddress);
            }
            else
            {
                yield return new AddressRange(ushort.MinValue, maxAddress);
                yield return new AddressRange(address, ushort.MaxValue);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"({Address}, {MaxAddress})";
    }
}