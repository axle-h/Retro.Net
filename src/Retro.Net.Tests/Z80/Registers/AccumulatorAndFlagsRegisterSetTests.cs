using AutoFixture;
using FluentAssertions;
using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.State;
using Xunit;

namespace Retro.Net.Tests.Z80.Registers
{
    public class AccumulatorAndFlagsRegisterSetTests : WithSubject<AccumulatorAndFlagsRegisterSet>
    {
        private const byte A = 0xaa, F = 0xff;
        private const ushort AF = 0xaaff;

        [Fact]
        public void When_reading_AF()
        {
            Subject.A = A;
            Mock<IFlagsRegister>().Setup(x => x.Register).Returns(F);
            Subject.AF.Should().Be(AF);
        }
        
        [Fact]
        public void When_writing_AF()
        {
            Subject.AF = AF;
            Subject.A.Should().Be(A);
            Mock<IFlagsRegister>().VerifySet(x => x.Register = It.Is<byte>(y => y == F));
        }

        [Fact]
        public void When_getting_register_state()
        {
            Subject.A = A;
            Mock<IFlagsRegister>().Setup(x => x.Register).Returns(F);

            var state = Subject.GetRegisterState();

            state.A.Should().Be(A);
            state.F.Should().Be(F);
        }
        
        [Fact]
        public void When_setting_register_state()
        {
            var state = new Fixture().Create<AccumulatorAndFlagsRegisterState>();

            Subject.ResetToState(state);

            Subject.A.Should().Be(state.A);
            
            Mock<IFlagsRegister>().VerifySet(x => x.Register = It.Is<byte>(y => y == state.F));
        }
    }
}
