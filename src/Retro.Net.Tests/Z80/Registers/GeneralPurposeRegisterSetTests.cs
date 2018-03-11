using AutoFixture;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.State;
using FluentAssertions;
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
            Subject.BC.Should().Be(Expected16);
        }

        [Fact]
        public void When_setting_8bit_DE()
        {
            Subject.D = ExpectedHigh;
            Subject.E = ExpectedLow;
            Subject.DE.Should().Be(Expected16);
        }

        [Fact]
        public void When_setting_8bit_HL()
        {
            Subject.H = ExpectedHigh;
            Subject.L = ExpectedLow;
            Subject.HL.Should().Be(Expected16);
        }

        [Fact]
        public void When_setting_16bit_BC()
        {
            Subject.BC = Expected16;
            Subject.B.Should().Be(ExpectedHigh);
            Subject.C.Should().Be(ExpectedLow);
        }

        [Fact]
        public void When_setting_16bit_DE()
        {
            Subject.DE = Expected16;
            Subject.D.Should().Be(ExpectedHigh);
            Subject.E.Should().Be(ExpectedLow);
        }

        [Fact]
        public void When_setting_16bit_HL()
        {
            Subject.HL = Expected16;
            Subject.H.Should().Be(ExpectedHigh);
            Subject.L.Should().Be(ExpectedLow);
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
            state.B.Should().Be(b);
            state.C.Should().Be(c);
            state.D.Should().Be(d);
            state.E.Should().Be(e);
            state.H.Should().Be(h);
            state.L.Should().Be(l);
        }

        [Fact]
        public void When_setting_register_state()
        {
            var state = new Fixture().Create<GeneralPurposeRegisterState>();
            Subject.ResetToState(state);

            Subject.B.Should().Be(state.B);
            Subject.C.Should().Be(state.C);
            Subject.D.Should().Be(state.D);
            Subject.E.Should().Be(state.E);
            Subject.H.Should().Be(state.H);
            Subject.L.Should().Be(state.L);
        }
    }
}
