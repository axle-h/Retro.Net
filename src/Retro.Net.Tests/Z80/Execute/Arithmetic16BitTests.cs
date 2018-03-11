using System;
using System.Linq.Expressions;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Core.Interfaces;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class Arithmetic16BitTests
    {
        [Fact] public void Add16() => TestAssign(OpCode.Add16, (alu, o1, o2) => alu.Add(o1, o2));

        [Fact] public void AddWithCarry16() => TestAssign(OpCode.AddWithCarry16, (alu, o1, o2) => alu.AddWithCarry(o1, o2));

        [Fact] public void SubtractWithCarry16() => TestAssign(OpCode.SubtractWithCarry16, (alu, o1, o2) => alu.SubtractWithCarry(o1, o2));

        [Fact]
        public void Increment16()
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Increment16).Random16BitRegister(out var o);
                fixture.Assert(c => c.Operand16(o).Should().Be(unchecked ((ushort) (c.InitialRegister16(o) + 1))));
            }
        }

        [Fact]
        public void Decrement16()
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Decrement16).Random16BitRegister(out var o);
                fixture.Assert(c => c.Operand16(o).Should().Be(unchecked((ushort) (c.InitialRegister16(o) - 1))));
            }
        }

        private static void TestAssign(OpCode op, Expression<Func<IAlu, ushort, ushort, ushort>> f)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).Random16BitRegisters(out var o1, out var o2);
                fixture.With(c => c.Alu.Setup(c.Alu16Call(f, o1, o2)).Returns(c.Word).Verifiable());
                fixture.Assert(c => c.Operand16(o1).Should().Be(c.Word));
            }
        }
    }
}
