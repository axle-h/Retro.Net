using System;
using System.Collections.Generic;
using System.Text;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class CallReturnResetTests
    {
        [Theory]
        [InlineData(PrimaryOpCode.RST_00, (ushort) 0x0000)]
        [InlineData(PrimaryOpCode.RST_08, (ushort) 0x0008)]
        [InlineData(PrimaryOpCode.RST_10, (ushort) 0x0010)]
        [InlineData(PrimaryOpCode.RST_18, (ushort) 0x0018)]
        [InlineData(PrimaryOpCode.RST_20, (ushort) 0x0020)]
        [InlineData(PrimaryOpCode.RST_28, (ushort) 0x0028)]
        [InlineData(PrimaryOpCode.RST_30, (ushort) 0x0030)]
        [InlineData(PrimaryOpCode.RST_38, (ushort) 0x0038)]
        public void RST_nn(PrimaryOpCode op, ushort address)
        {
            using (var fixture = new DecodeFixture(3, 11, op).DoNotHalt())
            {
                fixture.Expected.OpCode(OpCode.Reset).Operands(Operand.nn).WordLiteral(address);
            }
        }

        [Fact] public void CALL() => TestCall(PrimaryOpCode.CALL, FlagTest.None, 5, 17);

        [Fact] public void CALL_C() => TestCall(PrimaryOpCode.CALL_C, FlagTest.Carry);

        [Fact] public void CALL_M() => TestCall(PrimaryOpCode.CALL_M, FlagTest.Negative, gameboy: false);

        [Fact] public void CALL_NC() => TestCall(PrimaryOpCode.CALL_NC, FlagTest.NotCarry);

        [Fact] public void CALL_NZ() => TestCall(PrimaryOpCode.CALL_NZ, FlagTest.NotZero);

        [Fact] public void CALL_P() => TestCall(PrimaryOpCode.CALL_P, FlagTest.Positive, gameboy: false);

        [Fact] public void CALL_PE() => TestCall(PrimaryOpCode.CALL_PE, FlagTest.ParityEven, gameboy: false);

        [Fact] public void CALL_PO() => TestCall(PrimaryOpCode.CALL_PO, FlagTest.ParityOdd, gameboy: false);

        [Fact] public void CALL_Z() => TestCall(PrimaryOpCode.CALL_Z, FlagTest.Zero);

        [Fact] public void RET() => TestReturn(PrimaryOpCode.RET, FlagTest.None, 3, 10);

        [Fact] public void RET_C() => TestReturn(PrimaryOpCode.RET_C, FlagTest.Carry);

        [Fact] public void RET_M() => TestReturn(PrimaryOpCode.RET_M, FlagTest.Negative, gameboy: false);

        [Fact] public void RET_NC() => TestReturn(PrimaryOpCode.RET_NC, FlagTest.NotCarry);

        [Fact] public void RET_NZ() => TestReturn(PrimaryOpCode.RET_NZ, FlagTest.NotZero);

        [Fact] public void RET_P() => TestReturn(PrimaryOpCode.RET_P, FlagTest.Positive, gameboy: false);

        [Fact] public void RET_PE() => TestReturn(PrimaryOpCode.RET_PE, FlagTest.ParityEven, gameboy: false);

        [Fact] public void RET_PO() => TestReturn(PrimaryOpCode.RET_PO, FlagTest.ParityOdd, gameboy: false);

        [Fact] public void RET_Z() => TestReturn(PrimaryOpCode.RET_Z, FlagTest.Zero);
        
        [Fact] public void RETI() => TestPrefixEd(PrefixEdOpCode.RETI, OpCode.ReturnFromInterrupt);

        [Fact] public void RETN() => TestPrefixEd(PrefixEdOpCode.RETN, OpCode.ReturnFromNonmaskableInterrupt);

        private static void TestCall(PrimaryOpCode op, FlagTest flagTest, int machineCycles = 3, int throttlingStates = 10, bool gameboy = true)
        {
            var address = Rng.Word();
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op, address).DoNotHalt().ThrowOnGameboy(!gameboy))
            {
                fixture.Expected.OpCode(OpCode.Call).FlagTest(flagTest).Operands(Operand.nn).WordLiteral(address);
            }
        }

        private static void TestReturn(PrimaryOpCode op, FlagTest flagTest, int machineCycles = 1, int throttlingStates = 5, bool gameboy = true)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op).DoNotHalt().ThrowOnGameboy(!gameboy))
            {
                fixture.Expected.OpCode(OpCode.Return).FlagTest(flagTest);
            }
        }

        private static void TestPrefixEd(PrefixEdOpCode op, OpCode expected)
        {
            using (var fixture = new DecodeFixture(4, 14, PrimaryOpCode.Prefix_ED, op).DoNotHalt().ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected);
            }
        }
    }
}
