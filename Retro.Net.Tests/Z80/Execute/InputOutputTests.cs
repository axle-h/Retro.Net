using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    public class InputOutputTests
    {
        [Fact] public void When_reading_from_literal_indexed_port() => Test(OpCode.Input, Operand.n, false);

        [Fact] public void When_reading_from_register_indexed_port() => Test(OpCode.Input, Operand.C, false);

        [Fact] public void When_writing_to_literal_indexed_port() => Test(OpCode.Output, Operand.n, true);

        [Fact] public void When_writing_to_register_indexed_port() => Test(OpCode.Output, Operand.C, true);

        [Fact] public static void InputTransferDecrement() => Transfer(OpCode.InputTransferDecrement, true, false);

        [Fact] public static void InputTransferIncrement() => Transfer(OpCode.InputTransferIncrement, false, false);

        [Fact] public static void OutputTransferDecrement() => Transfer(OpCode.OutputTransferDecrement, true, true);

        [Fact] public static void OutputTransferIncrement() => Transfer(OpCode.OutputTransferIncrement, false, true);

        [Fact] public static void InputTransferDecrementRepeat() => TransferRepeat(OpCode.InputTransferDecrementRepeat, true, false);

        [Fact] public static void InputTransferIncrementRepeat() => TransferRepeat(OpCode.InputTransferIncrementRepeat, false, false);

        [Fact] public static void OutputTransferDecrementRepeat() => TransferRepeat(OpCode.OutputTransferDecrementRepeat, true, true);

        [Fact] public static void OutputTransferIncrementRepeat() => TransferRepeat(OpCode.OutputTransferIncrementRepeat, false, true);

        private static void Test(OpCode op, Operand o, bool isOutput)
        {
            var addressMsb = o == Operand.n ? Operand.A : Operand.B;
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op).RandomRegister(out var r).Operand2(o).RandomLiterals();

                if (isOutput)
                {
                    fixture.With(c => c.Io.Setup(x => x.WriteByteToPort(c.Operand8(o), c.InitialRegister8(addressMsb), c.Operand8(r))).Verifiable());
                }
                else
                {
                    fixture.With(c => c.Io.Setup(x => x.ReadByteFromPort(c.Operand8(o), c.InitialRegister8(addressMsb))).Returns(c.Byte).Verifiable());
                    fixture.Assert(c => c.Operand8(r).ShouldBe(c.Byte));
                }
            }
        }

        private static void Transfer(OpCode op, bool decrement, bool isOutput)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(op);

                if (isOutput)
                {
                    fixture.With(c => c.Mmu.Setup(x => x.ReadByte(c.Registers.HL)).Returns(c.Byte).Verifiable());
                    fixture.Assert(c => c.Io.Verify(x => x.WriteByteToPort(c.Registers.C, c.InitialRegisters.B, c.Byte)));
                }
                else
                {
                    fixture.With(c => c.Io.Setup(x => x.ReadByteFromPort(c.Registers.C, c.InitialRegisters.B)).Returns(c.Byte).Verifiable());
                    fixture.Assert(c => c.Mmu.Verify(x => x.WriteByte(c.InitialRegisters.HL, c.Byte)));
                }


                fixture.Assert(c => c.Registers.B.ShouldBe(unchecked ((byte) (c.InitialRegisters.B - 1))));
                fixture.AssertFlags(c => c.Registers.B, subtract: true, setResult: true);

                if (decrement)
                {
                    fixture.Assert(c => c.Registers.HL.ShouldBe(unchecked((ushort) (c.InitialRegister16(Operand.HL) - 1))));
                }
                else
                {
                    fixture.Assert(c => c.Registers.HL.ShouldBe(unchecked((ushort) (c.InitialRegister16(Operand.HL) + 1))));
                }
            }
        }

        private static void TransferRepeat(OpCode op, bool decrement, bool isOutput)
        {
            using (var fixture = new ExecuteFixture())
            {
                const ushort HL = 100; // Change HL so we don't need to worry about overflow.
                var repeats = Rng.Byte(2, 10);
                fixture.Operation.OpCode(op);
                fixture.With(c => c.Registers.B = repeats, c => c.Registers.HL = HL);
                fixture.RuntimeTiming((repeats - 1) * 5, (repeats - 1) * 21);

                if (decrement)
                {
                    if (isOutput)
                    {
                        fixture.With(c => c.Mmu.Setup(x => x.ReadByte(It.Is<ushort>(a => a <= HL && a > HL - repeats))).Returns<ushort>(a => (byte) (HL - a)));
                        fixture.Assert(c => c.Mmu.Verify(x => x.ReadByte(It.Is<ushort>(a => a <= HL && a > HL - repeats)), Times.Exactly(repeats)));
                    }
                    else
                    {
                        fixture.Assert(c => c.Mmu.Verify(x => x.WriteByte(It.Is<ushort>(a => a <= HL && a > HL - repeats), It.Is<byte>(b => b < repeats)), Times.Exactly(repeats)));
                    }
                }
                else
                {
                    if (isOutput)
                    {
                        fixture.With(c => c.Mmu.Setup(x => x.ReadByte(It.Is<ushort>(a => a >= HL && a < HL + repeats))).Returns<ushort>(a => (byte) (a - HL)));
                        fixture.Assert(c => c.Mmu.Verify(x => x.ReadByte(It.Is<ushort>(a => a >= HL && a < HL + repeats)), Times.Exactly(repeats)));
                    }
                    else
                    {
                        fixture.Assert(c => c.Mmu.Verify(x => x.WriteByte(It.Is<ushort>(a => a >= HL && a < HL + repeats), It.Is<byte>(b => b < repeats)), Times.Exactly(repeats)));
                    }
                }

                if (isOutput)
                {
                    fixture.Assert(c => c.Io.Verify(x => x.WriteByteToPort(c.Registers.C, It.Is<byte>(b => b > 0 && b <= repeats), It.Is<byte>(b => b < repeats))));
                }
                else
                {
                    fixture.With(c => c.Io.Setup(x => x.ReadByteFromPort(c.Registers.C, It.Is<byte>(b => b > 0 && b <= repeats)))
                        .Returns<byte, byte>((p, b) => (byte)(b - 1)));
                    fixture.Assert(c => c.Io.Verify(x => x.ReadByteFromPort(c.Registers.C, It.Is<byte>(b => b > 0 && b <= repeats)), Times.Exactly(repeats)));
                }
                
                fixture.Assert(c => c.Registers.B.ShouldBe((byte) 0));
            }
        }
    }
}
