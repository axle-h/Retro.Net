using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Registers;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class ProgramFlowTests
    {
        [Fact] public void Reset() => TestCall(OpCode.Reset);

        [Fact] public void Call() => TestCall(OpCode.Call);

        [Fact]
        public void Jump()
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                SetupJump(fixture, OpCode.Jump, FlagTest.None);
            }
        }

        [Fact]
        public void JumpRelative()
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                SetupRelativeJump(fixture, OpCode.JumpRelative, FlagTest.None);
            }
        }

        [Fact]
        public void DecrementJumpRelativeIfNonZero_Success()
        {
            using (var fixture = new ExecuteFixture().DoNotHalt().RuntimeTiming(1, 5))
            {
                fixture.With(x => x.Registers.B = 2);
                SetupRelativeJump(fixture, OpCode.DecrementJumpRelativeIfNonZero, FlagTest.None);
                fixture.Assert(x => x.Registers.B.ShouldBe((byte) 1));
            }
        }

        [Fact]
        public void DecrementJumpRelativeIfNonZero_Fail()
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                fixture.With(x => x.Registers.B = 1);
                SetupRelativeJump(fixture, OpCode.DecrementJumpRelativeIfNonZero, FlagTest.None, false);
                fixture.Assert(x => x.Registers.B.ShouldBe((byte) 0));
            }
        }

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void CallWithTest_Success(FlagTest test)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt().RuntimeTiming(2, 7))
            {
                fixture.With(c => c.Flags.Setup(GetFlagExpression(test)).Returns(GetPositiveValue(test)));
                SetupCall(fixture, OpCode.Call, test);
            }
        }

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void CallWithTest_Fail(FlagTest test)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                fixture.With(c => c.Flags.Setup(GetFlagExpression(test)).Returns(!GetPositiveValue(test)));
                SetupCall(fixture, OpCode.Call, test, false);
            }
        }

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void JumpWithTest_Success(FlagTest test)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                fixture.With(c => c.Flags.Setup(GetFlagExpression(test)).Returns(GetPositiveValue(test)));
                SetupJump(fixture, OpCode.Jump, test);
            }
        }

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void JumpWithTest_Fail(FlagTest test)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                fixture.With(c => c.Flags.Setup(GetFlagExpression(test)).Returns(!GetPositiveValue(test)));
                SetupJump(fixture, OpCode.Jump, test, false);
            }
        }

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void JumpRelativeWithTest_Success(FlagTest test)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt().RuntimeTiming(1, 5))
            {
                fixture.With(c => c.Flags.Setup(GetFlagExpression(test)).Returns(GetPositiveValue(test)));
                SetupRelativeJump(fixture, OpCode.JumpRelative, test);
            }
        }

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void JumpRelativeWithTest_Fail(FlagTest test)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                fixture.With(c => c.Flags.Setup(GetFlagExpression(test)).Returns(!GetPositiveValue(test)));
                SetupRelativeJump(fixture, OpCode.JumpRelative, test, false);
            }
        }

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void Return_Success(FlagTest test)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt().RuntimeTiming(2, 6))
            {
                fixture.With(c => c.Flags.Setup(GetFlagExpression(test)).Returns(GetPositiveValue(test)));
                SetupReturn(fixture, OpCode.Return, test);
            }
        }

        [Theory]
        [MemberData(nameof(FlagTests))]
        public void Return_Fail(FlagTest test)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                fixture.With(c => c.Flags.Setup(GetFlagExpression(test)).Returns(!GetPositiveValue(test)));
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

        [Fact] public void Stop() => TestHalt(OpCode.Stop);

        [Fact] public void Halt() => TestHalt(OpCode.Halt);

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
            SetupJump(fixture, op, test, success);
            var times = success ? Times.Exactly(1) : Times.Never();
            fixture.Assert(c => c.MockRegisters.VerifySet(r => r.StackPointer = c.PushedStackPointer, times),
                c => c.Mmu.Verify(x => x.WriteWord(c.StackPointer, c.SyncedProgramCounter), times));
        }

        private static void SetupJump(ExecuteFixture fixture, OpCode op, FlagTest test, bool success = true)
        {
            fixture.Operation.OpCode(op).Operands(Operand.nn).RandomLiterals().FlagTest(test);
            var times = success ? Times.Exactly(1) : Times.Never();
            fixture.Assert(c => c.MockRegisters.VerifySet(r => r.ProgramCounter = c.Operation.WordLiteral, times));
        }

        private static void SetupRelativeJump(ExecuteFixture fixture, OpCode op, FlagTest test, bool success = true)
        {
            fixture.Operation.OpCode(op).RandomLiterals().FlagTest(test);
            var times = success ? Times.Exactly(1) : Times.Never();
            fixture.Assert(c => c.MockRegisters.VerifySet(r => r.ProgramCounter = unchecked ((ushort) (c.InitialProgramCounter + c.BlockLength + c.Operation.Displacement)), times));
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

        private static void TestHalt(OpCode op)
        {
            using (var fixture = new ExecuteFixture().DoNotHalt())
            {
                fixture.Operation.OpCode(op);
            }
        }
    }
}
