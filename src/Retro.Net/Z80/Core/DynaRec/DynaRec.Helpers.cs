using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Z80.Core.DynaRec
{
    /// <summary>
    /// Helpers for the dynamic translation from Z80 to expression trees.
    /// </summary>
    /// <seealso cref="IInstructionBlockFactory" />
    public partial class DynaRec
    {
        /// <summary>
        /// Reads the first operand from the operation as an expression
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="is16Bit">if set to <c>true</c> [the operation has 16 bit operands].</param>
        /// <returns></returns>
        private Expression ReadOperand1(Operation operation, bool is16Bit = false) => ReadOperand(operation, operation.Operand1, is16Bit);

        /// <summary>
        /// Reads the second operand from the operation as an expression.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="is16Bit">if set to <c>true</c> [the operation has 16 bit operands].</param>
        /// <returns></returns>
        private Expression ReadOperand2(Operation operation, bool is16Bit = false) => ReadOperand(operation, operation.Operand2, is16Bit);

        /// <summary>
        /// Reads the specified operand according to the specified operation as an expression.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="operand">The operand.</param>
        /// <param name="is16Bit">if set to <c>true</c> [the operation has 16 bit operands].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">null</exception>
        private Expression ReadOperand(Operation operation, Operand operand, bool is16Bit)
        {
            Expression MmuRead(Expression arg) => Expression.Call(Mmu, is16Bit ? MmuReadWord : MmuReadByte, arg);

            switch (operand)
            {
                case Operand.A:
                    return A;
                case Operand.B:
                    return B;
                case Operand.C:
                    return C;
                case Operand.D:
                    return D;
                case Operand.E:
                    return E;
                case Operand.F:
                    return F;
                case Operand.H:
                    return H;
                case Operand.L:
                    return L;
                case Operand.HL:
                    return HL;
                case Operand.BC:
                    return BC;
                case Operand.DE:
                    return DE;
                case Operand.AF:
                    return AF;
                case Operand.SP:
                    return SP;
                case Operand.mHL:
                    return MmuRead(HL);
                case Operand.mBC:
                    return MmuRead(BC);
                case Operand.mDE:
                    return MmuRead(DE);
                case Operand.mSP:
                    return MmuRead(SP);
                case Operand.mnn:
                    return MmuRead(Expression.Constant(operation.WordLiteral));
                case Operand.nn:
                    return Expression.Constant(operation.WordLiteral);
                case Operand.n:
                    return Expression.Constant(operation.ByteLiteral);
                case Operand.IX:
                    return IX;
                case Operand.mIXd:
                    return MmuRead(Expression.Convert(Expression.Add(Expression.Convert(IX, typeof (int)), Expression.Constant((int) operation.Displacement)), typeof (ushort)));
                case Operand.IXl:
                    return IXl;
                case Operand.IXh:
                    return IXh;
                case Operand.IY:
                    return IY;
                case Operand.mIYd:
                    return MmuRead(Expression.Convert(Expression.Add(Expression.Convert(IY, typeof (int)), Expression.Constant((int) operation.Displacement)), typeof (ushort)));
                case Operand.IYl:
                    return IYl;
                case Operand.IYh:
                    return IYh;
                case Operand.I:
                    return I;
                case Operand.R:
                    return R;
                case Operand.mCl:
                    return MmuRead(Expression.Add(Expression.Convert(C, typeof (ushort)), Expression.Constant((ushort) 0xff00)));
                case Operand.mnl:
                    return MmuRead(Expression.Constant((ushort) (operation.ByteLiteral + 0xff00)));
                case Operand.SPd:
                    return Expression.Call(Alu, AluAddDisplacement, SP, Expression.Constant(operation.Displacement));

                default:
                    throw new ArgumentOutOfRangeException(nameof(operation.Operand2), operation.Operand2, null);
            }
        }

        /// <summary>
        /// Gets an expression that writes the specified value to the first operand from the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="value">The value.</param>
        /// <param name="is16Bit">if set to <c>true</c> [the operation has 16 bit operands].</param>
        /// <returns></returns>
        private Expression WriteOperand1(Operation operation, Expression value, bool is16Bit = false)
            => WriteOperand(operation, value, operation.Operand1, is16Bit);

        /// <summary>
        /// Gets an expression that writes the specified value to the second operand from the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="value">The value.</param>
        /// <param name="is16Bit">if set to <c>true</c> [the operation has 16 bit operands].</param>
        /// <returns></returns>
        private Expression WriteOperand2(Operation operation, Expression value, bool is16Bit = false)
            => WriteOperand(operation, value, operation.Operand2, is16Bit);

        /// <summary>
        /// Gets an expression that writes the specified value to the specified operand according to the specified operation. 
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="value">The value.</param>
        /// <param name="operand">The operand.</param>
        /// <param name="is16Bit">if set to <c>true</c> [is16 bit].</param>
        /// <returns></returns>
        private Expression WriteOperand(Operation operation, Expression value, Operand operand, bool is16Bit)
        {
            Expression MmuWrite(Expression address) => Expression.Call(Mmu, is16Bit ? MmuWriteWord : MmuWriteByte, address, value);

            switch (operand)
            {
                case Operand.A:
                    return Expression.Assign(A, value);
                case Operand.B:
                    return Expression.Assign(B, value);
                case Operand.C:
                    return Expression.Assign(C, value);
                case Operand.D:
                    return Expression.Assign(D, value);
                case Operand.E:
                    return Expression.Assign(E, value);
                case Operand.F:
                    return Expression.Assign(F, value);
                case Operand.H:
                    return Expression.Assign(H, value);
                case Operand.L:
                    return Expression.Assign(L, value);
                case Operand.HL:
                    return Expression.Assign(HL, value);
                case Operand.BC:
                    return Expression.Assign(BC, value);
                case Operand.DE:
                    return Expression.Assign(DE, value);
                case Operand.SP:
                    return Expression.Assign(SP, value);
                case Operand.AF:
                    return Expression.Assign(AF, value);
                case Operand.mHL:
                    return MmuWrite(HL);
                case Operand.mBC:
                    return MmuWrite(BC);
                case Operand.mDE:
                    return MmuWrite(DE);
                case Operand.mSP:
                    return MmuWrite(SP);
                case Operand.mnn:
                    return MmuWrite(Expression.Constant(operation.WordLiteral));
                case Operand.IX:
                    return Expression.Assign(IX, value);
                case Operand.mIXd:
                    return MmuWrite(Expression.Convert(Expression.Add(Expression.Convert(IX, typeof(int)), Expression.Constant((int)operation.Displacement)), typeof(ushort)));
                case Operand.IXl:
                    return Expression.Assign(IXl, value);
                case Operand.IXh:
                    return Expression.Assign(IXh, value);
                case Operand.IY:
                    return Expression.Assign(IY, value);
                case Operand.mIYd:
                    return MmuWrite(Expression.Convert(Expression.Add(Expression.Convert(IY, typeof(int)), Expression.Constant((int)operation.Displacement)), typeof(ushort)));
                case Operand.IYl:
                    return Expression.Assign(IYl, value);
                case Operand.IYh:
                    return Expression.Assign(IYh, value);
                case Operand.I:
                    return Expression.Assign(I, value);
                case Operand.R:
                    return Expression.Assign(R, value);
                case Operand.mCl:
                    return MmuWrite(Expression.Add(Expression.Convert(C, typeof(ushort)), Expression.Constant((ushort)0xff00)));
                case Operand.mnl:
                    return MmuWrite(Expression.Constant((ushort)(operation.ByteLiteral + 0xff00)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation.Operand1), operation.Operand1, null);
            }
        }

        /// <summary>
        /// Gets the flag test expression required by the specified operation.
        /// </summary>
        /// <param name="flagTest">The flag test.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">null</exception>
        private Expression GetFlagTestExpression(FlagTest flagTest)
        {
            switch (flagTest)
            {
                case FlagTest.NotZero:
                    return Expression.Not(Zero);
                case FlagTest.Zero:
                    return Zero;
                case FlagTest.NotCarry:
                    return Expression.Not(Carry);
                case FlagTest.Carry:
                    return Carry;
                case FlagTest.ParityOdd:
                    return Expression.Not(ParityOverflow);
                case FlagTest.ParityEven:
                    return ParityOverflow;
                case FlagTest.Positive:
                    return Expression.Not(Sign);
                case FlagTest.Negative:
                    return Sign;
                default:
                    throw new ArgumentOutOfRangeException(nameof(flagTest), flagTest, null);
            }
        }

        /// <summary>
        /// Gets an expression to add the specified timings to the dynamic timer.
        /// </summary>
        /// <param name="mCycles">The machine cycles.</param>
        /// <param name="tStates">The t-states.</param>
        /// <returns></returns>
        private Expression AddDynamicTimings(int mCycles, int tStates)
            => Expression.Call(DynamicTimer, DynamicTimerAdd, Expression.Constant(mCycles), Expression.Constant(tStates));

        /// <summary>
        /// Gets an expression that updates the memory refresh register.
        /// </summary>
        /// <param name="deltaExpression">The delta expression.</param>
        /// <returns></returns>
        private Expression GetMemoryRefreshDeltaExpression(Expression deltaExpression)
        {
            var increment7LsbR = Expression.And(Expression.Add(Expression.Convert(R, typeof (int)), deltaExpression),
                                                Expression.Constant(0x7f));
            return Expression.Assign(R, Expression.Convert(increment7LsbR, typeof (byte)));
        }

        /// <summary>
        /// Gets an expression that jumps the program counter by the displacement of the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        private Expression JumpToDisplacement(Operation operation)
        {
            return Expression.Assign(PC, Expression.Convert(Expression.Add(Expression.Convert(PC, typeof(int)),
                    Expression.Convert(Expression.Constant(operation.Displacement), typeof(int))),
                typeof(ushort)));
        }

        /// <summary>
        /// Gets the expressions required to execute a block transfer operation.
        /// </summary>
        /// <param name="decrement">if set to <c>true</c> [operation has a decrement modifier].</param>
        /// <returns></returns>
        private IEnumerable<Expression> GetBlockTransferExpressions(bool decrement = false)
        {
            yield return Expression.Call(Mmu, MmuTransferByte, HL, DE);
            yield return decrement ? Expression.PreDecrementAssign(HL) : Expression.PreIncrementAssign(HL);
            yield return decrement ? Expression.PreDecrementAssign(DE) : Expression.PreIncrementAssign(DE);
            yield return Expression.PreDecrementAssign(BC);
            yield return Expression.Assign(HalfCarry, Expression.Constant(false));
            yield return Expression.Assign(ParityOverflow, Expression.NotEqual(BC, Expression.Constant((ushort) 0)));
            yield return Expression.Assign(Subtract, Expression.Constant(false));
        }

        /// <summary>
        /// Gets the expressions required to execute a block transfer repeat operation.
        /// </summary>
        /// <param name="decrement">if set to <c>true</c> [operation has a decrement modifier].</param>
        /// <returns></returns>
        private IEnumerable<Expression> GetBlockTransferRepeatExpressions(bool decrement = false)
        {
            var breakLabel = Expression.Label();
            yield return
                Expression.Loop(Expression.Block(Expression.Call(Mmu, MmuTransferByte, HL, DE),
                                                 decrement ? Expression.PreDecrementAssign(HL) : Expression.PreIncrementAssign(HL),
                                                 decrement ? Expression.PreDecrementAssign(DE) : Expression.PreIncrementAssign(DE),
                                                 Expression.PreDecrementAssign(BC),
                                                 Expression.IfThen(Expression.Equal(BC, Expression.Constant((ushort) 0)),
                                                                   Expression.Break(breakLabel)),
                                                 AddDynamicTimings(5, 21),
                                                 GetMemoryRefreshDeltaExpression(Expression.Constant(2))),
                                // This function actually decreases the PC by two for each 'loop' hence need more refresh cycles.
                                breakLabel);

            yield return Expression.Assign(HalfCarry, Expression.Constant(false));
            yield return Expression.Assign(ParityOverflow, Expression.Constant(false));
            yield return Expression.Assign(Subtract, Expression.Constant(false));
        }

        /// <summary>
        /// Gets the expressions required to execute a search operation.
        /// </summary>
        /// <param name="decrement">if set to <c>true</c> [operation has a decrement modifier].</param>
        /// <returns></returns>
        private IEnumerable<Expression> GetSearchExpressions(bool decrement = false)
        {
            yield return Expression.Call(Alu, AluCompare, A, Expression.Call(Mmu, MmuReadByte, HL));
            yield return decrement ? Expression.PreDecrementAssign(HL) : Expression.PreIncrementAssign(HL);
            yield return Expression.PreDecrementAssign(BC);
        }

        /// <summary>
        /// Gets the expression required to execute a search repeat operation.
        /// </summary>
        /// <param name="decrement">if set to <c>true</c> [operation has a decrement modifier].</param>
        /// <returns></returns>
        private Expression GetSearchRepeatExpression(bool decrement = false)
        {
            var breakLabel = Expression.Label();
            var expressions = GetSearchExpressions(decrement);
            var iterationExpressions = new[]
                                       {
                                           Expression.IfThen(Expression.OrElse(Expression.Equal(BC, Expression.Constant((ushort) 0)), Zero),
                                                             Expression.Break(breakLabel)),
                                           AddDynamicTimings(5, 21),
                                           GetMemoryRefreshDeltaExpression(Expression.Constant(2))
                                       };
            return Expression.Loop(Expression.Block(expressions.Concat(iterationExpressions).ToArray()), breakLabel);
        }

        /// <summary>
        /// Gets the expressions required to execute a input transfer operation.
        /// </summary>
        /// <param name="decrement">if set to <c>true</c> [operation has a decrement modifier].</param>
        /// <returns></returns>
        private IEnumerable<Expression> GetInputTransferExpressions(bool decrement = false)
        {
            yield return Expression.Call(Mmu, MmuWriteByte, HL, Expression.Call(IO, IoReadByte, C, B));
            yield return decrement ? Expression.PreDecrementAssign(HL) : Expression.PreIncrementAssign(HL);
            yield return Expression.Assign(B, Expression.Convert(Expression.Subtract(Expression.Convert(B, typeof(int)), Expression.Constant(1)), typeof(byte)));
            yield return Expression.Assign(Subtract, Expression.Constant(true));
            yield return Expression.Call(Flags, SetResultFlags, B);
        }

        /// <summary>
        /// Gets the expression required to execute a input transfer repeat operation.
        /// </summary>
        /// <param name="decrement">if set to <c>true</c> [operation has a decrement modifier].</param>
        /// <returns></returns>
        private Expression GetInputTransferRepeatExpression(bool decrement = false)
        {
            var breakLabel = Expression.Label();

            var expressions = GetInputTransferExpressions(decrement);
            var iterationExpressions = new[]
                                       {
                                           Expression.IfThen(Expression.Equal(B, Expression.Constant((byte) 0)),
                                                             Expression.Break(breakLabel)),
                                           AddDynamicTimings(5, 21),
                                           GetMemoryRefreshDeltaExpression(Expression.Constant(2))
                                       };

            return Expression.Loop(Expression.Block(expressions.Concat(iterationExpressions).ToArray()), breakLabel);
        }

        /// <summary>
        /// Gets the expression required to execute a output transfer operation.
        /// </summary>
        /// <param name="decrement">if set to <c>true</c> [operation has a decrement modifier].</param>
        /// <returns></returns>
        private IEnumerable<Expression> GetOutTransferExpressions(bool decrement = false)
        {
            yield return Expression.Call(IO, IoWriteByte, C, B, ReadByteAtHL);
            yield return decrement ? Expression.PreDecrementAssign(HL) : Expression.PreIncrementAssign(HL);
            yield return Expression.Assign(B, Expression.Convert(Expression.Subtract(Expression.Convert(B, typeof (int)), Expression.Constant(1)), typeof (byte)));
            yield return Expression.Assign(Subtract, Expression.Constant(true));
            yield return Expression.Call(Flags, SetResultFlags, B);
        }

        /// <summary>
        /// Gets the expression required to execute a output transfer repeat operation.
        /// </summary>
        /// <param name="decrement">if set to <c>true</c> [operation has a decrement modifier].</param>
        /// <returns></returns>
        private Expression GetOutputTransferRepeatExpression(bool decrement = false)
        {
            var breakLabel = Expression.Label();

            var expressions = GetOutTransferExpressions(decrement);
            var iterationExpressions = new[]
                                       {
                                           Expression.IfThen(Expression.Equal(B, Expression.Constant((byte) 0)),
                                                             Expression.Break(breakLabel)),
                                           AddDynamicTimings(5, 21),
                                           GetMemoryRefreshDeltaExpression(Expression.Constant(2))
                                       };

            return Expression.Loop(Expression.Block(expressions.Concat(iterationExpressions).ToArray()), breakLabel);
        }
    }
}