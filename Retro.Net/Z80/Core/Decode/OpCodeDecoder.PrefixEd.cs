using Retro.Net.Z80.OpCodes;

namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// Core op-code decoder functions for op-codes prefixed with 0xED.
    /// </summary>
    public partial class OpCodeDecoder
    {
        /// <summary>
        /// Decodes an op-code that has been prefixed with 0xED.
        /// </summary>
        /// <returns></returns>
        private OpCode DecodePrefixEd()
        {
            var opCode = (PrefixEdOpCode) _prefetch.NextByte();

            _timer.Nop();

            switch (opCode)
            {
                // ********* 8-bit load *********
                // LD A, I
                case PrefixEdOpCode.LD_A_I:
                    _timer.Extend(1);
                    _operand1 = Operand.A;
                    _operand2 = Operand.I;
                    return OpCode.Load;

                // LD A, R
                case PrefixEdOpCode.LD_A_R:
                    _timer.Extend(1);
                    _operand1 = Operand.A;
                    _operand2 = Operand.R;
                    return OpCode.Load;

                // LD I, A
                case PrefixEdOpCode.LD_I_A:
                    _timer.Extend(1);
                    _operand1 = Operand.I;
                    _operand2 = Operand.A;
                    return OpCode.Load;

                // LD R, A
                case PrefixEdOpCode.LD_R_A:
                    _timer.Extend(1);
                    _operand1 = Operand.R;
                    _operand2 = Operand.A;
                    return OpCode.Load;

                // ********* 16-bit load *********
                // LD dd, (nn)
                case PrefixEdOpCode.LD_BC_mnn:
                    _timer.IndexAndMmuWord();
                    _operand1 = Operand.BC;
                    _operand2 = Operand.mnn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;
                case PrefixEdOpCode.LD_DE_mnn:
                    _timer.IndexAndMmuWord();
                    _operand1 = Operand.DE;
                    _operand2 = Operand.mnn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;
                case PrefixEdOpCode.LD_HL_mnn:
                    _timer.IndexAndMmuWord();
                    _operand1 = Operand.HL;
                    _operand2 = Operand.mnn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;
                case PrefixEdOpCode.LD_SP_mnn:
                    _timer.IndexAndMmuWord();
                    _operand1 = Operand.SP;
                    _operand2 = Operand.mnn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;

                // LD (nn), dd
                case PrefixEdOpCode.LD_mnn_BC:
                    _timer.IndexAndMmuWord();
                    _operand1 = Operand.mnn;
                    _operand2 = Operand.BC;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;
                case PrefixEdOpCode.LD_mnn_DE:
                    _timer.IndexAndMmuWord();
                    _operand1 = Operand.mnn;
                    _operand2 = Operand.DE;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;
                case PrefixEdOpCode.LD_mnn_HL:
                    _timer.IndexAndMmuWord();
                    _operand1 = Operand.mnn;
                    _operand2 = Operand.HL;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;
                case PrefixEdOpCode.LD_mnn_SP:
                    _timer.IndexAndMmuWord();
                    _operand1 = Operand.mnn;
                    _operand2 = Operand.SP;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;

                // ********* Block Transfer *********
                // LDI
                case PrefixEdOpCode.LDI:
                    _timer.MmuWord().Extend(2);
                    return OpCode.TransferIncrement;

                // LDIR
                case PrefixEdOpCode.LDIR:
                    _timer.MmuWord().Extend(2);
                    return OpCode.TransferIncrementRepeat;

                // LDD
                case PrefixEdOpCode.LDD:
                    _timer.MmuWord().Extend(2);
                    return OpCode.TransferDecrement;

                // LDDR
                case PrefixEdOpCode.LDDR:
                    _timer.MmuWord().Extend(2);
                    return OpCode.TransferDecrementRepeat;

                // ********* Search *********
                case PrefixEdOpCode.CPI:
                    _timer.MmuWord().Extend(2);
                    return OpCode.SearchIncrement;

                case PrefixEdOpCode.CPIR:
                    _timer.MmuWord().Extend(2);
                    return OpCode.SearchIncrementRepeat;

                case PrefixEdOpCode.CPD:
                    _timer.MmuWord().Extend(2);
                    return OpCode.SearchDecrement;

                case PrefixEdOpCode.CPDR:
                    _timer.MmuWord().Extend(2);
                    return OpCode.SearchDecrementRepeat;

                // ********* 16-Bit Arithmetic *********
                // ADC HL, ss
                case PrefixEdOpCode.ADC_HL_BC:
                    _timer.Arithmetic16();
                    _operand1 = Operand.HL;
                    _operand2 = Operand.BC;
                    return OpCode.AddWithCarry16;
                case PrefixEdOpCode.ADC_HL_DE:
                    _timer.Arithmetic16();
                    _operand1 = Operand.HL;
                    _operand2 = Operand.DE;
                    return OpCode.AddWithCarry16;
                case PrefixEdOpCode.ADC_HL_HL:
                    _timer.Arithmetic16();
                    _operand1 = _operand2 = Operand.HL;
                    return OpCode.AddWithCarry16;
                case PrefixEdOpCode.ADC_HL_SP:
                    _timer.Arithmetic16();
                    _operand1 = Operand.HL;
                    _operand2 = Operand.SP;
                    return OpCode.AddWithCarry16;

                // SBC HL, ss
                case PrefixEdOpCode.SBC_HL_BC:
                    _timer.Arithmetic16();
                    _operand1 = Operand.HL;
                    _operand2 = Operand.BC;
                    return OpCode.SubtractWithCarry16;
                case PrefixEdOpCode.SBC_HL_DE:
                    _timer.Arithmetic16();
                    _operand1 = Operand.HL;
                    _operand2 = Operand.DE;
                    return OpCode.SubtractWithCarry16;
                case PrefixEdOpCode.SBC_HL_HL:
                    _timer.Arithmetic16();
                    _operand1 = _operand2 = Operand.HL;
                    return OpCode.SubtractWithCarry16;
                case PrefixEdOpCode.SBC_HL_SP:
                    _timer.Arithmetic16();
                    _operand1 = Operand.HL;
                    _operand2 = Operand.SP;
                    return OpCode.SubtractWithCarry16;

                // ********* General-Purpose Arithmetic *********
                // NEG
                case PrefixEdOpCode.NEG:
                    return OpCode.NegateTwosComplement;

                // IM 0
                case PrefixEdOpCode.IM0:
                    return OpCode.InterruptMode0;

                // IM 1
                case PrefixEdOpCode.IM1:
                    return OpCode.InterruptMode1;

                // IM 2
                case PrefixEdOpCode.IM2:
                    return OpCode.InterruptMode2;

                // ********* Rotate *********
                // RLD
                case PrefixEdOpCode.RLD:
                    _timer.MmuWord().MmuByte().Extend(1);
                    return OpCode.RotateLeftDigit;

                // RRD
                case PrefixEdOpCode.RRD:
                    _timer.MmuWord().MmuByte().Extend(1);
                    return OpCode.RotateRightDigit;

                // ********* Return *********
                case PrefixEdOpCode.RETI:
                    _timer.MmuWord();
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.ReturnFromInterrupt;

                case PrefixEdOpCode.RETN:
                    _timer.MmuWord();
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.ReturnFromNonmaskableInterrupt;

                // ********* IO *********
                // IN r, (C)
                case PrefixEdOpCode.IN_A_C:
                    _timer.Io();
                    _operand1 = Operand.A;
                    _operand2 = Operand.C;
                    return OpCode.Input;

                case PrefixEdOpCode.IN_B_C:
                    _timer.Io();
                    _operand1 = Operand.B;
                    _operand2 = Operand.C;
                    return OpCode.Input;

                case PrefixEdOpCode.IN_C_C:
                    _timer.Io();
                    _operand1 = Operand.C;
                    _operand2 = Operand.C;
                    return OpCode.Input;

                case PrefixEdOpCode.IN_D_C:
                    _timer.Io();
                    _operand1 = Operand.D;
                    _operand2 = Operand.C;
                    return OpCode.Input;

                case PrefixEdOpCode.IN_E_C:
                    _timer.Io();
                    _operand1 = Operand.E;
                    _operand2 = Operand.C;
                    return OpCode.Input;

                case PrefixEdOpCode.IN_F_C:
                    _timer.Io();
                    _operand1 = Operand.F;
                    _operand2 = Operand.C;
                    return OpCode.Input;

                case PrefixEdOpCode.IN_H_C:
                    _timer.Io();
                    _operand1 = Operand.H;
                    _operand2 = Operand.C;
                    return OpCode.Input;

                case PrefixEdOpCode.IN_L_C:
                    _timer.Io();
                    _operand1 = Operand.L;
                    _operand2 = Operand.C;
                    return OpCode.Input;

                // INI
                case PrefixEdOpCode.INI:
                    _timer.MmuWord().Extend(2);
                    return OpCode.InputTransferIncrement;

                // INIR
                case PrefixEdOpCode.INIR:
                    _timer.MmuWord().Extend(2);
                    return OpCode.InputTransferIncrementRepeat;

                // IND
                case PrefixEdOpCode.IND:
                    _timer.MmuWord().Extend(2);
                    return OpCode.InputTransferDecrement;

                // INDR
                case PrefixEdOpCode.INDR:
                    _timer.MmuWord().Extend(2);
                    return OpCode.InputTransferDecrementRepeat;

                // OUT r, (C)
                case PrefixEdOpCode.OUT_A_C:
                    _timer.Io();
                    _operand1 = Operand.A;
                    _operand2 = Operand.C;
                    return OpCode.Output;

                case PrefixEdOpCode.OUT_B_C:
                    _timer.Io();
                    _operand1 = Operand.B;
                    _operand2 = Operand.C;
                    return OpCode.Output;

                case PrefixEdOpCode.OUT_C_C:
                    _timer.Io();
                    _operand1 = Operand.C;
                    _operand2 = Operand.C;
                    return OpCode.Output;

                case PrefixEdOpCode.OUT_D_C:
                    _timer.Io();
                    _operand1 = Operand.D;
                    _operand2 = Operand.C;
                    return OpCode.Output;

                case PrefixEdOpCode.OUT_E_C:
                    _timer.Io();
                    _operand1 = Operand.E;
                    _operand2 = Operand.C;
                    return OpCode.Output;

                case PrefixEdOpCode.OUT_F_C:
                    _timer.Io();
                    _operand1 = Operand.F;
                    _operand2 = Operand.C;
                    return OpCode.Output;

                case PrefixEdOpCode.OUT_H_C:
                    _timer.Io();
                    _operand1 = Operand.H;
                    _operand2 = Operand.C;
                    return OpCode.Output;

                case PrefixEdOpCode.OUT_L_C:
                    _timer.Io();
                    _operand1 = Operand.L;
                    _operand2 = Operand.C;
                    return OpCode.Output;

                // OUTI
                case PrefixEdOpCode.OUTI:
                    _timer.MmuWord().Extend(2);
                    return OpCode.OutputTransferIncrement;

                // OUTIR
                case PrefixEdOpCode.OUTIR:
                    _timer.MmuWord().Extend(2);
                    return OpCode.OutputTransferIncrementRepeat;

                // OUTD
                case PrefixEdOpCode.OUTD:
                    _timer.MmuWord().Extend(2);
                    return OpCode.OutputTransferDecrement;

                // OUTDR
                case PrefixEdOpCode.OUTDR:
                    _timer.MmuWord().Extend(2);
                    return OpCode.OutputTransferDecrementRepeat;

                default:
                    // The Prefix ED opcode set is not saturated
                    return _undefinedInstruction(opCode);
            }
        }
    }
}