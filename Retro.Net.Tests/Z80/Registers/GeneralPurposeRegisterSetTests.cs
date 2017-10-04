using Retro.Net.Tests.Util;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.State;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Registers
{
    public class GeneralPurposeRegisterSetTests : WithSubject<GeneralPurposeRegisterSet>
    {
        private const ushort Expected16 = 0x1234;
        private const byte ExpectedHigh = 0x12;
        private const byte ExpectedLow = 0x34;

        [Fact]
        public void When_setting_8bit_BC()
        {
            Subject.B = ExpectedHigh;
            Subject.C = ExpectedLow;
            Subject.BC.ShouldBe(Expected16);
        }

        [Fact]
        public void When_setting_8bit_DE()
        {
            Subject.D = ExpectedHigh;
            Subject.E = ExpectedLow;
            Subject.DE.ShouldBe(Expected16);
        }

        [Fact]
        public void When_setting_8bit_HL()
        {
            Subject.H = ExpectedHigh;
            Subject.L = ExpectedLow;
            Subject.HL.ShouldBe(Expected16);
        }

        [Fact]
        public void When_setting_16bit_BC()
        {
            Subject.BC = Expected16;
            Subject.B.ShouldBe(ExpectedHigh);
            Subject.C.ShouldBe(ExpectedLow);
        }

        [Fact]
        public void When_setting_16bit_DE()
        {
            Subject.DE = Expected16;
            Subject.D.ShouldBe(ExpectedHigh);
            Subject.E.ShouldBe(ExpectedLow);
        }

        [Fact]
        public void When_setting_16bit_HL()
        {
            Subject.HL = Expected16;
            Subject.H.ShouldBe(ExpectedHigh);
            Subject.L.ShouldBe(ExpectedLow);
        }

        [Fact]
        public void When_getting_register_state()
        {
            const byte b = 0xbb, c = 0xcc, d = 0xdd, e = 0xee, h = 0x11, l = 0x22;

            Subject.B = b;
            Subject.C = c;
            Subject.D = d;
            Subject.E = e;
            Subject.H = h;
            Subject.L = l;

            var state = Subject.GetRegisterState();
            state.B.ShouldBe(b);
            state.C.ShouldBe(c);
            state.D.ShouldBe(d);
            state.E.ShouldBe(e);
            state.H.ShouldBe(h);
            state.L.ShouldBe(l);
        }

        [Fact]
        public void When_setting_register_state()
        {
            var state = RngFactory.Build<GeneralPurposeRegisterState>()();
            Subject.ResetToState(state);

            Subject.B.ShouldBe(state.B);
            Subject.C.ShouldBe(state.C);
            Subject.D.ShouldBe(state.D);
            Subject.E.ShouldBe(state.E);
            Subject.H.ShouldBe(state.H);
            Subject.L.ShouldBe(state.L);
        }
    }
}
