namespace Retro.Net.Z80.OpCodes
{
    /// <summary>
    /// Op codes that are prefixed with <see cref="PrimaryOpCode.Prefix_ED"/>. 
    /// </summary>
    public enum PrefixEdOpCode : byte
    {
        //8 Bit Load
        LD_A_I = 0x57,
        LD_A_R = 0x5F,
        LD_I_A = 0x47,
        LD_R_A = 0x4F,

        //16 Bit Load
        LD_BC_mnn = 0x4B,
        LD_DE_mnn = 0x5B,
        LD_HL_mnn = 0x6B,
        LD_SP_mnn = 0x7B,

        LD_mnn_BC = 0x43,
        LD_mnn_DE = 0x53,
        LD_mnn_HL = 0x63,
        LD_mnn_SP = 0x73,

        //Exchange, block,, transfer and search
        LDI = 0xA0,
        LDIR = 0xB0,
        LDD = 0xA8,
        LDDR = 0xB8,
        CPI = 0xA1,
        CPIR = 0xB1,
        CPD = 0xA9,
        CPDR = 0xB9,

        //General purpose arithmetic and CPU control
        NEG = 0x44,
        IM0 = 0x46,
        IM1 = 0x56,
        IM2 = 0x5E,

        //16 Bit Arithmetic
        ADC_HL_BC = 0x4A,
        ADC_HL_DE = 0x5A,
        ADC_HL_HL = 0x6A,
        ADC_HL_SP = 0x7A,
        SBC_HL_BC = 0x42,
        SBC_HL_DE = 0x52,
        SBC_HL_HL = 0x62,
        SBC_HL_SP = 0x72,

        // Rotate and Shift
        RLD = 0x6F,
        RRD = 0x67,

        // Return
        RETI = 0x4D,
        RETN = 0x45,

        // IO
        IN_B_C = 0x40,
        IN_C_C = 0x48,
        IN_D_C = 0x50,
        IN_E_C = 0x58,
        IN_H_C = 0x60,
        IN_L_C = 0x68,
        IN_F_C = 0x70,
        IN_A_C = 0x78,

        INI = 0xA2,
        INIR = 0xB2,
        IND = 0xAA,
        INDR = 0xBA,

        OUT_B_C = 0x41,
        OUT_C_C = 0x49,
        OUT_D_C = 0x51,
        OUT_E_C = 0x59,
        OUT_H_C = 0x61,
        OUT_L_C = 0x69,
        OUT_F_C = 0x71,
        OUT_A_C = 0x79,

        OUTI = 0xA3,
        OUTIR = 0xB3,
        OUTD = 0xAB,
        OUTDR = 0xBB
    }
}