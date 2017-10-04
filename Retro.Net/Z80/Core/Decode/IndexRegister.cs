namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// A 8080/Z80 index register.
    /// </summary>
    internal enum IndexRegister
    {
        /// <summary>
        /// The HL register.
        /// </summary>
        HL = 0,

        /// <summary>
        /// The IX register (Z80 only).
        /// </summary>
        IX = 1,

        /// <summary>
        /// The IY register (Z80 only).
        /// </summary>
        IY = 2
    }
}