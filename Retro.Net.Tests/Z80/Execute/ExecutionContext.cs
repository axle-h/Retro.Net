using System;
using Autofac.Extras.Moq;
using Moq;
using Retro.Net.Memory;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Tests.Z80.Execute
{
    public class ExecutionContext
    {
        public ExecutionContext(AutoMock mock, Operation operation, GeneralPurposeRegisterSet registers, AccumulatorAndFlagsRegisterSet accumulator)
        {
            Operation = operation;
            Registers = registers;
            Accumulator = accumulator;
            MockRegisters = mock.Mock<IRegisters>();
            Alu = mock.Mock<IAlu>();
            Mmu = mock.Mock<IMmu>();
            Peripherals = mock.Mock<IPeripheralManager>();
                
            MockRegisters.SetupAllProperties();
            MockRegisters.Object.IX = Rng.Word();
            MockRegisters.Object.IY = Rng.Word();
            MockRegisters.Object.IXl = Rng.Byte();
            MockRegisters.Object.IXh = Rng.Byte();
            MockRegisters.Object.IYl = Rng.Byte();
            MockRegisters.Object.IYh = Rng.Byte();
            MockRegisters.Object.I = Rng.Byte();
            MockRegisters.Object.R = Rng.Byte();
            MockRegisters.Object.StackPointer = Rng.Word();
            MockRegisters.Object.ProgramCounter = ProgramCounter = Rng.Word();
            MockRegisters.Setup(x => x.GeneralPurposeRegisters).Returns(registers);
            MockRegisters.Setup(x => x.AccumulatorAndFlagsRegisters).Returns(accumulator);

            if (Operation.Operand1 == Operand.mHL || Operation.Operand2 == Operand.mHL)
            {
                Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == registers.HL))).Returns(IndexedByte).Verifiable();
            }

            if (Operation.Operand1 == Operand.mIXd || Operation.Operand2 == Operand.mIXd)
            {
                Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == MockRegisters.Object.IX + operation.Displacement))).Returns(IndexedXByte).Verifiable();
            }

            if (Operation.Operand1 == Operand.mIYd || Operation.Operand2 == Operand.mIYd)
            {
                Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == MockRegisters.Object.IY + operation.Displacement))).Returns(IndexedYByte).Verifiable();
            }

            if (Operation.Operand1 == Operand.mnn || Operation.Operand2 == Operand.mnn)
            {
                Mmu.Setup(m => m.ReadByte(It.Is<ushort>(a => a == Operation.WordLiteral))).Returns(LiteralIndexedByte).Verifiable();
            }
        }

        public Operation Operation { get; }

        public GeneralPurposeRegisterSet Registers { get; }

        public AccumulatorAndFlagsRegisterSet Accumulator { get; }

        public Mock<IRegisters> MockRegisters { get; }
        
        public Mock<IAlu> Alu { get; }

        public Mock<IMmu> Mmu { get; }

        public Mock<IPeripheralManager> Peripherals { get; }

        public ushort ProgramCounter { get; }

        public byte LiteralIndexedByte { get; } = Rng.Byte();

        public byte IndexedByte { get; } = Rng.Byte();

        public byte IndexedXByte { get; } = Rng.Byte();

        public byte IndexedYByte { get; } = Rng.Byte();

        public byte Byte { get; } = Rng.Byte();

        public ushort Word { get; } = Rng.Word();

        public byte ByteOperand(Operand r)
        {
            switch (r)
            {
                case Operand.A:
                    return Accumulator.A;
                case Operand.B:
                    return Registers.B;
                case Operand.C:
                    return Registers.C;
                case Operand.D:
                    return Registers.D;
                case Operand.E:
                    return Registers.E;
                case Operand.F:
                    return Accumulator.Flags.Register;
                case Operand.H:
                    return Registers.H;
                case Operand.L:
                    return Registers.L;
                case Operand.IXl:
                    return MockRegisters.Object.IXl;
                case Operand.IYl:
                    return MockRegisters.Object.IYl;
                case Operand.IXh:
                    return MockRegisters.Object.IXh;
                case Operand.IYh:
                    return MockRegisters.Object.IYh;

                case Operand.n:
                    return Operation.ByteLiteral;
                case Operand.mnn:
                    return LiteralIndexedByte;
                case Operand.mHL:
                    return IndexedByte;
                case Operand.mIXd:
                    return IndexedXByte;
                case Operand.mIYd:
                    return IndexedYByte;

                default:
                    throw new ArgumentOutOfRangeException(nameof(r), r, "Must be an 8-bit operand");
            }
        }

        public ushort WordOperand(Operand r)
        {
            switch (r)
            {
                case Operand.AF:
                    return Accumulator.AF;
                case Operand.BC:
                    return Registers.BC;
                case Operand.DE:
                    return Registers.DE;
                case Operand.HL:
                    return Registers.HL;
                case Operand.SP:
                    return MockRegisters.Object.StackPointer;

                case Operand.IX:
                    return MockRegisters.Object.IX;
                case Operand.IY:
                    return MockRegisters.Object.IY;

                case Operand.nn:
                    return Operation.WordLiteral;

                default:
                    throw new ArgumentOutOfRangeException(nameof(r), r, "Must be an 16-bit operand");
            }
        }
    }
}