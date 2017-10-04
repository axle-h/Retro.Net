using System;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.OpCodes;

namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// Core op-code decoder functions.
    /// </summary>
    public partial class OpCodeDecoder
    {
        /// <summary>
        /// Decodes the next opcode.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">null</exception>
        private OpCode? DecodeNextOpCode()
        {
            var code = (PrimaryOpCode) _prefetch.NextByte();

            // Add a NOP on every frame. Reduce timings by a NOP elsewhere.
            _timer.Nop();

            switch (code)
            {
                case PrimaryOpCode.NOP:
                    return OpCode.NoOperation;
                case PrimaryOpCode.HALT:
                    _decodeMeta = DecodeMeta.EndBlock;
                    _halt = true;
                    return OpCode.Halt;

                // ********* Prefixes *********
                case PrimaryOpCode.Prefix_CB:
                    if (_cpuMode == CpuMode.Z80 || _cpuMode == CpuMode.GameBoy)
                    {
                        // Only Z80 & GBCPU has prefix CB
                        if (!_index.IsDisplaced)
                        {
                            return DecodePrefixCb();
                        }
                        _displacement = _prefetch.NextByte();
                        return FixPrefixDdFdPrefixCbResult(DecodePrefixCb());
                    }
                    return _undefinedInstruction(code);

                case PrimaryOpCode.Prefix_DD:
                    if (_cpuMode == CpuMode.Z80)
                    {
                        // Only Z80 has prefix DD
                        _index = _indexRegisterOperands[IndexRegister.IX];
                        return null;
                    }
                    return _undefinedInstruction(code);

                case PrimaryOpCode.Prefix_ED:
                    if (_cpuMode == CpuMode.Z80)
                    {
                        // Only Z80 has prefix ED
                        return DecodePrefixEd();
                    }
                    return _undefinedInstruction(code);

                case PrimaryOpCode.Prefix_FD:
                    if (_cpuMode == CpuMode.Z80)
                    {
                        // Only Z80 has prefix FD
                        _index = _indexRegisterOperands[IndexRegister.IY];
                        return null;
                    }
                    return _undefinedInstruction(code);

                // ********* 8-bit load *********
                // LD r, r'
                case PrimaryOpCode.LD_A_A:
                    return OpCode.NoOperation;
                case PrimaryOpCode.LD_B_A:
                    _operand1 = Operand.B;
                    _operand2 = Operand.A;
                    return OpCode.Load;
                case PrimaryOpCode.LD_C_A:
                    _operand1 = Operand.C;
                    _operand2 = Operand.A;
                    return OpCode.Load;
                case PrimaryOpCode.LD_D_A:
                    _operand1 = Operand.D;
                    _operand2 = Operand.A;
                    return OpCode.Load;
                case PrimaryOpCode.LD_E_A:
                    _operand1 = Operand.E;
                    _operand2 = Operand.A;
                    return OpCode.Load;
                case PrimaryOpCode.LD_H_A:
                    _operand1 = _index.HighRegister;
                    _operand2 = Operand.A;
                    return OpCode.Load;
                case PrimaryOpCode.LD_L_A:
                    _operand1 = _index.LowRegister;
                    _operand2 = Operand.A;
                    return OpCode.Load;
                case PrimaryOpCode.LD_A_B:
                    _operand1 = Operand.A;
                    _operand2 = Operand.B;
                    return OpCode.Load;
                case PrimaryOpCode.LD_B_B:
                    return OpCode.NoOperation;
                case PrimaryOpCode.LD_C_B:
                    _operand1 = Operand.C;
                    _operand2 = Operand.B;
                    return OpCode.Load;
                case PrimaryOpCode.LD_D_B:
                    _operand1 = Operand.D;
                    _operand2 = Operand.B;
                    return OpCode.Load;
                case PrimaryOpCode.LD_E_B:
                    _operand1 = Operand.E;
                    _operand2 = Operand.B;
                    return OpCode.Load;
                case PrimaryOpCode.LD_H_B:
                    _operand1 = _index.HighRegister;
                    _operand2 = Operand.B;
                    return OpCode.Load;
                case PrimaryOpCode.LD_L_B:
                    _operand1 = _index.LowRegister;
                    _operand2 = Operand.B;
                    return OpCode.Load;
                case PrimaryOpCode.LD_A_C:
                    _operand1 = Operand.A;
                    _operand2 = Operand.C;
                    return OpCode.Load;
                case PrimaryOpCode.LD_B_C:
                    _operand1 = Operand.B;
                    _operand2 = Operand.C;
                    return OpCode.Load;
                case PrimaryOpCode.LD_C_C:
                    return OpCode.NoOperation;
                case PrimaryOpCode.LD_D_C:
                    _operand1 = Operand.D;
                    _operand2 = Operand.C;
                    return OpCode.Load;
                case PrimaryOpCode.LD_E_C:
                    _operand1 = Operand.E;
                    _operand2 = Operand.C;
                    return OpCode.Load;
                case PrimaryOpCode.LD_H_C:
                    _operand1 = _index.HighRegister;
                    _operand2 = Operand.C;
                    return OpCode.Load;
                case PrimaryOpCode.LD_L_C:
                    _operand1 = _index.LowRegister;
                    _operand2 = Operand.C;
                    return OpCode.Load;
                case PrimaryOpCode.LD_A_D:
                    _operand1 = Operand.A;
                    _operand2 = Operand.D;
                    return OpCode.Load;
                case PrimaryOpCode.LD_B_D:
                    _operand1 = Operand.B;
                    _operand2 = Operand.D;
                    return OpCode.Load;
                case PrimaryOpCode.LD_C_D:
                    _operand1 = Operand.C;
                    _operand2 = Operand.D;
                    return OpCode.Load;
                case PrimaryOpCode.LD_D_D:
                    return OpCode.NoOperation;
                case PrimaryOpCode.LD_E_D:
                    _operand1 = Operand.E;
                    _operand2 = Operand.D;
                    return OpCode.Load;
                case PrimaryOpCode.LD_H_D:
                    _operand1 = _index.HighRegister;
                    _operand2 = Operand.D;
                    return OpCode.Load;
                case PrimaryOpCode.LD_L_D:
                    _operand1 = _index.LowRegister;
                    _operand2 = Operand.D;
                    return OpCode.Load;
                case PrimaryOpCode.LD_A_E:
                    _operand1 = Operand.A;
                    _operand2 = Operand.E;
                    return OpCode.Load;
                case PrimaryOpCode.LD_B_E:
                    _operand1 = Operand.B;
                    _operand2 = Operand.E;
                    return OpCode.Load;
                case PrimaryOpCode.LD_C_E:
                    _operand1 = Operand.C;
                    _operand2 = Operand.E;
                    return OpCode.Load;
                case PrimaryOpCode.LD_D_E:
                    _operand1 = Operand.D;
                    _operand2 = Operand.E;
                    return OpCode.Load;
                case PrimaryOpCode.LD_E_E:
                    return OpCode.NoOperation;
                case PrimaryOpCode.LD_H_E:
                    _operand1 = _index.HighRegister;
                    _operand2 = Operand.E;
                    return OpCode.Load;
                case PrimaryOpCode.LD_L_E:
                    _operand1 = _index.LowRegister;
                    _operand2 = Operand.E;
                    return OpCode.Load;
                case PrimaryOpCode.LD_A_H:
                    _operand1 = Operand.A;
                    _operand2 = _index.HighRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_B_H:
                    _operand1 = Operand.B;
                    _operand2 = _index.HighRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_C_H:
                    _operand1 = Operand.C;
                    _operand2 = _index.HighRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_D_H:
                    _operand1 = Operand.D;
                    _operand2 = _index.HighRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_E_H:
                    _operand1 = Operand.E;
                    _operand2 = _index.HighRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_H_H:
                    return OpCode.NoOperation;
                case PrimaryOpCode.LD_L_H:
                    _operand1 = _index.LowRegister;
                    _operand2 = _index.HighRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_A_L:
                    _operand1 = Operand.A;
                    _operand2 = _index.LowRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_B_L:
                    _operand1 = Operand.B;
                    _operand2 = _index.LowRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_C_L:
                    _operand1 = Operand.C;
                    _operand2 = _index.LowRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_D_L:
                    _operand1 = Operand.D;
                    _operand2 = _index.LowRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_E_L:
                    _operand1 = Operand.E;
                    _operand2 = _index.LowRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_H_L:
                    _operand1 = _index.HighRegister;
                    _operand2 = _index.LowRegister;
                    return OpCode.Load;
                case PrimaryOpCode.LD_L_L:
                    return OpCode.NoOperation;

                // LD r,n
                case PrimaryOpCode.LD_A_n:
                    _timer.MmuByte();
                    _operand1 = Operand.A;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Load;
                case PrimaryOpCode.LD_B_n:
                    _timer.MmuByte();
                    _operand1 = Operand.B;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Load;
                case PrimaryOpCode.LD_C_n:
                    _timer.MmuByte();
                    _operand1 = Operand.C;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Load;
                case PrimaryOpCode.LD_D_n:
                    _timer.MmuByte();
                    _operand1 = Operand.D;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Load;
                case PrimaryOpCode.LD_E_n:
                    _timer.MmuByte();
                    _operand1 = Operand.E;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Load;
                case PrimaryOpCode.LD_H_n:
                    _timer.MmuByte();
                    _operand1 = _index.HighRegister;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Load;
                case PrimaryOpCode.LD_L_n:
                    _timer.MmuByte();
                    _operand1 = _index.LowRegister;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Load;

                // LD r, (HL)
                case PrimaryOpCode.LD_A_mHL:
                    _operand1 = Operand.A;
                    _operand2 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_B_mHL:
                    _operand1 = Operand.B;
                    _operand2 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_C_mHL:
                    _operand1 = Operand.C;
                    _operand2 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_D_mHL:
                    _operand1 = Operand.D;
                    _operand2 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_E_mHL:
                    _operand1 = Operand.E;
                    _operand2 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_H_mHL:
                    // H register is always assigned here
                    _operand1 = Operand.H;
                    _operand2 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_L_mHL:
                    // L register is always assigned here
                    _operand1 = Operand.L;
                    _operand2 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Load;

                // LD (HL), r
                case PrimaryOpCode.LD_mHL_A:
                    _operand1 = _index.Index;
                    _operand2 = Operand.A;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_mHL_B:
                    _operand1 = _index.Index;
                    _operand2 = Operand.B;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_mHL_C:
                    _operand1 = _index.Index;
                    _operand2 = Operand.C;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_mHL_D:
                    _operand1 = _index.Index;
                    _operand2 = Operand.D;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_mHL_E:
                    _operand1 = _index.Index;
                    _operand2 = Operand.E;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_mHL_H:
                    // Value of H register is always used here
                    _operand1 = _index.Index;
                    _operand2 = Operand.H;
                    UpdateDisplacement();
                    return OpCode.Load;
                case PrimaryOpCode.LD_mHL_L:
                    // Value of L register is always used here
                    _operand1 = _index.Index;
                    _operand2 = Operand.L;
                    UpdateDisplacement();
                    return OpCode.Load;

                // LD (HL), n
                case PrimaryOpCode.LD_mHL_n:
                    _timer.IndexAndMmuByte(_index.IsDisplaced);
                    _operand1 = _index.Index;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    if (_index.IsDisplaced)
                    {
                        _decodeMeta |= DecodeMeta.Displacement;
                    }
                    return OpCode.Load;

                // LD A, (BC)
                case PrimaryOpCode.LD_A_mBC:
                    _timer.Index(false);
                    _operand1 = Operand.A;
                    _operand2 = Operand.mBC;
                    return OpCode.Load;

                // LD A, (BC)
                case PrimaryOpCode.LD_A_mDE:
                    _timer.Index(false);
                    _operand1 = Operand.A;
                    _operand2 = Operand.mDE;
                    return OpCode.Load;

                // LD A, (nn)
                case PrimaryOpCode.LD_A_mnn:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LDD  A, (HL) on GB
                        _timer.MmuByte();
                        _operand1 = Operand.A;
                        _operand2 = _index.Index;
                        return OpCode.LoadDecrement;
                    }
                    _timer.Index(false).MmuWord();
                    _operand1 = Operand.A;
                    _operand2 = Operand.mnn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load;

                // LD (BC), A
                case PrimaryOpCode.LD_mBC_A:
                    _timer.Index(false);
                    _operand1 = Operand.mBC;
                    _operand2 = Operand.A;
                    return OpCode.Load;

                // LD (DE), A
                case PrimaryOpCode.LD_mDE_A:
                    _timer.Index(false);
                    _operand1 = Operand.mDE;
                    _operand2 = Operand.A;
                    return OpCode.Load;

                // LD (nn), A
                case PrimaryOpCode.LD_mnn_A:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LDD  (HL), A on GB
                        _timer.MmuByte();
                        _operand1 = _index.Index;
                        _operand2 = Operand.A;
                        return OpCode.LoadDecrement;
                    }
                    _timer.Index(false).MmuWord();
                    _operand1 = Operand.mnn;
                    _operand2 = Operand.A;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load;

                // ********* 16-bit load *********
                // LD dd, nn
                case PrimaryOpCode.LD_BC_nn:
                    _timer.MmuWord();
                    _operand1 = Operand.BC;
                    _operand2 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;
                case PrimaryOpCode.LD_DE_nn:
                    _timer.MmuWord();
                    _operand1 = Operand.DE;
                    _operand2 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;
                case PrimaryOpCode.LD_HL_nn:
                    _timer.MmuWord();
                    _operand1 = _index.Register;
                    _operand2 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;
                case PrimaryOpCode.LD_SP_nn:
                    _timer.MmuWord();
                    _operand1 = Operand.SP;
                    _operand2 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;

                // LD HL, (nn)
                case PrimaryOpCode.LD_HL_mnn:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LDI  A, (HL) on GB
                        _timer.MmuByte();
                        _operand1 = Operand.A;
                        _operand2 = _index.Index;
                        return OpCode.LoadIncrement;
                    }

                    _timer.IndexAndMmuWord();
                    _operand1 = _index.Register;
                    _operand2 = Operand.mnn;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;

                // LD (nn), HL
                case PrimaryOpCode.LD_mnn_HL:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LDI  (HL), A on GB
                        _timer.MmuByte();
                        _operand1 = _index.Index;
                        _operand2 = Operand.A;
                        return OpCode.LoadIncrement;
                    }

                    _timer.IndexAndMmuWord();
                    _operand1 = Operand.mnn;
                    _operand2 = _index.Register;
                    _decodeMeta = DecodeMeta.WordLiteral;
                    return OpCode.Load16;

                // LD SP, HL
                case PrimaryOpCode.LD_SP_HL:
                    _timer.Extend(2);
                    _operand1 = Operand.SP;
                    _operand2 = _index.Register;
                    return OpCode.Load16;

                // PUSH qq
                case PrimaryOpCode.PUSH_BC:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.BC;
                    return OpCode.Push;
                case PrimaryOpCode.PUSH_DE:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.DE;
                    return OpCode.Push;
                case PrimaryOpCode.PUSH_HL:
                    _timer.Extend(1).MmuWord();
                    _operand1 = _index.Register;
                    return OpCode.Push;
                case PrimaryOpCode.PUSH_AF:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.AF;
                    return OpCode.Push;

                // POP qq
                case PrimaryOpCode.POP_BC:
                    _timer.MmuWord();
                    _operand1 = Operand.BC;
                    return OpCode.Pop;
                case PrimaryOpCode.POP_DE:
                    _timer.MmuWord();
                    _operand1 = Operand.DE;
                    return OpCode.Pop;
                case PrimaryOpCode.POP_HL:
                    _timer.MmuWord();
                    _operand1 = _index.Register;
                    return OpCode.Pop;
                case PrimaryOpCode.POP_AF:
                    _timer.MmuWord();
                    _operand1 = Operand.AF;
                    return OpCode.Pop;

                // ********* Exchange *********
                // EX DE, HL
                case PrimaryOpCode.EX_DE_HL:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Instruction not on GBCPU
                        return _undefinedInstruction(code);
                    }

                    // This affects HL register directly, always ignoring index register prefixes
                    _operand1 = Operand.DE;
                    _operand2 = Operand.HL;
                    return OpCode.Exchange;

                // EX AF, AF′
                case PrimaryOpCode.EX_AF:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LD (nn),SP on GB
                        _timer.IndexAndMmuWord();
                        _operand1 = Operand.mnn;
                        _operand2 = Operand.SP;
                        _decodeMeta = DecodeMeta.WordLiteral;
                        return OpCode.Load16;
                    }

                    return OpCode.ExchangeAccumulatorAndFlags;

                // EXX
                case PrimaryOpCode.EXX:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as RETI on GB, retains NOP timing
                        _timer.MmuWord().Nop();
                        _decodeMeta = DecodeMeta.EndBlock;
                        return OpCode.ReturnFromInterrupt;
                    }

                    return OpCode.ExchangeGeneralPurpose;

                // EX (SP), HL
                case PrimaryOpCode.EX_mSP_HL:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Instruction not on GBCPU
                        return _undefinedInstruction(code);
                    }

                    _timer.Arithmetic16().MmuWord().Extend(2);
                    _operand1 = Operand.mSP;
                    _operand2 = _index.Register;
                    return OpCode.Exchange;

                // ********* 8-Bit Arithmetic *********
                // ADD A, r
                case PrimaryOpCode.ADD_A_A:
                    _operand1 = Operand.A;
                    return OpCode.Add;
                case PrimaryOpCode.ADD_A_B:
                    _operand1 = Operand.B;
                    return OpCode.Add;
                case PrimaryOpCode.ADD_A_C:
                    _operand1 = Operand.C;
                    return OpCode.Add;
                case PrimaryOpCode.ADD_A_D:
                    _operand1 = Operand.D;
                    return OpCode.Add;
                case PrimaryOpCode.ADD_A_E:
                    _operand1 = Operand.E;
                    return OpCode.Add;
                case PrimaryOpCode.ADD_A_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.Add;
                case PrimaryOpCode.ADD_A_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.Add;
                case PrimaryOpCode.ADD_A_n:
                    _timer.MmuByte();
                    _operand1 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Add;
                case PrimaryOpCode.ADD_A_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Add;

                // ADC A, r
                case PrimaryOpCode.ADC_A_A:
                    _operand1 = Operand.A;
                    return OpCode.AddWithCarry;
                case PrimaryOpCode.ADC_A_B:
                    _operand1 = Operand.B;
                    return OpCode.AddWithCarry;
                case PrimaryOpCode.ADC_A_C:
                    _operand1 = Operand.C;
                    return OpCode.AddWithCarry;
                case PrimaryOpCode.ADC_A_D:
                    _operand1 = Operand.D;
                    return OpCode.AddWithCarry;
                case PrimaryOpCode.ADC_A_E:
                    _operand1 = Operand.E;
                    return OpCode.AddWithCarry;
                case PrimaryOpCode.ADC_A_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.AddWithCarry;
                case PrimaryOpCode.ADC_A_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.AddWithCarry;
                case PrimaryOpCode.ADC_A_n:
                    _timer.MmuByte();
                    _operand1 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.AddWithCarry;
                case PrimaryOpCode.ADC_A_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.AddWithCarry;

                // SUB A, r
                case PrimaryOpCode.SUB_A_A:
                    _operand1 = Operand.A;
                    return OpCode.Subtract;
                case PrimaryOpCode.SUB_A_B:
                    _operand1 = Operand.B;
                    return OpCode.Subtract;
                case PrimaryOpCode.SUB_A_C:
                    _operand1 = Operand.C;
                    return OpCode.Subtract;
                case PrimaryOpCode.SUB_A_D:
                    _operand1 = Operand.D;
                    return OpCode.Subtract;
                case PrimaryOpCode.SUB_A_E:
                    _operand1 = Operand.E;
                    return OpCode.Subtract;
                case PrimaryOpCode.SUB_A_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.Subtract;
                case PrimaryOpCode.SUB_A_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.Subtract;
                case PrimaryOpCode.SUB_A_n:
                    _timer.MmuByte();
                    _operand1 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Subtract;
                case PrimaryOpCode.SUB_A_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Subtract;

                // SBC A, r
                case PrimaryOpCode.SBC_A_A:
                    _operand1 = Operand.A;
                    return OpCode.SubtractWithCarry;
                case PrimaryOpCode.SBC_A_B:
                    _operand1 = Operand.B;
                    return OpCode.SubtractWithCarry;
                case PrimaryOpCode.SBC_A_C:
                    _operand1 = Operand.C;
                    return OpCode.SubtractWithCarry;
                case PrimaryOpCode.SBC_A_D:
                    _operand1 = Operand.D;
                    return OpCode.SubtractWithCarry;
                case PrimaryOpCode.SBC_A_E:
                    _operand1 = Operand.E;
                    return OpCode.SubtractWithCarry;
                case PrimaryOpCode.SBC_A_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.SubtractWithCarry;
                case PrimaryOpCode.SBC_A_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.SubtractWithCarry;
                case PrimaryOpCode.SBC_A_n:
                    _timer.MmuByte();
                    _operand1 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.SubtractWithCarry;
                case PrimaryOpCode.SBC_A_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.SubtractWithCarry;

                // AND A, r
                case PrimaryOpCode.AND_A:
                    _operand1 = Operand.A;
                    return OpCode.And;
                case PrimaryOpCode.AND_B:
                    _operand1 = Operand.B;
                    return OpCode.And;
                case PrimaryOpCode.AND_C:
                    _operand1 = Operand.C;
                    return OpCode.And;
                case PrimaryOpCode.AND_D:
                    _operand1 = Operand.D;
                    return OpCode.And;
                case PrimaryOpCode.AND_E:
                    _operand1 = Operand.E;
                    return OpCode.And;
                case PrimaryOpCode.AND_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.And;
                case PrimaryOpCode.AND_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.And;
                case PrimaryOpCode.AND_n:
                    _timer.MmuByte();
                    _operand1 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.And;
                case PrimaryOpCode.AND_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.And;

                // OR A, r
                case PrimaryOpCode.OR_A:
                    _operand1 = Operand.A;
                    return OpCode.Or;
                case PrimaryOpCode.OR_B:
                    _operand1 = Operand.B;
                    return OpCode.Or;
                case PrimaryOpCode.OR_C:
                    _operand1 = Operand.C;
                    return OpCode.Or;
                case PrimaryOpCode.OR_D:
                    _operand1 = Operand.D;
                    return OpCode.Or;
                case PrimaryOpCode.OR_E:
                    _operand1 = Operand.E;
                    return OpCode.Or;
                case PrimaryOpCode.OR_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.Or;
                case PrimaryOpCode.OR_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.Or;
                case PrimaryOpCode.OR_n:
                    _timer.MmuByte();
                    _operand1 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Or;
                case PrimaryOpCode.OR_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Or;

                // XOR A, r
                case PrimaryOpCode.XOR_A:
                    _operand1 = Operand.A;
                    return OpCode.Xor;
                case PrimaryOpCode.XOR_B:
                    _operand1 = Operand.B;
                    return OpCode.Xor;
                case PrimaryOpCode.XOR_C:
                    _operand1 = Operand.C;
                    return OpCode.Xor;
                case PrimaryOpCode.XOR_D:
                    _operand1 = Operand.D;
                    return OpCode.Xor;
                case PrimaryOpCode.XOR_E:
                    _operand1 = Operand.E;
                    return OpCode.Xor;
                case PrimaryOpCode.XOR_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.Xor;
                case PrimaryOpCode.XOR_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.Xor;
                case PrimaryOpCode.XOR_n:
                    _timer.MmuByte();
                    _operand1 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Xor;
                case PrimaryOpCode.XOR_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Xor;

                // CP A, r
                case PrimaryOpCode.CP_A:
                    _operand1 = Operand.A;
                    return OpCode.Compare;
                case PrimaryOpCode.CP_B:
                    _operand1 = Operand.B;
                    return OpCode.Compare;
                case PrimaryOpCode.CP_C:
                    _operand1 = Operand.C;
                    return OpCode.Compare;
                case PrimaryOpCode.CP_D:
                    _operand1 = Operand.D;
                    return OpCode.Compare;
                case PrimaryOpCode.CP_E:
                    _operand1 = Operand.E;
                    return OpCode.Compare;
                case PrimaryOpCode.CP_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.Compare;
                case PrimaryOpCode.CP_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.Compare;
                case PrimaryOpCode.CP_n:
                    _timer.MmuByte();
                    _operand1 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Compare;
                case PrimaryOpCode.CP_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement();
                    return OpCode.Compare;

                // INC r
                case PrimaryOpCode.INC_A:
                    _operand1 = Operand.A;
                    return OpCode.Increment;
                case PrimaryOpCode.INC_B:
                    _operand1 = Operand.B;
                    return OpCode.Increment;
                case PrimaryOpCode.INC_C:
                    _operand1 = Operand.C;
                    return OpCode.Increment;
                case PrimaryOpCode.INC_D:
                    _operand1 = Operand.D;
                    return OpCode.Increment;
                case PrimaryOpCode.INC_E:
                    _operand1 = Operand.E;
                    return OpCode.Increment;
                case PrimaryOpCode.INC_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.Increment;
                case PrimaryOpCode.INC_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.Increment;
                case PrimaryOpCode.INC_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement().MmuByte().Extend(1);
                    return OpCode.Increment;

                // DEC r
                case PrimaryOpCode.DEC_A:
                    _operand1 = Operand.A;
                    return OpCode.Decrement;
                case PrimaryOpCode.DEC_B:
                    _operand1 = Operand.B;
                    return OpCode.Decrement;
                case PrimaryOpCode.DEC_C:
                    _operand1 = Operand.C;
                    return OpCode.Decrement;
                case PrimaryOpCode.DEC_D:
                    _operand1 = Operand.D;
                    return OpCode.Decrement;
                case PrimaryOpCode.DEC_E:
                    _operand1 = Operand.E;
                    return OpCode.Decrement;
                case PrimaryOpCode.DEC_H:
                    _operand1 = _index.HighRegister;
                    return OpCode.Decrement;
                case PrimaryOpCode.DEC_L:
                    _operand1 = _index.LowRegister;
                    return OpCode.Decrement;
                case PrimaryOpCode.DEC_mHL:
                    _operand1 = _index.Index;
                    UpdateDisplacement().MmuByte().Extend(1);
                    return OpCode.Decrement;

                // ********* 16-Bit Arithmetic *********
                // ADD HL, ss
                case PrimaryOpCode.ADD_HL_BC:
                    _timer.Arithmetic16();
                    _operand1 = _index.Register;
                    _operand2 = Operand.BC;
                    return OpCode.Add16;
                case PrimaryOpCode.ADD_HL_DE:
                    _timer.Arithmetic16();
                    _operand1 = _index.Register;
                    _operand2 = Operand.DE;
                    return OpCode.Add16;
                case PrimaryOpCode.ADD_HL_HL:
                    _timer.Arithmetic16();
                    _operand1 = _operand2 = _index.Register;
                    return OpCode.Add16;
                case PrimaryOpCode.ADD_HL_SP:
                    _timer.Arithmetic16();
                    _operand1 = _index.Register;
                    _operand2 = Operand.SP;
                    return OpCode.Add16;

                // INC ss
                case PrimaryOpCode.INC_BC:
                    _timer.Extend(2);
                    _operand1 = Operand.BC;
                    return OpCode.Increment16;
                case PrimaryOpCode.INC_DE:
                    _timer.Extend(2);
                    _operand1 = Operand.DE;
                    return OpCode.Increment16;
                case PrimaryOpCode.INC_HL:
                    _timer.Extend(2);
                    _operand1 = _index.Register;
                    return OpCode.Increment16;
                case PrimaryOpCode.INC_SP:
                    _timer.Extend(2);
                    _operand1 = Operand.SP;
                    return OpCode.Increment16;

                // DEC ss
                case PrimaryOpCode.DEC_BC:
                    _timer.Extend(2);
                    _operand1 = Operand.BC;
                    return OpCode.Decrement16;
                case PrimaryOpCode.DEC_DE:
                    _timer.Extend(2);
                    _operand1 = Operand.DE;
                    return OpCode.Decrement16;
                case PrimaryOpCode.DEC_HL:
                    _timer.Extend(2);
                    _operand1 = _index.Register;
                    return OpCode.Decrement16;
                case PrimaryOpCode.DEC_SP:
                    _timer.Extend(2);
                    _operand1 = Operand.SP;
                    return OpCode.Decrement16;

                // ********* General-Purpose Arithmetic *********
                // DAA
                case PrimaryOpCode.DAA:
                    return OpCode.DecimalArithmeticAdjust;

                // CPL
                case PrimaryOpCode.CPL:
                    return OpCode.NegateOnesComplement;

                // CCF
                case PrimaryOpCode.CCF:
                    return OpCode.InvertCarryFlag;

                // SCF
                case PrimaryOpCode.SCF:
                    return OpCode.SetCarryFlag;

                // DI
                case PrimaryOpCode.DI:
                    return OpCode.DisableInterrupts;

                // EI
                case PrimaryOpCode.EI:
                    return OpCode.EnableInterrupts;

                // ********* Rotate *********
                // RLCA
                case PrimaryOpCode.RLCA:
                    _operand1 = Operand.A;
                    _opCodeMeta = OpCodeMeta.UseAlternativeFlagAffection;
                    return OpCode.RotateLeftWithCarry;

                // RLA
                case PrimaryOpCode.RLA:
                    _operand1 = Operand.A;
                    _opCodeMeta = OpCodeMeta.UseAlternativeFlagAffection;
                    return OpCode.RotateLeft;

                // RRCA
                case PrimaryOpCode.RRCA:
                    _operand1 = Operand.A;
                    _opCodeMeta = OpCodeMeta.UseAlternativeFlagAffection;
                    return OpCode.RotateRightWithCarry;

                // RRA
                case PrimaryOpCode.RRA:
                    _operand1 = Operand.A;
                    _opCodeMeta = OpCodeMeta.UseAlternativeFlagAffection;
                    return OpCode.RotateRight;

                // ********* Jump *********
                case PrimaryOpCode.JP:
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    return OpCode.Jump;
                case PrimaryOpCode.JP_NZ:
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.NotZero;
                    return OpCode.Jump;
                case PrimaryOpCode.JP_Z:
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Zero;
                    return OpCode.Jump;
                case PrimaryOpCode.JP_NC:
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.NotCarry;
                    return OpCode.Jump;
                case PrimaryOpCode.JP_C:
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Carry;
                    return OpCode.Jump;
                case PrimaryOpCode.JP_PO:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LD (FF00+C), A on GB
                        _timer.MmuByte();
                        _operand1 = Operand.mCl;
                        _operand2 = Operand.A;
                        return OpCode.Load;
                    }
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.ParityOdd;
                    return OpCode.Jump;
                case PrimaryOpCode.JP_PE:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LD (nn), A on GB
                        _timer.MmuWord().MmuByte();
                        _operand1 = Operand.mnn;
                        _operand2 = Operand.A;
                        _decodeMeta = DecodeMeta.WordLiteral;
                        return OpCode.Load;
                    }
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.ParityEven;
                    return OpCode.Jump;
                case PrimaryOpCode.JP_P:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LD A, (FF00+C) on GB
                        _timer.MmuByte();
                        _operand1 = Operand.A;
                        _operand2 = Operand.mCl;
                        return OpCode.Load;
                    }
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Positive;
                    return OpCode.Jump;
                case PrimaryOpCode.JP_M:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LD A, (nn) on GB
                        _timer.MmuWord().MmuByte();
                        _operand1 = Operand.A;
                        _operand2 = Operand.mnn;
                        _decodeMeta = DecodeMeta.WordLiteral;
                        return OpCode.Load;
                    }
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Negative;
                    return OpCode.Jump;

                case PrimaryOpCode.JR:
                    _timer.MmuByte().ApplyDisplacement();
                    _operand1 = Operand.d;
                    _decodeMeta = DecodeMeta.ByteLiteral | DecodeMeta.EndBlock;
                    return OpCode.JumpRelative;
                case PrimaryOpCode.JR_C:
                    _timer.MmuByte();
                    _operand1 = Operand.d;
                    _decodeMeta = DecodeMeta.ByteLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Carry;
                    return OpCode.JumpRelative;
                case PrimaryOpCode.JR_NC:
                    _timer.MmuByte();
                    _operand1 = Operand.d;
                    _decodeMeta = DecodeMeta.ByteLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.NotCarry;
                    return OpCode.JumpRelative;
                case PrimaryOpCode.JR_Z:
                    _timer.MmuByte();
                    _operand1 = Operand.d;
                    _decodeMeta = DecodeMeta.ByteLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Zero;
                    return OpCode.JumpRelative;
                case PrimaryOpCode.JR_NZ:
                    _timer.MmuByte();
                    _operand1 = Operand.d;
                    _decodeMeta = DecodeMeta.ByteLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.NotZero;
                    return OpCode.JumpRelative;

                case PrimaryOpCode.JP_mHL:
                    _operand1 = _index.Register;
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Jump;

                case PrimaryOpCode.DJNZ:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as STOP on GB
                        _decodeMeta = DecodeMeta.EndBlock;
                        _stop = true;
                        return OpCode.Stop;
                    }

                    _timer.Extend(1).MmuByte();
                    _operand1 = Operand.d;
                    _decodeMeta = DecodeMeta.ByteLiteral | DecodeMeta.EndBlock;
                    return OpCode.DecrementJumpRelativeIfNonZero;

                // ********* Call *********
                case PrimaryOpCode.CALL:
                    _timer.MmuWord().Extend(1).MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    return OpCode.Call;
                case PrimaryOpCode.CALL_NZ:
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.NotZero;
                    return OpCode.Call;
                case PrimaryOpCode.CALL_Z:
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Zero;
                    return OpCode.Call;
                case PrimaryOpCode.CALL_NC:
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.NotCarry;
                    return OpCode.Call;
                case PrimaryOpCode.CALL_C:
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Carry;
                    return OpCode.Call;
                case PrimaryOpCode.CALL_PO:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Instruction not on GBCPU
                        return _undefinedInstruction(code);
                    }
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.ParityOdd;
                    return OpCode.Call;
                case PrimaryOpCode.CALL_PE:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Instruction not on GBCPU
                        return _undefinedInstruction(code);
                    }
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.ParityEven;
                    return OpCode.Call;
                case PrimaryOpCode.CALL_P:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Instruction not on GBCPU
                        return _undefinedInstruction(code);
                    }
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Positive;
                    return OpCode.Call;
                case PrimaryOpCode.CALL_M:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Instruction not on GBCPU
                        return _undefinedInstruction(code);
                    }
                    _timer.MmuWord();
                    _operand1 = Operand.nn;
                    _decodeMeta = DecodeMeta.WordLiteral | DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Negative;
                    return OpCode.Call;

                // ********* Return *********
                case PrimaryOpCode.RET:
                    _timer.MmuWord();
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Return;
                case PrimaryOpCode.RET_NZ:
                    _timer.Extend(1);
                    _decodeMeta = DecodeMeta.EndBlock;
                    _flagTest = FlagTest.NotZero;
                    return OpCode.Return;
                case PrimaryOpCode.RET_Z:
                    _timer.Extend(1);
                    _decodeMeta = DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Zero;
                    return OpCode.Return;
                case PrimaryOpCode.RET_NC:
                    _timer.Extend(1);
                    _decodeMeta = DecodeMeta.EndBlock;
                    _flagTest = FlagTest.NotCarry;
                    return OpCode.Return;
                case PrimaryOpCode.RET_C:
                    _timer.Extend(1);
                    _decodeMeta = DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Carry;
                    return OpCode.Return;
                case PrimaryOpCode.RET_PO:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LD (FF00+n), A on GB
                        _timer.MmuWord();
                        _operand1 = Operand.mnl;
                        _operand2 = Operand.A;
                        _decodeMeta = DecodeMeta.ByteLiteral;
                        return OpCode.Load;
                    }

                    _timer.Extend(1);
                    _decodeMeta = DecodeMeta.EndBlock;
                    _flagTest = FlagTest.ParityOdd;
                    return OpCode.Return;
                case PrimaryOpCode.RET_PE:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LD SP, SP+d on GB
                        _timer.MmuByte().Arithmetic16();
                        _operand1 = Operand.SP;
                        _operand2 = Operand.SPd;
                        _decodeMeta = DecodeMeta.Displacement;
                        return OpCode.Load16;
                    }

                    _timer.Extend(1);
                    _decodeMeta = DecodeMeta.EndBlock;
                    _flagTest = FlagTest.ParityEven;
                    return OpCode.Return;
                case PrimaryOpCode.RET_P:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LD A, (FF00+n) on GB
                        _timer.MmuWord();
                        _operand1 = Operand.A;
                        _operand2 = Operand.mnl;
                        _decodeMeta = DecodeMeta.ByteLiteral;
                        return OpCode.Load;
                    }

                    _timer.Extend(1);
                    _decodeMeta = DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Positive;
                    return OpCode.Return;
                case PrimaryOpCode.RET_M:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Runs as LD HL, SP+dd on GB
                        _timer.Arithmetic16();
                        _operand1 = Operand.HL;
                        _operand2 = Operand.SPd;
                        _decodeMeta = DecodeMeta.Displacement;
                        return OpCode.Load16;
                    }

                    _timer.Extend(1);
                    _decodeMeta = DecodeMeta.EndBlock;
                    _flagTest = FlagTest.Negative;
                    return OpCode.Return;

                // ********* Reset *********
                case PrimaryOpCode.RST_00:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.nn;
                    _wordLiteral = 0x0000;
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Reset;
                case PrimaryOpCode.RST_08:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.nn;
                    _wordLiteral = 0x0008;
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Reset;
                case PrimaryOpCode.RST_10:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.nn;
                    _wordLiteral = 0x0010;
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Reset;
                case PrimaryOpCode.RST_18:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.nn;
                    _wordLiteral = 0x0018;
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Reset;
                case PrimaryOpCode.RST_20:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.nn;
                    _wordLiteral = 0x0020;
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Reset;
                case PrimaryOpCode.RST_28:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.nn;
                    _wordLiteral = 0x0028;
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Reset;
                case PrimaryOpCode.RST_30:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.nn;
                    _wordLiteral = 0x0030;
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Reset;
                case PrimaryOpCode.RST_38:
                    _timer.Extend(1).MmuWord();
                    _operand1 = Operand.nn;
                    _wordLiteral = 0x0038;
                    _decodeMeta = DecodeMeta.EndBlock;
                    return OpCode.Reset;

                // ********* IO *********
                case PrimaryOpCode.IN_A_n:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Instruction not on GBCPU
                        return _undefinedInstruction(code);
                    }
                    _timer.MmuByte().Io();
                    _operand1 = Operand.A;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Input;
                case PrimaryOpCode.OUT_A_n:
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        // Instruction not on GBCPU
                        return _undefinedInstruction(code);
                    }
                    _timer.MmuByte().Io();
                    _operand1 = Operand.A;
                    _operand2 = Operand.n;
                    _decodeMeta = DecodeMeta.ByteLiteral;
                    return OpCode.Output;
                default:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
            }
        }
    }
}