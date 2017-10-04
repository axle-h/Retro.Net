using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.State;
using Shouldly;
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
            The<IFlagsRegister>().Setup(x => x.Register).Returns(F);
            Subject.AF.ShouldBe(AF);
        }
        
        [Fact]
        public void When_writing_AF()
        {
            Subject.AF = AF;
            Subject.A.ShouldBe(A);
            The<IFlagsRegister>().VerifySet(x => x.Register = It.Is<byte>(y => y == F));
        }

        [Fact]
        public void When_getting_register_state()
        {
            Subject.A = A;
            The<IFlagsRegister>().Setup(x => x.Register).Returns(F);

            var state = Subject.GetRegisterState();

            state.A.ShouldBe(A);
            state.F.ShouldBe(F);
        }
        
        [Fact]
        public void When_setting_register_state()
        {
            var state = RngFactory.Build<AccumulatorAndFlagsRegisterState>()();

            Subject.ResetToState(state);

            Subject.A.ShouldBe(state.A);
            
            The<IFlagsRegister>().VerifySet(x => x.Register = It.Is<byte>(y => y == state.F));
        }
    }
}
