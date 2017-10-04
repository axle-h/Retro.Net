using System.Security.Cryptography;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class InputOutputTests
    {
        [Theory]
        [InlineData(PrefixEdOpCode.IN_A_C, Operand.A)]
        [InlineData(PrefixEdOpCode.IN_B_C, Operand.B)]
        [InlineData(PrefixEdOpCode.IN_C_C, Operand.C)]
        [InlineData(PrefixEdOpCode.IN_D_C, Operand.D)]
        [InlineData(PrefixEdOpCode.IN_E_C, Operand.E)]
        [InlineData(PrefixEdOpCode.IN_F_C, Operand.F)]
        [InlineData(PrefixEdOpCode.IN_H_C, Operand.H)]
        [InlineData(PrefixEdOpCode.IN_L_C, Operand.L)]
        public void IN_r_C(PrefixEdOpCode op, Operand r) => TestZ80(op, r);

        [Theory]
        [InlineData(PrefixEdOpCode.OUT_A_C, Operand.A)]
        [InlineData(PrefixEdOpCode.OUT_B_C, Operand.B)]
        [InlineData(PrefixEdOpCode.OUT_C_C, Operand.C)]
        [InlineData(PrefixEdOpCode.OUT_D_C, Operand.D)]
        [InlineData(PrefixEdOpCode.OUT_E_C, Operand.E)]
        [InlineData(PrefixEdOpCode.OUT_F_C, Operand.F)]
        [InlineData(PrefixEdOpCode.OUT_H_C, Operand.H)]
        [InlineData(PrefixEdOpCode.OUT_L_C, Operand.L)]
        public void OUT_r_C(PrefixEdOpCode op, Operand r) => TestZ80(op, r, OpCode.Output);

        [Fact] public void IN_A_n() => TestByteLiteral(PrimaryOpCode.IN_A_n);

        [Fact] public void OUT_A_n() => TestByteLiteral(PrimaryOpCode.OUT_A_n, OpCode.Output);

        [Fact] public void IND() => TestZ80(PrefixEdOpCode.IND, OpCode.InputTransferDecrement);

        [Fact] public void INI() => TestZ80(PrefixEdOpCode.INI, OpCode.InputTransferIncrement);

        [Fact] public void INDR() => TestZ80(PrefixEdOpCode.INDR, OpCode.InputTransferDecrementRepeat);

        [Fact] public void INIR() => TestZ80(PrefixEdOpCode.INIR, OpCode.InputTransferIncrementRepeat);

        [Fact] public void OUTD() => TestZ80(PrefixEdOpCode.OUTD, OpCode.OutputTransferDecrement);

        [Fact] public void OUTI() => TestZ80(PrefixEdOpCode.OUTI, OpCode.OutputTransferIncrement);

        [Fact] public void OUTDR() => TestZ80(PrefixEdOpCode.OUTDR, OpCode.OutputTransferDecrementRepeat);

        [Fact] public void OUTIR() => TestZ80(PrefixEdOpCode.OUTIR, OpCode.OutputTransferIncrementRepeat);

        private static void TestZ80(PrefixEdOpCode op, OpCode excepted, int machineCycles = 4, int throttlingStates = 16)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, PrimaryOpCode.Prefix_ED, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(excepted);
            }
        }

        private static void TestZ80(PrefixEdOpCode op, Operand o0, OpCode excepted = OpCode.Input, int machineCycles = 3, int throttlingStates = 12)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, PrimaryOpCode.Prefix_ED, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(excepted).Operands(o0, Operand.C);
            }
        }

        private static void TestByteLiteral(PrimaryOpCode op, OpCode excepted = OpCode.Input)
        {
            var literal = Rng.Byte();
            using (var fixture = new DecodeFixture(3, 11, op, literal).ThrowOnGameboy())
            {
                fixture.Expected.OpCode(excepted).Operands(Operand.A, Operand.n).ByteLiteral(literal);
            }
        }
    }
}
