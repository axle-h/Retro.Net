using System.Collections.Generic;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Timing;

namespace Retro.Net.Memory.Dma
{
    /// <summary>
    /// A DMA operation to copy bytes from one address to another.
    /// </summary>
    public class DmaCopyOperation : IDmaOperation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DmaCopyOperation" /> class.
        /// </summary>
        /// <param name="sourceAddress">The source address.</param>
        /// <param name="destinationAddress">The destination address.</param>
        /// <param name="length">The length.</param>
        /// <param name="timings">The timings.</param>
        /// <param name="lockedAddressesRanges">The locked addresses ranges.</param>
        public DmaCopyOperation(ushort sourceAddress,
            ushort destinationAddress,
            int length,
            InstructionTimings timings,
            IEnumerable<AddressRange> lockedAddressesRanges)
        {
            SourceAddress = sourceAddress;
            DestinationAddress = destinationAddress;
            Length = length;
            Timings = timings;
            LockedAddressesRanges = lockedAddressesRanges;
        }

        /// <summary>
        /// Gets the source address.
        /// </summary>
        /// <value>
        /// The source address.
        /// </value>
        public ushort SourceAddress { get; }

        /// <summary>
        /// Gets the destination address.
        /// </summary>
        /// <value>
        /// The destination address.
        /// </value>
        public ushort DestinationAddress { get; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; }

        /// <summary>
        /// Gets the execution cpu timings.
        /// </summary>
        /// <value>
        /// The execution cpu timings.
        /// </value>
        public InstructionTimings Timings { get; }

        /// <summary>
        /// Executes the dma operation.
        /// </summary>
        /// <param name="mmu">The mmu.</param>
        public void Execute(IMmu mmu) => mmu.TransferBytes(SourceAddress, DestinationAddress, Length);

        /// <summary>
        /// Gets addresses ranges that should be locked for reading and writing during this dma operation.
        /// </summary>
        /// <value>
        /// The locked addresses ranges.
        /// </value>
        public IEnumerable<AddressRange> LockedAddressesRanges { get; }
    }
}