using GameBoy.Net.Devices;
using GameBoy.Net.Devices.Graphics;

namespace GameBoy.Net.Registers.Interfaces
{
    /// <summary>
    /// GameBoy GPU registers.
    /// </summary>
    public interface IGpuRegisters
    {
        /// <summary>
        /// Gets the scroll x register.
        /// </summary>
        /// <value>
        /// The scroll x register.
        /// </value>
        IRegister ScrollXRegister { get; }

        /// <summary>
        /// Gets the scroll y register.
        /// </summary>
        /// <value>
        /// The scroll y register.
        /// </value>
        IRegister ScrollYRegister { get; }

        /// <summary>
        /// Gets the LCD control register.
        /// </summary>
        /// <value>
        /// The LCD control register.
        /// </value>
        ILcdControlRegister LcdControlRegister { get; }

        /// <summary>
        /// Gets the current scanline register.
        /// FF44 - LY - LCDC Y-Coordinate (R)
        /// </summary>
        /// <value>
        /// The current scanline register.
        /// </value>
        ICurrentScanlineRegister CurrentScanlineRegister { get; }

        /// <summary>
        /// Gets the current scanline compare register.
        /// FF45 - LYC - LY Compare (R/W)
        /// </summary>
        /// <value>
        /// The current scanline compare register.
        /// </value>
        IRegister CurrentScanlineCompareRegister { get; }

        /// <summary>
        /// Gets the LCD monochrome palette register.
        /// </summary>
        /// <value>
        /// The LCD monochrome palette register.
        /// </value>
        ILcdMonochromePaletteRegister LcdMonochromePaletteRegister { get; }

        /// <summary>
        /// Gets the LCD status register.
        /// </summary>
        /// <value>
        /// The LCD status register.
        /// </value>
        ILcdStatusRegister LcdStatusRegister { get; }

        /// <summary>
        /// Gets the window X position register.
        /// This is the upper left corner of the window minus 7.
        /// Values can be 0 - 166.
        /// WX=7 locates the window at upper left of the screen, it is then completely covering normal background in the horizontal direction.
        /// </summary>
        /// <value>
        /// The window X position register.
        /// </value>
        IRegister WindowXPositionRegister { get; }

        /// <summary>
        /// Gets the window Y position register.
        /// This is the upper left corner of the window.
        /// Values can be 0 - 143.
        /// WY=0 locates the window at upper left of the screen, it is then completely covering normal background in the vertical direction.
        /// </summary>
        /// <value>
        /// The window Y position register.
        /// </value>
        IRegister WindowYPositionRegister { get; }

        /// <summary>
        /// Gets or sets the gpu mode.
        /// </summary>
        /// <value>
        /// The gpu mode.
        /// </value>
        GpuMode GpuMode { get; set; }

        /// <summary>
        /// Increments the current scanline, updates the LCD status register and checks for interrupts.
        /// </summary>
        void IncrementScanline();
    }
}