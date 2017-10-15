using System;
using System.Linq.Expressions;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Decode;
using Shouldly;
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
        
        private static void TestAccumulatorAssign(OpCode op, Expression<Func<IAlu, byte, byte, byte>> f)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).RandomRegister(out var o).RandomLiterals();
                fixture.With(c => c.Alu.Setup(c.Alu8Call(f, Operand.A, o)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Accumulator.A.ShouldBe(c.Byte));
            }
        }

        private static void TestCallAssign(OpCode op, Expression<Func<IAlu, byte, byte>> f)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).RandomRegister(out var o).RandomLiterals();
                fixture.With(c => c.Alu.Setup(c.Alu8Call(f, o)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Operand8(o).ShouldBe(c.Byte));
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
