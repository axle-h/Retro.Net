namespace Retro.Net.Z80.Core.Interfaces
{
    /// <summary>
    /// An 8-bit arithmetic logic unit for the 8080/GBCPU/Z80 CPU's.
    /// This abstraction is useful as it assumes all responsibility of setting and checking the flags register on each operation.
    /// </summary>
    public interface IAlu
    {
        /// <summary>
        /// Increments the specified byte.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        byte Increment(byte b);

        /// <summary>
        /// Decrements the specified byte.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        byte Decrement(byte b);

        /// <summary>
        /// Adds the byte values.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        byte Add(byte a, byte b);

        /// <summary>
        /// Adds the byte values, checking the carry flag and applying if necessary.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        byte AddWithCarry(byte a, byte b);

        /// <summary>
        /// Adds the word values.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        ushort Add(ushort a, ushort b);

        /// <summary>
        /// Adds the word values, checking the carry flag and applying if necessary.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        ushort AddWithCarry(ushort a, ushort b);

        /// <summary>
        /// Subtracts the byte value of b from a.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        byte Subtract(byte a, byte b);

        /// <summary>
        /// Subtracts the byte value of b from a, checking the carry flag and applying if necessary.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        byte SubtractWithCarry(byte a, byte b);

        /// <summary>
        /// Subtracts the word value of b from a, checking the carry flag and applying if necessary.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        ushort SubtractWithCarry(ushort a, ushort b);

        /// <summary>
        /// Compares the byte values, setting appropriate flag values.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        void Compare(byte a, byte b);

        /// <summary>
        /// Computes the logical AND of the specified bytes.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        byte And(byte a, byte b);

        /// <summary>
        /// Computes the logical OR of the specified bytes.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        byte Or(byte a, byte b);

        /// <summary>
        /// Computes the logical XOR of the specified bytes.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        byte Xor(byte a, byte b);

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
        byte DecimalAdjust(byte a, bool setHalfCarry);

        /// <summary>
        /// The value a is rotated left 1 bit position.
        /// The sign bit (bit 7) is copied to the Carry flag and also to bit 0. Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        byte RotateLeftWithCarry(byte a);

        /// <summary>
        /// The value a is rotated left 1 bit position through the Carry flag.
        /// The previous contents of the Carry flag are copied to bit 0. Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        byte RotateLeft(byte a);

        /// <summary>
        /// The value a is rotated right 1 bit position.
        /// Bit 0 is copied to the Carry flag and also to bit 7. Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        byte RotateRightWithCarry(byte a);

        /// <summary>
        /// The value a is rotated right 1 bit position through the Carry flag.
        /// The previous contents of the Carry flag are copied to bit 7. Bit 0 is the leastsignificant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        byte RotateRight(byte a);

        /// <summary>
        /// An arithmetic shift left 1 bit position is performed on the contents of operand m.
        /// The contents of bit 7 are copied to the Carry flag.
        /// Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        byte ShiftLeft(byte a);

        /// <summary>
        /// Undocumented Z80 instruction known as SLS, SLL or SL1.
        /// An arithmetic shift left 1 bit position is performed on the contents of operand m.
        /// The contents of bit 7 are copied to the Carry flag, and bit 0 is set.
        /// Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        byte ShiftLeftSet(byte a);

        /// <summary>
        /// An arithmetic shift right 1 bit position is performed on the contents of operand m.
        /// The contents of bit 0 are copied to the Carry flag and the previous contents of bit 7 remain unchanged.
        /// Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        byte ShiftRight(byte a);

        /// <summary>
        /// The contents of operand m are shifted right 1 bit position.
        /// The contents of bit 0 are copied to the Carry flag, and bit 7 is reset.
        /// Bit 0 is the least-significant bit.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        byte ShiftRightLogical(byte a);

        /// <summary>
        /// Performs a 4-bit clockwise (right) rotation of the 12-bit number whose 4 most signigifcant bits are the 4 least
        /// significant bits of accumulator, and its 8 least significant bits are b.
        /// </summary>
        /// <param name="accumulator"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        AccumulatorAndResult RotateLeftDigit(byte accumulator, byte b);

        /// <summary>
        /// Performs a 4-bit anti-clockwise (left) rotation of the 12-bit number whose 4 most signigifcant bits are the 4 least
        /// significant bits of accumulator, and its 8 least significant bits are b.
        /// </summary>
        /// <param name="accumulator"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        AccumulatorAndResult RotateRightDigit(byte accumulator, byte b);

        /// <summary>
        /// Tests bit <see cref="bit"/> in byte <see cref="a"/> and sets the Z flag accordingly.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="bit"></param>
        void BitTest(byte a, int bit);

        /// <summary>
        /// Sets bit <see cref="bit"/> in byte <see cref="a"/> and sets the Z flag accordingly.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="bit"></param>
        byte BitSet(byte a, int bit);

        /// <summary>
        /// Resets bit <see cref="bit"/> in byte <see cref="a"/> and sets the Z flag accordingly.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="bit">The bit.</param>
        /// <returns></returns>
        byte BitReset(byte a, int bit);

        /// <summary>
        /// Add signed displacement to 16-bit register.
        /// Specific to GB ALU.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        ushort AddDisplacement(ushort a, sbyte d);

        /// <summary>
        /// Swap lower and upper nibbles.
        /// Specific to GB ALU.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        byte Swap(byte a);
    }
}