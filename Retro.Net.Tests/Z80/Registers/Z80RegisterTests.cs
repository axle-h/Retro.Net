using System;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.State;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Registers
{
    public class Z80RegisterTests
    {
        private const ushort Expected16 = 0x1234;
        private const byte ExpectedHigh = 0x12;
        private const byte ExpectedLow = 0x34;
        private static readonly Func<Z80RegisterState> GetRegisterState = RngFactory.Build<Z80RegisterState>();

        private readonly Z80Registers _subject;
        private readonly Z80RegisterState _state;

        public Z80RegisterTests()
        {
            _state = GetRegisterState();
            _subject = new Z80Registers(_state);
        }

        [Fact]
        public void When_setting_8bit_IX()
        {
            _subject.IXh = ExpectedHigh;
            _subject.IXl = ExpectedLow;
            _subject.IX.ShouldBe(Expected16);
        }

        [Fact]
        public void When_setting_8bit_IY()
        {
            _subject.IYh = ExpectedHigh;
            _subject.IYl = ExpectedLow;
            _subject.IY.ShouldBe(Expected16);
        }

        [Fact]
        public void When_setting_16bit_IX()
        {
            _subject.IX = Expected16;
            _subject.IXh.ShouldBe(ExpectedHigh);
            _subject.IXl.ShouldBe(ExpectedLow);
        }

        [Fact]
        public void When_setting_16bit_IY()
        {
            _subject.IY = Expected16;
            _subject.IYh.ShouldBe(ExpectedHigh);
            _subject.IYl.ShouldBe(ExpectedLow);
        }

        [Fact]
        public void When_getting_register_state()
        {
            var state = _subject.GetZ80RegisterState();
            var asserts = GetAssertions(_state, state).ToArray();
            state.ShouldSatisfyAllConditions(asserts);
        }

        [Fact]
        public void When_setting_register_state()
        {
            var state = GetRegisterState();

            _subject.ResetToState(state);

            _subject.I.ShouldBe(state.I);
            _subject.R.ShouldBe(state.R);
            _subject.IX.ShouldBe(state.IX);
            _subject.IY.ShouldBe(state.IY);
            _subject.InterruptFlipFlop1.ShouldBe(state.InterruptFlipFlop1);
            _subject.InterruptFlipFlop2.ShouldBe(state.InterruptFlipFlop2);
            _subject.InterruptMode.ShouldBe(state.InterruptMode);
            _subject.ProgramCounter.ShouldBe(state.ProgramCounter);
            _subject.StackPointer.ShouldBe(state.StackPointer);
        }
        
        [Fact]
        public void When_switching_to_alternative_accumulator_and_flags_registers()
        {
            var primary = _subject.AccumulatorAndFlagsRegisters;
            _subject.SwitchToAlternativeAccumulatorAndFlagsRegisters();

            _subject.AccumulatorAndFlagsRegisters.ShouldNotBeSameAs(primary);
        }

        [Fact]
        public void When_switching_to_alternative_general_purpose_registers()
        {
            var primary = _subject.GeneralPurposeRegisters;
            _subject.SwitchToAlternativeGeneralPurposeRegisters();

            _subject.GeneralPurposeRegisters.ShouldNotBeSameAs(primary);
        }

        private static IEnumerable<Action> GetAssertions(Z80RegisterState expected, Z80RegisterState observed)
        {
            yield return () => observed.I.ShouldBe(expected.I);
            yield return () => observed.R.ShouldBe(expected.R);
            yield return () => observed.IX.ShouldBe(expected.IX);
            yield return () => observed.IY.ShouldBe(expected.IY);
            yield return () => observed.InterruptFlipFlop1.ShouldBe(expected.InterruptFlipFlop1);
            yield return () => observed.InterruptFlipFlop2.ShouldBe(expected.InterruptFlipFlop2);
            yield return () => observed.InterruptMode.ShouldBe(expected.InterruptMode);
            yield return () => observed.ProgramCounter.ShouldBe(expected.ProgramCounter);
            yield return () => observed.StackPointer.ShouldBe(expected.StackPointer);
        }
    }
}
