namespace Retro.Net.Z80.State
{
    /// <summary>
    /// The state of Intel 8080 general purpose registers B, C, D, E, H & L.
    /// </summary>
    public struct GeneralPurposeRegisterState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralPurposeRegisterState"/> struct.
        /// </summary>
        /// <param name="b">The B register.</param>
        /// <param name="c">The C register.</param>
        /// <param name="d">The D register.</param>
        /// <param name="e">The E register.</param>
        /// <param name="h">The H register.</param>
        /// <param name="l">The L register.</param>
        public GeneralPurposeRegisterState(byte b, byte c, byte d, byte e, byte h, byte l)
        {
            B = b;
            C = c;
            D = d;
            E = e;
            H = h;
            L = l;
        }

        /// <summary>
        /// Gets the B register.
        /// </summary>
        /// <value>
        /// The B register.
        /// </value>
        public byte B { get; }

        /// <summary>
        /// Gets the C register.
        /// </summary>
        /// <value>
        /// The C register.
        /// </value>
        public byte C { get; }

        /// <summary>
        /// Gets the D register.
        /// </summary>
        /// <value>
        /// The D register.
        /// </value>
        public byte D { get; }

        /// <summary>
        /// Gets the E register.
        /// </summary>
        /// <value>
        /// The E register.
        /// </value>
        public byte E { get; }

        /// <summary>
        /// Gets the H register.
        /// </summary>
        /// <value>
        /// The H register.
        /// </value>
        public byte H { get; }

        /// <summary>
        /// Gets the L register.
        /// </summary>
        /// <value>
        /// The L register.
        /// </value>
        public byte L { get; }
    }
}