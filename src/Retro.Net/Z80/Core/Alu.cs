using Retro.Net.Z80.Core.Interfaces;
using Retro.Net.Z80.Util;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.Core
{
    /// <summary>
    /// An 8-bit arithmetic logic unit for the 8080/GBCPU/Z80 CPU's.
    /// This abstraction is useful as it assumes all responsibility of setting and checking the flags register on each operation.
    /// </summary>
    /// <seealso cref="IAlu" />
    public class Alu : IAlu
    {
        private readonly IRegisters _registers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alu"/> class.
        /// </summary>
        /// <param name="registers">The registers.</param>
        public Alu(IRegisters registers)
        {
            _registers = registers;
        }

        /// <summary>
        /// Gets the flags.
        /// </summary>
        /// <value>
        /// The flags.
        /// </value>
        private IFlagsRegister Flags => _registers.AccumulatorAndFlagsRegisters.Flags;
        
        /// <summary>
        /// Increments the specified byte.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public byte Increment(byte b)
        {
            var result = b + 1;

            var flags = Flags;
            flags.HalfCarry = (b & 0x0f) == 0x0f;
            flags.ParityOverflow = b == 0x7f;
            flags.Subtract = false;

            b = (byte) result;
            flags.SetResultFlags(b);
            return b;
        }

        /// <summary>
        /// Decrements the specified byte.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public byte Decrement(byte b)
        {
            var result = b - 1;

            var flags = Flags;
            flags.HalfCarry = (b & 0x0f) == 0;
            flags.ParityOverflow = b == 0x80;
            flags.Subtract = true;

            b = (byte) result;
            flags.SetResultFlags(b);
            return b;
        }

        /// <summary>
        /// Adds the byte values.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public byte Add(byte a, byte b) => Add(a, b, false);

        /// <summary>
        /// Adds the byte values, checking the carry flag and applying if necessary.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public byte AddWithCarry(byte a, byte b) => Add(a, b, Flags.Carry);

        /// <summary>
        /// Adds the word values.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public ushort Add(ushort a, ushort b) => Add(a, b, false);

        /// <summary>
        /// Adds the word values, checking the carry flag and applying if necessary.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public ushort AddWithCarry(ushort a, ushort b) => Add(a, b, true);

        /// <summary>
        /// Subtracts the byte value of b from a.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public byte Subtract(byte a, byte b) => Subtract(a, b, false);

        /// <summary>
        /// Subtracts the byte value of b from a, checking the carry flag and applying if necessary.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public byte SubtractWithCarry(byte a, byte b) => Subtract(a, b, Flags.Carry);

        /// <summary>
        /// Subtracts the word value of b from a, checking the carry flag and applying if necessary.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public ushort SubtractWithCarry(ushort a, ushort b)
        {
            var flags = Flags;
            var carry = flags.Carry ? 1 : 0;
            var result = a - b - carry;

            flags.HalfCarry = (a & 0x0f00) < (b & 0x0f00) + carry;

            // Carry = result > ushort.MinValue;
            flags.Carry = result < 0;
            flags.Subtract = true;

            // Overflow = (added signs are same) && (result sign differs from the sign of either of operands)
            flags.ParityOverflow = (((a ^ ~b) & 0x8000) == 0) && (((result ^ a) & 0x8000) != 0);
            b = (ushort) result;
            flags.SetResultFlags(b);

            return b;
        }

        /// <summary>
        /// Compares the byte values, setting appropriate flag values.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        public void Compare(byte a, byte b)
        {
            Subtract(a, b, false);
            Flags.SetUndocumentedFlags(b);
        }

        /// <summary>
        /// Computes the logical AND of the specified bytes.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public byte And(byte a, byte b)
        {
            a &= b;

            var flags = Flags;
            flags.SetParityFlags(a);
            flags.HalfCarry = true;
            flags.Carry = false;
            return a;
        }

        /// <summary>
        /// Computes the logical OR of the specified bytes.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public byte Or(byte a, byte b)
        {
            a |= b;

            var flags = Flags;
            flags.SetParityFlags(a);
            flags.HalfCarry = false;
            flags.Carry = false;
            return a;
        }

        /// <summary>
        /// Computes the logical XOR of the specified bytes.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public byte Xor(byte a, byte b)
        {
            a ^= b;

            var flags = Flags;
            flags.SetParityFlags(a);
            flags.HalfCarry = false;
            flags.Carry = false;
            return a;
        }

        /// <summary>
        /// http://www.worldofspectrum.org/faq/reference/z80reference.htm#DAA
        /// - If the A register is greater than 0x99, OR the Carry flag is SET, then
        /// The upper four bits of the Correction Factor are set to 6,
        /// and the Carry flag will be SET.
        /// Else
        /// The upper four bits of the Correction Factor are set to 0,
        /// and the Carry flag will be CLEARED.
        /// - If the lower four bits of the A register (A AND 0x0F) is greater than 9,
        /// OR the Half-Carry(H) flag is SET, then
        /// The lower four bits of the Correction Factor are set to 6.
        /// Else
        /// The lower four bits of the Correction Factor are set to 0.
        /// - This results in a Correction Factor of 0x00, 0x06, 0x60 or 0x66.
        /// - If the N flag is CLEAR, then
        /// ADD the Correction Factor to the A register.
        /// Else
        /// SUBTRACT the Correction Factor from the A register.
        /// - The Flags are set as follows:
        /// Carry:      Set/clear as in the first step above.
        /// Half-Carry: Set if the correction operation caused a binary carry/borrow
        /// from bit 3 to bit 4.
        /// For this purpose, may be calculated as:
        /// Bit 4 of: A(before) XOR A(after).
        /// S,Z,P,5,3:  Set as for simple logic operations on the resultant A value.
        /// N:          Leave.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="setHalfCarry"></param>
        /// <returns></returns>
        public byte DecimalAdjust(byte a, bool setHalfCarry)
        {
            var flags = Flags;
            int result = a;

            if (!flags.Subtract)
            {
                if (flags.HalfCarry || ((result & 0xF) > 9))
                {
                    result += 0x06;
                }

                if (flags.Carry || (result > 0x9F))
                {
                    result += 0x60;
                }
            }
            else
            {
                if (flags.HalfCarry)
                {
                    result = (result - 6) & 0xFF;
                }

                if (flags.Carry)
                {
                    result -= 0x60;
                }
            }

            if ((result & 0x100) == 0x100)
            {
                flags.Carry = true;
            }

            a = (byte) (result & 0xFF);
            flags.HalfCarry = setHalfCarry && (((flags.HalfCarry ? 1 : 0) ^ (a >> 4)) & 1) > 0;
            flags.ParityOverflow = a.IsEvenParity();
            flags.SetResultFlags(a);

            return a;
        }

        /// <summary>
        /// The value a is rotated left 1 bit position.
        /// The sign bit (bit 7) is copied to the Carry flag and also to bit 0. Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public byte RotateLeftWithCarry(byte a)
        {
            var carry = (a & 0x80) > 0;
            var result = (byte) ((a << 1) | (carry ? 1 : 0));

            var flags = Flags;
            flags.Carry = carry;
            flags.HalfCarry = false;
            flags.Subtract = false;
            flags.SetResultFlags(result);

            return result;
        }

        /// <summary>
        /// The value a is rotated left 1 bit position through the Carry flag.
        /// The previous contents of the Carry flag are copied to bit 0. Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        public byte RotateLeft(byte a)
        {
            var flags = Flags;
            var result = (byte) ((a << 1) | (flags.Carry ? 1 : 0));

            flags.Carry = (a & 0x80) > 0;
            flags.HalfCarry = false;
            flags.Subtract = false;
            flags.SetResultFlags(result);

            return result;
        }

        /// <summary>
        /// The value a is rotated right 1 bit position.
        /// Bit 0 is copied to the Carry flag and also to bit 7. Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public byte RotateRightWithCarry(byte a)
        {
            var carry = (a & 1) > 0;
            var result = (byte) ((a >> 1) | (carry ? 0x80 : 0));

            var flags = Flags;
            flags.Carry = carry;
            flags.HalfCarry = false;
            flags.Subtract = false;
            flags.SetResultFlags(result);

            return result;
        }

        /// <summary>
        /// The value a is rotated right 1 bit position through the Carry flag.
        /// The previous contents of the Carry flag are copied to bit 7. Bit 0 is the leastsignificant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public byte RotateRight(byte a)
        {
            var flags = Flags;
            var result = (byte) ((a >> 1) | (flags.Carry ? 0x80 : 0));

            flags.Carry = (a & 1) > 0;
            flags.HalfCarry = false;
            flags.Subtract = false;
            flags.SetResultFlags(result);

            return result;
        }

        /// <summary>
        /// An arithmetic shift left 1 bit position is performed on the contents of operand m.
        /// The contents of bit 7 are copied to the Carry flag.
        /// Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public byte ShiftLeft(byte a)
        {
            var flags = Flags;
            flags.Carry = (a & 0x80) > 0;
            var result = (byte) (a << 1);

            flags.SetParityFlags(result);
            flags.HalfCarry = false;
            return result;
        }

        /// <summary>
        /// Undocumented Z80 instruction known as SLS, SLL or SL1.
        /// An arithmetic shift left 1 bit position is performed on the contents of operand m.
        /// The contents of bit 7 are copied to the Carry flag, and bit 0 is set.
        /// Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public byte ShiftLeftSet(byte a)
        {
            var flags = Flags;
            flags.Carry = (a & 0x80) > 0;
            var result = (byte) ((a << 1) | 0x01);

            flags.SetParityFlags(result);
            flags.HalfCarry = false;
            return result;
        }

        /// <summary>
        /// An arithmetic shift right 1 bit position is performed on the contents of operand m.
        /// The contents of bit 0 are copied to the Carry flag and the previous contents of bit 7 remain unchanged.
        /// Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public byte ShiftRight(byte a)
        {
            var flags = Flags;
            flags.Carry = (a & 0x01) > 0;
            var result = (byte) ((a >> 1) | (a & 0x80));

            flags.SetParityFlags(result);
            flags.HalfCarry = false;
            return result;
        }

        /// <summary>
        /// The contents of operand m are shifted right 1 bit position.
        /// The contents of bit 0 are copied to the Carry flag, and bit 7 is reset.
        /// Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public byte ShiftRightLogical(byte a)
        {
            var flags = Flags;
            flags.Carry = (a & 0x01) > 0;
            var result = (byte) (a >> 1);

            flags.SetParityFlags(result);
            flags.HalfCarry = false;
            return result;
        }

        /// <summary>
        /// Performs a 4-bit clockwise (right) rotation of the 12-bit number whose 4 most signigifcant bits are the 4 least
        /// significant bits of accumulator, and its 8 least significant bits are b.
        /// </summary>
        /// <param name="accumulator"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public AccumulatorAndResult RotateLeftDigit(byte accumulator, byte b)
        {
            var result = new AccumulatorAndResult((byte) ((accumulator & 0xf0) | ((b & 0xf0) >> 4)),
                                                  (byte) (((b & 0x0f) << 4) | (accumulator & 0x0f)));

            var flags = Flags;
            flags.SetParityFlags(result.Accumulator);
            flags.HalfCarry = false;
            return result;
        }

        /// <summary>
        /// Performs a 4-bit anti-clockwise (left) rotation of the 12-bit number whose 4 most signigifcant bits are the 4 least
        /// significant bits of accumulator, and its 8 least significant bits are b.
        /// </summary>
        /// <param name="accumulator"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public AccumulatorAndResult RotateRightDigit(byte accumulator, byte b)
        {
            var result = new AccumulatorAndResult((byte) ((accumulator & 0xf0) | (b & 0x0f)),
                                                  (byte) (((accumulator & 0x0f) << 4) | ((b & 0xf0) >> 4)));

            var flags = Flags;
            flags.SetParityFlags(result.Accumulator);
            flags.HalfCarry = false;
            return result;
        }

        /// <summary>
        /// Tests bit 'bit' in byte a and sets the Z flag accordingly.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="bit"></param>
        public void BitTest(byte a, int bit)
        {
            var flags = Flags;
            flags.Zero = ((0x1 << bit) & a) == 0;
            flags.HalfCarry = true;
            flags.Subtract = false;
            flags.SetUndocumentedFlags(a);

            // PV as Z, S set only if n=7 and b7 of r set
            flags.ParityOverflow = flags.Zero;
            flags.Sign = bit == 7 && flags.Zero;
        }

        /// <summary>
        /// Sets bit <see cref="!:bit" /> in byte <see cref="!:a" /> and sets the Z flag accordingly.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public byte BitSet(byte a, int bit) => (byte) (a | (0x1 << bit));

        /// <summary>
        /// Resets bit <see cref="!:bit" /> in byte <see cref="!:a" /> and sets the Z flag accordingly.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="bit">The bit.</param>
        /// <returns></returns>
        public byte BitReset(byte a, int bit) => (byte) (a & ~(0x1 << bit));

        /// <summary>
        /// Add signed displacement to 16-bit register.
        /// Specific to GB ALU.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public ushort AddDisplacement(ushort a, sbyte d)
        {
            var result = a + d;

            var flags = Flags;

            var carry = a ^ d ^ result;
            flags.Carry = (carry & 0x100) > 0;
            flags.HalfCarry = ((carry << 5) & 0x200) > 0;
            flags.Zero = false;
            flags.Subtract = false;

            return (ushort) result;
        }

        /// <summary>
        /// Swap lower and upper nibbles.
        /// Specific to GB ALU.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public byte Swap(byte a)
        {
            var result = (byte) (((a & 0xf) << 4) | ((a & 0xf0) >> 4));

            var flags = Flags;
            flags.Zero = result == 0;
            flags.Subtract = false;
            flags.HalfCarry = false;
            flags.Carry = false;
            return result;
        }

        /// <summary>
        /// Adds the specified a.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="addCarry">if set to <c>true</c> [add carry].</param>
        /// <returns></returns>
        private byte Add(byte a, byte b, bool addCarry)
        {
            var carry = addCarry ? 1 : 0;
            var result = a + b + carry;

            var flags = Flags;
            flags.HalfCarry = (((a & 0x0f) + (b & 0x0f) + (carry & 0x0f)) & 0xf0) > 0;

            // Overflow = (added signs are same) && (result sign differs from the sign of either of operands)
            flags.ParityOverflow = ((a ^ b) & 0x80) == 0 && ((result ^ a) & 0x80) != 0;

            // Carry = result > byte.MaxValue;
            flags.Carry = (result & 0x100) == 0x100;

            flags.Subtract = false;

            b = (byte) result;
            flags.SetResultFlags(b);
            return b;
        }

        /// <summary>
        /// Adds the specified a.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="addCarry">if set to <c>true</c> [add carry].</param>
        /// <returns></returns>
        private ushort Add(ushort a, ushort b, bool addCarry)
        {
            var flags = Flags;
            var carry = addCarry && flags.Carry ? 1 : 0;
            var result = a + b + carry;

            // Half carry is carry from bit 11
            flags.HalfCarry = (((a & 0x0fff) + (b & 0x0fff) + carry) & 0xf000) > 0;

            flags.Carry = (result & 0x10000) == 0x10000;

            flags.Subtract = false;

            if (addCarry)
            {
                // Overflow = (added signs are same) && (result sign differs from the sign of either of operands)
                flags.ParityOverflow = (((a ^ b) & 0x8000) == 0) && (((result ^ a) & 0x8000) != 0);
                b = (ushort) result;
                flags.SetResultFlags(b);
            }
            else
            {
                // S & Z are unaffected so we're only setting the undocumented flags from the last 8-bit addition
                var b0 = (result & 0xff00) >> 8;
                flags.SetUndocumentedFlags((byte) b0);
                b = (ushort) result;
            }

            return b;
        }

        /// <summary>
        /// Subtracts the specified a.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="addCarry">if set to <c>true</c> [add carry].</param>
        /// <returns></returns>
        private byte Subtract(byte a, byte b, bool addCarry)
        {
            var carry = addCarry ? 1 : 0;
            var result = a - b - carry;

            var flags = Flags;

            flags.HalfCarry = (a & 0x0f) < (b & 0x0f) + carry;

            // Overflow = (added signs are same) && (result sign differs from the sign of either of operands)
            flags.ParityOverflow = (((a ^ ~b) & 0x80) == 0) && (((result ^ a) & 0x80) != 0);

            // Carry = result > byte.MinValue;
            flags.Carry = result < 0;
            flags.Subtract = true;

            b = (byte) result;
            flags.SetResultFlags(b);
            return b;
        }
    }
}