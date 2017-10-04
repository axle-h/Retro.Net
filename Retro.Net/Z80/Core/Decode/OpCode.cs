using System;

namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// A Z80/8080 op-code.
    /// </summary>
    public enum OpCode : byte
    {
        NoOperation,
        Halt,

        // 8-Bit Load
        Load,

        // 16-Bit Load
        Load16,
        Push,
        Pop,

        // 8-Bit arithmetic
        Add,
        AddWithCarry,
        Subtract,
        SubtractWithCarry,
        And,
        Or,
        Xor,
        Compare,
        Increment,
        Decrement,

        // 16-Bit arithmetic
        Add16,
        AddCarry16,
        SubtractCarry16,
        Increment16,
        Decrement16,

        // Exchange
        Exchange,
        ExchangeAccumulatorAndFlags,
        ExchangeGeneralPurpose,

        // Jump
        Jump,
        JumpRelative,
        DecrementJumpRelativeIfNonZero,

        // Call/Return/Reset
        Call,
        Return,
        ReturnFromInterrupt,
        ReturnFromNonmaskableInterrupt,
        Reset,

        // IO
        Input,
        Output,

        // Rotate
        RotateLeftWithCarry,
        RotateLeft,
        RotateRightWithCarry,
        RotateRight,
        RotateLeftDigit,
        RotateRightDigit,

        // Shift
        ShiftLeft,
        ShiftLeftSet,
        ShiftRight,
        ShiftRightLogical,

        // Bit test, set & reset
        BitTest,
        BitSet,
        BitReset,

        // Block search & transfer
        TransferIncrement,
        TransferIncrementRepeat,
        TransferDecrement,
        TransferDecrementRepeat,
        SearchIncrement,
        SearchIncrementRepeat,
        SearchDecrement,
        SearchDecrementRepeat,
        InputTransferIncrement,
        InputTransferIncrementRepeat,
        InputTransferDecrement,
        InputTransferDecrementRepeat,
        OutputTransferIncrement,
        OutputTransferIncrementRepeat,
        OutputTransferDecrement,
        OutputTransferDecrementRepeat,

        // Gernal purpose arithmetic
        DecimalArithmeticAdjust,
        NegateOnesComplement,
        NegateTwosComplement,
        InvertCarryFlag,
        SetCarryFlag,

        DisableInterrupts,
        EnableInterrupts,

        InterruptMode0,
        InterruptMode1,
        InterruptMode2,

        // GB Specific
        Swap,
        LoadIncrement,
        LoadDecrement,
        Stop
    }

    /// <summary>
    /// Extension methods for <see cref="OpCode"/>.
    /// </summary>
    internal static class OpcodeExtensions
    {
        /// <summary>
        /// Checks whether the specified op-code has 16-bit operands.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static bool Is16Bit(this OpCode code)
        {
            switch (code)
            {
                case OpCode.Load16:
                case OpCode.Add16:
                case OpCode.AddCarry16:
                case OpCode.SubtractCarry16:
                case OpCode.Increment16:
                case OpCode.Decrement16:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the op-code mnemonic in Z80 assembler.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">null</exception>
        public static string GetMnemonic(this OpCode code)
        {
            switch (code)
            {
                case OpCode.NoOperation:
                    return "NOP";
                case OpCode.Halt:
                    return "HALT";
                case OpCode.Load:
                    return "LD";
                case OpCode.Load16:
                    return "LD";
                case OpCode.Push:
                    return "PUSH";
                case OpCode.Pop:
                    return "POP";
                case OpCode.Add:
                    return "ADD";
                case OpCode.AddWithCarry:
                    return "ADC";
                case OpCode.Subtract:
                    return "SUB";
                case OpCode.SubtractWithCarry:
                    return "SBC";
                case OpCode.And:
                    return "AND";
                case OpCode.Or:
                    return "OR";
                case OpCode.Xor:
                    return "XOR";
                case OpCode.Compare:
                    return "CP";
                case OpCode.Increment:
                    return "INC";
                case OpCode.Decrement:
                    return "DEC";
                case OpCode.Add16:
                    return "ADD";
                case OpCode.AddCarry16:
                    return "ADC";
                case OpCode.SubtractCarry16:
                    return "SBC";
                case OpCode.Increment16:
                    return "INC";
                case OpCode.Decrement16:
                    return "DEC";
                case OpCode.Exchange:
                    return "EX";
                case OpCode.ExchangeAccumulatorAndFlags:
                    return "EX AF, AF'";
                case OpCode.ExchangeGeneralPurpose:
                    return "EXX";
                case OpCode.Jump:
                    return "JP";
                case OpCode.JumpRelative:
                    return "JR";
                case OpCode.DecrementJumpRelativeIfNonZero:
                    return "DJNZ";
                case OpCode.Call:
                    return "CALL";
                case OpCode.ReturnFromInterrupt:
                    return "RETI";
                case OpCode.ReturnFromNonmaskableInterrupt:
                    return "RETN";
                case OpCode.Return:
                    return "RET";
                case OpCode.Reset:
                    return "RST";
                case OpCode.Input:
                    return "IN";
                case OpCode.Output:
                    return "OUT";
                case OpCode.RotateLeftWithCarry:
                    return "RLC";
                case OpCode.RotateLeft:
                    return "RL";
                case OpCode.RotateRightWithCarry:
                    return "RRC";
                case OpCode.RotateRight:
                    return "RR";
                case OpCode.RotateLeftDigit:
                    return "RLD";
                case OpCode.RotateRightDigit:
                    return "RRD";
                case OpCode.ShiftLeft:
                    return "SLA";
                case OpCode.ShiftLeftSet:
                    return "SLS";
                case OpCode.ShiftRightLogical:
                    return "SRL";
                case OpCode.ShiftRight:
                    return "SRA";
                case OpCode.BitTest:
                    return "BIT";
                case OpCode.BitSet:
                    return "SET";
                case OpCode.BitReset:
                    return "RES";
                case OpCode.TransferIncrement:
                    return "LDI";
                case OpCode.TransferIncrementRepeat:
                    return "LDIR";
                case OpCode.TransferDecrement:
                    return "LDD";
                case OpCode.TransferDecrementRepeat:
                    return "LDDR";
                case OpCode.SearchIncrement:
                    return "CPI";
                case OpCode.SearchIncrementRepeat:
                    return "CPIR";
                case OpCode.SearchDecrement:
                    return "CPD";
                case OpCode.SearchDecrementRepeat:
                    return "CPDR";
                case OpCode.InputTransferIncrement:
                    return "INI";
                case OpCode.InputTransferIncrementRepeat:
                    return "INIR";
                case OpCode.InputTransferDecrement:
                    return "IND";
                case OpCode.InputTransferDecrementRepeat:
                    return "INDR";
                case OpCode.OutputTransferIncrement:
                    return "OUTI";
                case OpCode.OutputTransferIncrementRepeat:
                    return "OUTIR";
                case OpCode.OutputTransferDecrement:
                    return "OUTD";
                case OpCode.OutputTransferDecrementRepeat:
                    return "OUTDR";
                case OpCode.DecimalArithmeticAdjust:
                    return "DAA";
                case OpCode.NegateOnesComplement:
                    return "CPL";
                case OpCode.InvertCarryFlag:
                    return "CCF";
                case OpCode.SetCarryFlag:
                    return "SCF";
                case OpCode.DisableInterrupts:
                    return "DI";
                case OpCode.EnableInterrupts:
                    return "EI";
                case OpCode.NegateTwosComplement:
                    return "NEG";
                case OpCode.InterruptMode0:
                    return "IM0";
                case OpCode.InterruptMode1:
                    return "IM1";
                case OpCode.InterruptMode2:
                    return "IM2";
                case OpCode.Swap:
                    return "SWAP";
                case OpCode.LoadIncrement:
                    return "LDI";
                case OpCode.LoadDecrement:
                    return "LDD";
                case OpCode.Stop:
                    return "STOP";
                default:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
            }
        }
    }
}