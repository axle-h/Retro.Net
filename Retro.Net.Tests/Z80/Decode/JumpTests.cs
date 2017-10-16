using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class JumpTests
    {
        [Fact]
        public void JP_mHL()
        {
            using (var fixture = new DecodeFixture(1, 4, PrimaryOpCode.JP_mHL).DoNotHalt())
            {
                fixture.Expected.OpCode(OpCode.Jump).Operands(Operand.HL);
            }
        }

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void JP_IXY(Operand index)
        {
            using (var fixture = new DecodeFixture(2, 8, index.GetZ80IndexPrefix(), PrimaryOpCode.JP_mHL).DoNotHalt().ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Jump).Operands(index);
            }
        }

        [Fact]
        public void DJNZ()
        {
            var literal = Rng.Byte();
            using (var fixture = new DecodeFixture(2, 8, PrimaryOpCode.DJNZ, literal).DoNotHalt().NotOnGameboy())
            {
                fixture.Expected.OpCode(OpCode.DecrementJumpRelativeIfNonZero).ByteLiteral(literal);
            }
        }

        [Fact] public void JP() => Test(PrimaryOpCode.JP);
        
        [Fact] public void JP_C() => Test(PrimaryOpCode.JP_C, FlagTest.Carry);

        [Fact] public void JP_NC() => Test(PrimaryOpCode.JP_NC, FlagTest.NotCarry);

        [Fact] public void JP_Z() => Test(PrimaryOpCode.JP_Z, FlagTest.Zero);

        [Fact] public void JP_NZ() => Test(PrimaryOpCode.JP_NZ, FlagTest.NotZero);

        [Fact] public void JP_P() => Test(PrimaryOpCode.JP_P, FlagTest.Positive, false);

        [Fact] public void JP_M() => Test(PrimaryOpCode.JP_M, FlagTest.Negative, false);

        [Fact] public void JP_PE() => Test(PrimaryOpCode.JP_PE, FlagTest.ParityEven, false);

        [Fact] public void JP_PO() => Test(PrimaryOpCode.JP_PO, FlagTest.ParityOdd, false);

        [Fact] public void JR() => TestRelative(PrimaryOpCode.JR);

        [Fact] public void JR_C() => TestRelative(PrimaryOpCode.JR_C, FlagTest.Carry);

        [Fact] public void JR_NC() => TestRelative(PrimaryOpCode.JR_NC, FlagTest.NotCarry);

        [Fact] public void JR_Z() => TestRelative(PrimaryOpCode.JR_Z, FlagTest.Zero);

        [Fact] public void JR_NZ() => TestRelative(PrimaryOpCode.JR_NZ, FlagTest.NotZero);
        
        private static void Test(PrimaryOpCode op, FlagTest test = FlagTest.None, bool gameBoy = true)
        {
            var literal = Rng.Word();
            using (var fixture = new DecodeFixture(3, 10, op, literal).DoNotHalt())
            {
                if (!gameBoy)
                {
                    fixture.NotOnGameboy();
                }

                fixture.Expected.OpCode(OpCode.Jump).Operands(Operand.nn).WordLiteral(literal);

                if (test != FlagTest.None)
                {
                    fixture.Expected.FlagTest(test);
                }
            }
        }

        private static void TestRelative(PrimaryOpCode op, FlagTest test = FlagTest.None, bool gameBoy = true)
        {
            var literal = Rng.SByte();

            var (machineCycles, throttlingStates) = test == FlagTest.None ? (3, 12) : (2, 7);

            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op, literal).DoNotHalt())
            {
                if (!gameBoy)
                {
                    fixture.NotOnGameboy();
                }

                fixture.Expected.OpCode(OpCode.JumpRelative).Displacement(literal);

                if (test != FlagTest.None)
                {
                    fixture.Expected.FlagTest(test);
                }
            }
        }
    }
}
