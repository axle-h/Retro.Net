using System.Collections.Generic;
using System.Linq;
using Bogus;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class RotateAndShiftTests
    {
        private static readonly Randomizer Rng = new Faker().Random;

        [Theory]
        [InlineData(PrefixCbOpCode.RLC_A, Operand.A)]
        [InlineData(PrefixCbOpCode.RLC_B, Operand.B)]
        [InlineData(PrefixCbOpCode.RLC_C, Operand.C)]
        [InlineData(PrefixCbOpCode.RLC_D, Operand.D)]
        [InlineData(PrefixCbOpCode.RLC_E, Operand.E)]
        [InlineData(PrefixCbOpCode.RLC_H, Operand.H)]
        [InlineData(PrefixCbOpCode.RLC_L, Operand.L)]
        public void RLC_r(PrefixCbOpCode op, Operand r) => TestCb(op, r, OpCode.RotateLeftWithCarry);

        [Fact] public void RLC_mHL() => TestIndex(PrefixCbOpCode.RLC_mHL, OpCode.RotateLeftWithCarry);

        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] {PrefixCbOpCode.RLC_A, PrefixCbOpCode.RLC_B, PrefixCbOpCode.RLC_C, PrefixCbOpCode.RLC_D, PrefixCbOpCode.RLC_E, PrefixCbOpCode.RLC_H, PrefixCbOpCode.RLC_L})]
        public void RLC_r_mIXYd(PrefixCbOpCode op, Operand r, Operand index) => TestZ80AutocopyIndex(op, index, r, index, OpCode.RotateLeftWithCarry);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void RLC_mIXYd(Operand index) => TestZ80Index(PrefixCbOpCode.RLC_mHL, index, OpCode.RotateLeftWithCarry);
        
        [Theory]
        [InlineData(PrefixCbOpCode.RL_A, Operand.A)]
        [InlineData(PrefixCbOpCode.RL_B, Operand.B)]
        [InlineData(PrefixCbOpCode.RL_C, Operand.C)]
        [InlineData(PrefixCbOpCode.RL_D, Operand.D)]
        [InlineData(PrefixCbOpCode.RL_E, Operand.E)]
        [InlineData(PrefixCbOpCode.RL_H, Operand.H)]
        [InlineData(PrefixCbOpCode.RL_L, Operand.L)]
        public void RL_r(PrefixCbOpCode op, Operand r) => TestCb(op, r, OpCode.RotateLeft);

        [Fact] public void RL_mHL() => TestIndex(PrefixCbOpCode.RL_mHL, OpCode.RotateLeft);

        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] { PrefixCbOpCode.RL_A, PrefixCbOpCode.RL_B, PrefixCbOpCode.RL_C, PrefixCbOpCode.RL_D, PrefixCbOpCode.RL_E, PrefixCbOpCode.RL_H, PrefixCbOpCode.RL_L })]
        public void RL_r_mIXYd(PrefixCbOpCode op, Operand r, Operand index) => TestZ80AutocopyIndex(op, index, r, index, OpCode.RotateLeft);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void RL_mIXYd(Operand index) => TestZ80Index(PrefixCbOpCode.RL_mHL, index, OpCode.RotateLeft);

        [Theory]
        [InlineData(PrefixCbOpCode.RRC_A, Operand.A)]
        [InlineData(PrefixCbOpCode.RRC_B, Operand.B)]
        [InlineData(PrefixCbOpCode.RRC_C, Operand.C)]
        [InlineData(PrefixCbOpCode.RRC_D, Operand.D)]
        [InlineData(PrefixCbOpCode.RRC_E, Operand.E)]
        [InlineData(PrefixCbOpCode.RRC_H, Operand.H)]
        [InlineData(PrefixCbOpCode.RRC_L, Operand.L)]
        public void RRC_r(PrefixCbOpCode op, Operand r) => TestCb(op, r, OpCode.RotateRightWithCarry);

        [Fact] public void RRC_mHL() => TestIndex(PrefixCbOpCode.RRC_mHL, OpCode.RotateRightWithCarry);

        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] { PrefixCbOpCode.RRC_A, PrefixCbOpCode.RRC_B, PrefixCbOpCode.RRC_C, PrefixCbOpCode.RRC_D, PrefixCbOpCode.RRC_E, PrefixCbOpCode.RRC_H, PrefixCbOpCode.RRC_L })]
        public void RRC_r_mIXYd(PrefixCbOpCode op, Operand r, Operand index) => TestZ80AutocopyIndex(op, index, r, index, OpCode.RotateRightWithCarry);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void RRC_mIXYd(Operand index) => TestZ80Index(PrefixCbOpCode.RRC_mHL, index, OpCode.RotateRightWithCarry);

        [Theory]
        [InlineData(PrefixCbOpCode.RR_A, Operand.A)]
        [InlineData(PrefixCbOpCode.RR_B, Operand.B)]
        [InlineData(PrefixCbOpCode.RR_C, Operand.C)]
        [InlineData(PrefixCbOpCode.RR_D, Operand.D)]
        [InlineData(PrefixCbOpCode.RR_E, Operand.E)]
        [InlineData(PrefixCbOpCode.RR_H, Operand.H)]
        [InlineData(PrefixCbOpCode.RR_L, Operand.L)]
        public void RR_r(PrefixCbOpCode op, Operand r) => TestCb(op, r, OpCode.RotateRight);

        [Fact] public void RR_mHL() => TestIndex(PrefixCbOpCode.RR_mHL, OpCode.RotateRight);

        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] { PrefixCbOpCode.RR_A, PrefixCbOpCode.RR_B, PrefixCbOpCode.RR_C, PrefixCbOpCode.RR_D, PrefixCbOpCode.RR_E, PrefixCbOpCode.RR_H, PrefixCbOpCode.RR_L })]
        public void RR_r_mIXYd(PrefixCbOpCode op, Operand r, Operand index) => TestZ80AutocopyIndex(op, index, r, index, OpCode.RotateRight);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void RR_mIXYd(Operand index) => TestZ80Index(PrefixCbOpCode.RR_mHL, index, OpCode.RotateRight);

        [Fact] public void RLA() => Test(PrimaryOpCode.RLA, OpCode.RotateLeft);

        [Fact] public void RLCA() => Test(PrimaryOpCode.RLCA, OpCode.RotateLeftWithCarry);

        [Fact] public void RRA() => Test(PrimaryOpCode.RRA, OpCode.RotateRight);

        [Fact] public void RRCA() => Test(PrimaryOpCode.RRCA, OpCode.RotateRightWithCarry);

        [Fact] public void RLD() => TestEd(PrefixEdOpCode.RLD, OpCode.RotateLeftDigit);

        [Fact] public void RRD() => TestEd(PrefixEdOpCode.RRD, OpCode.RotateRightDigit);

        [Theory]
        [InlineData(PrefixCbOpCode.SLA_A, Operand.A)]
        [InlineData(PrefixCbOpCode.SLA_B, Operand.B)]
        [InlineData(PrefixCbOpCode.SLA_C, Operand.C)]
        [InlineData(PrefixCbOpCode.SLA_D, Operand.D)]
        [InlineData(PrefixCbOpCode.SLA_E, Operand.E)]
        [InlineData(PrefixCbOpCode.SLA_H, Operand.H)]
        [InlineData(PrefixCbOpCode.SLA_L, Operand.L)]
        public void SLA_r(PrefixCbOpCode op, Operand r) => TestCb(op, r, OpCode.ShiftLeft);

        [Fact] public void SLA_mHL() => TestIndex(PrefixCbOpCode.SLA_mHL, OpCode.ShiftLeft);

        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] { PrefixCbOpCode.SLA_A, PrefixCbOpCode.SLA_B, PrefixCbOpCode.SLA_C, PrefixCbOpCode.SLA_D, PrefixCbOpCode.SLA_E, PrefixCbOpCode.SLA_H, PrefixCbOpCode.SLA_L })]
        public void SLA_r_mIXYd(PrefixCbOpCode op, Operand r, Operand index) => TestZ80AutocopyIndex(op, index, r, index, OpCode.ShiftLeft);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void SLA_mIXYd(Operand index) => TestZ80Index(PrefixCbOpCode.SLA_mHL, index, OpCode.ShiftLeft);

        [Theory]
        [InlineData(PrefixCbOpCode.SLS_A, Operand.A)]
        [InlineData(PrefixCbOpCode.SLS_B, Operand.B)]
        [InlineData(PrefixCbOpCode.SLS_C, Operand.C)]
        [InlineData(PrefixCbOpCode.SLS_D, Operand.D)]
        [InlineData(PrefixCbOpCode.SLS_E, Operand.E)]
        [InlineData(PrefixCbOpCode.SLS_H, Operand.H)]
        [InlineData(PrefixCbOpCode.SLS_L, Operand.L)]
        public void SLS_r(PrefixCbOpCode op, Operand r) => TestCb(op, r, OpCode.ShiftLeftSet, false);

        [Fact] public void SLS_mHL() => TestIndex(PrefixCbOpCode.SLS_mHL, OpCode.ShiftLeftSet, false);

        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] { PrefixCbOpCode.SLS_A, PrefixCbOpCode.SLS_B, PrefixCbOpCode.SLS_C, PrefixCbOpCode.SLS_D, PrefixCbOpCode.SLS_E, PrefixCbOpCode.SLS_H, PrefixCbOpCode.SLS_L })]
        public void SLS_r_mIXYd(PrefixCbOpCode op, Operand r, Operand index) => TestZ80AutocopyIndex(op, index, r, index, OpCode.ShiftLeftSet);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void SLS_mIXYd(Operand index) => TestZ80Index(PrefixCbOpCode.SLS_mHL, index, OpCode.ShiftLeftSet);
        
        [Theory]
        [InlineData(PrefixCbOpCode.SRA_A, Operand.A)]
        [InlineData(PrefixCbOpCode.SRA_B, Operand.B)]
        [InlineData(PrefixCbOpCode.SRA_C, Operand.C)]
        [InlineData(PrefixCbOpCode.SRA_D, Operand.D)]
        [InlineData(PrefixCbOpCode.SRA_E, Operand.E)]
        [InlineData(PrefixCbOpCode.SRA_H, Operand.H)]
        [InlineData(PrefixCbOpCode.SRA_L, Operand.L)]
        public void SRA_r(PrefixCbOpCode op, Operand r) => TestCb(op, r, OpCode.ShiftRight);

        [Fact] public void SRA_mHL() => TestIndex(PrefixCbOpCode.SRA_mHL, OpCode.ShiftRight);

        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] { PrefixCbOpCode.SRA_A, PrefixCbOpCode.SRA_B, PrefixCbOpCode.SRA_C, PrefixCbOpCode.SRA_D, PrefixCbOpCode.SRA_E, PrefixCbOpCode.SRA_H, PrefixCbOpCode.SRA_L })]
        public void SRA_r_mIXYd(PrefixCbOpCode op, Operand r, Operand index) => TestZ80AutocopyIndex(op, index, r, index, OpCode.ShiftRight);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void SRA_mIXYd(Operand index) => TestZ80Index(PrefixCbOpCode.SRA_mHL, index, OpCode.ShiftRight);

        [Theory]
        [InlineData(PrefixCbOpCode.SRL_A, Operand.A)]
        [InlineData(PrefixCbOpCode.SRL_B, Operand.B)]
        [InlineData(PrefixCbOpCode.SRL_C, Operand.C)]
        [InlineData(PrefixCbOpCode.SRL_D, Operand.D)]
        [InlineData(PrefixCbOpCode.SRL_E, Operand.E)]
        [InlineData(PrefixCbOpCode.SRL_H, Operand.H)]
        [InlineData(PrefixCbOpCode.SRL_L, Operand.L)]
        public void SRL_r(PrefixCbOpCode op, Operand r) => TestCb(op, r, OpCode.ShiftRightLogical);

        [Fact] public void SRL_mHL() => TestIndex(PrefixCbOpCode.SRL_mHL, OpCode.ShiftRightLogical);

        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] { PrefixCbOpCode.SRL_A, PrefixCbOpCode.SRL_B, PrefixCbOpCode.SRL_C, PrefixCbOpCode.SRL_D, PrefixCbOpCode.SRL_E, PrefixCbOpCode.SRL_H, PrefixCbOpCode.SRL_L })]
        public void SRL_r_mIXYd(PrefixCbOpCode op, Operand r, Operand index) => TestZ80AutocopyIndex(op, index, r, index, OpCode.ShiftRightLogical);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void SRL_mIXYd(Operand index) => TestZ80Index(PrefixCbOpCode.SRL_mHL, index, OpCode.ShiftRightLogical);
        
        private static void Test(PrimaryOpCode op, OpCode expected)
        {
            using (var fixture = new DecodeFixture(1, 4, op))
            {
                fixture.Expected.OpCode(expected).Operands(Operand.A).UseAlternativeFlagAffection();
            }
        }

        private static void TestCb(PrefixCbOpCode op, Operand r, OpCode expected, bool gameboy = true)
        {
            using (var fixture = new DecodeFixture(2, 8, PrimaryOpCode.Prefix_CB, op).ThrowOn8080().OnGameboy(gameboy))
            {
                fixture.Expected.OpCode(expected).Operands(r);
            }
        }

        private static void TestEd(PrefixEdOpCode op, OpCode expected)
        {
            using (var fixture = new DecodeFixture(5, 18, PrimaryOpCode.Prefix_ED, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected);
            }
        }

        private static void TestZ80AutocopyIndex(PrefixCbOpCode op, Operand o0, Operand o1, Operand index, OpCode expected)
        {
            var displacement = Rng.SByte();
            using (var fixture = new DecodeFixture(8, 31, index.GetZ80IndexPrefix(), PrimaryOpCode.Prefix_CB, displacement, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected).Operands(o0, o1).Displacement(displacement).AutoCopy();
            }
        }

        private static void TestZ80Index(PrefixCbOpCode op, Operand index, OpCode expected)
        {
            var displacement = Rng.SByte();
            using (var fixture = new DecodeFixture(6, 23, index.GetZ80IndexPrefix(), PrimaryOpCode.Prefix_CB, displacement, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected).Operands(index).Displacement(displacement);
            }
        }

        private static void TestIndex(PrefixCbOpCode op, OpCode expected, bool gameboy = true)
        {
            using (var fixture = new DecodeFixture(4, 15, PrimaryOpCode.Prefix_CB, op).ThrowOn8080().OnGameboy(gameboy))
            {
                fixture.Expected.OpCode(expected).Operands(Operand.mHL);
            }
        }

        public static IEnumerable<object[]> Z80IndexOpcodes(PrefixCbOpCode[] ops) => new[] { Operand.mIXd, Operand.mIYd }
            .SelectMany(index => new[] { Operand.A, Operand.B, Operand.C, Operand.D, Operand.E, Operand.H, Operand.L }.Zip(ops, (o, op) => new object[] { op, o, index })).ToArray();
    }
}
