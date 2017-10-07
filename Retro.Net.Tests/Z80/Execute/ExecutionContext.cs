using System;
using System.Linq.Expressions;
using Autofac.Extras.Moq;
using Moq;
using Retro.Net.Memory;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.State;
using Retro.Net.Z80.Util;
using Shouldly;

namespace Retro.Net.Tests.Z80.Execute
{
    public class ExecutionContext
    {
        private readonly ushort _IX, _IY;

        public ExecutionContext(AutoMock mock, Operation operation, GeneralPurposeRegisterState initialRegisters, AccumulatorAndFlagsRegisterState initialAccumulator)
        {
            InitialRegisters = initialRegisters;
            InitialAccumulator = initialAccumulator;

            Flags = mock.Mock<IFlagsRegister>();
            Flags.SetupAllProperties();

            var accumulator = new AccumulatorAndFlagsRegisterSet(Flags.Object);
            var registers = new GeneralPurposeRegisterSet();
            registers.ResetToState(initialRegisters);
            accumulator.ResetToState(initialAccumulator);
            Flags.ResetCalls(); // Don't want the initialization calls hanging around for verification.

            Operation = operation;
            Registers = registers;
            Accumulator = accumulator;
            MockRegisters = mock.Mock<IRegisters>();
            Alu = mock.Mock<IAlu>();
            Mmu = mock.Mock<IMmu>();
            Peripherals = mock.Mock<IPeripheralManager>();

            MockRegisters.SetupAllProperties();
            MockRegisters.Object.IX = _IX = Rng.Word();
            MockRegisters.Object.IY = _IY = Rng.Word();
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

        public GeneralPurposeRegisterState InitialRegisters { get; }

        public AccumulatorAndFlagsRegisterState InitialAccumulator { get; }

        public Operation Operation { get; }

        public GeneralPurposeRegisterSet Registers { get; }

        public AccumulatorAndFlagsRegisterSet Accumulator { get; }

        public Mock<IRegisters> MockRegisters { get; }

        public Mock<IFlagsRegister> Flags { get; }

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

        public byte InitialRegister8(Operand r)
        {
            switch (r)
            {
                case Operand.A:
                    return InitialAccumulator.A;
                case Operand.B:
                    return InitialRegisters.B;
                case Operand.C:
                    return InitialRegisters.C;
                case Operand.D:
                    return InitialRegisters.D;
                case Operand.E:
                    return InitialRegisters.E;
                case Operand.F:
                    return InitialAccumulator.F;
                case Operand.H:
                    return InitialRegisters.H;
                case Operand.L:
                    return InitialRegisters.L;

                default:
                    throw new ArgumentOutOfRangeException(nameof(r), r, "Must be an 8-bit register");
            }
        }

        public ushort InitialRegister16(Operand r)
        {
            switch (r)
            {
                case Operand.AF:
                    return InitialAccumulator.AF;
                case Operand.BC:
                    return InitialRegisters.BC;
                case Operand.DE:
                    return InitialRegisters.DE;
                case Operand.HL:
                    return InitialRegisters.HL;
                case Operand.IX:
                    return _IX;
                case Operand.IY:
                    return _IY;

                default:
                    throw new ArgumentOutOfRangeException(nameof(r), r, "Must be an 8-bit register");
            }
        }

        public byte Operand8(Operand r)
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

        public ushort Operand16(Operand r)
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

        public void VerifyFlag(Expression<Func<IFlagsRegister, bool>> flag, bool? value)
        {
            var flagsExpression = Expression.Parameter(typeof(IFlagsRegister), "flags");
            var property = flagsExpression.GetPropertyExpression(flag);
            var getLambda = Expression.Lambda<Func<IFlagsRegister, bool>>(property, flagsExpression);

            if (!value.HasValue)
            {
                Flags.Verify(getLambda, Times.Never);
                return;
            }

            var setExpression = Expression.Assign(property, Expression.Constant(value.Value));
            var setLambda = Expression.Lambda<Action<IFlagsRegister>>(setExpression, flagsExpression);

            Flags.VerifySet(setLambda.Compile(), Times.AtLeastOnce);
            getLambda.Compile()(Flags.Object).ShouldBe(value.Value);
        }

        public Expression<Func<IAlu, byte>> Alu8Call(LambdaExpression f, Operand o1, Operand? o2 = null) => GetAluExpression<Func<IAlu, byte>>(f, o1, o2);

        public Expression<Func<IAlu, ushort>> Alu16Call(LambdaExpression f, Operand o1, Operand? o2 = null) => GetAluExpression<Func<IAlu, ushort>>(f, o1, o2);

        public Expression<Action<IAlu>> AluCall(LambdaExpression f, Operand o1, Operand? o2 = null) => GetAluExpression<Action<IAlu>>(f, o1, o2);

        private Expression<TFunc> GetAluExpression<TFunc>(LambdaExpression f, Operand o1, Operand? o2 = null)
        {
            var method = (f.Body as MethodCallExpression)?.Method ?? throw new ArgumentException("not a method call");
            var alu = Expression.Parameter(typeof(IAlu));

            var parameters = method.GetParameters();
            var is16Bit = parameters[0].ParameterType == typeof(ushort);

            Expression GetExpression(Operand o) => is16Bit ? Expression.Constant(Operand16(o)) : Expression.Constant(Operand8(o));

            var call = method.GetParameters().Length == 2 && o2.HasValue
                ? Expression.Call(alu, method, GetExpression(o1), GetExpression(o2.Value))
                : Expression.Call(alu, method, GetExpression(o1));
            return Expression.Lambda<TFunc>(call, alu);
        }
    }
}