using System.Collections.Generic;
using System.Linq;
using Bogus;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class Load8BitTests
    {
        private static readonly Randomizer Rng = new Faker().Random;

        [Theory]
        [MemberData(nameof(SimpleLoadOpCodes))]
        public void LD_r_r(PrimaryOpCode op, Operand o0, Operand o1) => Test(op, o0, o1);

        [Theory]
        [MemberData(nameof(Opcodes), new[] { PrimaryOpCode.LD_A_n, PrimaryOpCode.LD_B_n, PrimaryOpCode.LD_C_n, PrimaryOpCode.LD_D_n, PrimaryOpCode.LD_E_n, PrimaryOpCode.LD_H_n, PrimaryOpCode.LD_L_n })]
        public void LD_r_n(PrimaryOpCode op, Operand r) => TestByteLiteral(op, r);

        [Theory]
        [MemberData(nameof(Opcodes), new[] { PrimaryOpCode.LD_A_mHL, PrimaryOpCode.LD_B_mHL, PrimaryOpCode.LD_C_mHL, PrimaryOpCode.LD_D_mHL, PrimaryOpCode.LD_E_mHL, PrimaryOpCode.LD_H_mHL, PrimaryOpCode.LD_L_mHL })]
        public void LD_r_mHL(PrimaryOpCode op, Operand r) => Test(op, r, Operand.mHL, 2, 7);

        [Theory]
        [MemberData(nameof(Opcodes), new[] { PrimaryOpCode.LD_mHL_A, PrimaryOpCode.LD_mHL_B, PrimaryOpCode.LD_mHL_C, PrimaryOpCode.LD_mHL_D, PrimaryOpCode.LD_mHL_E, PrimaryOpCode.LD_mHL_H, PrimaryOpCode.LD_mHL_L })]
        public void LD_mHL_r(PrimaryOpCode op, Operand r) => Test(op, Operand.mHL, r, 2, 7);
        
        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] { PrimaryOpCode.LD_A_mHL, PrimaryOpCode.LD_B_mHL, PrimaryOpCode.LD_C_mHL, PrimaryOpCode.LD_D_mHL, PrimaryOpCode.LD_E_mHL, PrimaryOpCode.LD_H_mHL, PrimaryOpCode.LD_L_mHL })]
        public void LD_r_mIXYd(PrimaryOpCode op, Operand r, Operand index) => TestZ80Index(index.GetZ80IndexPrefix(), op, r, index);

        [Theory]
        [MemberData(nameof(Z80IndexOpcodes), new[] { PrimaryOpCode.LD_mHL_A, PrimaryOpCode.LD_mHL_B, PrimaryOpCode.LD_mHL_C, PrimaryOpCode.LD_mHL_D, PrimaryOpCode.LD_mHL_E, PrimaryOpCode.LD_mHL_H, PrimaryOpCode.LD_mHL_L })]
        public void LD_mIXYd_r(PrimaryOpCode op, Operand r, Operand index) => TestZ80Index(index.GetZ80IndexPrefix(), op, index, r);
        
        [Fact] public void LD_mHL_n() => TestByteLiteral(PrimaryOpCode.LD_mHL_n, Operand.mHL, 3, 10);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void LD_mIXYd_n(Operand index)
        {
            var displacement = Rng.SByte();
            var literal = Rng.Byte();
            using (var fixture = new DecodeFixture(5, 19, index.GetZ80IndexPrefix(), PrimaryOpCode.LD_mHL_n, displacement, literal).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Load).Operands(index, Operand.n).ByteLiteral(literal).Displacement(displacement);
            }
        }

        [Fact] public void LD_A_mBC() => Test(PrimaryOpCode.LD_A_mBC, Operand.A, Operand.mBC, 2, 7);

        [Fact] public void LD_A_mDE() => Test(PrimaryOpCode.LD_A_mDE, Operand.A, Operand.mDE, 2, 7);
        
        [Fact] public void LD_mBC_A() => Test(PrimaryOpCode.LD_mBC_A, Operand.mBC, Operand.A, 2, 7);

        [Fact] public void LD_mDE_A() => Test(PrimaryOpCode.LD_mDE_A, Operand.mDE, Operand.A, 2, 7);
        
        [Fact] public void LD_A_mnn() => TestLiteralIndexed(PrimaryOpCode.LD_A_mnn, Operand.A, Operand.mnn);

        [Fact] public void LD_mnn_A() => TestLiteralIndexed(PrimaryOpCode.LD_mnn_A, Operand.mnn, Operand.A);

        [Fact] public void LD_A_I() => TestZ80(PrefixEdOpCode.LD_A_I, Operand.A, Operand.I);

        [Fact] public void LD_A_R() => TestZ80(PrefixEdOpCode.LD_A_R, Operand.A, Operand.R);

        [Fact] public void LD_I_A() => TestZ80(PrefixEdOpCode.LD_I_A, Operand.I, Operand.A);

        [Fact] public void LD_R_A() => TestZ80(PrefixEdOpCode.LD_R_A, Operand.R, Operand.A);
        
        private static void Test(PrimaryOpCode op, Operand o0, Operand o1, int machineCycles = 1, int throttlingStates = 4)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op))
            {
                if (o0 == o1)
                {
                    fixture.Expected.OpCode(OpCode.NoOperation);
                }
                else
                {
                    fixture.Expected.OpCode(OpCode.Load).Operands(o0, o1);
                }
            }
        }

        private static void TestByteLiteral(PrimaryOpCode op, Operand r, int machineCycles = 2, int throttlingStates = 7)
        {
            var literal = Rng.Byte();
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op, literal))
            {
                fixture.Expected.OpCode(OpCode.Load).Operands(r, Operand.n).ByteLiteral(literal);
            }
        }

        private static void TestLiteralIndexed(PrimaryOpCode op, Operand o0, Operand o1)
        {
            var literal = Rng.UShort();
            using (var fixture = new DecodeFixture(4, 13, op, literal).NotOnGameboy())
            {
                fixture.Expected.OpCode(OpCode.Load).Operands(o0, o1).WordLiteral(literal);
            }
        }

        private static void TestZ80(PrefixEdOpCode op, Operand o0, Operand o1)
        {
            using (var fixture = new DecodeFixture(2, 9, PrimaryOpCode.Prefix_ED, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Load).Operands(o0, o1);
            }
        }

        private static void TestZ80Index(PrimaryOpCode prefix, PrimaryOpCode op, Operand o0, Operand o1)
        {
            var displacement = Rng.SByte();
            using (var fixture = new DecodeFixture(5, 19, prefix, op, displacement).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Load).Operands(o0, o1).Displacement(displacement);
            }
        }

        public static IEnumerable<object[]> SimpleLoadOpCodes() => new[]
        {
            new object[] {PrimaryOpCode.LD_A_A, Operand.A, Operand.A}, new object[] {PrimaryOpCode.LD_A_B, Operand.A, Operand.B}, new object[] {PrimaryOpCode.LD_A_C, Operand.A, Operand.C},
            new object[] {PrimaryOpCode.LD_A_D, Operand.A, Operand.D}, new object[] {PrimaryOpCode.LD_A_E, Operand.A, Operand.E}, new object[] {PrimaryOpCode.LD_A_H, Operand.A, Operand.H},
            new object[] {PrimaryOpCode.LD_A_L, Operand.A, Operand.L}, new object[] {PrimaryOpCode.LD_B_A, Operand.B, Operand.A}, new object[] {PrimaryOpCode.LD_B_B, Operand.B, Operand.B},
            new object[] {PrimaryOpCode.LD_B_C, Operand.B, Operand.C}, new object[] {PrimaryOpCode.LD_B_D, Operand.B, Operand.D}, new object[] {PrimaryOpCode.LD_B_E, Operand.B, Operand.E},
            new object[] {PrimaryOpCode.LD_B_H, Operand.B, Operand.H}, new object[] {PrimaryOpCode.LD_B_L, Operand.B, Operand.L}, new object[] {PrimaryOpCode.LD_C_A, Operand.C, Operand.A},
            new object[] {PrimaryOpCode.LD_C_B, Operand.C, Operand.B}, new object[] {PrimaryOpCode.LD_C_C, Operand.C, Operand.C}, new object[] {PrimaryOpCode.LD_C_D, Operand.C, Operand.D},
            new object[] {PrimaryOpCode.LD_C_E, Operand.C, Operand.E}, new object[] {PrimaryOpCode.LD_C_H, Operand.C, Operand.H}, new object[] {PrimaryOpCode.LD_C_L, Operand.C, Operand.L},
            new object[] {PrimaryOpCode.LD_D_A, Operand.D, Operand.A}, new object[] {PrimaryOpCode.LD_D_B, Operand.D, Operand.B}, new object[] {PrimaryOpCode.LD_D_C, Operand.D, Operand.C},
            new object[] {PrimaryOpCode.LD_D_D, Operand.D, Operand.D}, new object[] {PrimaryOpCode.LD_D_E, Operand.D, Operand.E}, new object[] {PrimaryOpCode.LD_D_H, Operand.D, Operand.H},
            new object[] {PrimaryOpCode.LD_D_L, Operand.D, Operand.L}, new object[] {PrimaryOpCode.LD_E_A, Operand.E, Operand.A}, new object[] {PrimaryOpCode.LD_E_B, Operand.E, Operand.B},
            new object[] {PrimaryOpCode.LD_E_C, Operand.E, Operand.C}, new object[] {PrimaryOpCode.LD_E_D, Operand.E, Operand.D}, new object[] {PrimaryOpCode.LD_E_E, Operand.E, Operand.E},
            new object[] {PrimaryOpCode.LD_E_H, Operand.E, Operand.H}, new object[] {PrimaryOpCode.LD_E_L, Operand.E, Operand.L}, new object[] {PrimaryOpCode.LD_H_A, Operand.H, Operand.A},
            new object[] {PrimaryOpCode.LD_H_B, Operand.H, Operand.B}, new object[] {PrimaryOpCode.LD_H_C, Operand.H, Operand.C}, new object[] {PrimaryOpCode.LD_H_D, Operand.H, Operand.D},
            new object[] {PrimaryOpCode.LD_H_E, Operand.H, Operand.E}, new object[] {PrimaryOpCode.LD_H_H, Operand.H, Operand.H}, new object[] {PrimaryOpCode.LD_H_L, Operand.H, Operand.L},
            new object[] {PrimaryOpCode.LD_L_A, Operand.L, Operand.A}, new object[] {PrimaryOpCode.LD_L_B, Operand.L, Operand.B}, new object[] {PrimaryOpCode.LD_L_C, Operand.L, Operand.C},
            new object[] {PrimaryOpCode.LD_L_D, Operand.L, Operand.D}, new object[] {PrimaryOpCode.LD_L_E, Operand.L, Operand.E}, new object[] {PrimaryOpCode.LD_L_H, Operand.L, Operand.H},
            new object[] {PrimaryOpCode.LD_L_L, Operand.L, Operand.L}
        };

        public static IEnumerable<object[]> Z80IndexOpcodes(PrimaryOpCode[] ops) => new[] { Operand.mIXd, Operand.mIYd }
            .SelectMany(index => new[] { Operand.A, Operand.B, Operand.C, Operand.D, Operand.E, Operand.H, Operand.L }.Zip(ops, (o, op) => new object[] { op, o, index })).ToArray();

        public static IEnumerable<object[]> Opcodes(IEnumerable<PrimaryOpCode> ops) => new[] { Operand.A, Operand.B, Operand.C, Operand.D, Operand.E, Operand.H, Operand.L }.Zip(ops, (o, op) => new object[] { op, o }).ToArray();
    }
}
