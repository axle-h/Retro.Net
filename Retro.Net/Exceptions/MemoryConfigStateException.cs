using System;

namespace Retro.Net.Exceptions
{
    /// <summary>
    /// Memory configuration state exception.
    /// Thrown when attempting to configure an address segment with an invalid state.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class MemoryConfigStateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryConfigStateException"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="segmentLength">Length of the segment.</param>
        /// <param name="stateLength">Length of the state.</param>
        public MemoryConfigStateException(ushort address, int segmentLength, int stateLength)
            : base(
                $"Segment configured at address 0x{address:x4} - 0x{address + segmentLength - 1:x4} has invalid state length: {stateLength}"
                )
        {
            Adddress = address;
            SegmentLength = segmentLength;
            StateLength = stateLength;
        }

        /// <summary>
        /// Gets the adddress.
        /// </summary>
        /// <value>
        /// The adddress.
        /// </value>
        public ushort Adddress { get; private set; }

        /// <summary>
        /// Gets the length of the segment.
        /// </summary>
        /// <value>
        /// The length of the segment.
        /// </value>
        public int SegmentLength { get; private set; }

        /// <summary>
        /// Gets the length of the state.
        /// </summary>
        /// <value>
        /// The length of the state.
        /// </value>
        public int StateLength { get; private set; }
    }
}