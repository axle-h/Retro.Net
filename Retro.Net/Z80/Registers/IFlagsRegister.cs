namespace Retro.Net.Z80.Registers
{
    /// <summary>
    /// The flags register, F.
    /// </summary>
    public interface IFlagsRegister
    {
        /// <summary>
        /// The byte value of the F register, constructed from the component flags
        /// </summary>
        byte Register { get; set; }

        /// <summary>
        /// S - Sign flag
        /// Set if the 2-complement value is negative (copy of MSB)
        /// </summary>
        bool Sign { get; set; }

        /// <summary>
        /// Z - Zero flag
        /// Set if the value is zero
        /// </summary>
        bool Zero { get; set; }

        /// <summary>
        /// F5 - undocumented flag
        /// Copy of bit 5
        /// </summary>
        bool Flag5 { get; set; }

        /// <summary>
        /// H - Half Carry
        /// Carry from bit 3 to bit 4
        /// </summary>
        bool HalfCarry { get; set; }

        /// <summary>
        /// F3 - undocumented flag
        /// Copy of bit 3
        /// </summary>
        bool Flag3 { get; set; }

        /// <summary>
        /// P/V - Parity or Overflow
        /// Parity set if even number of bits set
        /// Overflow set if the 2-complement result does not fit in the register
        /// </summary>
        bool ParityOverflow { get; set; }

        /// <summary>
        /// N - Subtract
        /// Set if the last operation was a subtraction
        /// </summary>
        bool Subtract { get; set; }

        /// <summary>
        /// C - Carry
        /// Set if the result did not fit in the register
        /// </summary>
        bool Carry { get; set; }
        
        /// <summary>
        /// et Flag3 & Flag5 according to the result.
        /// </summary>
        /// <param name="result">The result to use when setting the flags.</param>
        void SetUndocumentedFlags(byte result);

        /// <summary>
        /// Set Sign, Zero, Flag3 & Flag5 according to the 8-bit result
        /// </summary>
        /// <param name="result">The result to use when setting the flags</param>
        void SetResultFlags(byte result);

        /// <summary>
        /// Set Sign, Zero, Flag3 & Flag5 according to the 16-bit result
        /// </summary>
        /// <param name="result">The result to use when setting the flags</param>
        void SetResultFlags(ushort result);

        /// <summary>
        /// Set all flags for a parity result
        /// </summary>
        /// <param name="result">The result to use when setting the flags</param>
        void SetParityFlags(byte result);

        /// <summary>
        /// Reset all flags
        /// </summary>
        void ResetFlags();

        /// <summary>
        /// Set all flags
        /// </summary>
        void SetFlags();
    }
}