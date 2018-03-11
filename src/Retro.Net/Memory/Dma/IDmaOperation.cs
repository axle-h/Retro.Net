using System.Collections.Generic;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Timing;

namespace Retro.Net.Memory.Dma
{
    /// <summary>
    /// A DMA operation.
    /// </summary>
    public interface IDmaOperation
    {
        /// <summary>
        /// Gets the execution cpu timings.
        /// </summary>
        /// <value>
        /// The execution cpu timings.
        /// </value>
        InstructionTimings Timings { get; }

        /// <summary>
        /// Gets addresses ranges that should be locked for reading and writing during this dma operation.
        /// </summary>
        /// <value>
        /// The locked addresses ranges.
        /// </value>
        IEnumerable<AddressRange> LockedAddressesRanges { get; }

        /// <summary>
        /// Executes the dma operation.
        /// </summary>
        /// <param name="mmu">The mmu.</param>
        void Execute(IMmu mmu);
    }
}