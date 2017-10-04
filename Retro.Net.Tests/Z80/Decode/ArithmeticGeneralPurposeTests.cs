using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class ArithmeticGeneralPurposeTests
    {
        [Fact] public void CCF() => Test(PrimaryOpCode.CCF, OpCode.InvertCarryFlag);

        [Fact] public void CPL() => Test(PrimaryOpCode.CPL, OpCode.NegateOnesComplement);

        [Fact] public void DAA() => Test(PrimaryOpCode.DAA, OpCode.DecimalArithmeticAdjust);

        [Fact] public void DI() => Test(PrimaryOpCode.DI, OpCode.DisableInterrupts);

        [Fact] public void EI() => Test(PrimaryOpCode.EI, OpCode.EnableInterrupts);

        [Fact] public void SCF() => Test(PrimaryOpCode.SCF, OpCode.SetCarryFlag);

        [Fact] public void IM0() => Z80Test(PrefixEdOpCode.IM0, OpCode.InterruptMode0);

        [Fact] public void IM1() => Z80Test(PrefixEdOpCode.IM1, OpCode.InterruptMode1);

        [Fact] public void IM2() => Z80Test(PrefixEdOpCode.IM2, OpCode.InterruptMode2);

        [Fact] public void NEG() => Z80Test(PrefixEdOpCode.NEG, OpCode.NegateTwosComplement);
        
        private static void Test(PrimaryOpCode op, OpCode expected)
        {
            using (var fixture = new DecodeFixture(1, 4, op))
            {
                fixture.Expected.OpCode(expected);
            }
        }

        private static void Z80Test(PrefixEdOpCode op, OpCode expected)
        {
            using (var fixture = new DecodeFixture(2, 8, PrimaryOpCode.Prefix_ED, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected);
            }
        }
    }
}
