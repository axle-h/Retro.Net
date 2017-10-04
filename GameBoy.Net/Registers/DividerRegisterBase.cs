using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// FF04 - DIV - Divider Register (R/W)
    /// This register is incremented at rate of 16384Hz(~16779Hz on SGB).
    /// In CGB Double Speed Mode it is incremented twice as fast, ie.at 32768Hz.
    /// Writing any value to this register resets it to 00h.
    /// </summary>
    public abstract class DividerRegisterBase : IRegister
    {
        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address => 0xff04;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Divider (DIV R/W)";

        /// <summary>
        /// Gets or sets the raw register value.
        /// </summary>
        /// <value>
        /// The raw register value.
        /// </value>
        public abstract byte Register { get; set; }

        /// <summary>
        /// Gets the debug view.
        /// </summary>
        /// <value>
        /// The debug view.
        /// </value>
        public string DebugView => ToString();

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Name} ({Address}) = {Register}";
    }
}