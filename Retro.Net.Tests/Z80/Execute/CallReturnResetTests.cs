using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Registers;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class CallReturnResetTests
    {
        [Fact] public void Reset() => TestCall(OpCode.Reset);

        [Fact] public void Call() => TestCall(OpCode.Call);

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void CallWithTest(FlagTest test)
        {
            var positiveValue = GetPositiveValue(test);
            var flag = GetFlagExpression(test);

            using (var fixture = new ExecuteFixture().DoNotHalt().RuntimeTiming(2, 7))
            {
                fixture.With(c => c.Flags.Setup(flag).Returns(positiveValue));
                SetupCall(fixture, OpCode.Call, test);
            }

            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                fixture.With(c => c.Flags.Setup(flag).Returns(!positiveValue));
                SetupCall(fixture, OpCode.Call, test, false);
            }
        }

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void Return(FlagTest test)
        {
            var positiveValue = GetPositiveValue(test);
            var flag = GetFlagExpression(test);

            using (var fixture = new ExecuteFixture().DoNotHalt().RuntimeTiming(2, 6))
            {
                fixture.With(c => c.Flags.Setup(flag).Returns(positiveValue));
                SetupReturn(fixture, OpCode.Return, test);
            }

            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                fixture.With(c => c.Flags.Setup(flag).Returns(!positiveValue));
                SetupReturn(fixture, OpCode.Return, test, false);
            }
        }

        [Fact]
        public void ReturnFromInterrupt()
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                SetupReturn(fixture, OpCode.ReturnFromInterrupt, FlagTest.None);
                fixture.Assert(c => c.MockRegisters.VerifySet(x => x.InterruptFlipFlop1 = true));
            }
        }

        [Fact]
        public void ReturnFromNonmaskableInterrupt()
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                SetupReturn(fixture, OpCode.ReturnFromNonmaskableInterrupt, FlagTest.None);
                fixture.With(c => c.MockRegisters.Setup(x => x.InterruptFlipFlop2).Returns(true));
                fixture.Assert(c => c.MockRegisters.VerifySet(x => x.InterruptFlipFlop1 = true));
            }
        }

        private static IEnumerable<object[]> FlagTests() => new SimpleTheorySource<FlagTest>(FlagTest.Carry, FlagTest.Negative,
            FlagTest.NotCarry, FlagTest.NotZero, FlagTest.ParityEven, FlagTest.ParityOdd, FlagTest.Positive, FlagTest.Zero);

        private static void TestCall(OpCode op)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                SetupCall(fixture, op, FlagTest.None);
            }
        }

        private static bool GetPositiveValue(FlagTest test)
        {
            switch (test)
            {
                case FlagTest.NotZero:
                case FlagTest.NotCarry:
                case FlagTest.ParityOdd:
                case FlagTest.Positive:
                    return false;
                case FlagTest.Zero:
                case FlagTest.Carry:
                case FlagTest.ParityEven:
                case FlagTest.Negative:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException(nameof(test), test, null);
            }
        }

        private static Expression<Func<IFlagsRegister, bool>> GetFlagExpression(FlagTest test)
        {
            switch (test)
            {
                case FlagTest.NotZero:
                case FlagTest.Zero:
                    return f => f.Zero;
                case FlagTest.NotCarry:
                case FlagTest.Carry:
                    return f => f.Carry;
                case FlagTest.ParityOdd:
                case FlagTest.ParityEven:
                    return f => f.ParityOverflow;
                case FlagTest.Positive:
                case FlagTest.Negative:
                    return f => f.Sign;
                default:
                    throw new ArgumentOutOfRangeException(nameof(test), test, null);
            }
        }

        private static void SetupCall(ExecuteFixture fixture, OpCode op, FlagTest test, bool success = true)
        {
            fixture.Operation.OpCode(op).Operands(Operand.nn).RandomLiterals().FlagTest(test);
            var times = success ? Times.Exactly(1) : Times.Never();
            fixture.Assert(c => c.MockRegisters.VerifySet(r => r.ProgramCounter = c.Operation.WordLiteral, times),
                c => c.MockRegisters.VerifySet(r => r.StackPointer = c.PushedStackPointer, times),
                c => c.Mmu.Verify(x => x.WriteWord(c.StackPointer, c.SyncedProgramCounter), times));

        }

        private static void SetupReturn(ExecuteFixture fixture, OpCode op, FlagTest test, bool success = true)
        {
            fixture.Operation.OpCode(op).FlagTest(test);
            if (success)
            {
                fixture.With(c => c.Mmu.Setup(x => x.ReadWord(c.InitialStackPointer)).Returns(c.Word).Verifiable());
                fixture.Assert(c => c.MockRegisters.VerifySet(r => r.StackPointer = c.PoppedStackPointer),
                    c => c.MockRegisters.VerifySet(r => r.ProgramCounter = c.Word));
            }
            else
            {
                fixture.Assert(c => c.Mmu.Verify(x => x.ReadWord(c.InitialStackPointer), Times.Never),
                    c => c.MockRegisters.VerifySet(r => r.StackPointer = c.PoppedStackPointer, Times.Never));
            }
        }
    }
}
