using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class TransferTests
    {
        [Theory]
        [InlineData(OpCode.TransferDecrement, true, true)]
        [InlineData(OpCode.TransferDecrement, true, false)]
        [InlineData(OpCode.TransferIncrement, false, true)]
        [InlineData(OpCode.TransferIncrement, false, false)]
        public void Transfer(OpCode op, bool decrement, bool overflow)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op);
                fixture.With(c => c.Registers.BC = (ushort) (overflow ? 2 : 1));
                fixture.Assert(c => c.Mmu.Verify(x => x.TransferByte(c.InitialRegisters.HL, c.InitialRegisters.DE)),
                    c => c.Registers.BC.ShouldBe((ushort) (overflow ? 1 : 0)));

                if (decrement)
                {
                    fixture.Assert(c => c.Registers.HL.ShouldBe(unchecked((ushort) (c.InitialRegisters.HL - 1))),
                        c => c.Registers.DE.ShouldBe(unchecked((ushort) (c.InitialRegisters.DE - 1))));
                }
                else
                {
                    fixture.Assert(c => c.Registers.HL.ShouldBe(unchecked((ushort) (c.InitialRegisters.HL + 1))),
                        c => c.Registers.DE.ShouldBe(unchecked((ushort) (c.InitialRegisters.DE + 1))));
                }
                
                fixture.AssertFlags(halfCarry: false, parityOverflow: overflow, subtract: false);
            }
        }

        [Theory]
        [InlineData(OpCode.TransferDecrementRepeat, true)]
        [InlineData(OpCode.TransferIncrementRepeat, false)]
        public void TransferRepeat(OpCode op, bool decrement)
        {
            var repeats = Rng.Word(2, 10);
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op);
                fixture.RuntimeTiming((repeats - 1) * 5, (repeats - 1) * 21);
                const ushort HL = 100, DE = 1000; // Avoiding overflows.
                fixture.With(c => c.Registers.BC = repeats, c => c.Registers.HL = HL, c => c.Registers.DE = DE);
                
                if (decrement)
                {
                    fixture.Assert(c => c.Mmu.Verify(x => x.TransferByte(It.Is<ushort>(b => b > HL - repeats && b <= HL),
                        It.Is<ushort>(b => b > DE - repeats && b <= DE)), Times.Exactly(repeats)));
                    fixture.Assert(c => c.Registers.HL.ShouldBe((ushort) (HL - repeats)),
                        c => c.Registers.DE.ShouldBe((ushort) (DE - repeats)));
                }
                else
                {
                    fixture.Assert(c => c.Mmu.Verify(x => x.TransferByte(It.Is<ushort>(b => b >= HL && b < HL + repeats),
                        It.Is<ushort>(b => b >= DE && b < DE + repeats)), Times.Exactly(repeats)));
                    fixture.Assert(c => c.Registers.HL.ShouldBe((ushort)(HL + repeats)),
                        c => c.Registers.DE.ShouldBe((ushort)(DE + repeats)));
                }

                fixture.Assert(c => c.Registers.BC.ShouldBe((ushort) 0));
            }
        }
    }
}
