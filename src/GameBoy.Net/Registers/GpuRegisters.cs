using GameBoy.Net.Devices;
using GameBoy.Net.Devices.Graphics;
using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// GameBoy GPU registers.
    /// </summary>
    /// <seealso cref="IGpuRegisters" />
    public class GpuRegisters : IGpuRegisters
    {
        private readonly IInterruptFlagsRegister _interruptFlagsRegister;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GpuRegisters"/> class.
        /// </summary>
        /// <param name="lcdControlRegister">The LCD control register.</param>
        /// <param name="currentScanlineRegister">The current scanline register.</param>
        /// <param name="lcdMonochromePaletteRegister">The LCD monochrome palette register.</param>
        /// <param name="lcdStatusRegister">The LCD status register.</param>
        /// <param name="interruptFlagsRegister">The interrupt flags register.</param>
        public GpuRegisters(ILcdControlRegister lcdControlRegister,
            ICurrentScanlineRegister currentScanlineRegister,
            ILcdMonochromePaletteRegister lcdMonochromePaletteRegister,
            ILcdStatusRegister lcdStatusRegister,
            IInterruptFlagsRegister interruptFlagsRegister)
        {
            LcdStatusRegister = lcdStatusRegister;
            _interruptFlagsRegister = interruptFlagsRegister;
            LcdControlRegister = lcdControlRegister;
            CurrentScanlineRegister = currentScanlineRegister;
            LcdMonochromePaletteRegister = lcdMonochromePaletteRegister;
            ScrollXRegister = new SimpleRegister(0xff43, "Background Horizontal Scrolling (SCROLLX R/W)");
            ScrollYRegister = new SimpleRegister(0xff42, "Background Vertical Scrolling (SCROLLY R/W)");
            CurrentScanlineCompareRegister = new SimpleRegister(0xff45, "LY Compare (LYC R/W)");
            WindowYPositionRegister = new SimpleRegister(0xff4a, "Window Y Position (WY R/W)");
            WindowXPositionRegister = new SimpleRegister(0xff4b, "Window X Position minus 7 (WX R/W)");
        }

        /// <summary>
        /// Gets the scroll x register.
        /// </summary>
        /// <value>
        /// The scroll x register.
        /// </value>
        public IRegister ScrollXRegister { get; }

        /// <summary>
        /// Gets the scroll y register.
        /// </summary>
        /// <value>
        /// The scroll y register.
        /// </value>
        public IRegister ScrollYRegister { get; }

        /// <summary>
        /// Gets the LCD control register.
        /// </summary>
        /// <value>
        /// The LCD control register.
        /// </value>
        public ILcdControlRegister LcdControlRegister { get; }

        /// <summary>
        /// Gets the current scanline register.
        /// FF44 - LY - LCDC Y-Coordinate (R)
        /// </summary>
        /// <value>
        /// The current scanline register.
        /// </value>
        public ICurrentScanlineRegister CurrentScanlineRegister { get; }

        /// <summary>
        /// Gets the current scanline compare register.
        /// FF45 - LYC - LY Compare (R/W)
        /// </summary>
        /// <value>
        /// The current scanline compare register.
        /// </value>
        public IRegister CurrentScanlineCompareRegister { get; }

        /// <summary>
        /// Gets the LCD monochrome palette register.
        /// </summary>
        /// <value>
        /// The LCD monochrome palette register.
        /// </value>
        public ILcdMonochromePaletteRegister LcdMonochromePaletteRegister { get; }

        /// <summary>
        /// Gets the LCD status register.
        /// </summary>
        /// <value>
        /// The LCD status register.
        /// </value>
        public ILcdStatusRegister LcdStatusRegister { get; }

        /// <summary>
        /// Gets the window X position register.
        /// This is the upper left corner of the window minus 7.
        /// Values can be 0 - 166.
        /// WX=7 locates the window at upper left of the screen, it is then completely covering normal background in the horizontal direction.
        /// </summary>
        /// <value>
        /// The window X position register.
        /// </value>
        public IRegister WindowXPositionRegister { get; }

        /// <summary>
        /// Gets the window Y position register.
        /// This is the upper left corner of the window.
        /// Values can be 0 - 143.
        /// WY=0 locates the window at upper left of the screen, it is then completely covering normal background in the vertical direction.
        /// </summary>
        /// <value>
        /// The window Y position register.
        /// </value>
        public IRegister WindowYPositionRegister { get; }

        /// <summary>
        /// Increments the current scanline, updates the LCD status register and checks for interrupts.
        /// </summary>
        public void IncrementScanline()
        {
            CurrentScanlineRegister.IncrementScanline();
            
            LcdStatusRegister.CoincidenceFlag = CurrentScanlineRegister.Scanline == CurrentScanlineCompareRegister.Register;

            if (LcdStatusRegister.CoincidenceFlag && LcdStatusRegister.CoincidenceInterrupt)
            {
                _interruptFlagsRegister.UpdateInterrupts(InterruptFlag.LcdStatusTriggers);
            }
        }


        /// <summary>
        /// Gets or sets the gpu mode.
        /// </summary>
        /// <value>
        /// The gpu mode.
        /// </value>
        public GpuMode GpuMode
        {
            get { return LcdStatusRegister.GpuMode; }
            set
            {
                LcdStatusRegister.GpuMode = value;
                if (LcdStatusRegister.Mode0HBlankInterrupt && LcdStatusRegister.GpuMode == GpuMode.HorizontalBlank)
                {
                    _interruptFlagsRegister.UpdateInterrupts(InterruptFlag.LcdStatusTriggers);
                }
                else if (LcdStatusRegister.Mode1VBlankInterrupt && LcdStatusRegister.GpuMode == GpuMode.VerticalBlank)
                {
                    _interruptFlagsRegister.UpdateInterrupts(InterruptFlag.LcdStatusTriggers);
                }
                else if (LcdStatusRegister.Mode2OamInterrupt && LcdStatusRegister.GpuMode == GpuMode.ReadingOam)
                {
                    _interruptFlagsRegister.UpdateInterrupts(InterruptFlag.LcdStatusTriggers);
                }
            }
        }
    }
}