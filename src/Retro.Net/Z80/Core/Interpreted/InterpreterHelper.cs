using System;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.Timing;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Z80.Core.Interpreted
{
    public class InterpreterHelper
    {
        private readonly IRegisters _registers;
        private readonly IMmu _mmu;
        private readonly IAlu _alu;
        private readonly IPeripheralManager _peripheralManager;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="InterpreterHelper"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="registers">The registers.</param>
        /// <param name="mmu">The mmu.</param>
        /// <param name="alu">The alu.</param>
        /// <param name="peripheralManager">The peripheral manager.</param>
        public InterpreterHelper(IRegisters registers, IMmu mmu, IAlu alu, IPeripheralManager peripheralManager)
        {
            _registers = registers;
            _mmu = mmu;
            _alu = alu;
            _peripheralManager = peripheralManager;
        }

        public Operation Operation { get; set; }

        public byte Operand1
        {
            get => ReadOperand(Operation.Operand1);
            set => WriteOperand(value, Operation.Operand1);
        }

        public byte Operand2
        {
            get => ReadOperand(Operation.Operand2);
            set => WriteOperand(value, Operation.Operand2);
        }

        public ushort WordOperand1
        {
            get => Read16BitOperand(Operation.Operand1);
            set => Write16BitOperand(value, Operation.Operand1);
        }

        public ushort WordOperand2
        {
            get => Read16BitOperand(Operation.Operand2);
            set => Write16BitOperand(value, Operation.Operand2);
        }

        public byte ReadOperand(Operand operand)
        {
            switch (operand)
            {
                case Operand.A:
                    return _registers.AccumulatorAndFlagsRegisters.A;
                case Operand.B:
                    return _registers.GeneralPurposeRegisters.B;
                case Operand.C:
                    return _registers.GeneralPurposeRegisters.C;
                case Operand.D:
                    return _registers.GeneralPurposeRegisters.D;
                case Operand.E:
                    return _registers.GeneralPurposeRegisters.E;
                case Operand.F:
                    return _registers.AccumulatorAndFlagsRegisters.Flags.Register;
                case Operand.H:
                    return _registers.GeneralPurposeRegisters.H;
                case Operand.L:
                    return _registers.GeneralPurposeRegisters.L;
                case Operand.mHL:
                    return _mmu.ReadByte(_registers.GeneralPurposeRegisters.HL);
                case Operand.mBC:
                    return _mmu.ReadByte(_registers.GeneralPurposeRegisters.BC);
                case Operand.mDE:
                    return _mmu.ReadByte(_registers.GeneralPurposeRegisters.DE);
                case Operand.mSP:
                    return _mmu.ReadByte(_registers.StackPointer);
                case Operand.mnn:
                    return _mmu.ReadByte(Operation.WordLiteral);
                case Operand.n:
                    return Operation.ByteLiteral;
                case Operand.mIXd:
                    return _mmu.ReadByte((ushort) (_registers.IX + Operation.Displacement));
                case Operand.IXl:
                    return _registers.IXl;
                case Operand.IXh:
                    return _registers.IXh;
                case Operand.mIYd:
                    return _mmu.ReadByte((ushort)(_registers.IY + Operation.Displacement));
                case Operand.IYl:
                    return _registers.IYl;
                case Operand.IYh:
                    return _registers.IYh;
                case Operand.I:
                    return _registers.I;
                case Operand.R:
                    return _registers.R;
                case Operand.mCl:
                    return _mmu.ReadByte((ushort) (_registers.GeneralPurposeRegisters.C + 0xff00));
                case Operand.mnl:
                    return _mmu.ReadByte((ushort) (Operation.ByteLiteral + 0xff00));
                default:
                    throw new ArgumentOutOfRangeException(nameof(Operation.Operand2), Operation.Operand2, null);
            }
        }

        public ushort Read16BitOperand(Operand operand)
        {
            switch (operand)
            {
                case Operand.HL:
                    return _registers.GeneralPurposeRegisters.HL;
                case Operand.BC:
                    return _registers.GeneralPurposeRegisters.BC;
                case Operand.DE:
                    return _registers.GeneralPurposeRegisters.DE;
                case Operand.AF:
                    return _registers.AccumulatorAndFlagsRegisters.AF;
                case Operand.SP:
                    return _registers.StackPointer;
                case Operand.mHL:
                    return _mmu.ReadWord(_registers.GeneralPurposeRegisters.HL);
                case Operand.mBC:
                    return _mmu.ReadWord(_registers.GeneralPurposeRegisters.BC);
                case Operand.mDE:
                    return _mmu.ReadWord(_registers.GeneralPurposeRegisters.DE);
                case Operand.mSP:
                    return _mmu.ReadWord(_registers.StackPointer);
                case Operand.mnn:
                    return _mmu.ReadWord(Operation.WordLiteral);
                case Operand.nn:
                    return Operation.WordLiteral;
                case Operand.n:
                    return Operation.ByteLiteral;
                case Operand.IX:
                    return _registers.IX;
                case Operand.mIXd:
                    return _mmu.ReadWord((ushort)(_registers.IX + Operation.Displacement));
                case Operand.IY:
                    return _registers.IY;
                case Operand.mIYd:
                    return _mmu.ReadWord((ushort)(_registers.IY + Operation.Displacement));
                case Operand.mCl:
                    return _mmu.ReadWord((ushort) (_registers.GeneralPurposeRegisters.C + 0xff00));
                case Operand.mnl:
                    return _mmu.ReadWord((ushort)(Operation.ByteLiteral + 0xff00));
                case Operand.SPd:
                    return _alu.AddDisplacement(_registers.StackPointer, Operation.Displacement);
                default:
                    throw new ArgumentOutOfRangeException(nameof(Operation.Operand2), Operation.Operand2, null);
            }
        }

        public void WriteOperand(byte value, Operand operand)
        {
            switch (operand)
            {
                case Operand.A:
                    _registers.AccumulatorAndFlagsRegisters.A = value;
                    break;
                case Operand.B:
                    _registers.GeneralPurposeRegisters.B = value;
                    break;
                case Operand.C:
                    _registers.GeneralPurposeRegisters.C = value;
                    break;
                case Operand.D:
                    _registers.GeneralPurposeRegisters.D = value;
                    break;
                case Operand.E:
                    _registers.GeneralPurposeRegisters.E = value;
                    break;
                case Operand.F:
                    _registers.AccumulatorAndFlagsRegisters.Flags.Register = value;
                    break;
                case Operand.H:
                    _registers.GeneralPurposeRegisters.H = value;
                    break;
                case Operand.L:
                    _registers.GeneralPurposeRegisters.L = value;
                    break;
                case Operand.mHL:
                    _mmu.WriteByte(_registers.GeneralPurposeRegisters.HL, value);
                    break;
                case Operand.mBC:
                    _mmu.WriteByte(_registers.GeneralPurposeRegisters.BC, value);
                    break;
                case Operand.mDE:
                    _mmu.WriteByte(_registers.GeneralPurposeRegisters.DE, value);
                    break;
                case Operand.mSP:
                    _mmu.WriteByte(_registers.StackPointer, value);
                    break;
                case Operand.mnn:
                    _mmu.WriteByte(Operation.WordLiteral, value);
                    break;
                case Operand.n:
                    _mmu.WriteByte(Operation.ByteLiteral, value);
                    break;
                case Operand.mIXd:
                    _mmu.WriteByte((ushort)(_registers.IX + Operation.Displacement), value);
                    break;
                case Operand.IXl:
                    _registers.IXl = value;
                    break;
                case Operand.IXh:
                    _registers.IXh = value;
                    break;
                case Operand.mIYd:
                    _mmu.WriteByte((ushort)(_registers.IY + Operation.Displacement), value);
                    break;
                case Operand.IYl:
                    _registers.IYl = value;
                    break;
                case Operand.IYh:
                    _registers.IYh = value;
                    break;
                case Operand.I:
                    _registers.I = value;
                    break;
                case Operand.R:
                    _registers.R = value;
                    break;
                case Operand.mCl:
                    _mmu.WriteByte((ushort)(_registers.GeneralPurposeRegisters.C + 0xff00), value);
                    break;
                case Operand.mnl:
                    _mmu.WriteByte((ushort)(Operation.ByteLiteral + 0xff00), value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Operation.Operand2), Operation.Operand2, null);
            }
        }

        public void Write16BitOperand(ushort value, Operand operand)
        {
            switch (operand)
            {
                case Operand.HL:
                    _registers.GeneralPurposeRegisters.HL = value;
                    break;
                case Operand.BC:
                    _registers.GeneralPurposeRegisters.BC = value;
                    break;
                case Operand.DE:
                    _registers.GeneralPurposeRegisters.DE = value;
                    break;
                case Operand.AF:
                    _registers.AccumulatorAndFlagsRegisters.AF = value;
                    break;
                case Operand.SP:
                    _registers.StackPointer = value;
                    break;
                case Operand.mHL:
                    _mmu.WriteWord(_registers.GeneralPurposeRegisters.HL, value);
                    break;
                case Operand.mBC:
                    _mmu.WriteWord(_registers.GeneralPurposeRegisters.BC, value);
                    break;
                case Operand.mDE:
                    _mmu.WriteWord(_registers.GeneralPurposeRegisters.DE, value);
                    break;
                case Operand.mSP:
                    _mmu.WriteWord(_registers.StackPointer, value);
                    break;
                case Operand.mnn:
                    _mmu.WriteWord(Operation.WordLiteral, value);
                    break;
                case Operand.IX:
                    _registers.IX = value;
                    break;
                case Operand.mIXd:
                    _mmu.WriteWord((ushort)(_registers.IX + Operation.Displacement), value);
                    break;
                case Operand.IY:
                    _registers.IY = value;
                    break;
                case Operand.mIYd:
                    _mmu.WriteWord((ushort)(_registers.IY + Operation.Displacement), value);
                    break;
                case Operand.mCl:
                    _mmu.WriteWord((ushort)(_registers.GeneralPurposeRegisters.C + 0xff00), value);
                    break;
                case Operand.mnl:
                    _mmu.WriteWord((ushort)(Operation.ByteLiteral + 0xff00), value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Operation.Operand2), Operation.Operand2, null);
            }
        }

        public bool DoFlagTest()
        {
            switch (Operation.FlagTest)
            {
                case FlagTest.NotZero:
                    return !_registers.AccumulatorAndFlagsRegisters.Flags.Zero;
                case FlagTest.Zero:
                    return _registers.AccumulatorAndFlagsRegisters.Flags.Zero;
                case FlagTest.NotCarry:
                    return !_registers.AccumulatorAndFlagsRegisters.Flags.Carry;
                case FlagTest.Carry:
                    return _registers.AccumulatorAndFlagsRegisters.Flags.Carry;
                case FlagTest.ParityOdd:
                    return !_registers.AccumulatorAndFlagsRegisters.Flags.ParityOverflow;
                case FlagTest.ParityEven:
                    return _registers.AccumulatorAndFlagsRegisters.Flags.ParityOverflow;
                case FlagTest.Positive:
                    return !_registers.AccumulatorAndFlagsRegisters.Flags.Sign;
                case FlagTest.Negative:
                    return _registers.AccumulatorAndFlagsRegisters.Flags.Sign;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Operation.FlagTest), Operation.FlagTest, null);
            }
        }

        public void UpdateMemoryRefresh(int delta) => _registers.R = (byte) ((_registers.R + delta) & 0x7f);

        public void JumpToDisplacement()
        {
            _registers.ProgramCounter = (ushort) (_registers.ProgramCounter + Operation.Displacement);
        }

        private void BlockTransferIteration(bool decrement)
        {
            _mmu.TransferByte(_registers.GeneralPurposeRegisters.HL, _registers.GeneralPurposeRegisters.DE);
            
            if (decrement)
            {
                _registers.GeneralPurposeRegisters.HL--;
                _registers.GeneralPurposeRegisters.DE--;
            }
            else
            {
                _registers.GeneralPurposeRegisters.HL++;
                _registers.GeneralPurposeRegisters.DE++;
            }

            _registers.GeneralPurposeRegisters.BC--;
        }

        public void BlockTransfer(bool decrement = false)
        {
            BlockTransferIteration(decrement);
            _registers.AccumulatorAndFlagsRegisters.Flags.HalfCarry = false;
            _registers.AccumulatorAndFlagsRegisters.Flags.ParityOverflow = _registers.GeneralPurposeRegisters.BC != 0;
            _registers.AccumulatorAndFlagsRegisters.Flags.Subtract = false;
        }

        public void BlockTransferRepeat(IInstructionTimingsBuilder timer, bool decrement = false)
        {
            SimpleRepeat(() => BlockTransferIteration(decrement), () => _registers.GeneralPurposeRegisters.BC == 0, timer);
            
            _registers.AccumulatorAndFlagsRegisters.Flags.HalfCarry = false;
            _registers.AccumulatorAndFlagsRegisters.Flags.ParityOverflow = false;
            _registers.AccumulatorAndFlagsRegisters.Flags.Subtract = false;
        }
        
        public void BlockSearch(bool decrement = false)
        {
            _alu.Compare(_registers.AccumulatorAndFlagsRegisters.A, _mmu.ReadByte(_registers.GeneralPurposeRegisters.HL));
            
            if (decrement)
            {
                _registers.GeneralPurposeRegisters.HL--;
            }
            else
            {
                _registers.GeneralPurposeRegisters.HL++;
            }

            _registers.GeneralPurposeRegisters.BC--;
        }

        public void BlockSearchRepeat(IInstructionTimingsBuilder timer, bool decrement = false)
            => SimpleRepeat(() => BlockSearch(decrement),
                            () => _registers.GeneralPurposeRegisters.BC == 0 || _registers.AccumulatorAndFlagsRegisters.Flags.Zero,
                            timer);

        public void InputTransfer(bool decrement = false)
        {
            var value = _peripheralManager.ReadByteFromPort(_registers.GeneralPurposeRegisters.C, _registers.GeneralPurposeRegisters.B);
            _mmu.WriteByte(_registers.GeneralPurposeRegisters.HL, value);
            UpdateRegistersForIoIteration(decrement);
        }

        public void InputTransferRepeat(IInstructionTimingsBuilder timer, bool decrement = false)
            => SimpleRepeat(() => InputTransfer(decrement), () => _registers.GeneralPurposeRegisters.B == 0, timer);

        public void OutputTransfer(bool decrement = false)
        {
            var value = _mmu.ReadByte(_registers.GeneralPurposeRegisters.HL);
            _peripheralManager.WriteByteToPort(_registers.GeneralPurposeRegisters.C, _registers.GeneralPurposeRegisters.B, value);
            UpdateRegistersForIoIteration(decrement);
        }

        public void OutputTransferRepeat(IInstructionTimingsBuilder timer, bool decrement = false)
            => SimpleRepeat(() => OutputTransfer(decrement), () => _registers.GeneralPurposeRegisters.B == 0, timer);

        public void PushStackPointer() => _registers.StackPointer -= 2;

        public void PopStackPointer() => _registers.StackPointer += 2;

        public void Alu8BitOperation(Func<byte, byte, byte> aluFunc)
            => _registers.AccumulatorAndFlagsRegisters.A = aluFunc(_registers.AccumulatorAndFlagsRegisters.A, Operand1);

        public void Alu16BitOperation(Func<ushort, ushort, ushort> aluFunc) => WordOperand1 = aluFunc(WordOperand1, WordOperand2);
        
        private void UpdateRegistersForIoIteration(bool decrement)
        {
            if (decrement)
            {
                _registers.GeneralPurposeRegisters.HL--;
            }
            else
            {
                _registers.GeneralPurposeRegisters.HL++;
            }

            _registers.GeneralPurposeRegisters.B--;

            _registers.AccumulatorAndFlagsRegisters.Flags.Subtract = true;
            _registers.AccumulatorAndFlagsRegisters.Flags.SetResultFlags(_registers.GeneralPurposeRegisters.B);
        }

        private void SimpleRepeat(Action iteration, Func<bool> breakWhen, IInstructionTimingsBuilder timer)
        {
            var memoryRefreshDelta = 0;
            while (true)
            {
                iteration();

                if (breakWhen())
                {
                    break;
                }

                timer.Add(5, 21);
                memoryRefreshDelta += 2;
            }

            UpdateMemoryRefresh(memoryRefreshDelta);
        }
    }
}
