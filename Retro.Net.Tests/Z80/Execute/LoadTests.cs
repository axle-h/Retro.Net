using System.Collections.Generic;
using System.Linq;
using Moq;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80.Execute
{
    /// <summary>
    /// Tests load function and reading & wriitng of all operands.
    /// </summary>
    public class LoadTests
    {
        private static readonly Operand[] Writable8BitOperands =
        {
            Operand.A, Operand.B, Operand.C, Operand.D, Operand.E, Operand.F, Operand.H, Operand.L,
            Operand.mHL, Operand.mIXd, Operand.mIYd, Operand.IXl, Operand.IXh, Operand.IYl, Operand.IYh, Operand.I,
            Operand.mnn, Operand.mCl, Operand.mnl, Operand.mBC, Operand.mDE, Operand.mSP
        };

        private static readonly Operand[] Writable16BitOperands = { Operand.AF, Operand.BC, Operand.DE, Operand.HL, Operand.IX, Operand.IY };

        [Theory]
        [MemberData(nameof(Readable8Bit))]
        public void When_reading_8bit_operands(Operand r)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Load).RandomRegister(out var o).Operand2(r).RandomLiterals();

                var value = Rng.Byte();
                fixture.With(c =>
                {
                    switch (c.Operation.Operand2)
                    {
                        case Operand.mHL:
                            c.Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == c.Registers.HL))).Returns(value).Verifiable();
                            break;

                        case Operand.mIXd:
                            c.Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == c.MockRegisters.Object.IX + c.Operation.Displacement))).Returns(value).Verifiable();
                            break;
                        case Operand.mIYd:
                            c.Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == c.MockRegisters.Object.IY + c.Operation.Displacement))).Returns(value).Verifiable();
                            break;

                        case Operand.mnn:
                            c.Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == c.Operation.WordLiteral))).Returns(value).Verifiable();
                            break;

                        case Operand.mCl:
                            c.Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == c.Registers.C + 0xff00))).Returns(value).Verifiable();
                            break;
                        case Operand.mnl:
                            c.Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == c.Operation.ByteLiteral + 0xff00))).Returns(value).Verifiable();
                            break;

                        case Operand.mBC:
                            c.Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == c.Registers.BC))).Returns(value).Verifiable();
                            break;
                        case Operand.mDE:
                            c.Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == c.Registers.DE))).Returns(value).Verifiable();
                            break;
                        case Operand.mSP:
                            c.Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == c.MockRegisters.Object.StackPointer))).Returns(value).Verifiable();
                            break;

                        default:
                            value = c.Operand8(c.Operation.Operand2);
                            break;
                    }
                });
                fixture.Assert(c => c.Operand8(o).ShouldBe(value));
            }
        }

        [Theory]
        [MemberData(nameof(Writable8Bit))]
        public void When_writing_8bit_operands(Operand r)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Load).Operands(r).RandomRegister2(out var o).RandomLiterals();
                fixture.Assert(c =>
                {
                    var value = c.Operand8(c.Operation.Operand2);
                    switch (c.Operation.Operand1)
                    {
                        case Operand.mHL:
                            c.Mmu.Verify(m => m.WriteByte(It.Is<ushort>(a => a == c.Registers.HL), value));
                            break;

                        case Operand.mIXd:
                            c.Mmu.Verify(m => m.WriteByte(It.Is<ushort>(a => a == c.MockRegisters.Object.IX + c.Operation.Displacement), value));
                            break;
                        case Operand.mIYd:
                            c.Mmu.Verify(m => m.WriteByte(It.Is<ushort>(a => a == c.MockRegisters.Object.IY + c.Operation.Displacement), value));
                            break;

                        case Operand.mnn:
                            c.Mmu.Verify(m => m.WriteByte(It.Is<ushort>(a => a == c.Operation.WordLiteral), value));
                            break;

                        case Operand.mCl:
                            c.Mmu.Verify(m => m.WriteByte(It.Is<ushort>(a => a == c.Registers.C + 0xff00), value));
                            break;
                        case Operand.mnl:
                            c.Mmu.Verify(m => m.WriteByte(It.Is<ushort>(a => a == c.Operation.ByteLiteral + 0xff00), value));
                            break;

                        case Operand.mBC:
                            c.Mmu.Verify(m => m.WriteByte(It.Is<ushort>(a => a == c.Registers.BC), value));
                            break;
                        case Operand.mDE:
                            c.Mmu.Verify(m => m.WriteByte(It.Is<ushort>(a => a == c.Registers.DE), value));
                            break;
                        case Operand.mSP:
                            c.Mmu.Verify(m => m.WriteByte(It.Is<ushort>(a => a == c.MockRegisters.Object.StackPointer), value));
                            break;
                            
                        default:
                            c.Operand8(c.Operation.Operand1).ShouldBe(value);
                            break;
                    }
                });
            }
        }

        [Theory]
        [MemberData(nameof(Readable16Bit))]
        public void When_reading_16bit_operands(Operand r)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Load16).Random16BitRegister(out var o).Operand2(r).RandomLiterals();

                var value = Rng.Word();
                fixture.With(c =>
                {
                    if (r == Operand.SPd)
                    {
                        c.Alu.Setup(a => a.AddDisplacement(c.MockRegisters.Object.StackPointer, c.Operation.Displacement)).Returns(value);
                    }
                    else
                    {
                        value = c.Operand16(r);
                    }
                });

                fixture.Assert(c => c.Operand16(o).ShouldBe(value));
            }
        }

        [Theory]
        [MemberData(nameof(Writeble16Bit))]
        public void When_writing_16bit_operands(Operand r)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(OpCode.Load16).Operands(r).Random16BitRegister2(out var o).RandomLiterals();
                fixture.Assert(c => c.Operand16(r).ShouldBe(c.Operand16(o)));
            }
        }

        [Fact] public void LoadDecrement_Write() => LoadIndexWrite(OpCode.LoadDecrement);

        [Fact] public void LoadIncrement_Write() => LoadIndexWrite(OpCode.LoadIncrement);

        [Fact] public void LoadDecrement_Read() => LoadIndexRead(OpCode.LoadDecrement);

        [Fact] public void LoadIncrement_Read() => LoadIndexRead(OpCode.LoadIncrement);

        private static void LoadIndexRead(OpCode o)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(o).Operands(Operand.A, Operand.mHL);
                fixture.With(c => c.Mmu.Setup(x => x.ReadByte(c.InitialRegisters.HL)).Returns(c.Byte).Verifiable());
                fixture.Assert(c => c.Accumulator.A.ShouldBe(c.Byte));

                switch (o)
                {
                    case OpCode.LoadIncrement:
                        fixture.Assert(c => c.Registers.HL.ShouldBe(unchecked((ushort)(c.InitialRegisters.HL + 1))));
                        break;

                    case OpCode.LoadDecrement:
                        fixture.Assert(c => c.Registers.HL.ShouldBe(unchecked((ushort)(c.InitialRegisters.HL - 1))));
                        break;
                }
            }
        }

        private static void LoadIndexWrite(OpCode o)
        {
            using (var fixture = new ExecuteFixture())
            {
                fixture.Operation.OpCode(o).Operands(Operand.mHL, Operand.A);
                fixture.Assert(c => c.Mmu.Verify(x => x.WriteByte(c.InitialRegisters.HL, c.Accumulator.A)));

                switch (o)
                {
                    case OpCode.LoadIncrement:
                        fixture.Assert(c => c.Registers.HL.ShouldBe(unchecked((ushort) (c.InitialRegisters.HL + 1))));
                        break;

                    case OpCode.LoadDecrement:
                        fixture.Assert(c => c.Registers.HL.ShouldBe(unchecked((ushort)(c.InitialRegisters.HL - 1))));
                        break;
                }
            }
        }


        public static IEnumerable<object[]> Readable8Bit() =>
            new SimpleTheorySource<Operand>(Writable8BitOperands.Concat(new[] {Operand.n, Operand.R}).ToArray());

        public static IEnumerable<object[]> Writable8Bit() => new SimpleTheorySource<Operand>(Writable8BitOperands);

        public static IEnumerable<object[]> Readable16Bit() =>
            new SimpleTheorySource<Operand>(Writable16BitOperands.Concat(new[] {Operand.nn, Operand.SPd}).ToArray());

        public static IEnumerable<object[]> Writeble16Bit() => new SimpleTheorySource<Operand>(Writable16BitOperands);
    }
}
