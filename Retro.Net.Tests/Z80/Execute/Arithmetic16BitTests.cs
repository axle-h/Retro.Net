using System;
using System.Linq.Expressions;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Decode;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class Arithmetic16BitTests
    {
        [Theory]
        [MemberData(nameof(ExecuteFixture.Registers16Bit), MemberType = typeof(ExecuteFixture))]
        public void Add16(Operand o) => TestAssign(OpCode.Add16, (alu, o1, o2) => alu.Add(o1, o2), Operand.HL, o);

        [Theory]
        [MemberData(nameof(ExecuteFixture.Registers16Bit), MemberType = typeof(ExecuteFixture))]
        public void AddWithCarry16(Operand o) => TestAssign(OpCode.AddWithCarry16, (alu, o1, o2) => alu.AddWithCarry(o1, o2), Operand.HL, o);

        [Theory]
        [MemberData(nameof(ExecuteFixture.Registers16Bit), MemberType = typeof(ExecuteFixture))]
        public void SubtractWithCarry16(Operand o) => TestAssign(OpCode.SubtractWithCarry16, (alu, o1, o2) => alu.SubtractWithCarry(o1, o2), Operand.HL, o);

        [Theory]
        [MemberData(nameof(ExecuteFixture.Registers16Bit), MemberType = typeof(ExecuteFixture))]
        public void Increment16(Operand o)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Increment16).Operands(o);
                fixture.Assert(c => c.Operand16(o).ShouldBe(unchecked ((ushort) (c.InitialRegister16(o) + 1))));
            }
        }

        [Theory]
        [MemberData(nameof(ExecuteFixture.Registers16Bit), MemberType = typeof(ExecuteFixture))]
        public void Decrement16(Operand o)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Decrement16).Operands(o);
                fixture.Assert(c => c.Operand16(o).ShouldBe(unchecked((ushort)(c.InitialRegister16(o) - 1))));
            }
        }

        private static void TestAssign(OpCode op, Expression<Func<IAlu, ushort, ushort, ushort>> f, Operand o1, Operand? o2 = null)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).Operands(o1, o2);
                fixture.With(c => c.Alu.Setup(c.Alu16Call(f, o1, o2)).Returns(c.Word).Verifiable());
                fixture.Assert(c => c.Operand16(o1).ShouldBe(c.Word));
            }
        }
    }
}
