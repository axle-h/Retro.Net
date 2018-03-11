namespace Retro.Net.Z80.OpCodes
{
    /// <summary>
    /// Gameboy specific modifications to <see cref="PrimaryOpCode"/> op codes.
    /// </summary>
    public enum GameBoyPrimaryOpCode : byte
    {
        LD_mnn_SP = PrimaryOpCode.EX_AF,

        STOP = PrimaryOpCode.DJNZ,

        LDI_mHL_A = PrimaryOpCode.LD_mnn_HL,
        LDI_A_mHL = PrimaryOpCode.LD_HL_mnn,
        LDD_mHL_A = PrimaryOpCode.LD_mnn_A,
        LDD_A_mHL = PrimaryOpCode.LD_A_mnn,

        RETI = PrimaryOpCode.EXX,

        LD_mFF00n_A = PrimaryOpCode.RET_PO,
        LD_mFF00C_A = PrimaryOpCode.JP_PO,
        LD_A_mFF00n = PrimaryOpCode.RET_P,
        LD_A_mFF00C = PrimaryOpCode.JP_P,

        LD_mnn_A = PrimaryOpCode.JP_PE,
        LD_A_mnn = PrimaryOpCode.JP_M,

        ADD_SP_d = PrimaryOpCode.RET_PE,
        LD_HL_SPd = PrimaryOpCode.RET_M
    }
}