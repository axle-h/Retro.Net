using System;
using System.Linq.Expressions;
using AutoFixture;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Core.Interfaces;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class Arithmetic8BitTests
    {
        [Fact] public void Add() => TestAccumulatorAssign(OpCode.Add, (alu, o0, o1) => alu.Add(o0, o1));

        [Fact] public void AddCarry() => TestAccumulatorAssign(OpCode.AddWithCarry, (alu, o0, o1) => alu.AddWithCarry(o0, o1));

        [Fact] public void Subtract() => TestAccumulatorAssign(OpCode.Subtract, (alu, o0, o1) => alu.Subtract(o0, o1));

        [Fact] public void SubtractCarry() => TestAccumulatorAssign(OpCode.SubtractWithCarry, (alu, o0, o1) => alu.SubtractWithCarry(o0, o1));

        [Fact] public void And() => TestAccumulatorAssign(OpCode.And, (alu, o0, o1) => alu.And(o0, o1));

        [Fact] public void Or() => TestAccumulatorAssign(OpCode.Or, (alu, o0, o1) => alu.Or(o0, o1));

        [Fact] public void Xor() => TestAccumulatorAssign(OpCode.Xor, (alu, o0, o1) => alu.Xor(o0, o1));

        [Fact] public void Compare() => TestCall(OpCode.Compare, (alu, o0, o1) => alu.Compare(o0, o1));

        [Fact] public void Increment() => TestCallAssign(OpCode.Increment, (alu, o0) => alu.Increment(o0));

        [Fact] public void Decrement() => TestCallAssign(OpCode.Decrement, (alu, o0) => alu.Decrement(o0));

        [Fact] public void RotateLeftWithCarry() => TestCallAssign(OpCode.RotateLeftWithCarry, (alu, o) => alu.RotateLeftWithCarry(o));

        [Fact] public void RotateLeftWithCarry_AlternativeFlags() => TestCallAssign(OpCode.RotateLeftWithCarry, (alu, o) => alu.RotateLeftWithCarry(o), true);

        [Fact] public void RotateLeft() => TestCallAssign(OpCode.RotateLeft, (alu, o) => alu.RotateLeft(o));

        [Fact] public void RotateLeft_AlternativeFlags() => TestCallAssign(OpCode.RotateLeft, (alu, o) => alu.RotateLeft(o), true);

        [Fact] public void RotateRightWithCarry() => TestCallAssign(OpCode.RotateRightWithCarry, (alu, o) => alu.RotateRightWithCarry(o));

        [Fact] public void RotateRightWithCarry_AlternativeFlags() => TestCallAssign(OpCode.RotateRightWithCarry, (alu, o) => alu.RotateRightWithCarry(o), true);

        [Fact] public void RotateRight() => TestCallAssign(OpCode.RotateRight, (alu, o) => alu.RotateRight(o));

        [Fact] public void RotateRight_AlternativeFlags() => TestCallAssign(OpCode.RotateRight, (alu, o) => alu.RotateRight(o), true);

        [Fact]
        public void RotateLeftDigit()
        {
            using (var fixture = new ExecuteFixture())
            {
                var result = new Fixture().Create<AccumulatorAndResult>();
                fixture.Operation.OpCode(OpCode.RotateLeftDigit);
                fixture.With(c => c.Mmu.Setup(x => x.ReadByte(c.Registers.HL)).Returns(c.Byte).Verifiable(),
                    c => c.Alu.Setup(x => x.RotateLeftDigit(c.InitialAccumulator.A, c.Byte)).Returns(result).Verifiable());
                fixture.Assert(c => c.Accumulator.A.Should().Be(result.Accumulator),
                    c => c.Mmu.Verify(x => x.WriteByte(c.Registers.HL, result.Result)));
            }
        }

        [Fact]
        public void RotateRightDigit()
        {
            using (var fixture = new ExecuteFixture())
            {
                var result = new Fixture().Create<AccumulatorAndResult>();
                fixture.Operation.OpCode(OpCode.RotateRightDigit);
                fixture.With(c => c.Mmu.Setup(x => x.ReadByte(c.Registers.HL)).Returns(c.Byte).Verifiable(),
                    c => c.Alu.Setup(x => x.RotateRightDigit(c.InitialAccumulator.A, c.Byte)).Returns(result).Verifiable());
                fixture.Assert(c => c.Accumulator.A.Should().Be(result.Accumulator),
                    c => c.Mmu.Verify(x => x.WriteByte(c.Registers.HL, result.Result)));
            }
        }

        [Fact] public void ShiftLeft() => TestCallAssign(OpCode.ShiftLeft, (alu, o) => alu.ShiftLeft(o));

        [Fact] public void ShiftLeftSet() => TestCallAssign(OpCode.ShiftLeftSet, (alu, o) => alu.ShiftLeftSet(o));

        [Fact] public void ShiftRight() => TestCallAssign(OpCode.ShiftRight, (alu, o) => alu.ShiftRight(o));

        [Fact] public void ShiftRightLogical() => TestCallAssign(OpCode.ShiftRightLogical, (alu, o) => alu.ShiftRightLogical(o));

        private static void TestAccumulatorAssign(OpCode op, Expression<Func<IAlu, byte, byte, byte>> f)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).RandomRegister(out var o).RandomLiterals();
                fixture.With(c => c.Alu.Setup(c.Alu8Call(f, Operand.A, o)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Accumulator.A.Should().Be(c.Byte));
            }
        }

        private static void TestCallAssign(OpCode op, Expression<Func<IAlu, byte, byte>> f, bool alternativeFlags = false)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).RandomRegister(out var o).RandomLiterals().UseAlternativeFlagAffection(alternativeFlags);
                fixture.With(c => c.Alu.Setup(c.Alu8Call(f, o)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Operand8(o).Should().Be(c.Byte));

                if (alternativeFlags)
                {
                    fixture.AssertFlags(zero: false, sign: false);
                }
            }
        }

        private static void TestCall(OpCode op, Expression<Action<IAlu, byte, byte>> f)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).RandomRegister(out var o).RandomLiterals();
                fixture.With(c => c.Alu.Setup(c.AluAction(f, Operand.A, o)).Verifiable());
            }
        }
    }
}
