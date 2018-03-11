using System.Collections.Generic;
using System.Linq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class BitTestSetResetTests
    {
        [Theory]
        [MemberData(nameof(RegistersAndBits))]
        public void BitTest(Operand o, byte bit)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.BitTest).Operands(o).ByteLiteral(bit);
                fixture.With(c => c.Alu.Setup(x => x.BitTest(c.Operand8(o), bit)).Verifiable());
            }
        }

        [Theory]
        [MemberData(nameof(RegistersAndBits))]
        public void BitSet(Operand o, byte bit)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.BitSet).Operands(o).ByteLiteral(bit);
                fixture.With(c => c.Alu.Setup(x => x.BitSet(c.Operand8(o), bit)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Operand8(o).Should().Be(c.Byte));
            }
        }

        [Theory]
        [MemberData(nameof(RegistersAndBits))]
        public void BitReset(Operand o, byte bit)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.BitReset).Operands(o).ByteLiteral(bit);
                fixture.With(c => c.Alu.Setup(x => x.BitReset(c.Operand8(o), bit)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Operand8(o).Should().Be(c.Byte));
            }
        }

        public static IEnumerable<object[]> RegistersAndBits() =>
            new CartesianTheorySource<Operand, byte>(Operand.A, Operand.B, Operand.C, Operand.D, Operand.E, Operand.H, Operand.L)
                .With(Enumerable.Range(0, 8).Select(x => (byte) x).ToArray());
    }
}
