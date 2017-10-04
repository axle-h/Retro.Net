using Retro.Net.Util;
using Retro.Net.Z80.State;

namespace Retro.Net.Z80.Registers
{
    /// <summary>
    /// General purpose Intel 8080 registers B, C, D, E, H & L. Also their 16-bit equivalents BC, DE & HL.
    /// </summary>
    public class GeneralPurposeRegisterSet
    {
        /// <summary>
        /// Gets or sets the B register.
        /// </summary>
        /// <value>
        /// The B register.
        /// </value>
        public byte B { get; set; }

        /// <summary>
        /// Gets or sets the C register.
        /// </summary>
        /// <value>
        /// The C register.
        /// </value>
        public byte C { get; set; }

        /// <summary>
        /// Gets or sets the D register.
        /// </summary>
        /// <value>
        /// The D register.
        /// </value>
        public byte D { get; set; }

        /// <summary>
        /// Gets or sets the E register.
        /// </summary>
        /// <value>
        /// The E register.
        /// </value>
        public byte E { get; set; }

        /// <summary>
        /// Gets or sets the H register.
        /// </summary>
        /// <value>
        /// The H register.
        /// </value>
        public byte H { get; set; }

        /// <summary>
        /// Gets or sets the L register.
        /// </summary>
        /// <value>
        /// The L register.
        /// </value>
        public byte L { get; set; }

        /// <summary>
        /// Gets or sets the BC register.
        /// </summary>
        /// <value>
        /// The BC register.
        /// </value>
        public ushort BC
        {
            get => BitConverterHelpers.To16Bit(B, C);
            set
            {
                var bytes = BitConverterHelpers.To8Bit(value);
                B = bytes[1];
                C = bytes[0];
            }
        }

        /// <summary>
        /// Gets or sets the DE register.
        /// </summary>
        /// <value>
        /// The DE register.
        /// </value>
        public ushort DE
        {
            get => BitConverterHelpers.To16Bit(D, E);
            set
            {
                var bytes = BitConverterHelpers.To8Bit(value);
                D = bytes[1];
                E = bytes[0];
            }
        }

        /// <summary>
        /// Gets or sets the HL register.
        /// </summary>
        /// <value>
        /// The HL register.
        /// </value>
        public ushort HL
        {
            get => BitConverterHelpers.To16Bit(H, L);
            set
            {
                var bytes = BitConverterHelpers.To8Bit(value);
                H = bytes[1];
                L = bytes[0];
            }
        }

        /// <summary>
        /// Resets the register values to the specified state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void ResetToState(GeneralPurposeRegisterState state)
        {
            B = state.B;
            C = state.C;
            D = state.D;
            E = state.E;
            H = state.H;
            L = state.L;
        }

        /// <summary>
        /// Gets the state of the registers.
        /// </summary>
        /// <returns></returns>
        public GeneralPurposeRegisterState GetRegisterState() => new GeneralPurposeRegisterState(B, C, D, E, H, L);
    }
}