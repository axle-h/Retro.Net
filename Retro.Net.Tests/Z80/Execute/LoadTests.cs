using Retro.Net.Z80.Core.Decode;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class LoadTests
    {
        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands8Bit), MemberType = typeof(ExecuteFixture))]
        public void Load(Operand r)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Load).RandomRegister(out var o).Operand2(r).RandomLiterals();
                fixture.Assert(c => c.Operand8(o).ShouldBe(c.Operand8(r)));
            }
        }

        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands16Bit), MemberType = typeof(ExecuteFixture))]
        public void Load16(Operand r)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Load16).Random16BitRegister(out var o).Operand2(r).RandomLiterals();
                fixture.Assert(c => c.Operand16(o).ShouldBe(c.Operand16(r)));
            }
        }
    }
}
