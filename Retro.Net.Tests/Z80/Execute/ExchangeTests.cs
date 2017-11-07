using System.Collections.Generic;
using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class ExchangeTests
    {
        [Fact]
        public void ExchangeAccumulatorAndFlags()
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.ExchangeAccumulatorAndFlags);
                fixture.Assert(c => c.MockRegisters.Verify(x => x.SwitchToAlternativeAccumulatorAndFlagsRegisters(), Times.Once));
            }
        }

        [Fact]
        public void ExchangeGeneralPurpose()
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.ExchangeGeneralPurpose);
                fixture.Assert(c => c.MockRegisters.Verify(x => x.SwitchToAlternativeGeneralPurposeRegisters(), Times.Once));
            }
        }


        [Theory]
        [MemberData(nameof(Registers16Bit))]
        public void Exchange(Operand o0, Operand o1)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Exchange).Operands(o0, o1);
                fixture.Assert(c => c.Operand16(o0).ShouldBe(c.InitialRegister16(o1)),
                    c => c.Operand16(o1).ShouldBe(c.InitialRegister16(o0)));
            }
        }

        public static IEnumerable<object[]> Registers16Bit() => new CartesianTheorySource<Operand, Operand>(Operand.AF, Operand.BC, Operand.DE, Operand.HL, Operand.IX, Operand.IY)
            .With(Operand.AF, Operand.BC, Operand.DE, Operand.HL, Operand.IX, Operand.IY);
    }
}
