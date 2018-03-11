using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Z80.Core.DynaRec
{
    /// <summary>
    /// Instruction block decoder using a dynamic translation from Z80 operations to expression trees.
    /// </summary>
    /// <seealso cref="IInstructionBlockFactory" />
    public partial class DynaRec
    {
        /// <summary>
        /// Recompiles the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="block">The block.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private IEnumerable<Expression> Recompile(Operation operation, DecodedBlock block)
        {
            switch (operation.OpCode)
            {
                case OpCode.NoOperation:
                    break;
                case OpCode.Stop:
                case OpCode.Halt:
                    yield return SyncProgramCounter(block);
                    break;

                case OpCode.Load:
                    if (operation.Operand1 == operation.Operand2)
                    {
                        break;
                    }

                    yield return WriteOperand1(operation, ReadOperand2(operation));

                    if (operation.Operand2 == Operand.I || operation.Operand2 == Operand.R)
                    {
                        // LD A, R & LD A, I also reset H & N and copy IFF2 to P/V
                        yield return Expression.Call(Flags, SetResultFlags, A);
                        yield return Expression.Assign(HalfCarry, Expression.Constant(false));
                        yield return Expression.Assign(Subtract, Expression.Constant(false));
                        yield return Expression.Assign(ParityOverflow, IFF2);
                    }
                    break;

                case OpCode.Load16:
                    yield return WriteOperand1(operation, ReadOperand2(operation, true), true);
                    break;

                case OpCode.Push:
                    yield return PushSP;
                    yield return Expression.Call(Mmu, MmuWriteWord, SP, ReadOperand1(operation, true));
                    break;

                case OpCode.Pop:
                    yield return WriteOperand1(operation, Expression.Call(Mmu, MmuReadWord, SP), true);
                    yield return PopSP;
                    break;

                case OpCode.Add:
                    yield return Expression.Assign(A, Expression.Call(Alu, AluAdd, A, ReadOperand1(operation)));
                    break;

                case OpCode.AddWithCarry:
                    yield return Expression.Assign(A, Expression.Call(Alu, AluAddWithCarry, A, ReadOperand1(operation)));
                    break;

                case OpCode.Subtract:
                    yield return Expression.Assign(A, Expression.Call(Alu, AluSubtract, A, ReadOperand1(operation)));
                    break;

                case OpCode.SubtractWithCarry:
                    yield return Expression.Assign(A, Expression.Call(Alu, AluSubtractWithCarry, A, ReadOperand1(operation)));
                    break;

                case OpCode.And:
                    yield return Expression.Assign(A, Expression.Call(Alu, AluAnd, A, ReadOperand1(operation)));
                    break;

                case OpCode.Or:
                    yield return Expression.Assign(A, Expression.Call(Alu, AluOr, A, ReadOperand1(operation)));
                    break;

                case OpCode.Xor:
                    yield return Expression.Assign(A, Expression.Call(Alu, AluXor, A, ReadOperand1(operation)));
                    break;

                case OpCode.Compare:
                    yield return Expression.Call(Alu, AluCompare, A, ReadOperand1(operation));
                    break;

                case OpCode.Increment:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluIncrement, ReadOperand1(operation)));
                    break;

                case OpCode.Decrement:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluDecrement, ReadOperand1(operation)));
                    break;

                case OpCode.Add16:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluAdd16, ReadOperand1(operation, true), ReadOperand2(operation, true)), true);
                    break;

                case OpCode.AddWithCarry16:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluAdd16WithCarry, ReadOperand1(operation, true), ReadOperand2(operation, true)), true);
                    break;

                case OpCode.SubtractWithCarry16:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluSubtract16WithCarry, ReadOperand1(operation, true), ReadOperand2(operation, true)), true);
                    break;

                case OpCode.Increment16:
                    // INC ss (no flags changes so implemented directly)
                    yield return Expression.PreIncrementAssign(ReadOperand1(operation, true));
                    break;

                case OpCode.Decrement16:
                    // DEC ss (no flags changes so implemented directly)
                    yield return Expression.PreDecrementAssign(ReadOperand1(operation, true));
                    break;

                case OpCode.Exchange:
                    _usesLocalWord = true;
                    yield return Expression.Assign(LocalWord, ReadOperand2(operation, true));
                    yield return WriteOperand2(operation, ReadOperand1(operation, true), true);
                    yield return WriteOperand1(operation, LocalWord, true);
                    break;

                case OpCode.ExchangeAccumulatorAndFlags:
                    yield return SwitchToAlternativeAccumulatorAndFlagsRegisters;
                    break;

                case OpCode.ExchangeGeneralPurpose:
                    yield return SwitchToAlternativeGeneralPurposeRegisters;
                    break;

                case OpCode.Jump:
                    if (operation.FlagTest == FlagTest.None)
                    {
                        yield return Expression.Assign(PC, ReadOperand1(operation, true));
                    }
                    else
                    {
                        var trueBlock = Expression.Assign(PC, ReadOperand1(operation, true));
                        yield return Expression.IfThenElse(GetFlagTestExpression(operation.FlagTest), trueBlock, SyncProgramCounter(block));
                    }
                    break;

                case OpCode.JumpRelative:
                    if (operation.FlagTest == FlagTest.None)
                    {
                        yield return JumpToDisplacement(operation);
                    }
                    else
                    {
                        _usesDynamicTimings = true;
                        yield return Expression.IfThen(GetFlagTestExpression(operation.FlagTest), Expression.Block(JumpToDisplacement(operation), AddDynamicTimings(1, 5)));
                    }

                    // Relative jump so must also sync the PC.
                    yield return SyncProgramCounter(block);
                    break;

                case OpCode.DecrementJumpRelativeIfNonZero:
                    _usesDynamicTimings = true;
                    yield return Expression.Assign(B, Expression.Convert(Expression.Decrement(Expression.Convert(B, typeof (int))), typeof (byte)));
                    yield return Expression.IfThen(Expression.NotEqual(B, Expression.Constant((byte) 0)),
                        Expression.Block(JumpToDisplacement(operation), AddDynamicTimings(1, 5)));

                    // Relative jump so must also sync the PC.
                    yield return SyncProgramCounter(block);
                    break;

                case OpCode.Call:
                    yield return SyncProgramCounter(block);

                    if (operation.FlagTest == FlagTest.None)
                    {
                        yield return PushSP;
                        yield return WritePCToStack;
                        yield return Expression.Assign(PC, ReadOperand1(operation));
                    }
                    else
                    {
                        _usesDynamicTimings = true;
                        var trueBlock = Expression.Block(PushSP, WritePCToStack, Expression.Assign(PC, ReadOperand1(operation)), AddDynamicTimings(2, 7));
                        yield return Expression.IfThen(GetFlagTestExpression(operation.FlagTest), trueBlock);
                    }
                    break;

                case OpCode.Return:
                    if (operation.FlagTest == FlagTest.None)
                    {
                        yield return ReadPCFromStack;
                        yield return PopSP;
                    }
                    else
                    {
                        _usesDynamicTimings = true;
                        var trueBlock = Expression.Block(ReadPCFromStack, PopSP, AddDynamicTimings(2, 6));
                        yield return Expression.IfThenElse(GetFlagTestExpression(operation.FlagTest), trueBlock, SyncProgramCounter(block));
                    }
                    break;

                case OpCode.ReturnFromInterrupt:
                    yield return ReadPCFromStack;
                    yield return PopSP;
                    yield return Expression.Assign(IFF1, Expression.Constant(true));
                    break;

                case OpCode.ReturnFromNonmaskableInterrupt:
                    yield return ReadPCFromStack;
                    yield return PopSP;
                    yield return Expression.Assign(IFF1, IFF2);
                    break;

                case OpCode.Reset:
                    yield return SyncProgramCounter(block);
                    yield return PushSP;
                    yield return WritePCToStack;
                    yield return Expression.Assign(PC, ReadOperand1(operation, true));
                    break;

                case OpCode.Input:
                    yield return WriteOperand1(operation, Expression.Call(IO, IoReadByte, ReadOperand2(operation), operation.Operand2 == Operand.n ? A : B));
                    break;

                case OpCode.Output:
                    yield return Expression.Call(IO, IoWriteByte, ReadOperand2(operation), operation.Operand2 == Operand.n ? A : B, ReadOperand1(operation));
                    break;

                case OpCode.RotateLeftWithCarry:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluRotateLeftWithCarry, ReadOperand1(operation)));
                    if (operation.OpCodeMeta == OpCodeMeta.UseAlternativeFlagAffection)
                    {
                        yield return Expression.Assign(Zero, Expression.Constant(false));
                        yield return Expression.Assign(Sign, Expression.Constant(false));
                    }
                    break;

                case OpCode.RotateLeft:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluRotateLeft, ReadOperand1(operation)));
                    if (operation.OpCodeMeta == OpCodeMeta.UseAlternativeFlagAffection)
                    {
                        yield return Expression.Assign(Zero, Expression.Constant(false));
                        yield return Expression.Assign(Sign, Expression.Constant(false));
                    }
                    break;

                case OpCode.RotateRightWithCarry:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluRotateRightWithCarry, ReadOperand1(operation)));
                    if (operation.OpCodeMeta == OpCodeMeta.UseAlternativeFlagAffection)
                    {
                        yield return Expression.Assign(Zero, Expression.Constant(false));
                        yield return Expression.Assign(Sign, Expression.Constant(false));
                    }
                    break;

                case OpCode.RotateRight:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluRotateRight, ReadOperand1(operation)));
                    if (operation.OpCodeMeta == OpCodeMeta.UseAlternativeFlagAffection)
                    {
                        yield return Expression.Assign(Zero, Expression.Constant(false));
                        yield return Expression.Assign(Sign, Expression.Constant(false));
                    }
                    break;

                case OpCode.RotateLeftDigit:
                    _usesAccumulatorAndResult = true;
                    yield return Expression.Assign(AccumulatorAndResult, Expression.Call(Alu, AluRotateLeftDigit, A, ReadByteAtHL));
                    yield return Expression.Assign(A, AccumulatorAndResult_Accumulator);
                    yield return Expression.Call(Mmu, MmuWriteByte, HL, AccumulatorAndResult_Result);
                    break;

                case OpCode.RotateRightDigit:
                    _usesAccumulatorAndResult = true;
                    yield return Expression.Assign(AccumulatorAndResult, Expression.Call(Alu, AluRotateRightDigit, A, ReadByteAtHL));
                    yield return Expression.Assign(A, AccumulatorAndResult_Accumulator);
                    yield return Expression.Call(Mmu, MmuWriteByte, HL, AccumulatorAndResult_Result);
                    break;

                case OpCode.ShiftLeft:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluShiftLeft, ReadOperand1(operation)));
                    break;

                case OpCode.ShiftLeftSet:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluShiftLeftSet, ReadOperand1(operation)));
                    break;

                case OpCode.ShiftRight:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluShiftRight, ReadOperand1(operation)));
                    break;

                case OpCode.ShiftRightLogical:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluShiftRightLogical, ReadOperand1(operation)));
                    break;

                case OpCode.BitTest:
                    yield return Expression.Call(Alu, AluBitTest, ReadOperand1(operation), Expression.Constant((int) operation.ByteLiteral));
                    break;

                case OpCode.BitSet:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluBitSet, ReadOperand1(operation), Expression.Constant((int) operation.ByteLiteral)));
                    break;

                case OpCode.BitReset:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluBitReset, ReadOperand1(operation), Expression.Constant((int) operation.ByteLiteral)));
                    break;

                case OpCode.TransferIncrement:
                    yield return Expression.Block(GetBlockTransferExpressions());
                    break;

                case OpCode.TransferIncrementRepeat:
                    _usesDynamicTimings = true;
                    yield return Expression.Block(GetBlockTransferRepeatExpressions());
                    break;

                case OpCode.TransferDecrement:
                    yield return Expression.Block(GetBlockTransferExpressions(true));
                    break;

                case OpCode.TransferDecrementRepeat:
                    _usesDynamicTimings = true;
                    yield return Expression.Block(GetBlockTransferRepeatExpressions(true));
                    break;

                case OpCode.SearchIncrement:
                    yield return Expression.Block(GetSearchExpressions());
                    break;

                case OpCode.SearchIncrementRepeat:
                    _usesDynamicTimings = true;
                    yield return GetSearchRepeatExpression();
                    break;

                case OpCode.SearchDecrement:
                    yield return Expression.Block(GetSearchExpressions(true));
                    break;

                case OpCode.SearchDecrementRepeat:
                    _usesDynamicTimings = true;
                    yield return GetSearchRepeatExpression(true);
                    break;

                case OpCode.InputTransferIncrement:
                    yield return Expression.Block(GetInputTransferExpressions());
                    break;

                case OpCode.InputTransferIncrementRepeat:
                    _usesDynamicTimings = true;
                    yield return GetInputTransferRepeatExpression();
                    break;

                case OpCode.InputTransferDecrement:
                    yield return Expression.Block(GetInputTransferExpressions(true));
                    break;

                case OpCode.InputTransferDecrementRepeat:
                    _usesDynamicTimings = true;
                    yield return GetInputTransferRepeatExpression(true);
                    break;

                case OpCode.OutputTransferIncrement:
                    yield return Expression.Block(GetOutTransferExpressions());
                    break;

                case OpCode.OutputTransferIncrementRepeat:
                    _usesDynamicTimings = true;
                    yield return GetOutputTransferRepeatExpression();
                    break;

                case OpCode.OutputTransferDecrement:
                    yield return Expression.Block(GetOutTransferExpressions(true));
                    break;

                case OpCode.OutputTransferDecrementRepeat:
                    _usesDynamicTimings = true;
                    yield return GetOutputTransferRepeatExpression(true);
                    break;

                case OpCode.DecimalArithmeticAdjust:
                    yield return Expression.Assign(A, Expression.Call(Alu, AluDecimalAdjust, A, Expression.Constant(_cpuMode == CpuMode.Z80)));
                    break;

                case OpCode.NegateOnesComplement:
                    yield return Expression.Assign(A, Expression.Not(A));
                    yield return Expression.Call(Flags, SetUndocumentedFlags, A);
                    yield return Expression.Assign(HalfCarry, Expression.Constant(true));
                    yield return Expression.Assign(Subtract, Expression.Constant(true));
                    break;

                case OpCode.NegateTwosComplement:
                    yield return Expression.Assign(A, Expression.Call(Alu, AluSubtract, Expression.Constant((byte) 0), A));
                    break;

                case OpCode.InvertCarryFlag:
                    yield return Expression.Call(Flags, SetUndocumentedFlags, A);
                    if (_cpuMode == CpuMode.GameBoy)
                    {
                        yield return Expression.Assign(HalfCarry, Expression.Constant(false));
                    }
                    else
                    {
                        yield return Expression.Assign(HalfCarry, Carry);
                    }

                    yield return Expression.Assign(Subtract, Expression.Constant(false));
                    yield return Expression.Assign(Carry, Expression.Not(Carry));
                    break;

                case OpCode.SetCarryFlag:
                    yield return Expression.Call(Flags, SetUndocumentedFlags, A);
                    yield return Expression.Assign(HalfCarry, Expression.Constant(false));
                    yield return Expression.Assign(Subtract, Expression.Constant(false));
                    yield return Expression.Assign(Carry, Expression.Constant(true));
                    break;

                case OpCode.DisableInterrupts:
                    yield return Expression.Assign(IFF1, Expression.Constant(false));
                    yield return Expression.Assign(IFF2, Expression.Constant(false));
                    break;

                case OpCode.EnableInterrupts:
                    yield return Expression.Assign(IFF1, Expression.Constant(true));
                    yield return Expression.Assign(IFF2, Expression.Constant(true));
                    break;

                case OpCode.InterruptMode0:
                    yield return Expression.Assign(IM, Expression.Constant(InterruptMode.InterruptMode0));
                    break;

                case OpCode.InterruptMode1:
                    yield return Expression.Assign(IM, Expression.Constant(InterruptMode.InterruptMode1));
                    break;

                case OpCode.InterruptMode2:
                    yield return Expression.Assign(IM, Expression.Constant(InterruptMode.InterruptMode2));
                    break;

                case OpCode.Swap:
                    yield return WriteOperand1(operation, Expression.Call(Alu, AluSwap, ReadOperand1(operation)));
                    break;

                case OpCode.LoadIncrement:
                    yield return WriteOperand1(operation, ReadOperand2(operation));
                    yield return Expression.PreIncrementAssign(HL); // No support for indexes but GB doesnt have them
                    break;

                case OpCode.LoadDecrement:
                    yield return WriteOperand1(operation, ReadOperand2(operation));
                    yield return Expression.PreDecrementAssign(HL); // No support for indexes but GB doesnt have them
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (operation.OpCodeMeta == OpCodeMeta.AutoCopy)
            {
                // Autocopy for DD/FD prefix
                yield return WriteOperand2(operation, ReadOperand1(operation));
            }
        }
    }
}