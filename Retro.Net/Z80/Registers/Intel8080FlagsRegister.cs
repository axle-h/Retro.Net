using Retro.Net.Z80.Util;

namespace Retro.Net.Z80.Registers
{
    /// <summary>
    /// Flags register used by the Z80 & Intel 8080 (I think)
    /// </summary>
    public class Intel8080FlagsRegister : IFlagsRegister
    {
        private const byte SignMask = 1 << 7;
        private const byte ZeroMask = 1 << 6;
        private const byte Flag5Mask = 1 << 5;
        private const byte HalfCarryMask = 1 << 4;
        private const byte Flag3Mask = 1 << 3;
        private const byte ParityOverflowMask = 1 << 2;
        private const byte SubtractMask = 1 << 1;
        private const byte CarryMask = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Intel8080FlagsRegister"/> class.
        /// </summary>
        public Intel8080FlagsRegister()
        {
            ResetFlags();
        }

        /// <summary>
        /// The byte value of the F register, constructed from the component flags
        /// </summary>
        public byte Register
        {
            get
            {
                byte ans = 0x00;

                if (Sign)
                {
                    ans |= SignMask;
                }

                if (Zero)
                {
                    ans |= ZeroMask;
                }

                if (Flag5)
                {
                    ans |= Flag5Mask;
                }

                if (HalfCarry)
                {
                    ans |= HalfCarryMask;
                }

                if (Flag3)
                {
                    ans |= Flag3Mask;
                }

                if (ParityOverflow)
                {
                    ans |= ParityOverflowMask;
                }

                if (Subtract)
                {
                    ans |= SubtractMask;
                }

                if (Carry)
                {
                    ans |= CarryMask;
                }

                return ans;
            }
            set
            {
                Sign = (value & SignMask) > 0;
                Zero = (value & ZeroMask) > 0;
                Flag5 = (value & Flag5Mask) > 0;
                HalfCarry = (value & HalfCarryMask) > 0;
                Flag3 = (value & Flag3Mask) > 0;
                ParityOverflow = (value & ParityOverflowMask) > 0;
                Subtract = (value & SubtractMask) > 0;
                Carry = (value & CarryMask) > 0;
            }
        }
        
        /// <summary>
        /// S - Sign flag
        /// Set if the 2-complement value is negative (copy of MSB)
        /// </summary>
        public bool Sign { get; set; }

        /// <summary>
        /// Z - Zero flag
        /// Set if the value is zero
        /// </summary>
        public bool Zero { get; set; }

        /// <summary>
        /// F5 - undocumented flag
        /// Copy of bit 5
        /// </summary>
        public bool Flag5 { get; set; }

        /// <summary>
        /// H - Half Carry
        /// Carry from bit 3 to bit 4
        /// </summary>
        public bool HalfCarry { get; set; }

        /// <summary>
        /// F3 - undocumented flag
        /// Copy of bit 3
        /// </summary>
        public bool Flag3 { get; set; }

        /// <summary>
        /// P/V - Parity or Overflow
        /// Parity set if even number of bits set
        /// Overflow set if the 2-complement result does not fit in the register
        /// </summary>
        public bool ParityOverflow { get; set; }

        /// <summary>
        /// N - Subtract
        /// Set if the last operation was a subtraction
        /// </summary>
        public bool Subtract { get; set; }

        /// <summary>
        /// C - Carry
        /// Set if the result did not fit in the register
        /// </summary>
        public bool Carry { get; set; }

        /// <summary>
        /// et Flag3 & Flag5 according to the result.
        /// </summary>
        /// <param name="result">The result to use when setting the flags.</param>
        public void SetUndocumentedFlags(byte result)
        {
            // Undocumented flags are set from corresponding result bits.
            Flag5 = (result & Flag5Mask) > 0;
            Flag3 = (result & Flag3Mask) > 0;
        }

        /// <summary>
        /// Set Sign, Zero, Flag3 & Flag5 according to the 8-bit result
        /// </summary>
        /// <param name="result">The result to use when setting the flags</param>
        public void SetResultFlags(byte result)
        {
            // Sign flag is a copy of the sign bit.
            Sign = (result & SignMask) > 0;

            // Set Zero flag is result = 0
            Zero = result == 0;

            SetUndocumentedFlags(result);
        }

        /// <summary>
        /// Set Sign, Zero, Flag3 & Flag5 according to the 16-bit result
        /// </summary>
        /// <param name="result">The result to use when setting the flags</param>
        public void SetResultFlags(ushort result)
        {
            // Sign flag is a copy of the sign bit.
            Sign = (result & (SignMask << 8)) > 0;

            // Set Zero flag is result = 0
            Zero = result == 0;

            // Flag is affected by the high - byte addition.
            SetUndocumentedFlags((byte) (result >> 8));
        }

        /// <summary>
        /// Set all flags for a parity result
        /// </summary>
        /// <param name="result">The result to use when setting the flags</param>
        public void SetParityFlags(byte result)
        {
            SetResultFlags(result);
            ParityOverflow = result.IsEvenParity();
            Subtract = false;
        }

        /// <summary>
        /// Reset all flags
        /// </summary>
        public void ResetFlags()
        {
            Sign = false;
            Zero = false;
            Flag5 = false;
            HalfCarry = false;
            Flag3 = false;
            ParityOverflow = false;
            Subtract = false;
            Carry = false;
        }

        /// <summary>
        /// Set all flags
        /// </summary>
        public void SetFlags()
        {
            Sign = true;
            Zero = true;
            Flag5 = true;
            HalfCarry = true;
            Flag3 = true;
            ParityOverflow = true;
            Subtract = true;
            Carry = true;
        }

        public override string ToString()
        {
            return $"[0x{Register:x2}], {nameof(Sign)}: {Sign}, {nameof(Zero)}: {Zero}, {nameof(Flag5)}: {Flag5}, {nameof(HalfCarry)}: {HalfCarry}, {nameof(Flag3)}: {Flag3}, {nameof(ParityOverflow)}: {ParityOverflow}, {nameof(Subtract)}: {Subtract}, {nameof(Carry)}: {Carry}";
        }
    }
}