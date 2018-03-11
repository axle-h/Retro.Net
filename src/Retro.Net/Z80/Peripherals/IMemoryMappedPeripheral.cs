using System.Collections.Generic;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Z80.Peripherals
{
    /// <summary>
    /// A peripheral called through the z80's address space.
    /// </summary>
    public interface IMemoryMappedPeripheral : IPeripheral
    {
        /// <summary>
        /// Gets the address segments.
        /// </summary>
        /// <value>
        /// The address segments.
        /// </value>
        IEnumerable<IAddressSegment> AddressSegments { get; }
    }
}