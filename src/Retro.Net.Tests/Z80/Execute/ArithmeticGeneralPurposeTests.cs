using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Registers;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class ArithmeticGeneralPurposeTests
    {
        [Fact] public void InvertCarryFlag() => FlagAflection(OpCode.InvertCarryFlag, halfCarry: false, subtract: false, carry: true);

        [Fact] public void SetCarryFlag() => FlagAflection(OpCode.InvertCarryFlag, halfCarry: false, subtract: false, carry: true);

        [Fact]
        public void NegateOnesComplement()
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.NegateOnesComplement);
                fixture.Assert(c => c.Accumulator.A.Should().Be((byte)~c.InitialAccumulator.A));
                fixture.AssertFlags(c => c.Accumulator.A, halfCarry: true, subtract: true);
            }
        }

        [Fact]
        public void DecimalArithmeticAdjust()
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.DecimalArithmeticAdjust);
                fixture.With(c => c.Alu.Setup(alu => alu.DecimalAdjust(c.Accumulator.A, true)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Accumulator.A.Should().Be(c.Byte));
            }
        }

        [Fact] public void DisableInterrupts() => ManageInterrupts(OpCode.DisableInterrupts, false);

        [Fact] public void EnableInterrupts() => ManageInterrupts(OpCode.EnableInterrupts, true);

        [Fact] public void InterruptMode0() => SetInterruptMode(OpCode.InterruptMode0, InterruptMode.InterruptMode0);

        [Fact] public void InterruptMode1() => SetInterruptMode(OpCode.InterruptMode1, InterruptMode.InterruptMode1);

        [Fact] public void InterruptMode2() => SetInterruptMode(OpCode.InterruptMode2, InterruptMode.InterruptMode2);

        [Fact]
        public void NegateTwosComplement()
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.NegateTwosComplement);
                fixture.With(c => c.Alu.Setup(alu => alu.Subtract(0, c.Accumulator.A)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Accumulator.A.Should().Be(c.Byte));
            }
        }

        private static void ManageInterrupts(OpCode o, bool value)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(o);
                fixture.Assert(c => c.MockRegisters.VerifySet(r => r.InterruptFlipFlop1 = value), c => c.MockRegisters.VerifySet(r => r.InterruptFlipFlop2 = value));
            }
        }

        private static void FlagAflection(OpCode op, bool? sign = null, bool? zero = null, bool? halfCarry = null,
            bool? parityOverflow = null, bool? subtract = null, bool? carry = null)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op);
                fixture.AssertFlags(c => c.Accumulator.A, sign, zero, halfCarry, parityOverflow, subtract, carry);
            }
        }

        private static void SetInterruptMode(OpCode op, InterruptMode mode)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op);
                fixture.Assert(c => c.MockRegisters.VerifySet(x => x.InterruptMode = mode));
            }
        }
    }
}
