using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.State;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Z80.Registers
{
    public class Z80RegisterTests
    {
        private const ushort Expected16 = 0x1234;
        private const byte ExpectedHigh = 0x12;
        private const byte ExpectedLow = 0x34;

        private readonly Z80Registers _subject;
        private readonly Z80RegisterState _state;

        public Z80RegisterTests()
        {
            _state = new Fixture().Create<Z80RegisterState>();
            _subject = new Z80Registers(_state);
        }

        [Fact]
        public void When_setting_8bit_IX()
        {
            _subject.IXh = ExpectedHigh;
            _subject.IXl = ExpectedLow;
            _subject.IX.Should().Be(Expected16);
        }

        [Fact]
        public void When_setting_8bit_IY()
        {
            _subject.IYh = ExpectedHigh;
            _subject.IYl = ExpectedLow;
            _subject.IY.Should().Be(Expected16);
        }

        [Fact]
        public void When_setting_16bit_IX()
        {
            _subject.IX = Expected16;
            _subject.IXh.Should().Be(ExpectedHigh);
            _subject.IXl.Should().Be(ExpectedLow);
        }

        [Fact]
        public void When_setting_16bit_IY()
        {
            _subject.IY = Expected16;
            _subject.IYh.Should().Be(ExpectedHigh);
            _subject.IYl.Should().Be(ExpectedLow);
        }

        [Fact]
        public void When_getting_register_state()
        {
            var state = _subject.GetZ80RegisterState();
            state.I.Should().Be(_state.I);
            state.R.Should().Be(_state.R);
            state.IX.Should().Be(_state.IX);
            state.IY.Should().Be(_state.IY);
            state.InterruptFlipFlop1.Should().Be(_state.InterruptFlipFlop1);
            state.InterruptFlipFlop2.Should().Be(_state.InterruptFlipFlop2);
            state.InterruptMode.Should().Be(_state.InterruptMode);
            state.ProgramCounter.Should().Be(_state.ProgramCounter);
            state.StackPointer.Should().Be(_state.StackPointer);
        }

        [Fact]
        public void When_setting_register_state()
        {
            var state = new Fixture().Create<Z80RegisterState>();

            _subject.ResetToState(state);

            _subject.I.Should().Be(state.I);
            _subject.R.Should().Be(state.R);
            _subject.IX.Should().Be(state.IX);
            _subject.IY.Should().Be(state.IY);
            _subject.InterruptFlipFlop1.Should().Be(state.InterruptFlipFlop1);
            _subject.InterruptFlipFlop2.Should().Be(state.InterruptFlipFlop2);
            _subject.InterruptMode.Should().Be(state.InterruptMode);
            _subject.ProgramCounter.Should().Be(state.ProgramCounter);
            _subject.StackPointer.Should().Be(state.StackPointer);
        }
        
        [Fact]
        public void When_switching_to_alternative_accumulator_and_flags_registers()
        {
            var primary = _subject.AccumulatorAndFlagsRegisters;
            _subject.SwitchToAlternativeAccumulatorAndFlagsRegisters();

            _subject.AccumulatorAndFlagsRegisters.Should().NotBeSameAs(primary);
        }

        [Fact]
        public void When_switching_to_alternative_general_purpose_registers()
        {
            var primary = _subject.GeneralPurposeRegisters;
            _subject.SwitchToAlternativeGeneralPurposeRegisters();

            _subject.GeneralPurposeRegisters.Should().NotBeSameAs(primary);
        }
    }
}
