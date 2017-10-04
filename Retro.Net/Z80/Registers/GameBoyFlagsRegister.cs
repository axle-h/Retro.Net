namespace Retro.Net.Z80.Registers
{
    /// <summary>
    /// GB flags register.
    /// 7 6 5 4 3 2 1 0
    /// Z N H C 0 0 0 0
    /// </summary>
    public class GameBoyFlagsRegister : IFlagsRegister
    {
        private const byte ZeroMask = 1 << 7;
        private const byte SubtractMask = 1 << 6;
        private const byte HalfCarryMask = 1 << 5;
        private const byte CarryMask = 1 << 4;

        /// <summary>
        /// The byte value of the F register, constructed from the component flags
        /// </summary>
        public byte Register
        {
            get
            {
                byte ans = 0x00;

                if (Zero)
                {
                    ans |= ZeroMask;
                }

                if (HalfCarry)
                {
                    ans |= HalfCarryMask;
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
                Zero = (value & ZeroMask) > 0;
                HalfCarry = (value & HalfCarryMask) > 0;
                Subtract = (value & SubtractMask) > 0;
                Carry = (value & CarryMask) > 0;
            }
        }

        /// <summary>
        /// Unused
        /// </summary>
        public bool Sign
        {
            get { return false; }
            set { }
        }

        /// <summary>
        /// Z - Zero flag
        /// Set if the value is zero
        /// </summary>
        public bool Zero { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool Flag5
        {
            get { return false; }
            set { }
        }

        /// <summary>
        /// H - Half Carry
        /// Carry from bit 3 to bit 4
        /// </summary>
        public bool HalfCarry { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool Flag3
        {
            get { return false; }
            set { }
        }

        /// <summary>
        /// Unused
        /// </summary>
        public bool ParityOverflow
        {
            get { return false; }
            set { }
        }

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
            // No undocumented flags on the GB
        }

        /// <summary>
        /// Set Sign, Zero, Flag3 & Flag5 according to the 8-bit result
        /// </summary>
        /// <param name="result">The result to use when setting the flags</param>
        public void SetResultFlags(byte result)
        {
            // Set Zero flag is result = 0
            Zero = result == 0;
        }

        /// <summary>
        /// Set Sign, Zero, Flag3 & Flag5 according to the 16-bit result
        /// </summary>
        /// <param name="result">The result to use when setting the flags</param>
        public void SetResultFlags(ushort result)
        {
            // Set Zero flag is result = 0
            Zero = result == 0;
        }

        /// <summary>
        /// Set all flags for a parity result
        /// </summary>
        /// <param name="result">The result to use when setting the flags</param>
        public void SetParityFlags(byte result)
        {
            // No parity flag in GB so just set the results flags.
            SetResultFlags(result);
            Subtract = false;
        }

        /// <summary>
        /// Reset all flags
        /// </summary>
        public void ResetFlags()
        {
            Zero = false;
            HalfCarry = false;
            Subtract = false;
            Carry = false;
        }

        /// <summary>
        /// Set all flags
        /// </summary>
        public void SetFlags()
        {
            Zero = true;
            HalfCarry = true;
            Subtract = true;
            Carry = true;
        }
    }
}