using System.Linq;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Retro.Net.Z80.Registers;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class ExchangeTests
    {
        [Fact] public void EX_AF() => Test(PrimaryOpCode.EX_AF, OpCode.ExchangeAccumulatorAndFlags);

        [Fact] public void EX_DE_HL() => Test(PrimaryOpCode.EX_DE_HL, OpCode.Exchange, Operand.DE, Operand.HL);

        [Fact] public void EXX() => Test(PrimaryOpCode.EXX, OpCode.ExchangeGeneralPurpose);

        [Fact]
        public void EX_mSP_HL()
        {
            using (var fixture = new DecodeFixture(5, 19, PrimaryOpCode.EX_mSP_HL).NotOnGameboy())
            {
                fixture.Expected.OpCode(OpCode.Exchange).Operands(Operand.mSP, Operand.HL);
            }
        }

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void EX_mSP_IXY(Operand index) => Z80IndexExchangeTest(index);

        private static void Test(PrimaryOpCode op, OpCode expected, params Operand[] operands)
        {
            using (var fixture = new DecodeFixture(1, 4, op).NotOnGameboy())
            {
                fixture.Expected.OpCode(expected);

                if (operands.Length == 2)
                {
                    fixture.Expected.Operands(operands[0], operands[1]);
                }
            }
        }

        private static void Z80IndexExchangeTest(Operand index)
        {
            using (var fixture = new DecodeFixture(6, 23, index.GetZ80IndexPrefix(), PrimaryOpCode.EX_mSP_HL).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Exchange).Operands(Operand.mSP, index);
            }
        }
    }
}
