using System;

namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// Internal decode meta flags for specifying post decode behaviour.
    /// </summary>
    [Flags]
    public enum DecodeMeta
    {
        /// <summary>
        /// No meta provided.
        /// </summary>
        None = 0,

        /// <summary>
        /// Operation uses a byte literal.
        /// </summary>
        ByteLiteral = 0x01,

        /// <summary>
        /// Operation uses a word literal.
        /// </summary>
        WordLiteral = 0x02,

        /// <summary>
        /// Operation uses a displacement.
        /// </summary>
        Displacement = 0x04,

        /// <summary>
        /// The end of this execution block. I.e. a jump, call, return, halt.
        /// </summary>
        EndBlock = 0x08,
    }
}
