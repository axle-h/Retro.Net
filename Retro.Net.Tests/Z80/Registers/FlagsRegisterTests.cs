using System;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Tests.Util;
using Retro.Net.Tests.Z80.Registers.Data;
using Retro.Net.Z80.Registers;
using Shouldly;
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

            Subject.Register.ShouldBe(test.Value);
        }

        private void AssertSubject(TestDataEntry<byte, Intel8080FlagsRegister> test)
        {
            var assertions = GetAssertions(test.Expected, Subject).ToArray();
            Subject.ShouldSatisfyAllConditions(test.ToString(), assertions);
        }

        private static IEnumerable<Action> GetAssertions(IFlagsRegister expected, IFlagsRegister observed)
        {
            yield return () => observed.Sign.ShouldBe(expected.Sign);
            yield return () => observed.Zero.ShouldBe(expected.Zero);
            yield return () => observed.Flag5.ShouldBe(expected.Flag5);
            yield return () => observed.HalfCarry.ShouldBe(expected.HalfCarry);
            yield return () => observed.Flag3.ShouldBe(expected.Flag3);
            yield return () => observed.ParityOverflow.ShouldBe(expected.ParityOverflow);
            yield return () => observed.Subtract.ShouldBe(expected.Subtract);
            yield return () => observed.Carry.ShouldBe(expected.Carry);
        }
    }
}
