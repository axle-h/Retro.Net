using System;
using System.Text;

namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// A Z80/8080 operation - an op-code with optional operands, flag tests, literals, displacement and meta describing which of the former are actually used.
    /// </summary>
    public struct Operation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operation"/> struct.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="opCode">The op code.</param>
        /// <param name="operand1">The operand1.</param>
        /// <param name="operand2">The operand2.</param>
        /// <param name="flagTest">The flag test.</param>
        /// <param name="opCodeMeta">The op code meta.</param>
        /// <param name="byteLiteral">The byte literal.</param>
        /// <param name="wordLiteral">The word literal.</param>
        /// <param name="displacement">The displacement.</param>
        public Operation(ushort address,
            OpCode opCode,
            Operand operand1,
            Operand operand2,
            FlagTest flagTest,
            OpCodeMeta opCodeMeta,
            byte byteLiteral,
            ushort wordLiteral,
            sbyte displacement) : this()
        {
            Address = address;
            OpCode = opCode;
            Operand1 = operand1;
            Operand2 = operand2;
            FlagTest = flagTest;
            OpCodeMeta = opCodeMeta;
            ByteLiteral = byteLiteral;
            WordLiteral = wordLiteral;
            Displacement = displacement;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address { get; }

        /// <summary>
        /// Gets the op code.
        /// </summary>
        /// <value>
        /// The op code.
        /// </value>
        public OpCode OpCode { get; }

        /// <summary>
        /// Gets the first operand.
        /// </summary>
        /// <value>
        /// The first operand.
        /// </value>
        public Operand Operand1 { get; }

        /// <summary>
        /// Gets the second operand.
        /// </summary>
        /// <value>
        /// The second operand.
        /// </value>
        public Operand Operand2 { get; }

        /// <summary>
        /// Gets the flag test performed by this operation.
        /// </summary>
        /// <value>
        /// The flag test performed by this operation.
        /// </value>
        public FlagTest FlagTest { get; }

        /// <summary>
        /// Gets the op code meta.
        /// </summary>
        /// <value>
        /// The op code meta.
        /// </value>
        public OpCodeMeta OpCodeMeta { get; }

        /// <summary>
        /// Gets the byte literal.
        /// </summary>
        /// <value>
        /// The byte literal.
        /// </value>
        public byte ByteLiteral { get; }

        /// <summary>
        /// Gets the word literal.
        /// </summary>
        /// <value>
        /// The word literal.
        /// </value>
        public ushort WordLiteral { get; }

        /// <summary>
        /// Gets the displacement.
        /// </summary>
        /// <value>
        /// The displacement.
        /// </value>
        public sbyte Displacement { get; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this operation.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this operation.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder($"0x{Address:x4}   {OpCode.GetMnemonic()}");
            if (FlagTest != FlagTest.None)
            {
                sb.Append($" {GetFlagTestString(FlagTest)}");
            }

            if (Operand1 != Operand.None)
            {
                sb.Append($" {GetOperandString(Operand1)}");
            }

            if (Operand2 != Operand.None)
            {
                sb.Append($", {GetOperandString(Operand2)}");
            }

            if (OpCode == OpCode.JumpRelative || OpCode == OpCode.DecrementJumpRelativeIfNonZero)
            {
                sb.Append($" {Displacement}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the flag test string.
        /// </summary>
        /// <param name="flagTest">The flag test.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">null</exception>
        private static string GetFlagTestString(FlagTest flagTest)
        {
            switch (flagTest)
            {
                case FlagTest.NotZero:
                    return "NZ";
                case FlagTest.Zero:
                    return "Z";
                case FlagTest.NotCarry:
                    return "NC";
                case FlagTest.Carry:
                    return "C";
                case FlagTest.ParityOdd:
                    return "PO";
                case FlagTest.ParityEven:
                    return "PE";
                case FlagTest.Positive:
                    return "P";
                case FlagTest.Negative:
                    return "M";
                default:
                    throw new ArgumentOutOfRangeException(nameof(flagTest), flagTest, null);
            }
        }

        /// <summary>
        /// Gets the operand string.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">null</exception>
        private string GetOperandString(Operand operand)
        {
            switch (operand)
            {
                case Operand.A:
                case Operand.B:
                case Operand.C:
                case Operand.D:
                case Operand.E:
                case Operand.F:
                case Operand.H:
                case Operand.L:
                case Operand.HL:
                case Operand.BC:
                case Operand.DE:
                case Operand.AF:
                case Operand.SP:
                case Operand.IX:
                case Operand.IXl:
                case Operand.IXh:
                case Operand.IY:
                case Operand.IYl:
                case Operand.IYh:
                case Operand.I:
                case Operand.R:
                    return operand.ToString();
                case Operand.mHL:
                    return "(HL)";
                case Operand.mBC:
                    return "(BC)";
                case Operand.mDE:
                    return "(DE)";
                case Operand.mSP:
                    return "(SP)";
                case Operand.mnn:
                    return $"(0x{WordLiteral:x4})";
                case Operand.nn:
                    return $"0x{WordLiteral:x4}";
                case Operand.n:
                    return $"0x{ByteLiteral:x2}";
                case Operand.mIXd:
                    return Displacement > 0 ? $"(IX+{Displacement})" : $"(IX{Displacement})";
                case Operand.mIYd:
                    return Displacement > 0 ? $"(IY+{Displacement})" : $"(IY{Displacement})";
                case Operand.mCl:
                    return "(0xff00 + C)";
                case Operand.mnl:
                    return $"(0xff00 + 0x{ByteLiteral:x2})";
                case Operand.SPd:
                    var d = Displacement;
                    return d > 0 ? $"SP+{d}" : $"SP{d}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(operand), operand, null);
            }
        }
    }
}