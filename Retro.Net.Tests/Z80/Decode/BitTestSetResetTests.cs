using System;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class BitTestSetResetTests
    {
        [Theory]
        [MemberData(nameof(TestArgs), "BIT")]
        public void BIT_n_r(PrefixCbOpCode op, byte bit, Operand r) => Test(op, bit, r, OpCode.BitTest, 3, 12);

        [Theory]
        [MemberData(nameof(TestArgs), "RES")]
        public void RES_n_r(PrefixCbOpCode op, byte bit, Operand r) => Test(op, bit, r, OpCode.BitReset);

        [Theory]
        [MemberData(nameof(TestArgs), "SET")]
        public void SET_n_r(PrefixCbOpCode op, byte bit, Operand r) => Test(op, bit, r, OpCode.BitSet);

        [Theory]
        [MemberData(nameof(Z80IndexTestArgs), "BIT")]
        public void BIT_n_IXYd(Operand index, PrefixCbOpCode op, byte bit, Operand r) => Z80IndexTest(index, op, bit, r, OpCode.BitTest, 5, 20, false);

        [Theory]
        [MemberData(nameof(Z80IndexTestArgs), "RES")]
        public void RES_n_IXYd(Operand index, PrefixCbOpCode op, byte bit, Operand r) => Z80IndexTest(index, op, bit, r, OpCode.BitReset);

        [Theory]
        [MemberData(nameof(Z80IndexTestArgs), "SET")]
        public void SET_n_IXYd(Operand index, PrefixCbOpCode op, byte bit, Operand r) => Z80IndexTest(index, op, bit, r, OpCode.BitSet);

        public static IEnumerable<object[]> TestArgs(string op) =>
            Enum.GetValues(typeof(PrefixCbOpCode))
                .Cast<PrefixCbOpCode>()
                .Select(x => (op: x, tokens: x.ToString().Split(new[] { '_' }, 3)))
                .Where(x => x.tokens[0] == op)
                .Select(x => new object[] { x.op, byte.Parse(x.tokens[1]), (Operand)Enum.Parse(typeof(Operand), x.tokens[2]) });

        private static void Test(PrefixCbOpCode op, byte bit, Operand r, OpCode expected, int indexMachineCycles = 4, int indexthrottlingStates = 15)
        {
            var (machineCycles, throttlingStates) = r == Operand.mHL ? (indexMachineCycles, indexthrottlingStates) : (2, 8);
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, PrimaryOpCode.Prefix_CB, op).ThrowOn8080())
            {
                fixture.Expected.OpCode(expected).Operands(r).ByteLiteral(bit);
            }
        }

        public static IEnumerable<object[]> Z80IndexTestArgs(string op) =>
            new [] { Operand.mIXd, Operand.mIYd }.SelectMany(index => TestArgs(op).Select(x => new object[] { index }.Concat(x).ToArray()));


        private static void Z80IndexTest(Operand index, PrefixCbOpCode op, byte bit, Operand r, OpCode expected, int indexMachineCycles = 6, int indexthrottlingStates = 23, bool autoCopy = true)
        {
            var displacement = Rng.SByte();
            if (autoCopy && r != Operand.mHL)
            {
                // Autocopy has extra cycles to run LD_r_IXYd instruction (5, 19) - Prefix_NOP(1, 4) (which has already been run) = (4, 15)
                indexMachineCycles += 4;
                indexthrottlingStates += 15;
            }

            using (var fixture = new DecodeFixture(indexMachineCycles, indexthrottlingStates, index.GetZ80IndexPrefix(), PrimaryOpCode.Prefix_CB, displacement, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected).ByteLiteral(bit).Displacement(displacement);
                if (r == Operand.mHL)
                {
                    fixture.Expected.Operands(index);
                }
                else
                {
                    fixture.Expected.Operands(index, r).AutoCopy(autoCopy);
                }
            }
        }
    }
}
