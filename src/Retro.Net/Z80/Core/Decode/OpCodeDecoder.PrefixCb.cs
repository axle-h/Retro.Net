using System;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.OpCodes;

namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// Core op-code decoder functions for op-codes prefixed with 0xCB.
    /// </summary>
    public partial class OpCodeDecoder
    {
        /// <summary>
        /// Fixes the timings and operands of a CB and DD/FD prefixed operation.
        /// This is necessary due to the undocumented operations e.g. PrefixDD, PrefixCB, displacement, BIT_0_A.
        /// Which actually does a bit test on the displaced index followed by an autocopy to A.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private OpCode FixPrefixDdFdPrefixCbResult(OpCode result)
        {
            if (_operand1 == _index.Index)
            {
                // Documented operation, no autocopy
                return result;
            }
            
            // Add index operand
            _operand2 = _operand1;
            _operand1 = _index.Index;

            if (result != OpCode.BitTest)
            {
                // Only BIT has no autocopy
                // Add autocopy timings
                _opCodeMeta = OpCodeMeta.AutoCopy;
                _timer.IndexAndMmuByte(true);
            }

            _timer.ApplyDisplacement().MmuByte();
            return result;
        }

        /// <summary>
        /// Decodes an op-code that has been prefixed with 0xCB.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private OpCode DecodePrefixCb()
        {
            var code = (PrefixCbOpCode) _prefetch.NextByte();

            _timer.Nop();

            switch (code)
            {
                // ********* Rotate *********
                // RLC r
                case PrefixCbOpCode.RLC_A:
                    _operand1 = Operand.A;
                    return OpCode.RotateLeftWithCarry;
                case PrefixCbOpCode.RLC_B:
                    _operand1 = Operand.B;
                    return OpCode.RotateLeftWithCarry;
                case PrefixCbOpCode.RLC_C:
                    _operand1 = Operand.C;
                    return OpCode.RotateLeftWithCarry;
                case PrefixCbOpCode.RLC_D:
                    _operand1 = Operand.D;
                    return OpCode.RotateLeftWithCarry;
                case PrefixCbOpCode.RLC_E:
                    _operand1 = Operand.E;
                    return OpCode.RotateLeftWithCarry;
                case PrefixCbOpCode.RLC_H:
                    _operand1 = Operand.H;
                    return OpCode.RotateLeftWithCarry;
                case PrefixCbOpCode.RLC_L:
                    _operand1 = Operand.L;
                    return OpCode.RotateLeftWithCarry;
                case PrefixCbOpCode.RLC_mHL:
                    _timer.IndexAndMmuByte(_index.IsDisplaced);
                    if (!_index.IsDisplaced)
                    {
                        _timer.Extend(1);
                    }
                    _operand1 = _index.Index;
                    return OpCode.RotateLeftWithCarry;

                // RL r
                case PrefixCbOpCode.RL_A:
                    _operand1 = Operand.A;
                    return OpCode.RotateLeft;
                case PrefixCbOpCode.RL_B:
                    _operand1 = Operand.B;
                    return OpCode.RotateLeft;
                case PrefixCbOpCode.RL_C:
                    _operand1 = Operand.C;
                    return OpCode.RotateLeft;
                case PrefixCbOpCode.RL_D:
                    _operand1 = Operand.D;
                    return OpCode.RotateLeft;
                case PrefixCbOpCode.RL_E:
                    _operand1 = Operand.E;
                    return OpCode.RotateLeft;
                case PrefixCbOpCode.RL_H:
                    _operand1 = Operand.H;
                    return OpCode.RotateLeft;
                case PrefixCbOpCode.RL_L:
                    _operand1 = Operand.L;
                    return OpCode.RotateLeft;
                case PrefixCbOpCode.RL_mHL:
                    _timer.IndexAndMmuByte(_index.IsDisplaced);
                    if (!_index.IsDisplaced)
                    {
                        _timer.Extend(1);
                    }
                    _operand1 = _index.Index;
                    return OpCode.RotateLeft;

                // RRC r
                case PrefixCbOpCode.RRC_A:
                    _operand1 = Operand.A;
                    return OpCode.RotateRightWithCarry;
                case PrefixCbOpCode.RRC_B:
                    _operand1 = Operand.B;
                    return OpCode.RotateRightWithCarry;
                case PrefixCbOpCode.RRC_C:
                    _operand1 = Operand.C;
                    return OpCode.RotateRightWithCarry;
                case PrefixCbOpCode.RRC_D:
                    _operand1 = Operand.D;
                    return OpCode.RotateRightWithCarry;
                case PrefixCbOpCode.RRC_E:
                    _operand1 = Operand.E;
                    return OpCode.RotateRightWithCarry;
                case PrefixCbOpCode.RRC_H:
                    _operand1 = Operand.H;
                    return OpCode.RotateRightWithCarry;
                case PrefixCbOpCode.RRC_L:
                    _operand1 = Operand.L;
                    return OpCode.RotateRightWithCarry;
                case PrefixCbOpCode.RRC_mHL:
                    _timer.IndexAndMmuByte(_index.IsDisplaced);
                    if (!_index.IsDisplaced)
                    {
                        _timer.Extend(1);
                    }
                    _operand1 = _index.Index;
                    return OpCode.RotateRightWithCarry;

                // RR r
                case PrefixCbOpCode.RR_A:
                    _operand1 = Operand.A;
                    return OpCode.RotateRight;
                case PrefixCbOpCode.RR_B:
                    _operand1 = Operand.B;
                    return OpCode.RotateRight;
                case PrefixCbOpCode.RR_C:
                    _operand1 = Operand.C;
                    return OpCode.RotateRight;
                case PrefixCbOpCode.RR_D:
                    _operand1 = Operand.D;
                    return OpCode.RotateRight;
                case PrefixCbOpCode.RR_E:
                    _operand1 = Operand.E;
                    return OpCode.RotateRight;
                case PrefixCbOpCode.RR_H:
                    _operand1 = Operand.H;
                    return OpCode.RotateRight;
                case PrefixCbOpCode.RR_L:
                    _operand1 = Operand.L;
                    return OpCode.RotateRight;
                case PrefixCbOpCode.RR_mHL:
                    _timer.IndexAndMmuByte(_index.IsDisplaced);
                    if (!_index.IsDisplaced)
                    {
                        _timer.Extend(1);
                    }
                    _operand1 = _index.Index;
                    return OpCode.RotateRight;

                // ********* Shift *********
                // SLA r
                case PrefixCbOpCode.SLA_A:
                    _operand1 = Operand.A;
                    return OpCode.ShiftLeft;
                case PrefixCbOpCode.SLA_B:
                    _operand1 = Operand.B;
                    return OpCode.ShiftLeft;
                case PrefixCbOpCode.SLA_C:
                    _operand1 = Operand.C;
                    return OpCode.ShiftLeft;
                case PrefixCbOpCode.SLA_D:
                    _operand1 = Operand.D;
                    return OpCode.ShiftLeft;
                case PrefixCbOpCode.SLA_E:
                    _operand1 = Operand.E;
                    return OpCode.ShiftLeft;
                case PrefixCbOpCode.SLA_H:
                    _operand1 = Operand.H;
                    return OpCode.ShiftLeft;
                case PrefixCbOpCode.SLA_L:
                    _operand1 = Operand.L;
                    return OpCode.ShiftLeft;
                case PrefixCbOpCode.SLA_mHL:
                    _timer.IndexAndMmuByte(_index.IsDisplaced);
                    if (!_index.IsDisplaced)
                    {
                        _timer.Extend(1);
                    }
                    _operand1 = _index.Index;
                    return OpCode.ShiftLeft;

                // SLS r (undocumented)
                // Runs as SWAP on GB
                case PrefixCbOpCode.SLS_A:
                    _operand1 = Operand.A;
                    return _cpuMode == CpuMode.GameBoy ? OpCode.Swap : OpCode.ShiftLeftSet;
                case PrefixCbOpCode.SLS_B:
                    _operand1 = Operand.B;
                    return _cpuMode == CpuMode.GameBoy ? OpCode.Swap : OpCode.ShiftLeftSet;
                case PrefixCbOpCode.SLS_C:
                    _operand1 = Operand.C;
                    return _cpuMode == CpuMode.GameBoy ? OpCode.Swap : OpCode.ShiftLeftSet;
                case PrefixCbOpCode.SLS_D:
                    _operand1 = Operand.D;
                    return _cpuMode == CpuMode.GameBoy ? OpCode.Swap : OpCode.ShiftLeftSet;
                case PrefixCbOpCode.SLS_E:
                    _operand1 = Operand.E;
                    return _cpuMode == CpuMode.GameBoy ? OpCode.Swap : OpCode.ShiftLeftSet;
                case PrefixCbOpCode.SLS_H:
                    _operand1 = Operand.H;
                    return _cpuMode == CpuMode.GameBoy ? OpCode.Swap : OpCode.ShiftLeftSet;
                case PrefixCbOpCode.SLS_L:
                    _operand1 = Operand.L;
                    return _cpuMode == CpuMode.GameBoy ? OpCode.Swap : OpCode.ShiftLeftSet;
                case PrefixCbOpCode.SLS_mHL:
                    _timer.IndexAndMmuByte(_index.IsDisplaced);
                    if (!_index.IsDisplaced)
                    {
                        _timer.Extend(1);
                    }
                    _operand1 = _index.Index;
                    return _cpuMode == CpuMode.GameBoy ? OpCode.Swap : OpCode.ShiftLeftSet;

                // SRA r
                case PrefixCbOpCode.SRA_A:
                    _operand1 = Operand.A;
                    return OpCode.ShiftRight;
                case PrefixCbOpCode.SRA_B:
                    _operand1 = Operand.B;
                    return OpCode.ShiftRight;
                case PrefixCbOpCode.SRA_C:
                    _operand1 = Operand.C;
                    return OpCode.ShiftRight;
                case PrefixCbOpCode.SRA_D:
                    _operand1 = Operand.D;
                    return OpCode.ShiftRight;
                case PrefixCbOpCode.SRA_E:
                    _operand1 = Operand.E;
                    return OpCode.ShiftRight;
                case PrefixCbOpCode.SRA_H:
                    _operand1 = Operand.H;
                    return OpCode.ShiftRight;
                case PrefixCbOpCode.SRA_L:
                    _operand1 = Operand.L;
                    return OpCode.ShiftRight;
                case PrefixCbOpCode.SRA_mHL:
                    _timer.IndexAndMmuByte(_index.IsDisplaced);
                    if (!_index.IsDisplaced)
                    {
                        _timer.Extend(1);
                    }
                    _operand1 = _index.Index;
                    return OpCode.ShiftRight;

                // SRL r
                case PrefixCbOpCode.SRL_A:
                    _operand1 = Operand.A;
                    return OpCode.ShiftRightLogical;
                case PrefixCbOpCode.SRL_B:
                    _operand1 = Operand.B;
                    return OpCode.ShiftRightLogical;
                case PrefixCbOpCode.SRL_C:
                    _operand1 = Operand.C;
                    return OpCode.ShiftRightLogical;
                case PrefixCbOpCode.SRL_D:
                    _operand1 = Operand.D;
                    return OpCode.ShiftRightLogical;
                case PrefixCbOpCode.SRL_E:
                    _operand1 = Operand.E;
                    return OpCode.ShiftRightLogical;
                case PrefixCbOpCode.SRL_H:
                    _operand1 = Operand.H;
                    return OpCode.ShiftRightLogical;
                case PrefixCbOpCode.SRL_L:
                    _operand1 = Operand.L;
                    return OpCode.ShiftRightLogical;
                case PrefixCbOpCode.SRL_mHL:
                    _timer.IndexAndMmuByte(_index.IsDisplaced);
                    if (!_index.IsDisplaced)
                    {
                        _timer.Extend(1);
                    }
                    _operand1 = _index.Index;
                    return OpCode.ShiftRightLogical;

                // ********* Bit Test, Set & Reset *********
                // BIT r
                case PrefixCbOpCode.BIT_0_B:
                    return BitTest(Operand.B, 0);
                case PrefixCbOpCode.BIT_1_B:
                    return BitTest(Operand.B, 1);
                case PrefixCbOpCode.BIT_2_B:
                    return BitTest(Operand.B, 2);
                case PrefixCbOpCode.BIT_3_B:
                    return BitTest(Operand.B, 3);
                case PrefixCbOpCode.BIT_4_B:
                    return BitTest(Operand.B, 4);
                case PrefixCbOpCode.BIT_5_B:
                    return BitTest(Operand.B, 5);
                case PrefixCbOpCode.BIT_6_B:
                    return BitTest(Operand.B, 6);
                case PrefixCbOpCode.BIT_7_B:
                    return BitTest(Operand.B, 7);
                case PrefixCbOpCode.BIT_0_C:
                    return BitTest(Operand.C, 0);
                case PrefixCbOpCode.BIT_1_C:
                    return BitTest(Operand.C, 1);
                case PrefixCbOpCode.BIT_2_C:
                    return BitTest(Operand.C, 2);
                case PrefixCbOpCode.BIT_3_C:
                    return BitTest(Operand.C, 3);
                case PrefixCbOpCode.BIT_4_C:
                    return BitTest(Operand.C, 4);
                case PrefixCbOpCode.BIT_5_C:
                    return BitTest(Operand.C, 5);
                case PrefixCbOpCode.BIT_6_C:
                    return BitTest(Operand.C, 6);
                case PrefixCbOpCode.BIT_7_C:
                    return BitTest(Operand.C, 7);
                case PrefixCbOpCode.BIT_0_D:
                    return BitTest(Operand.D, 0);
                case PrefixCbOpCode.BIT_1_D:
                    return BitTest(Operand.D, 1);
                case PrefixCbOpCode.BIT_2_D:
                    return BitTest(Operand.D, 2);
                case PrefixCbOpCode.BIT_3_D:
                    return BitTest(Operand.D, 3);
                case PrefixCbOpCode.BIT_4_D:
                    return BitTest(Operand.D, 4);
                case PrefixCbOpCode.BIT_5_D:
                    return BitTest(Operand.D, 5);
                case PrefixCbOpCode.BIT_6_D:
                    return BitTest(Operand.D, 6);
                case PrefixCbOpCode.BIT_7_D:
                    return BitTest(Operand.D, 7);
                case PrefixCbOpCode.BIT_0_E:
                    return BitTest(Operand.E, 0);
                case PrefixCbOpCode.BIT_1_E:
                    return BitTest(Operand.E, 1);
                case PrefixCbOpCode.BIT_2_E:
                    return BitTest(Operand.E, 2);
                case PrefixCbOpCode.BIT_3_E:
                    return BitTest(Operand.E, 3);
                case PrefixCbOpCode.BIT_4_E:
                    return BitTest(Operand.E, 4);
                case PrefixCbOpCode.BIT_5_E:
                    return BitTest(Operand.E, 5);
                case PrefixCbOpCode.BIT_6_E:
                    return BitTest(Operand.E, 6);
                case PrefixCbOpCode.BIT_7_E:
                    return BitTest(Operand.E, 7);
                case PrefixCbOpCode.BIT_0_H:
                    return BitTest(Operand.H, 0);
                case PrefixCbOpCode.BIT_1_H:
                    return BitTest(Operand.H, 1);
                case PrefixCbOpCode.BIT_2_H:
                    return BitTest(Operand.H, 2);
                case PrefixCbOpCode.BIT_3_H:
                    return BitTest(Operand.H, 3);
                case PrefixCbOpCode.BIT_4_H:
                    return BitTest(Operand.H, 4);
                case PrefixCbOpCode.BIT_5_H:
                    return BitTest(Operand.H, 5);
                case PrefixCbOpCode.BIT_6_H:
                    return BitTest(Operand.H, 6);
                case PrefixCbOpCode.BIT_7_H:
                    return BitTest(Operand.H, 7);
                case PrefixCbOpCode.BIT_0_L:
                    return BitTest(Operand.L, 0);
                case PrefixCbOpCode.BIT_1_L:
                    return BitTest(Operand.L, 1);
                case PrefixCbOpCode.BIT_2_L:
                    return BitTest(Operand.L, 2);
                case PrefixCbOpCode.BIT_3_L:
                    return BitTest(Operand.L, 3);
                case PrefixCbOpCode.BIT_4_L:
                    return BitTest(Operand.L, 4);
                case PrefixCbOpCode.BIT_5_L:
                    return BitTest(Operand.L, 5);
                case PrefixCbOpCode.BIT_6_L:
                    return BitTest(Operand.L, 6);
                case PrefixCbOpCode.BIT_7_L:
                    return BitTest(Operand.L, 7);
                case PrefixCbOpCode.BIT_0_mHL:
                    return BitTestFromIndex(0);
                case PrefixCbOpCode.BIT_1_mHL:
                    return BitTestFromIndex(1);
                case PrefixCbOpCode.BIT_2_mHL:
                    return BitTestFromIndex(2);
                case PrefixCbOpCode.BIT_3_mHL:
                    return BitTestFromIndex(3);
                case PrefixCbOpCode.BIT_4_mHL:
                    return BitTestFromIndex(4);
                case PrefixCbOpCode.BIT_5_mHL:
                    return BitTestFromIndex(5);
                case PrefixCbOpCode.BIT_6_mHL:
                    return BitTestFromIndex(6);
                case PrefixCbOpCode.BIT_7_mHL:
                    return BitTestFromIndex(7);
                case PrefixCbOpCode.BIT_0_A:
                    return BitTest(Operand.A, 0);
                case PrefixCbOpCode.BIT_1_A:
                    return BitTest(Operand.A, 1);
                case PrefixCbOpCode.BIT_2_A:
                    return BitTest(Operand.A, 2);
                case PrefixCbOpCode.BIT_3_A:
                    return BitTest(Operand.A, 3);
                case PrefixCbOpCode.BIT_4_A:
                    return BitTest(Operand.A, 4);
                case PrefixCbOpCode.BIT_5_A:
                    return BitTest(Operand.A, 5);
                case PrefixCbOpCode.BIT_6_A:
                    return BitTest(Operand.A, 6);
                case PrefixCbOpCode.BIT_7_A:
                    return BitTest(Operand.A, 7);

                // SET r
                case PrefixCbOpCode.SET_0_B:
                    return BitSet(Operand.B, 0);
                case PrefixCbOpCode.SET_1_B:
                    return BitSet(Operand.B, 1);
                case PrefixCbOpCode.SET_2_B:
                    return BitSet(Operand.B, 2);
                case PrefixCbOpCode.SET_3_B:
                    return BitSet(Operand.B, 3);
                case PrefixCbOpCode.SET_4_B:
                    return BitSet(Operand.B, 4);
                case PrefixCbOpCode.SET_5_B:
                    return BitSet(Operand.B, 5);
                case PrefixCbOpCode.SET_6_B:
                    return BitSet(Operand.B, 6);
                case PrefixCbOpCode.SET_7_B:
                    return BitSet(Operand.B, 7);
                case PrefixCbOpCode.SET_0_C:
                    return BitSet(Operand.C, 0);
                case PrefixCbOpCode.SET_1_C:
                    return BitSet(Operand.C, 1);
                case PrefixCbOpCode.SET_2_C:
                    return BitSet(Operand.C, 2);
                case PrefixCbOpCode.SET_3_C:
                    return BitSet(Operand.C, 3);
                case PrefixCbOpCode.SET_4_C:
                    return BitSet(Operand.C, 4);
                case PrefixCbOpCode.SET_5_C:
                    return BitSet(Operand.C, 5);
                case PrefixCbOpCode.SET_6_C:
                    return BitSet(Operand.C, 6);
                case PrefixCbOpCode.SET_7_C:
                    return BitSet(Operand.C, 7);
                case PrefixCbOpCode.SET_0_D:
                    return BitSet(Operand.D, 0);
                case PrefixCbOpCode.SET_1_D:
                    return BitSet(Operand.D, 1);
                case PrefixCbOpCode.SET_2_D:
                    return BitSet(Operand.D, 2);
                case PrefixCbOpCode.SET_3_D:
                    return BitSet(Operand.D, 3);
                case PrefixCbOpCode.SET_4_D:
                    return BitSet(Operand.D, 4);
                case PrefixCbOpCode.SET_5_D:
                    return BitSet(Operand.D, 5);
                case PrefixCbOpCode.SET_6_D:
                    return BitSet(Operand.D, 6);
                case PrefixCbOpCode.SET_7_D:
                    return BitSet(Operand.D, 7);
                case PrefixCbOpCode.SET_0_E:
                    return BitSet(Operand.E, 0);
                case PrefixCbOpCode.SET_1_E:
                    return BitSet(Operand.E, 1);
                case PrefixCbOpCode.SET_2_E:
                    return BitSet(Operand.E, 2);
                case PrefixCbOpCode.SET_3_E:
                    return BitSet(Operand.E, 3);
                case PrefixCbOpCode.SET_4_E:
                    return BitSet(Operand.E, 4);
                case PrefixCbOpCode.SET_5_E:
                    return BitSet(Operand.E, 5);
                case PrefixCbOpCode.SET_6_E:
                    return BitSet(Operand.E, 6);
                case PrefixCbOpCode.SET_7_E:
                    return BitSet(Operand.E, 7);
                case PrefixCbOpCode.SET_0_H:
                    return BitSet(Operand.H, 0);
                case PrefixCbOpCode.SET_1_H:
                    return BitSet(Operand.H, 1);
                case PrefixCbOpCode.SET_2_H:
                    return BitSet(Operand.H, 2);
                case PrefixCbOpCode.SET_3_H:
                    return BitSet(Operand.H, 3);
                case PrefixCbOpCode.SET_4_H:
                    return BitSet(Operand.H, 4);
                case PrefixCbOpCode.SET_5_H:
                    return BitSet(Operand.H, 5);
                case PrefixCbOpCode.SET_6_H:
                    return BitSet(Operand.H, 6);
                case PrefixCbOpCode.SET_7_H:
                    return BitSet(Operand.H, 7);
                case PrefixCbOpCode.SET_0_L:
                    return BitSet(Operand.L, 0);
                case PrefixCbOpCode.SET_1_L:
                    return BitSet(Operand.L, 1);
                case PrefixCbOpCode.SET_2_L:
                    return BitSet(Operand.L, 2);
                case PrefixCbOpCode.SET_3_L:
                    return BitSet(Operand.L, 3);
                case PrefixCbOpCode.SET_4_L:
                    return BitSet(Operand.L, 4);
                case PrefixCbOpCode.SET_5_L:
                    return BitSet(Operand.L, 5);
                case PrefixCbOpCode.SET_6_L:
                    return BitSet(Operand.L, 6);
                case PrefixCbOpCode.SET_7_L:
                    return BitSet(Operand.L, 7);
                case PrefixCbOpCode.SET_0_mHL:
                    return BitSetFromIndex(0);
                case PrefixCbOpCode.SET_1_mHL:
                    return BitSetFromIndex(1);
                case PrefixCbOpCode.SET_2_mHL:
                    return BitSetFromIndex(2);
                case PrefixCbOpCode.SET_3_mHL:
                    return BitSetFromIndex(3);
                case PrefixCbOpCode.SET_4_mHL:
                    return BitSetFromIndex(4);
                case PrefixCbOpCode.SET_5_mHL:
                    return BitSetFromIndex(5);
                case PrefixCbOpCode.SET_6_mHL:
                    return BitSetFromIndex(6);
                case PrefixCbOpCode.SET_7_mHL:
                    return BitSetFromIndex(7);
                case PrefixCbOpCode.SET_0_A:
                    return BitSet(Operand.A, 0);
                case PrefixCbOpCode.SET_1_A:
                    return BitSet(Operand.A, 1);
                case PrefixCbOpCode.SET_2_A:
                    return BitSet(Operand.A, 2);
                case PrefixCbOpCode.SET_3_A:
                    return BitSet(Operand.A, 3);
                case PrefixCbOpCode.SET_4_A:
                    return BitSet(Operand.A, 4);
                case PrefixCbOpCode.SET_5_A:
                    return BitSet(Operand.A, 5);
                case PrefixCbOpCode.SET_6_A:
                    return BitSet(Operand.A, 6);
                case PrefixCbOpCode.SET_7_A:
                    return BitSet(Operand.A, 7);

                // RES r
                case PrefixCbOpCode.RES_0_B:
                    return BitReset(Operand.B, 0);
                case PrefixCbOpCode.RES_1_B:
                    return BitReset(Operand.B, 1);
                case PrefixCbOpCode.RES_2_B:
                    return BitReset(Operand.B, 2);
                case PrefixCbOpCode.RES_3_B:
                    return BitReset(Operand.B, 3);
                case PrefixCbOpCode.RES_4_B:
                    return BitReset(Operand.B, 4);
                case PrefixCbOpCode.RES_5_B:
                    return BitReset(Operand.B, 5);
                case PrefixCbOpCode.RES_6_B:
                    return BitReset(Operand.B, 6);
                case PrefixCbOpCode.RES_7_B:
                    return BitReset(Operand.B, 7);
                case PrefixCbOpCode.RES_0_C:
                    return BitReset(Operand.C, 0);
                case PrefixCbOpCode.RES_1_C:
                    return BitReset(Operand.C, 1);
                case PrefixCbOpCode.RES_2_C:
                    return BitReset(Operand.C, 2);
                case PrefixCbOpCode.RES_3_C:
                    return BitReset(Operand.C, 3);
                case PrefixCbOpCode.RES_4_C:
                    return BitReset(Operand.C, 4);
                case PrefixCbOpCode.RES_5_C:
                    return BitReset(Operand.C, 5);
                case PrefixCbOpCode.RES_6_C:
                    return BitReset(Operand.C, 6);
                case PrefixCbOpCode.RES_7_C:
                    return BitReset(Operand.C, 7);
                case PrefixCbOpCode.RES_0_D:
                    return BitReset(Operand.D, 0);
                case PrefixCbOpCode.RES_1_D:
                    return BitReset(Operand.D, 1);
                case PrefixCbOpCode.RES_2_D:
                    return BitReset(Operand.D, 2);
                case PrefixCbOpCode.RES_3_D:
                    return BitReset(Operand.D, 3);
                case PrefixCbOpCode.RES_4_D:
                    return BitReset(Operand.D, 4);
                case PrefixCbOpCode.RES_5_D:
                    return BitReset(Operand.D, 5);
                case PrefixCbOpCode.RES_6_D:
                    return BitReset(Operand.D, 6);
                case PrefixCbOpCode.RES_7_D:
                    return BitReset(Operand.D, 7);
                case PrefixCbOpCode.RES_0_E:
                    return BitReset(Operand.E, 0);
                case PrefixCbOpCode.RES_1_E:
                    return BitReset(Operand.E, 1);
                case PrefixCbOpCode.RES_2_E:
                    return BitReset(Operand.E, 2);
                case PrefixCbOpCode.RES_3_E:
                    return BitReset(Operand.E, 3);
                case PrefixCbOpCode.RES_4_E:
                    return BitReset(Operand.E, 4);
                case PrefixCbOpCode.RES_5_E:
                    return BitReset(Operand.E, 5);
                case PrefixCbOpCode.RES_6_E:
                    return BitReset(Operand.E, 6);
                case PrefixCbOpCode.RES_7_E:
                    return BitReset(Operand.E, 7);
                case PrefixCbOpCode.RES_0_H:
                    return BitReset(Operand.H, 0);
                case PrefixCbOpCode.RES_1_H:
                    return BitReset(Operand.H, 1);
                case PrefixCbOpCode.RES_2_H:
                    return BitReset(Operand.H, 2);
                case PrefixCbOpCode.RES_3_H:
                    return BitReset(Operand.H, 3);
                case PrefixCbOpCode.RES_4_H:
                    return BitReset(Operand.H, 4);
                case PrefixCbOpCode.RES_5_H:
                    return BitReset(Operand.H, 5);
                case PrefixCbOpCode.RES_6_H:
                    return BitReset(Operand.H, 6);
                case PrefixCbOpCode.RES_7_H:
                    return BitReset(Operand.H, 7);
                case PrefixCbOpCode.RES_0_L:
                    return BitReset(Operand.L, 0);
                case PrefixCbOpCode.RES_1_L:
                    return BitReset(Operand.L, 1);
                case PrefixCbOpCode.RES_2_L:
                    return BitReset(Operand.L, 2);
                case PrefixCbOpCode.RES_3_L:
                    return BitReset(Operand.L, 3);
                case PrefixCbOpCode.RES_4_L:
                    return BitReset(Operand.L, 4);
                case PrefixCbOpCode.RES_5_L:
                    return BitReset(Operand.L, 5);
                case PrefixCbOpCode.RES_6_L:
                    return BitReset(Operand.L, 6);
                case PrefixCbOpCode.RES_7_L:
                    return BitReset(Operand.L, 7);
                case PrefixCbOpCode.RES_0_mHL:
                    return BitResetFromIndex(0);
                case PrefixCbOpCode.RES_1_mHL:
                    return BitResetFromIndex(1);
                case PrefixCbOpCode.RES_2_mHL:
                    return BitResetFromIndex(2);
                case PrefixCbOpCode.RES_3_mHL:
                    return BitResetFromIndex(3);
                case PrefixCbOpCode.RES_4_mHL:
                    return BitResetFromIndex(4);
                case PrefixCbOpCode.RES_5_mHL:
                    return BitResetFromIndex(5);
                case PrefixCbOpCode.RES_6_mHL:
                    return BitResetFromIndex(6);
                case PrefixCbOpCode.RES_7_mHL:
                    return BitResetFromIndex(7);
                case PrefixCbOpCode.RES_0_A:
                    return BitReset(Operand.A, 0);
                case PrefixCbOpCode.RES_1_A:
                    return BitReset(Operand.A, 1);
                case PrefixCbOpCode.RES_2_A:
                    return BitReset(Operand.A, 2);
                case PrefixCbOpCode.RES_3_A:
                    return BitReset(Operand.A, 3);
                case PrefixCbOpCode.RES_4_A:
                    return BitReset(Operand.A, 4);
                case PrefixCbOpCode.RES_5_A:
                    return BitReset(Operand.A, 5);
                case PrefixCbOpCode.RES_6_A:
                    return BitReset(Operand.A, 6);
                case PrefixCbOpCode.RES_7_A:
                    return BitReset(Operand.A, 7);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Sets up index based operands and returns a bit reset opcode for the specified bit.
        /// </summary>
        /// <param name="bit">The bit.</param>
        /// <returns></returns>
        private OpCode BitResetFromIndex(int bit)
        {
            _timer.IndexAndMmuByte(_index.IsDisplaced);
            if (!_index.IsDisplaced)
            {
                _timer.Extend(1);
            }

            _operand1 = _index.Index;
            _byteLiteral = (byte) bit;
            return OpCode.BitReset;
        }

        /// <summary>
        /// Sets up register based operands and returns a bit reset opcode for the specified bit.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="bit">The bit.</param>
        /// <returns></returns>
        private OpCode BitReset(Operand register, int bit)
        {
            if (_index.IsDisplaced)
            {
                _timer.MmuWord().Extend(1);
            }

            _operand1 = register;
            _byteLiteral = (byte) bit;
            return OpCode.BitReset;
        }

        /// <summary>
        /// Sets up index based operands and returns a bit set opcode for the specified bit.
        /// </summary>
        /// <param name="bit">The bit.</param>
        /// <returns></returns>
        private OpCode BitSetFromIndex(int bit)
        {
            _timer.IndexAndMmuByte(_index.IsDisplaced);
            if (!_index.IsDisplaced)
            {
                _timer.Extend(1);
            }

            _operand1 = _index.Index;
            _byteLiteral = (byte) bit;
            return OpCode.BitSet;
        }

        /// <summary>
        /// Sets up register based operands and returns a bit set opcode for the specified bit.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="bit">The bit.</param>
        /// <returns></returns>
        private OpCode BitSet(Operand register, int bit)
        {
            if (_index.IsDisplaced)
            {
                _timer.MmuWord().Extend(1);
            }

            _operand1 = register;
            _byteLiteral = (byte) bit;
            return OpCode.BitSet;
        }

        /// <summary>
        /// Sets up index based operands and returns a bit test opcode for the specified bit.
        /// </summary>
        /// <param name="bit">The bit.</param>
        /// <returns></returns>
        private OpCode BitTestFromIndex(int bit)
        {
            if (_index.IsDisplaced)
            {
                _timer.ApplyDisplacement().MmuByte();
            }
            else
            {
                _timer.MmuByte().Extend(1);
            }

            _operand1 = _index.Index;
            _byteLiteral = (byte) bit;
            return OpCode.BitTest;
        }

        /// <summary>
        /// Sets up register based operands and returns a bit test opcode for the specified bit.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="bit">The bit.</param>
        /// <returns></returns>
        private OpCode BitTest(Operand register, int bit)
        {
            _operand1 = register;
            _byteLiteral = (byte) bit;
            return OpCode.BitTest;
        }
    }
}