using Retro.Net.Z80.Core.Decode;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class InputOutputTests
    {
        [Fact] public void When_reading_from_literal_indexed_port() => Input(Operand.n);

        [Fact] public void When_reading_from_register_indexed_port() => Input(Operand.C);

        [Fact] public void When_writing_to_literal_indexed_port() => Output(Operand.n);

        [Fact] public void When_writing_to_register_indexed_port() => Output(Operand.C);
        
        private static void Input(Operand o)
        {
            var addressMsb = o == Operand.n ? Operand.A : Operand.B;
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Input).RandomRegister(out var r).Operand2(o).RandomLiterals();
                fixture.With(c => c.Io.Setup(x => x.ReadByteFromPort(c.Operand8(o), c.InitialRegister8(addressMsb))).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Operand8(r).ShouldBe(c.Byte));
            }
        }

        private static void Output(Operand o)
        {
            var addressMsb = o == Operand.n ? Operand.A : Operand.B;
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Output).RandomRegister(out var r).Operand2(o).RandomLiterals();
                fixture.With(c => c.Io.Setup(x => x.WriteByteToPort(c.Operand8(o), c.InitialRegister8(addressMsb), c.Operand8(r))).Verifiable());
            }
        }
    }
}
