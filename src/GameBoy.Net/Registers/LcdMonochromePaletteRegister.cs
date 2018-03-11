using System.Collections.Generic;
using GameBoy.Net.Devices.Graphics.Models;
using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// FF47 - BGP - BG Palette Data (R/W) - Non CGB Mode Only
    /// This register assigns gray shades to the color numbers of the BG and Window tiles.
    /// Bit 7-6 - Shade for Color Number 3
    /// Bit 5-4 - Shade for Color Number 2
    /// Bit 3-2 - Shade for Color Number 1
    /// Bit 1-0 - Shade for Color Number 0
    /// The four possible gray shades are:
    /// 0 White
    /// 1 Light gray
    /// 2 Dark gray
    /// 3 Black
    /// In CGB Mode the Color Palettes are taken from CGB Palette Memory instead.
    /// </summary>
    /// <seealso cref="ILcdMonochromePaletteRegister" />
    public class LcdMonochromePaletteRegister : ILcdMonochromePaletteRegister
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LcdMonochromePaletteRegister" /> class.
        /// </summary>
        public LcdMonochromePaletteRegister()
        {
            Pallette = new Dictionary<Palette, MonochromeShade>
                       {
                           [Palette.Colour0] = MonochromeShade.White,
                           [Palette.Colour1] = MonochromeShade.White,
                           [Palette.Colour2] = MonochromeShade.White,
                           [Palette.Colour3] = MonochromeShade.White
                       };
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address => 0xff47;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "BG Palette Data (BGP R/W)";

        /// <summary>
        /// Gets or sets the raw register value.
        /// </summary>
        /// <value>
        /// The raw register value.
        /// </value>
        public byte Register
        {
            get
            {
                var value = 0x00;

                // Bit 7-6 - Shade for Color Number 3.
                value |= (int) Pallette[Palette.Colour3] << 6;

                // Bit 5-4 - Shade for Color Number 2.
                value |= (int) Pallette[Palette.Colour2] << 4;

                // Bit 3-2 - Shade for Color Number 1.
                value |= (int) Pallette[Palette.Colour1] << 2;

                // Bit 1-0 - Shade for Color Number 0.
                value |= (int) Pallette[Palette.Colour0];

                return (byte) value;
            }
            set
            {
                // Bit 7-6 - Shade for Color Number 3.
                Pallette[Palette.Colour3] = (MonochromeShade) (value >> 6);

                // Bit 5-4 - Shade for Color Number 2.
                Pallette[Palette.Colour2] = (MonochromeShade) ((value >> 4) & 0x3);

                // Bit 3-2 - Shade for Color Number 1.
                Pallette[Palette.Colour1] = (MonochromeShade) ((value >> 2) & 0x3);

                // Bit 1-0 - Shade for Color Number 0.
                Pallette[Palette.Colour0] = (MonochromeShade) (value & 0x3);
            }
        }

        /// <summary>
        /// Gets the debug view.
        /// </summary>
        /// <value>
        /// The debug view.
        /// </value>
        public string DebugView => ToString();

        /// <summary>
        /// Gets the pallette.
        /// </summary>
        /// <value>
        /// The pallette.
        /// </value>
        public Dictionary<Palette, MonochromeShade> Pallette { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString() => $"{Name} ({Address}) = {Register}, {Pallette}";
    }
}