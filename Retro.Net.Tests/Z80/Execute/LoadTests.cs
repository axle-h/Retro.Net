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
                fixture.Assert(c => c.ByteOperand(o).ShouldBe(c.ByteOperand(r)));
            }
        }

        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands16Bit), MemberType = typeof(ExecuteFixture))]
        public void Load16(Operand r)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Load16).Random16BitRegister(out var o).Operand2(r).RandomLiterals();
                fixture.Assert(c => c.WordOperand(o).ShouldBe(c.WordOperand(r)));
            }
        }
    }
}
