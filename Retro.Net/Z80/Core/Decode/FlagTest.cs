namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// A method of testing a register value or ALU result.
    /// </summary>
    public enum FlagTest : byte
    {
        /// <summary>
        /// No testing.
        /// </summary>
        None = 0,

        /// <summary>
        /// Test if the result is non-zero.
        /// </summary>
        NotZero = 0x01,

        /// <summary>
        /// Test if the result is zero.
        /// </summary>
        Zero = 0x02,

        /// <summary>
        /// Test if the result does not have carry.
        /// </summary>
        NotCarry = 0x03,

        /// <summary>
        /// Test if the result has carry.
        /// </summary>
        Carry = 0x04,

        /// <summary>
        /// Test if the result has odd parity.
        /// </summary>
        ParityOdd = 0x05,

        /// <summary>
        /// Test if the result has even parity.
        /// </summary>
        ParityEven = 0x06,

        /// <summary>
        /// Test if the result is even.
        /// </summary>
        Positive = 0x07,

        /// <summary>
        /// Test if the result is negative.
        /// </summary>
        Negative = 0x08
    }
}