using Bogus;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class GameBoySpecificOpCodesTests
    {
        private static readonly Randomizer Rng = new Faker().Random;

        [Theory]
        [InlineData(GameBoyPrefixCbOpCode.SWAP_A, Operand.A)]
        [InlineData(GameBoyPrefixCbOpCode.SWAP_B, Operand.B)]
        [InlineData(GameBoyPrefixCbOpCode.SWAP_C, Operand.C)]
        [InlineData(GameBoyPrefixCbOpCode.SWAP_E, Operand.E)]
        [InlineData(GameBoyPrefixCbOpCode.SWAP_H, Operand.H)]
        [InlineData(GameBoyPrefixCbOpCode.SWAP_L, Operand.L)]
        public void SWAP_r(GameBoyPrefixCbOpCode op, Operand r)
        {
            using (var fixture = new DecodeFixture(2, 8, PrimaryOpCode.Prefix_CB, op).OnlyGameboy())
            {
                fixture.Expected.OpCode(OpCode.Swap).Operands(r);
            }
        }

        [Fact] public void LD_A_mFF00C() => TestPrimary(GameBoyPrimaryOpCode.LD_A_mFF00C, Operand.A, Operand.mCl);

        [Fact] public void LD_mFF00C_A() => TestPrimary(GameBoyPrimaryOpCode.LD_mFF00C_A, Operand.mCl, Operand.A);

        [Fact] public void LD_A_mFF00n() => TestByteLiteral(GameBoyPrimaryOpCode.LD_A_mFF00n, Operand.A, Operand.mnl);

        [Fact] public void LD_mFF00n_A() => TestByteLiteral(GameBoyPrimaryOpCode.LD_mFF00n_A, Operand.mnl, Operand.A);

        [Fact] public void LD_A_mnn() => TestWordLiteral(GameBoyPrimaryOpCode.LD_A_mnn, Operand.A, Operand.mnn);

        [Fact] public void LD_mnn_A() => TestWordLiteral(GameBoyPrimaryOpCode.LD_mnn_A, Operand.mnn, Operand.A);

        [Fact] public void LD_HL_SPd() => TestDisplacedIndex(GameBoyPrimaryOpCode.LD_HL_SPd, Operand.HL, Operand.SPd);

        [Fact] public void LD_mnn_SP() => TestWordLiteral16(GameBoyPrimaryOpCode.LD_mnn_SP, Operand.mnn, Operand.SP);

        [Fact] public void LD_SP_SPd() => TestDisplacedIndex(GameBoyPrimaryOpCode.ADD_SP_d, Operand.SP, Operand.SPd, 4, 14);

        [Fact] public void LDD_A_mHL() => TestPrimary(GameBoyPrimaryOpCode.LDD_A_mHL, Operand.A, Operand.mHL, OpCode.LoadDecrement);

        [Fact] public void LDD_mHL_A() => TestPrimary(GameBoyPrimaryOpCode.LDD_mHL_A, Operand.mHL, Operand.A, OpCode.LoadDecrement);

        [Fact] public void LDI_A_mHL() => TestPrimary(GameBoyPrimaryOpCode.LDI_A_mHL, Operand.A, Operand.mHL, OpCode.LoadIncrement);

        [Fact] public void LDI_mHL_A() => TestPrimary(GameBoyPrimaryOpCode.LDI_mHL_A, Operand.mHL, Operand.A, OpCode.LoadIncrement);

        [Fact] public void RETI() => TestPrimaryNoHalt(GameBoyPrimaryOpCode.RETI, OpCode.ReturnFromInterrupt, 4, 14);

        [Fact] public void STOP() => TestPrimaryNoHalt(GameBoyPrimaryOpCode.STOP, OpCode.Stop);


        private static void TestPrimary(GameBoyPrimaryOpCode op, Operand o0, Operand o1, OpCode excepted = OpCode.Load, int machineCycles = 2, int throttlingStates = 7)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op).OnlyGameboy())
            {
                fixture.Expected.OpCode(excepted).Operands(o0, o1);
            }
        }

        private static void TestPrimaryNoHalt(GameBoyPrimaryOpCode op, OpCode excepted = OpCode.Load, int machineCycles = 1, int throttlingStates = 4)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op).OnlyGameboy().DoNotHalt())
            {
                fixture.Expected.OpCode(excepted);
            }
        }

        private static void TestByteLiteral(GameBoyPrimaryOpCode op, Operand o0, Operand o1)
        {
            var n = Rng.Byte();
            using (var fixture = new DecodeFixture(3, 10, op, n).OnlyGameboy())
            {
                fixture.Expected.OpCode(OpCode.Load).Operands(o0, o1).ByteLiteral(n);
            }
        }

        private static void TestWordLiteral(GameBoyPrimaryOpCode op, Operand o0, Operand o1)
        {
            var w = Rng.UShort();
            using (var fixture = new DecodeFixture(4, 13, op, w).OnlyGameboy())
            {
                fixture.Expected.OpCode(OpCode.Load).Operands(o0, o1).WordLiteral(w);
            }
        }

        private static void TestWordLiteral16(GameBoyPrimaryOpCode op, Operand o0, Operand o1)
        {
            var w = Rng.UShort();
            using (var fixture = new DecodeFixture(5, 16, op, w).OnlyGameboy())
            {
                fixture.Expected.OpCode(OpCode.Load16).Operands(o0, o1).WordLiteral(w);
            }
        }

        private static void TestDisplacedIndex(GameBoyPrimaryOpCode op, Operand o0, Operand o1, int machineCycles = 3, int throttlingStates = 11)
        {
            var displacement = Rng.SByte();
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op, displacement).OnlyGameboy())
            {
                fixture.Expected.OpCode(OpCode.Load16).Operands(o0, o1).Displacement(displacement);
            }
        }
    }
}
