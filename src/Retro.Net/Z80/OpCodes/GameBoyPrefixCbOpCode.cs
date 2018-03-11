namespace Retro.Net.Z80.OpCodes
{
    /// <summary>
    /// Gameboy specific modifications to <see cref="PrefixCbOpCode"/> op codes.
    /// </summary>
    public enum GameBoyPrefixCbOpCode : byte
    {
        SWAP_B = PrefixCbOpCode.SLS_B,
        SWAP_C = PrefixCbOpCode.SLS_C,
        SWAP_D = PrefixCbOpCode.SLS_D,
        SWAP_E = PrefixCbOpCode.SLS_E,
        SWAP_H = PrefixCbOpCode.SLS_H,
        SWAP_L = PrefixCbOpCode.SLS_L,
        SWAP_mHL = PrefixCbOpCode.SLS_mHL,
        SWAP_A = PrefixCbOpCode.SLS_A
    }
}