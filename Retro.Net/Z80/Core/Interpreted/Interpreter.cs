using System;
using System.Linq;
using Retro.Net.Memory;
using Retro.Net.Timing;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Timing;

namespace Retro.Net.Z80.Core.Interpreted
{
    /// <summary>
    /// Simple, interpreted instruction block factory.
    /// </summary>
    /// <seealso cref="IInstructionBlockFactory" />
    public class Interpreter : IInstructionBlockFactory
    {
        private readonly CpuMode _cpuMode;
        private readonly bool _debug;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Interpreter"/> class.
        /// </summary>
        /// <param name="platformConfig">The platform configuration.</param>
        /// <param name="runtimeConfig">The runtime configuration.</param>
        public Interpreter(IPlatformConfig platformConfig, IRuntimeConfig runtimeConfig)
        {
            _cpuMode = platformConfig.CpuMode;
            _debug = runtimeConfig.DebugMode;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IInstructionBlockFactory"/> [supports instruction block caching].
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="IInstructionBlockFactory"/> [supports instruction block caching]; otherwise, <c>false</c>.
        /// </value>
        public bool SupportsInstructionBlockCaching => false;

        /// <summary>
        /// Builds a new <see cref="IInstructionBlock"/> from the specified decoded block.
        /// </summary>
        /// <param name="block">The decoded instruction block.</param>
        /// <returns></returns>
        public IInstructionBlock Build(DecodedBlock block)
        {
            var debugInfo = _debug ? $"{string.Join("\n", block.Operations.Select(x => x.ToString()))}" : null;

            InstructionTimings Run(IRegisters registers, IMmu mmu, IAlu alu, IPeripheralManager peripherals) => Interpret(registers, mmu, alu, peripherals, block);
            return new InstructionBlock(block.Address, block.Length, Run, block.Timings, block.Halt, block.Stop, debugInfo);
        }

        private InstructionTimings Interpret(IRegisters registers, IMmu mmu, IAlu alu, IPeripheralManager peripherals, DecodedBlock block)
        {
            var helper = new InterpreterHelper(registers, mmu, alu, peripherals);
            var result = block.Operations.Select(o => Interpret(registers, mmu, alu, peripherals, helper, o, block)).Aggregate((t0, t1) => t0 + t1);

            if (_cpuMode == CpuMode.Z80)
            {
                // Add the block length to the 7 lsb of memory refresh register.
                registers.R = (byte) ((registers.R + block.Length) & 0x7f);
            }

            return result;
        }

        private static void SyncProgramCounter(IRegisters registers, DecodedBlock block) => registers.ProgramCounter = (ushort) (registers.ProgramCounter + block.Length);

        private InstructionTimings Interpret(IRegisters registers, IMmu mmu, IAlu alu, IPeripheralManager peripherals, InterpreterHelper helper, Operation operation, DecodedBlock block)
        {
            helper.Operation = operation;
            var timer = new InstructionTimingsBuilder();

            switch (operation.OpCode)
            {
                case OpCode.NoOperation:
                    break;
                case OpCode.Stop:
                case OpCode.Halt:
                    SyncProgramCounter(registers, block);
                    break;

                case OpCode.Load:
                    if (operation.Operand1 == operation.Operand2)
                    {
                        break;
                    }

                    helper.Operand1 = helper.Operand2;
                    if (operation.Operand2 == Operand.I || operation.Operand2 == Operand.R)
                    {
                        // LD A, R & LD A, I also reset H & N and copy IFF2 to P/V
                        registers.AccumulatorAndFlagsRegisters.Flags.SetResultFlags(registers.AccumulatorAndFlagsRegisters.A);
                        registers.AccumulatorAndFlagsRegisters.Flags.HalfCarry = false;
                        registers.AccumulatorAndFlagsRegisters.Flags.Subtract = false;
                        registers.AccumulatorAndFlagsRegisters.Flags.ParityOverflow = registers.InterruptFlipFlop2;
                    }
                    break;
                    
                case OpCode.Load16:
                    helper.WordOperand1 = helper.WordOperand2;
                    break;

                case OpCode.Push:
                    helper.PushStackPointer();
                    mmu.WriteWord(registers.StackPointer, helper.WordOperand1);
                    break;

                case OpCode.Pop:
                    helper.WordOperand1 = mmu.ReadWord(registers.StackPointer);
                    helper.PopStackPointer();
                    break;

                case OpCode.Add:
                    helper.Alu8BitOperation(alu.Add);
                    break;

                case OpCode.AddWithCarry:
                    helper.Alu8BitOperation(alu.AddWithCarry);
                    break;

                case OpCode.Subtract:
                    helper.Alu8BitOperation(alu.Subtract);
                    break;

                case OpCode.SubtractWithCarry:
                    helper.Alu8BitOperation(alu.SubtractWithCarry);
                    break;

                case OpCode.And:
                    helper.Alu8BitOperation(alu.And);
                    break;

                case OpCode.Or:
                    helper.Alu8BitOperation(alu.Or);
                    break;

                case OpCode.Xor:
                    helper.Alu8BitOperation(alu.Xor);
                    break;

                case OpCode.Compare:
                    alu.Compare(registers.AccumulatorAndFlagsRegisters.A, helper.Operand1);
                    break;

                case OpCode.Increment:
                    helper.Operand1 = alu.Increment(helper.Operand1);
                    break;

                case OpCode.Decrement:
                    helper.Operand1 = alu.Decrement(helper.Operand1);
                    break;

                case OpCode.Add16:
                    helper.Alu16BitOperation(alu.Add);
                    break;

                case OpCode.AddWithCarry16:
                    helper.Alu16BitOperation(alu.AddWithCarry);
                    break;

                case OpCode.SubtractWithCarry16:
                    helper.Alu16BitOperation(alu.SubtractWithCarry);
                    break;

                case OpCode.Increment16:
                    // INC ss (no flags changes so implemented directly)
                    helper.WordOperand1 = (ushort) (helper.WordOperand1 + 1);
                    break;

                case OpCode.Decrement16:
                    // DEC ss (no flags changes so implemented directly)
                    helper.WordOperand1 = (ushort) (helper.WordOperand1 - 1);
                    break;

                case OpCode.Exchange:
                    {
                        var w = helper.WordOperand2;
                        helper.WordOperand2 = helper.WordOperand1;
                        helper.WordOperand1 = w;
                    }
                    break;

                case OpCode.ExchangeAccumulatorAndFlags:
                    registers.SwitchToAlternativeAccumulatorAndFlagsRegisters();
                    break;

                case OpCode.ExchangeGeneralPurpose:
                    registers.SwitchToAlternativeGeneralPurposeRegisters();
                    break;

                case OpCode.Jump:
                    if (operation.FlagTest == FlagTest.None || helper.DoFlagTest())
                    {
                        registers.ProgramCounter = helper.WordOperand1;
                    }
                    else
                    {
                        SyncProgramCounter(registers, block);
                    }
                    break;

                case OpCode.JumpRelative:
                    if (operation.FlagTest == FlagTest.None || helper.DoFlagTest())
                    {
                        helper.JumpToDisplacement();

                        if (operation.FlagTest != FlagTest.None)
                        {
                            timer.Add(1, 5);
                        }
                    }
                    SyncProgramCounter(registers, block);
                    break;

                case OpCode.DecrementJumpRelativeIfNonZero:
                    registers.GeneralPurposeRegisters.B--;
                    if (registers.GeneralPurposeRegisters.B != 0)
                    {
                        helper.JumpToDisplacement();
                        timer.Add(1, 5);
                    }
                    SyncProgramCounter(registers, block);
                    break;

                case OpCode.Call:
                    SyncProgramCounter(registers, block);

                    if (operation.FlagTest == FlagTest.None || helper.DoFlagTest())
                    {
                        helper.PushStackPointer();
                        mmu.WriteWord(registers.StackPointer, registers.ProgramCounter);
                        registers.ProgramCounter = helper.WordOperand1;

                        if (operation.FlagTest != FlagTest.None)
                        {
                            timer.Add(2, 7);
                        }
                    }
                    break;

                case OpCode.Return:
                    if (operation.FlagTest == FlagTest.None || helper.DoFlagTest())
                    {
                        registers.ProgramCounter = mmu.ReadWord(registers.StackPointer);
                        helper.PopStackPointer();

                        if (operation.FlagTest != FlagTest.None)
                        {
                            timer.Add(2, 6);
                        }
                    }
                    else
                    {
                        SyncProgramCounter(registers, block);
                    }
                    break;

                case OpCode.ReturnFromInterrupt:
                    registers.ProgramCounter = mmu.ReadWord(registers.StackPointer);
                    helper.PopStackPointer();
                    registers.InterruptFlipFlop1 = true;
                    break;

                case OpCode.ReturnFromNonmaskableInterrupt:
                    registers.ProgramCounter = mmu.ReadWord(registers.StackPointer);
                    helper.PopStackPointer();
                    registers.InterruptFlipFlop1 = registers.InterruptFlipFlop2;
                    break;

                case OpCode.Reset:
                    SyncProgramCounter(registers, block);
                    helper.PushStackPointer();
                    mmu.WriteWord(registers.StackPointer, registers.ProgramCounter);
                    registers.ProgramCounter = helper.WordOperand1;
                    break;

                case OpCode.Input:
                    helper.Operand1 = peripherals.ReadByteFromPort(helper.Operand2,
                                                                   operation.Operand2 == Operand.n
                                                                       ? registers.AccumulatorAndFlagsRegisters.A
                                                                       : registers.GeneralPurposeRegisters.B);
                    break;

                case OpCode.Output:
                    peripherals.WriteByteToPort(helper.Operand2,
                                                operation.Operand2 == Operand.n
                                                    ? registers.AccumulatorAndFlagsRegisters.A
                                                    : registers.GeneralPurposeRegisters.B,
                                                helper.Operand1);
                    break;

                case OpCode.RotateLeftWithCarry:
                    helper.Operand1 = alu.RotateLeftWithCarry(helper.Operand1);
                    if (operation.OpCodeMeta == OpCodeMeta.UseAlternativeFlagAffection)
                    {
                        registers.AccumulatorAndFlagsRegisters.Flags.Zero = false;
                        registers.AccumulatorAndFlagsRegisters.Flags.Sign = false;
                    }
                    break;

                case OpCode.RotateLeft:
                    helper.Operand1 = alu.RotateLeft(helper.Operand1);
                    if (operation.OpCodeMeta == OpCodeMeta.UseAlternativeFlagAffection)
                    {
                        registers.AccumulatorAndFlagsRegisters.Flags.Zero = false;
                        registers.AccumulatorAndFlagsRegisters.Flags.Sign = false;
                    }
                    break;

                case OpCode.RotateRightWithCarry:
                    helper.Operand1 = alu.RotateRightWithCarry(helper.Operand1);
                    if (operation.OpCodeMeta == OpCodeMeta.UseAlternativeFlagAffection)
                    {
                        registers.AccumulatorAndFlagsRegisters.Flags.Zero = false;
                        registers.AccumulatorAndFlagsRegisters.Flags.Sign = false;
                    }
                    break;

                case OpCode.RotateRight:
                    helper.Operand1 = alu.RotateRight(helper.Operand1);
                    if (operation.OpCodeMeta == OpCodeMeta.UseAlternativeFlagAffection)
                    {
                        registers.AccumulatorAndFlagsRegisters.Flags.Zero = false;
                        registers.AccumulatorAndFlagsRegisters.Flags.Sign = false;
                    }
                    break;

                case OpCode.RotateLeftDigit:
                    {
                        var result = alu.RotateLeftDigit(registers.AccumulatorAndFlagsRegisters.A,
                                                         mmu.ReadByte(registers.GeneralPurposeRegisters.HL));
                        registers.AccumulatorAndFlagsRegisters.A = result.Accumulator;
                        mmu.WriteByte(registers.GeneralPurposeRegisters.HL, result.Result);
                    }
                    break;

                case OpCode.RotateRightDigit:
                    {
                        var result = alu.RotateRightDigit(registers.AccumulatorAndFlagsRegisters.A,
                                                          mmu.ReadByte(registers.GeneralPurposeRegisters.HL));
                        registers.AccumulatorAndFlagsRegisters.A = result.Accumulator;
                        mmu.WriteByte(registers.GeneralPurposeRegisters.HL, result.Result);
                    }
                    break;

                case OpCode.ShiftLeft:
                    helper.Operand1 = alu.ShiftLeft(helper.Operand1);
                    break;

                case OpCode.ShiftLeftSet:
                    helper.Operand1 = alu.ShiftLeftSet(helper.Operand1);
                    break;

                case OpCode.ShiftRight:
                    helper.Operand1 = alu.ShiftRight(helper.Operand1);
                    break;

                case OpCode.ShiftRightLogical:
                    helper.Operand1 = alu.ShiftRightLogical(helper.Operand1);
                    break;

                case OpCode.BitTest:
                    alu.BitTest(helper.Operand1, operation.ByteLiteral);
                    break;

                case OpCode.BitSet:
                    helper.Operand1 = alu.BitSet(helper.Operand1, operation.ByteLiteral);
                    break;

                case OpCode.BitReset:
                    helper.Operand1 = alu.BitReset(helper.Operand1, operation.ByteLiteral);
                    break;

                case OpCode.TransferIncrement:
                    helper.BlockTransfer();
                    break;

                case OpCode.TransferIncrementRepeat:
                    helper.BlockTransferRepeat(timer);
                    break;

                case OpCode.TransferDecrement:
                    helper.BlockTransfer(true);
                    break;

                case OpCode.TransferDecrementRepeat:
                    helper.BlockTransferRepeat(timer, true);
                    break;

                case OpCode.SearchIncrement:
                    helper.BlockSearch();
                    break;

                case OpCode.SearchIncrementRepeat:
                    helper.BlockSearchRepeat(timer);
                    break;

                case OpCode.SearchDecrement:
                    helper.BlockSearch(true);
                    break;

                case OpCode.SearchDecrementRepeat:
                    helper.BlockSearchRepeat(timer, true);
                    break;

                case OpCode.InputTransferIncrement:
                    helper.InputTransfer();
                    break;

                case OpCode.InputTransferIncrementRepeat:
                    helper.InputTransferRepeat(timer);
                    break;

                case OpCode.InputTransferDecrement:
                    helper.InputTransfer(true);
                    break;

                case OpCode.InputTransferDecrementRepeat:
                    helper.InputTransferRepeat(timer, true);
                    break;

                case OpCode.OutputTransferIncrement:
                    helper.OutputTransfer();
                    break;

                case OpCode.OutputTransferIncrementRepeat:
                    helper.OutputTransferRepeat(timer);
                    break;

                case OpCode.OutputTransferDecrement:
                    helper.OutputTransfer(true);
                    break;

                case OpCode.OutputTransferDecrementRepeat:
                    helper.OutputTransferRepeat(timer, true);
                    break;

                case OpCode.DecimalArithmeticAdjust:
                    registers.AccumulatorAndFlagsRegisters.A = alu.DecimalAdjust(registers.AccumulatorAndFlagsRegisters.A,
                                                                                 _cpuMode == CpuMode.Z80);
                    break;

                case OpCode.NegateOnesComplement:
                    registers.AccumulatorAndFlagsRegisters.A = (byte) ~registers.AccumulatorAndFlagsRegisters.A;
                    registers.AccumulatorAndFlagsRegisters.Flags.SetUndocumentedFlags(registers.AccumulatorAndFlagsRegisters.A);
                    registers.AccumulatorAndFlagsRegisters.Flags.HalfCarry = true;
                    registers.AccumulatorAndFlagsRegisters.Flags.Subtract = true;
                    break;

                case OpCode.NegateTwosComplement:
                    registers.AccumulatorAndFlagsRegisters.A = alu.Subtract(0, registers.AccumulatorAndFlagsRegisters.A);
                    break;

                case OpCode.InvertCarryFlag:
                    registers.AccumulatorAndFlagsRegisters.Flags.SetUndocumentedFlags(registers.AccumulatorAndFlagsRegisters.A);
                    registers.AccumulatorAndFlagsRegisters.Flags.HalfCarry = _cpuMode != CpuMode.GameBoy &&
                                                                             registers.AccumulatorAndFlagsRegisters.Flags.Carry;
                    registers.AccumulatorAndFlagsRegisters.Flags.Subtract = false;
                    registers.AccumulatorAndFlagsRegisters.Flags.Carry = !registers.AccumulatorAndFlagsRegisters.Flags.Carry;
                    break;

                case OpCode.SetCarryFlag:
                    registers.AccumulatorAndFlagsRegisters.Flags.SetUndocumentedFlags(registers.AccumulatorAndFlagsRegisters.A);
                    registers.AccumulatorAndFlagsRegisters.Flags.HalfCarry = false;
                    registers.AccumulatorAndFlagsRegisters.Flags.Subtract = false;
                    registers.AccumulatorAndFlagsRegisters.Flags.Carry = true;
                    break;

                case OpCode.DisableInterrupts:
                    registers.InterruptFlipFlop1 = false;
                    registers.InterruptFlipFlop2 = false;
                    break;

                case OpCode.EnableInterrupts:
                    registers.InterruptFlipFlop1 = true;
                    registers.InterruptFlipFlop2 = true;
                    break;

                case OpCode.InterruptMode0:
                    registers.InterruptMode = InterruptMode.InterruptMode0;
                    break;

                case OpCode.InterruptMode1:
                    registers.InterruptMode = InterruptMode.InterruptMode1;
                    break;

                case OpCode.InterruptMode2:
                    registers.InterruptMode = InterruptMode.InterruptMode2;
                    break;

                case OpCode.Swap:
                    helper.Operand1 = alu.Swap(helper.Operand1);
                    break;

                case OpCode.LoadIncrement:
                    helper.Operand1 = helper.Operand2;
                    registers.GeneralPurposeRegisters.HL++;
                    break;

                case OpCode.LoadDecrement:
                    helper.Operand1 = helper.Operand2;
                    registers.GeneralPurposeRegisters.HL--;
                    break;

                    default:
                        throw new ArgumentOutOfRangeException();
            }

            if (operation.OpCodeMeta == OpCodeMeta.AutoCopy)
            {
                // Autocopy for DD/FD prefix
                helper.Operand2 = helper.Operand1;
            }

            return timer.GetInstructionTimings();
        }
    }
}