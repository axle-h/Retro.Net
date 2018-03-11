using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Z80.Core
{
    /// <summary>
    /// An accumulator and result structure.
    /// Used by the <see cref="IAlu"/> to avoid nasty out param expression.
    /// </summary>
    public struct AccumulatorAndResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccumulatorAndResult"/> struct.
        /// </summary>
        /// <param name="accumulator">The accumulator.</param>
        /// <param name="result">The result.</param>
        public AccumulatorAndResult(byte accumulator, byte result)
        {
            Accumulator = accumulator;
            Result = result;
        }

        /// <summary>
        /// Gets the accumulator.
        /// </summary>
        /// <value>
        /// The accumulator.
        /// </value>
        public byte Accumulator { get; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public byte Result { get; }
    }
}