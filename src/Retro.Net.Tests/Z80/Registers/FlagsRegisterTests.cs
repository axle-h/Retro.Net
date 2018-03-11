using System;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Tests.Util;
using Retro.Net.Tests.Z80.Registers.Data;
using Retro.Net.Z80.Registers;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Z80.Registers
{
    public class FlagsRegisterTests : WithSubject<Intel8080FlagsRegister>
    {
        [Theory]
        [MemberData(nameof(FlagsTestData.SetUndocumentedFlags), MemberType = typeof(FlagsTestData))]
        public void When_setting_undocumented_flags(TestDataEntry<byte, Intel8080FlagsRegister> test)
        {
            Subject.SetUndocumentedFlags(test.Value);
            AssertSubject(test);
        }

        [Theory]
        [MemberData(nameof(FlagsTestData.SetResultFlags), MemberType = typeof(FlagsTestData))]
        public void When_setting_result_flags(TestDataEntry<byte, Intel8080FlagsRegister> test)
        {
            Subject.SetResultFlags(test.Value);
            AssertSubject(test);
        }

        [Theory]
        [MemberData(nameof(FlagsTestData.SetParityFlags), MemberType = typeof(FlagsTestData))]
        public void When_setting_parity_flags(TestDataEntry<byte, Intel8080FlagsRegister> test)
        {
            Subject.SetParityFlags(test.Value);
            AssertSubject(test);
        }

        [Theory]
        [MemberData(nameof(FlagsTestData.SetFlags), MemberType = typeof(FlagsTestData))]
        public void When_setting_flags_via_register(TestDataEntry<byte, Intel8080FlagsRegister> test)
        {
            Subject.Register = test.Value;
            AssertSubject(test);
        }

        [Theory]
        [MemberData(nameof(FlagsTestData.SetFlags), MemberType = typeof(FlagsTestData))]
        public void When_setting_flags_directly(TestDataEntry<byte, Intel8080FlagsRegister> test)
        {
            Subject.Sign = test.Expected.Sign;
            Subject.Zero = test.Expected.Zero;
            Subject.Flag5 = test.Expected.Flag5;
            Subject.HalfCarry = test.Expected.HalfCarry;
            Subject.Flag3 = test.Expected.Flag3;
            Subject.ParityOverflow = test.Expected.ParityOverflow;
            Subject.Subtract = test.Expected.Subtract;
            Subject.Carry = test.Expected.Carry;

            Subject.Register.Should().Be(test.Value);
        }

        private void AssertSubject(TestDataEntry<byte, Intel8080FlagsRegister> test)
        {
            Subject.Sign.Should().Be(test.Expected.Sign);
            Subject.Zero.Should().Be(test.Expected.Zero);
            Subject.Flag5.Should().Be(test.Expected.Flag5);
            Subject.HalfCarry.Should().Be(test.Expected.HalfCarry);
            Subject.Flag3.Should().Be(test.Expected.Flag3);
            Subject.ParityOverflow.Should().Be(test.Expected.ParityOverflow);
            Subject.Subtract.Should().Be(test.Expected.Subtract);
            Subject.Carry.Should().Be(test.Expected.Carry);
        }
    }
}
