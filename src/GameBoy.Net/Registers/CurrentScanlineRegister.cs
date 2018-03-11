using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// FF44 - LY - LCDC Y-Coordinate (R)
    /// The LY indicates the vertical line to which the present data is transferred to the LCD
    /// Driver.The LY can take on any value between 0 through 153. The values between 144
    /// and 153 indicate the V-Blank period.Writing will reset the counter.
    /// </summary>
    /// <seealso cref="ICurrentScanlineRegister" />
    public class CurrentScanlineRegister : ICurrentScanlineRegister
    {
        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address => 0xff44;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Current Scanline (LY R)";

        /// <summary>
        /// Gets or sets the raw register value.
        /// </summary>
        /// <value>
        /// The raw register value.
        /// </value>
        public byte Register
        {
            get { return Scanline; }
            set { }
        }

        /// <summary>
        /// Gets the debug view.
        /// </summary>
        /// <value>
        /// The debug view.
        /// </value>
        public string DebugView => ToString();

        /// <summary>
        /// Increments the scanline.
        /// </summary>
        public void IncrementScanline() => Scanline = (byte) ((Scanline + 1) % 154);

        /// <summary>
        /// Gets or sets the scanline.
        /// </summary>
        /// <value>
        /// The scanline.
        /// </value>
        public byte Scanline { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Name} ({Address}) = {Register}";
    }
}