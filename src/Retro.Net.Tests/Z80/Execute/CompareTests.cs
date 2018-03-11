using Bogus;
using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class CompareTests
    {
        private static readonly Randomizer Rng = new Faker().Random;

        [Fact] public void SearchDecrement() => Test(OpCode.SearchDecrement, true);

        [Fact] public void SearchIncrement() => Test(OpCode.SearchIncrement, false);

        [Fact] public void SearchDecrementRepeat() => TestRepeat(OpCode.SearchDecrementRepeat, true);

        [Fact] public void SearchIncrementRepeat() => TestRepeat(OpCode.SearchIncrementRepeat, false);

        private static void Test(OpCode op, bool decrement)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op);
                fixture.With(c => c.Mmu.Setup(x => x.ReadByte(c.Registers.HL)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Alu.Verify(x => x.Compare(c.Accumulator.A, c.Byte)),
                    c => c.Registers.BC.Should().Be(unchecked((ushort) (c.InitialRegister16(Operand.BC) - 1))));

                if (decrement)
                {
                    fixture.Assert(c => c.Registers.HL.Should().Be(unchecked((ushort) (c.InitialRegister16(Operand.HL) - 1))));
                }
                else
                {
                    fixture.Assert(c => c.Registers.HL.Should().Be(unchecked((ushort) (c.InitialRegister16(Operand.HL) + 1))));
                }
            }
        }

        private static void TestRepeat(OpCode op, bool decrement)
        {
            using (var fixture = new ExecuteFixture())
            {
                const ushort HL = 100; // Change HL so we don't need to worry about overflow.
                var repeats = Rng.UShort(2, 10);
                fixture.Operation.OpCode(op);
                fixture.With(c => c.Registers.BC = repeats, c => c.Registers.HL = HL);
                fixture.RuntimeTiming((repeats - 1) * 5, (repeats - 1) * 21);
                
                if (decrement)
                {
                    fixture.With(c => c.Mmu.Setup(x => x.ReadByte(It.Is<ushort>(a => a <= HL && a > HL - repeats))).Returns<ushort>(a => (byte) (HL - a)));
                    fixture.Assert(c => c.Mmu.Verify(x => x.ReadByte(It.Is<ushort>(a => a <= HL && a > HL - repeats)), Times.Exactly(repeats)));
                }
                else
                {
                    fixture.With(c => c.Mmu.Setup(x => x.ReadByte(It.Is<ushort>(a => a >= HL && a < HL + repeats))).Returns<ushort>(a => (byte) (a - HL)));
                    fixture.Assert(c => c.Mmu.Verify(x => x.ReadByte(It.Is<ushort>(a => a >= HL && a < HL + repeats)), Times.Exactly(repeats)));
                }

                fixture.Assert(c => c.Alu.Verify(x => x.Compare(c.Accumulator.A, It.Is<byte>(b => b < repeats))));
                fixture.Assert(c => c.Registers.BC.Should().Be((ushort) 0));
            }
        }
    }
}
