using System.Collections.Generic;
using System.Linq;
using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Registers;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80
{
    public class ArithmeticLogicUnitTests : WithSubject<Alu>
    {
        private readonly Mock<IFlagsRegister> _flags;

        public ArithmeticLogicUnitTests()
        {
            _flags = The<IFlagsRegister>().SetupAllProperties();
            var registers = The<IRegisters>();
            registers.Setup(x => x.AccumulatorAndFlagsRegisters).Returns(new AccumulatorAndFlagsRegisterSet(_flags.Object));
        }

        [Theory]
        [InlineData(0x4e, 0x4f, false, false)]
        [InlineData(0x0f, 0x10, true, false)]
        [InlineData(0x7f, 0x80, true, true)]
        public void Increment(byte a, byte expected, bool halfCarry, bool overflow)
        {
            var result = Subject.Increment(a);

            result.ShouldBe(expected);

            AssertFlags(result, null, null, halfCarry, overflow, false, null);
        }

        [Theory]
        [InlineData(0x4e, 0x4d, false, false)]
        [InlineData(0xf0, 0xef, true, false)]
        [InlineData(0x80, 0x7f, true, true)]
        public void Decrement(byte a, byte expected, bool halfCarry, bool overflow)
        {
            var result = Subject.Decrement(a);

            result.ShouldBe(expected);

            AssertFlags(result, null, null, halfCarry, overflow, true, null);
        }

        [Theory]
        [InlineData(0, 0, 0, false, false, false)]
        [InlineData(0, 1, 1, false, false, false)]
        [InlineData(0, 127, 127, false, false, false)]
        [InlineData(0, 128, 128, false, false, false)]
        [InlineData(0, 129, 129, false, false, false)]
        [InlineData(0, 255, 255, false, false, false)]
        [InlineData(1, 0, 1, false, false, false)]
        [InlineData(1, 1, 2, false, false, false)]
        [InlineData(1, 127, 128, true, false, true)]
        [InlineData(1, 128, 129, false, false, false)]
        [InlineData(1, 129, 130, false, false, false)]
        [InlineData(1, 255, 0, true, true, false)]
        [InlineData(127, 0, 127, false, false, false)]
        [InlineData(127, 1, 128, true, false, true)]
        [InlineData(127, 127, 254, true, false, true)]
        [InlineData(127, 128, 255, false, false, false)]
        [InlineData(127, 129, 0, true, true, false)]
        [InlineData(127, 255, 126, true, true, false)]
        [InlineData(128, 0, 128, false, false, false)]
        [InlineData(128, 1, 129, false, false, false)]
        [InlineData(128, 127, 255, false, false, false)]
        [InlineData(128, 128, 0, false, true, true)]
        [InlineData(128, 129, 1, false, true, true)]
        [InlineData(128, 255, 127, false, true, true)]
        [InlineData(129, 0, 129, false, false, false)]
        [InlineData(129, 1, 130, false, false, false)]
        [InlineData(129, 127, 0, true, true, false)]
        [InlineData(129, 128, 1, false, true, true)]
        [InlineData(129, 129, 2, false, true, true)]
        [InlineData(129, 255, 128, true, true, false)]
        [InlineData(255, 0, 255, false, false, false)]
        [InlineData(255, 1, 0, true, true, false)]
        [InlineData(255, 127, 126, true, true, false)]
        [InlineData(255, 128, 127, false, true, true)]
        [InlineData(255, 129, 128, true, true, false)]
        [InlineData(255, 255, 254, true, true, false)]
        public void Add(byte a, byte b, byte expected, bool halfCarry, bool carry, bool overflow)
        {
            var result = Subject.Add(a, b);

            result.ShouldBe(expected);

            AssertFlags(result, null, null, halfCarry, overflow, false, carry);
        }

        [Theory]
        [InlineData(0, 0, 1, false, false, false)]
        [InlineData(0, 1, 2, false, false, false)]
        [InlineData(0, 127, 128, true, false, true)]
        [InlineData(0, 128, 129, false, false, false)]
        [InlineData(0, 129, 130, false, false, false)]
        [InlineData(0, 255, 0, true, true, false)]
        [InlineData(1, 0, 2, false, false, false)]
        [InlineData(1, 1, 3, false, false, false)]
        [InlineData(1, 127, 129, true, false, true)]
        [InlineData(1, 128, 130, false, false, false)]
        [InlineData(1, 129, 131, false, false, false)]
        [InlineData(1, 255, 1, true, true, false)]
        [InlineData(127, 0, 128, true, false, true)]
        [InlineData(127, 1, 129, true, false, true)]
        [InlineData(127, 127, 255, true, false, true)]
        [InlineData(127, 128, 0, true, true, false)]
        [InlineData(127, 129, 1, true, true, false)]
        [InlineData(127, 255, 127, true, true, false)]
        [InlineData(128, 0, 129, false, false, false)]
        [InlineData(128, 1, 130, false, false, false)]
        [InlineData(128, 127, 0, true, true, false)]
        [InlineData(128, 128, 1, false, true, true)]
        [InlineData(128, 129, 2, false, true, true)]
        [InlineData(128, 255, 128, true, true, false)]
        [InlineData(129, 0, 130, false, false, false)]
        [InlineData(129, 1, 131, false, false, false)]
        [InlineData(129, 127, 1, true, true, false)]
        [InlineData(129, 128, 2, false, true, true)]
        [InlineData(129, 129, 3, false, true, true)]
        [InlineData(129, 255, 129, true, true, false)]
        [InlineData(255, 0, 0, true, true, false)]
        [InlineData(255, 1, 1, true, true, false)]
        [InlineData(255, 127, 127, true, true, false)]
        [InlineData(255, 128, 128, true, true, false)]
        [InlineData(255, 129, 129, true, true, false)]
        [InlineData(255, 255, 255, true, true, false)]
        public void AddWithCarry(byte a, byte b, byte expected, bool halfCarry, bool carry, bool overflow)
        {
            _flags.SetupProperty(x => x.Carry, true);

            var result = Subject.AddWithCarry(a, b);
            result.ShouldBe(expected);

            AssertFlags(result, null, null, halfCarry, overflow, false, carry);
        }

        [Theory]
        [InlineData(0, 0, 0, false, false, false)]
        [InlineData(0, 1, 255, true, true, false)]
        [InlineData(0, 127, 129, true, true, false)]
        [InlineData(0, 128, 128, false, true, true)]
        [InlineData(0, 129, 127, true, true, false)]
        [InlineData(0, 255, 1, true, true, false)]
        [InlineData(1, 0, 1, false, false, false)]
        [InlineData(1, 1, 0, false, false, false)]
        [InlineData(1, 127, 130, true, true, false)]
        [InlineData(1, 128, 129, false, true, true)]
        [InlineData(1, 129, 128, false, true, true)]
        [InlineData(1, 255, 2, true, true, false)]
        [InlineData(127, 0, 127, false, false, false)]
        [InlineData(127, 1, 126, false, false, false)]
        [InlineData(127, 127, 0, false, false, false)]
        [InlineData(127, 128, 255, false, true, true)]
        [InlineData(127, 129, 254, false, true, true)]
        [InlineData(127, 255, 128, false, true, true)]
        [InlineData(128, 0, 128, false, false, false)]
        [InlineData(128, 1, 127, true, false, true)]
        [InlineData(128, 127, 1, true, false, true)]
        [InlineData(128, 128, 0, false, false, false)]
        [InlineData(128, 129, 255, true, true, false)]
        [InlineData(128, 255, 129, true, true, false)]
        [InlineData(129, 0, 129, false, false, false)]
        [InlineData(129, 1, 128, false, false, false)]
        [InlineData(129, 127, 2, true, false, true)]
        [InlineData(129, 128, 1, false, false, false)]
        [InlineData(129, 129, 0, false, false, false)]
        [InlineData(129, 255, 130, true, true, false)]
        [InlineData(255, 0, 255, false, false, false)]
        [InlineData(255, 1, 254, false, false, false)]
        [InlineData(255, 127, 128, false, false, false)]
        [InlineData(255, 128, 127, false, false, false)]
        [InlineData(255, 129, 126, false, false, false)]
        [InlineData(255, 255, 0, false, false, false)]
        public void Subtract(byte a, byte b, byte expected, bool halfCarry, bool carry, bool overflow)
        {
            var result = Subject.Subtract(a, b);
            result.ShouldBe(expected);

            AssertFlags(result, null, null, halfCarry, overflow, true, carry);
        }

        [Theory]
        [InlineData(0, 0, 255, true, true, false)]
        [InlineData(0, 1, 254, true, true, false)]
        [InlineData(0, 127, 128, true, true, false)]
        [InlineData(0, 128, 127, true, true, false)]
        [InlineData(0, 129, 126, true, true, false)]
        [InlineData(0, 255, 0, true, true, false)]
        [InlineData(1, 0, 0, false, false, false)]
        [InlineData(1, 1, 255, true, true, false)]
        [InlineData(1, 127, 129, true, true, false)]
        [InlineData(1, 128, 128, false, true, true)]
        [InlineData(1, 129, 127, true, true, false)]
        [InlineData(1, 255, 1, true, true, false)]
        [InlineData(127, 0, 126, false, false, false)]
        [InlineData(127, 1, 125, false, false, false)]
        [InlineData(127, 127, 255, true, true, false)]
        [InlineData(127, 128, 254, false, true, true)]
        [InlineData(127, 129, 253, false, true, true)]
        [InlineData(127, 255, 127, true, true, false)]
        [InlineData(128, 0, 127, true, false, true)]
        [InlineData(128, 1, 126, true, false, true)]
        [InlineData(128, 127, 0, true, false, true)]
        [InlineData(128, 128, 255, true, true, false)]
        [InlineData(128, 129, 254, true, true, false)]
        [InlineData(128, 255, 128, true, true, false)]
        [InlineData(129, 0, 128, false, false, false)]
        [InlineData(129, 1, 127, true, false, true)]
        [InlineData(129, 127, 1, true, false, true)]
        [InlineData(129, 128, 0, false, false, false)]
        [InlineData(129, 129, 255, true, true, false)]
        [InlineData(129, 255, 129, true, true, false)]
        [InlineData(255, 0, 254, false, false, false)]
        [InlineData(255, 1, 253, false, false, false)]
        [InlineData(255, 127, 127, true, false, true)]
        [InlineData(255, 128, 126, false, false, false)]
        [InlineData(255, 129, 125, false, false, false)]
        [InlineData(255, 255, 255, true, true, false)]
        public void SubtractWithCarry(byte a, byte b, byte expected, bool halfCarry, bool carry, bool overflow)
        {
            _flags.SetupProperty(x => x.Carry, true);

            var result = Subject.SubtractWithCarry(a, b);
            result.ShouldBe(expected);

            AssertFlags(result, null, null, halfCarry, overflow, true, carry);
        }

        [Theory]
        [InlineData(0, 0, 0, false, false)]
        [InlineData(0, 1, 255, true, false)]
        [InlineData(0, 127, 129, true, false)]
        [InlineData(0, 128, 128, false, true)]
        [InlineData(0, 129, 127, true, false)]
        [InlineData(0, 255, 1, true, false)]
        [InlineData(1, 0, 1, false, false)]
        [InlineData(1, 1, 0, false, false)]
        [InlineData(1, 127, 130, true, false)]
        [InlineData(1, 128, 129, false, true)]
        [InlineData(1, 129, 128, false, true)]
        [InlineData(1, 255, 2, true, false)]
        [InlineData(127, 0, 127, false, false)]
        [InlineData(127, 1, 126, false, false)]
        [InlineData(127, 127, 0, false, false)]
        [InlineData(127, 128, 255, false, true)]
        [InlineData(127, 129, 254, false, true)]
        [InlineData(127, 255, 128, false, true)]
        [InlineData(128, 0, 128, false, false)]
        [InlineData(128, 1, 127, true, true)]
        [InlineData(128, 127, 1, true, true)]
        [InlineData(128, 128, 0, false, false)]
        [InlineData(128, 129, 255, true, false)]
        [InlineData(128, 255, 129, true, false)]
        [InlineData(129, 0, 129, false, false)]
        [InlineData(129, 1, 128, false, false)]
        [InlineData(129, 127, 2, true, true)]
        [InlineData(129, 128, 1, false, false)]
        [InlineData(129, 129, 0, false, false)]
        [InlineData(129, 255, 130, true, false)]
        [InlineData(255, 0, 255, false, false)]
        [InlineData(255, 1, 254, false, false)]
        [InlineData(255, 127, 128, false, false)]
        [InlineData(255, 128, 127, false, false)]
        [InlineData(255, 129, 126, false, false)]
        [InlineData(255, 255, 0, false, false)]
        public void Compare(byte a, byte b, byte expected, bool halfCarry, bool overflow)
        {
            Subject.Compare(a, b);
            AssertFlags(expected, null, null, halfCarry, overflow, true, null);
        }

        [Theory]
        [InlineData(0x7b, 0xc3, 0x43)]
        public void And(byte a, byte b, byte expected)
        {
            var result = Subject.And(a, b);

            result.ShouldBe(expected);
            _flags.Verify(x => x.SetParityFlags(expected), Times.Once);
            _flags.VerifySet(x => x.HalfCarry = true, Times.Once);
            _flags.VerifySet(x => x.Carry = false, Times.Once);
        }

        [Theory]
        [InlineData(0x48, 0x12, 0x5a)]
        public void Or(byte a, byte b, byte expected)
        {
            var result = Subject.Or(a, b);

            result.ShouldBe(expected);
            _flags.Verify(x => x.SetParityFlags(expected), Times.Once);
            _flags.VerifySet(x => x.HalfCarry = false, Times.Once);
            _flags.VerifySet(x => x.Carry = false, Times.Once);
        }

        [Theory]
        [InlineData(0x96, 0x5d, 0xcb)]
        public void Xor(byte a, byte b, byte expected)
        {
            var result = Subject.Xor(a, b);

            result.ShouldBe(expected);
            _flags.Verify(x => x.SetParityFlags(expected), Times.Once);
            _flags.VerifySet(x => x.HalfCarry = false, Times.Once);
            _flags.VerifySet(x => x.Carry = false, Times.Once);
        }

        [Theory]
        [InlineData(0x15, 0x27, 0x42)]
        [InlineData(0x50, 0x18, 0x68)]
        public void DecimalAdjustAddition(byte a, byte b, byte expected)
        {
            var result = Subject.Add(a, b);
            var daa = Subject.DecimalAdjust(result, true);

            daa.ShouldBe(expected);
            _flags.Verify(x => x.SetResultFlags(expected), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(0x15, 0x27, 0x88)]
        [InlineData(0x50, 0x18, 0x32)]
        public void DecimalAdjustSubraction(byte a, byte b, byte expected)
        {
            var result = Subject.Subtract(a, b);
            var daa = Subject.DecimalAdjust(result, true);

            daa.ShouldBe(expected);
            _flags.Verify(x => x.SetResultFlags(expected), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData((ushort)0x4242, (ushort)0x1111, (ushort)0x5353, false, false)]
        [InlineData((ushort)0x0100, (ushort)0x7f00, (ushort)0x8000, true, false)]
        [InlineData((ushort)0xffff, (ushort)0x0001, (ushort)0x0000, true, true)]
        [InlineData((ushort)0xaaaa, (ushort)0xbbbb, (ushort)0x6665, true, true)]
        public void Add16(ushort a, ushort b, ushort expected, bool halfCarry, bool carry)
        {
            var result = Subject.Add(a, b);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, halfCarry, null, false, carry);
        }

        [Theory]
        [InlineData((ushort)0x4242, (ushort)0x1111, (ushort)0x5354, false, false, false)]
        [InlineData((ushort)0x0100, (ushort)0x7f00, (ushort)0x8001, true, true, false)]
        [InlineData((ushort)0xffff, (ushort)0x0001, (ushort)0x0001, true, false, true)]
        [InlineData((ushort)0xaaaa, (ushort)0xbbbb, (ushort)0x6666, true, true, true)]
        public void Add16WithCarry(ushort a, ushort b, ushort expected, bool halfCarry, bool overflow, bool carry)
        {
            _flags.SetupProperty(x => x.Carry, true);

            var result = Subject.AddWithCarry(a, b);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, halfCarry, overflow, false, carry);
            _flags.Verify(x => x.SetResultFlags(result), Times.Once);
        }

        [Theory]
        [InlineData((ushort)0x9999, (ushort)0x1111, (ushort)0x8887, false, false, false)]
        [InlineData((ushort)0x4242, (ushort)0x1111, (ushort)0x3130, false, false, false)]
        [InlineData((ushort)0x0100, (ushort)0x7f00, (ushort)0x81ff, true, false, true)]
        [InlineData((ushort)0xaaaa, (ushort)0x4444, (ushort)0x6665, false, true, false)]
        public void Subtract16WithCarry(ushort a, ushort b, ushort expected, bool halfCarry, bool overflow, bool carry)
        {
            _flags.SetupProperty(x => x.Carry, true);

            var result = Subject.SubtractWithCarry(a, b);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, halfCarry, overflow, true, carry);
            _flags.Verify(x => x.SetResultFlags(result), Times.Once);
        }

        [Theory]
        [InlineData(0x88, 0x11, true)]
        [InlineData(0x11, 0x22, false)]
        public void RotateLeftWithCarry(byte a, byte expected, bool expectedCarry)
        {
            var result = Subject.RotateLeftWithCarry(a);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, false, expectedCarry);

            _flags.Verify(x => x.SetResultFlags(result), Times.Once);
        }

        [Theory]
        [InlineData(false, 0x88, 0x10, true)]
        [InlineData(false, 0x11, 0x22, false)]
        [InlineData(true, 0x88, 0x11, true)]
        [InlineData(true, 0x11, 0x23, false)]
        public void RotateLeft(bool carry, byte a, byte expected, bool expectedCarry)
        {
            _flags.SetupProperty(x => x.Carry, carry);

            var result = Subject.RotateLeft(a);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, false, expectedCarry);

            _flags.Verify(x => x.SetResultFlags(result), Times.Once);
        }

        [Theory]
        [InlineData(0x11, 0x88, true)]
        [InlineData(0x22, 0x11, false)]
        public void RotateRightWithCarry(byte a, byte expected, bool expectedCarry)
        {
            var result = Subject.RotateRightWithCarry(a);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, false, expectedCarry);

            _flags.Verify(x => x.SetResultFlags(result), Times.Once);
        }

        [Theory]
        [InlineData(false, 0x11, 0x08, true)]
        [InlineData(false, 0x22, 0x11, false)]
        [InlineData(true, 0x11, 0x88, true)]
        [InlineData(true, 0x22, 0x91, false)]
        public void RotateRight(bool carry, byte a, byte expected, bool expectedCarry)
        {
            _flags.SetupProperty(x => x.Carry, carry);

            var result = Subject.RotateRight(a);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, false, expectedCarry);

            _flags.Verify(x => x.SetResultFlags(result), Times.Once);
        }

        [Theory]
        [InlineData(0x88, 0x10, true)]
        [InlineData(0x11, 0x22, false)]
        public void ShiftLeft(byte a, byte expected, bool expectedCarry)
        {
            var result = Subject.ShiftLeft(a);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, null, expectedCarry);

            _flags.Verify(x => x.SetParityFlags(result), Times.Once);
        }

        [Theory]
        [InlineData(0x88, 0x11, true)]
        [InlineData(0x11, 0x23, false)]
        public void ShiftLeftSet(byte a, byte expected, bool expectedCarry)
        {
            var result = Subject.ShiftLeftSet(a);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, null, expectedCarry);

            _flags.Verify(x => x.SetParityFlags(result), Times.Once);
        }

        [Theory]
        [InlineData(0x88, 0xc4, false)]
        [InlineData(0x11, 0x08, true)]
        public void ShiftRight(byte a, byte expected, bool expectedCarry)
        {
            var result = Subject.ShiftRight(a);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, null, expectedCarry);

            _flags.Verify(x => x.SetParityFlags(result), Times.Once);
        }

        [Theory]
        [InlineData(0x88, 0x44, false)]
        [InlineData(0x11, 0x08, true)]
        public void ShiftRightLogical(byte a, byte expected, bool expectedCarry)
        {
            var result = Subject.ShiftRightLogical(a);

            result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, null, expectedCarry);

            _flags.Verify(x => x.SetParityFlags(result), Times.Once);
        }

        [Theory]
        [InlineData(0x7a, 0x31, 0x73, 0x1a)]
        public void RotateLeftDigit(byte accumulator, byte b, byte expectedAccumulator, byte expected)
        {
            var result = Subject.RotateLeftDigit(accumulator, b);

            result.Accumulator.ShouldBe(expectedAccumulator);
            result.Result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, null, null);

            _flags.Verify(x => x.SetParityFlags(result.Accumulator), Times.Once);
        }

        [Theory]
        [InlineData(0x84, 0x20, 0x80, 0x42)]
        public void RotateRightDigit(byte accumulator, byte b, byte expectedAccumulator, byte expected)
        {
            var result = Subject.RotateRightDigit(accumulator, b);

            result.Accumulator.ShouldBe(expectedAccumulator);
            result.Result.ShouldBe(expected);

            AssertFlags(null, null, null, false, null, null, null);

            _flags.Verify(x => x.SetParityFlags(result.Accumulator), Times.Once);
        }

        private static IEnumerable<object[]> Bits() => Enumerable.Range(0, 8).Select(b => new object[] { b });

        [Theory]
        [MemberData(nameof(Bits))]
        public void BitReset(int i)
        {
            var result = Subject.BitReset(0xff, i);
            var expected = (byte) ((1 << i) ^ 0xff);
            result.ShouldBe(expected);
        }

        [Theory]
        [MemberData(nameof(Bits))]
        public void BitSet(int i)
        {
            var result = Subject.BitSet(0x00, i);
            var expected = (byte)(1 << i);
            result.ShouldBe(expected);
        }

        [Theory]
        [MemberData(nameof(Bits))]
        public void BitTest0(int i)
        {
            Subject.BitTest(0x00, i);
            AssertFlags(null, i == 7, true, true, true, false, null);
            _flags.Verify(x => x.SetUndocumentedFlags(0x00), Times.Once);
        }

        [Theory]
        [MemberData(nameof(Bits))]
        public void BitTest1(int i)
        {
            Subject.BitTest(0xff, i);
            AssertFlags(null, false, false, true, false, false, null);
            _flags.Verify(x => x.SetUndocumentedFlags(0xff), Times.Once);
        }

        private void AssertFlags(byte? result, bool? sign, bool? zero, bool? halfCarry, bool? parityOverflow, bool? subtract, bool? carry)
        {
            if (result.HasValue)
            {
                _flags.Verify(x => x.SetResultFlags(result.Value), Times.Once);
            }
            
            FlagsHelpers.VerifyFlag(_flags, x => x.Sign, sign);
            FlagsHelpers.VerifyFlag(_flags, x => x.Zero, zero);
            FlagsHelpers.VerifyFlag(_flags, x => x.HalfCarry, halfCarry);
            FlagsHelpers.VerifyFlag(_flags, x => x.ParityOverflow, parityOverflow);
            FlagsHelpers.VerifyFlag(_flags, x => x.Subtract, subtract);
            FlagsHelpers.VerifyFlag(_flags, x => x.Carry, carry);
        }
    }
}
