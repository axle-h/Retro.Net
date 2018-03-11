using System.Collections.Generic;
using GameBoy.Net.Devices.Graphics.Models;

namespace GameBoy.Net.Registers.Interfaces
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
    /// <seealso cref="IRegister" />
    public interface ILcdMonochromePaletteRegister : IRegister
    {
        /// <summary>
        /// Gets the pallette.
        /// </summary>
        /// <value>
        /// The pallette.
        /// </value>
        Dictionary<Palette, MonochromeShade> Pallette { get; }
    }
}