using System.Collections.Generic;
using Retro.Net.Memory;
using Retro.Net.Z80.Core;

namespace Retro.Net.Z80.Cache
{
    /// <summary>
    /// Instruction block cache wrapper with a single normal range.
    /// </summary>
    internal class NormalCachedInstructionBlock : ICachedInstructionBlock
    {
        private readonly AddressRange _addressRange;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalCachedInstructionBlock"/> class.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="instructionBlock">The instruction block.</param>
        public NormalCachedInstructionBlock(AddressRange range, IInstructionBlock instructionBlock)
        {
            InstructionBlock = instructionBlock;
            _addressRange = range;
        }

        /// <summary>
        /// Gets or sets the accessed count.
        /// </summary>
        /// <value>
        /// The accessed count.
        /// </value>
        public uint AccessedCount { get; set; }

        /// <summary>
        /// Gets the instruction block.
        /// </summary>
        /// <value>
        /// The instruction block.
        /// </value>
        public IInstructionBlock InstructionBlock { get; }

        /// <summary>
        /// Checks if the specified range intersects this cached instruction block.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public bool Intersects(AddressRange range) => range.Intersects(_addressRange);

        /// <summary>
        /// Gets the address ranges.
        /// </summary>
        /// <value>
        /// The address ranges.
        /// </value>
        public IEnumerable<AddressRange> AddressRanges
        {
            get { yield return _addressRange; }
        } 
    }
}