namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// Internal decode meta flags for communicating with CPU execution.
    /// </summary>
    public enum OpCodeMeta : byte
    {
        /// <summary>
        /// No meta provided.
        /// </summary>
        None = 0,

        /// <summary>
        /// Apply auto copy to register on CB DD/FD prefixed opcodes.
        /// </summary>
        AutoCopy = 1,

        /// <summary>
        /// Use the alternative flag affection scheme e.g. RLCA and RLC A call same ALU function but effect flags slightly
        /// differently.
        /// </summary>
        UseAlternativeFlagAffection = 2
    }
}