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
        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands8Bit), MemberType = typeof(ExecuteFixture))]
        public void Add(Operand o) => TestAccumulatorAssign(OpCode.Add, o, (alu, o0, o1) => alu.Add(o0, o1));

        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands8Bit), MemberType = typeof(ExecuteFixture))]
        public void AddCarry(Operand o) => TestAccumulatorAssign(OpCode.AddWithCarry, o, (alu, o0, o1) => alu.AddWithCarry(o0, o1));

        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands8Bit), MemberType = typeof(ExecuteFixture))]
        public void Subtract(Operand o) => TestAccumulatorAssign(OpCode.Subtract, o, (alu, o0, o1) => alu.Subtract(o0, o1));

        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands8Bit), MemberType = typeof(ExecuteFixture))]
        public void SubtractCarry(Operand o) => TestAccumulatorAssign(OpCode.SubtractWithCarry, o, (alu, o0, o1) => alu.SubtractWithCarry(o0, o1));

        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands8Bit), MemberType = typeof(ExecuteFixture))]
        public void And(Operand o) => TestAccumulatorAssign(OpCode.And, o, (alu, o0, o1) => alu.And(o0, o1));

        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands8Bit), MemberType = typeof(ExecuteFixture))]
        public void Or(Operand o) => TestAccumulatorAssign(OpCode.Or, o, (alu, o0, o1) => alu.Or(o0, o1));

        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands8Bit), MemberType = typeof(ExecuteFixture))]
        public void Xor(Operand o) => TestAccumulatorAssign(OpCode.Xor, o, (alu, o0, o1) => alu.Xor(o0, o1));

        [Theory]
        [MemberData(nameof(ExecuteFixture.Operands8Bit), MemberType = typeof(ExecuteFixture))]
        public void Compare(Operand o) => TestCall(OpCode.Compare, o, (alu, o0, o1) => alu.Compare(o0, o1));

        [Theory]
        [MemberData(nameof(ExecuteFixture.Registers8Bit), MemberType = typeof(ExecuteFixture))]
        public void Increment(Operand o) => TestCallAssign(OpCode.Increment, o, (alu, o0) => alu.Increment(o0));

        [Theory]
        [MemberData(nameof(ExecuteFixture.Registers8Bit), MemberType = typeof(ExecuteFixture))]
        public void Decrement(Operand o) => TestCallAssign(OpCode.Decrement, o, (alu, o0) => alu.Decrement(o0));
        
        private static void TestAccumulatorAssign(OpCode op, Operand o, Expression<Func<IAlu, byte, byte, byte>> f)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).Operands(o).RandomLiterals();
                fixture.With(c => c.Alu.Setup(c.Alu8Call(f, Operand.A, o)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Accumulator.A.ShouldBe(c.Byte));
            }
        }

        private static void TestCallAssign(OpCode op, Operand o, Expression<Func<IAlu, byte, byte>> f)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).Operands(o).RandomLiterals();
                fixture.With(c => c.Alu.Setup(c.Alu8Call(f, o)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Operand8(o).ShouldBe(c.Byte));
            }
        }

        private static void TestCall(OpCode op, Operand o, Expression<Action<IAlu, byte, byte>> f)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).Operands(o).RandomLiterals();
                fixture.With(c => c.Alu.Setup(c.AluCall(f, Operand.A, o)).Verifiable());
            }
        }
    }
}
