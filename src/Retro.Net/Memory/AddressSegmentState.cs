using System.Collections.Generic;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Memory
{
    public class AddressSegmentState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressSegmentState"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="type">The type.</param>
        /// <param name="banks">The banks.</param>
        public AddressSegmentState(ushort address, MemoryBankType type, IDictionary<byte, byte[]> banks)
        {
            Address = address;
            Type = type;
            Banks = banks;
        }

        /// <summary>
        /// Gets the address of the segment that created this state.
        /// </summary>
        public ushort Address { get; }

        /// <summary>
        /// Gets the type of the segment that created this state.
        /// </summary>
        public MemoryBankType Type { get; }

        /// <summary>
        /// Gets the banks.
        /// </summary>
        public IDictionary<byte, byte[]> Banks { get; }

        public static AddressSegmentState FromReadableSegment(IReadableAddressSegment segment)
        {
            var buffer = new byte[segment.Length];
            segment.ReadBytes(0, buffer, 0, buffer.Length);
            return new AddressSegmentState(segment.Address, segment.Type, new Dictionary<byte, byte[]> { [1] = buffer });
        }

    }
}
