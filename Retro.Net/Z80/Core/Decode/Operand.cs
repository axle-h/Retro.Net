namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// An operand.
    /// </summary>
    public enum Operand : byte
    {
        None = 0,

        // 8-bit registers
        A,
        B,
        C,
        D,
        E,
        F,
        H,
        L,

        // 16-bit registers
        HL,
        BC,
        DE,
        AF,
        SP,

        // Indexes
        mHL,
        mBC,
        mDE,
        mSP,

        // Literals
        mnn,
        nn,
        n,
        d,

        // Z80 indexes
        IX,
        mIXd,
        IXl,
        IXh,
        IY,
        mIYd,
        IYl,
        IYh,

        // Z80 specific
        I,
        R,

        // Gameboy Specific
        /// <summary>
        /// (0xFF00+C)
        /// </summary>
        mCl,

        /// <summary>
        /// (0xFF00+n)
        /// </summary>
        mnl,

        /// <summary>
        /// SP + signed byte literal
        /// </summary>
        SPd
    }
}