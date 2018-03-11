using Retro.Net.Z80.Core.Decode;

namespace Retro.Net.Z80.Core.Interfaces
{
    /// <summary>
    /// Instruction block decoder.
    /// </summary>
    public interface IInstructionBlockFactory
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IInstructionBlockFactory"/> [supports instruction block caching].
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="IInstructionBlockFactory"/> [supports instruction block caching]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsInstructionBlockCaching { get; }

        /// <summary>
        /// Builds a new <see cref="IInstructionBlock"/> from the specified decoded block.
        /// </summary>
        /// <param name="block">The decoded instruction block.</param>
        /// <returns></returns>
        IInstructionBlock Build(DecodedBlock block);
    }
}