namespace GameBoy.Net.Registers.Interfaces
{
    /// <summary>
    /// FF44 - LY - LCDC Y-Coordinate (R)
    /// The LY indicates the vertical line to which the present data is transferred to the LCD
    /// Driver.The LY can take on any value between 0 through 153. The values between 144
    /// and 153 indicate the V-Blank period.Writing will reset the counter.
    /// </summary>
    /// <seealso cref="IRegister" />
    public interface ICurrentScanlineRegister : IRegister
    {
        /// <summary>
        /// Gets or sets the scanline.
        /// </summary>
        /// <value>
        /// The scanline.
        /// </value>
        byte Scanline { get; set; }

        /// <summary>
        /// Increments the scanline.
        /// </summary>
        void IncrementScanline();
    }
}