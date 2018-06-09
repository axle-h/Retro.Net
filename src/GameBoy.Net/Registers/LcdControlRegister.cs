using System;
using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// FF40 - LCDC - LCD Control (R/W)
    /// Bit 7 - LCD Display Enable(0=Off, 1=On)
    /// Bit 6 - Window Tile Map Display Select(0=9800-9BFF, 1=9C00-9FFF)
    /// Bit 5 - Window Display Enable(0=Off, 1=On)
    /// Bit 4 - BG & Window Tile Data Select(0=8800-97FF, 1=8000-8FFF)
    /// Bit 3 - BG Tile Map Display Select(0=9800-9BFF, 1=9C00-9FFF)
    /// Bit 2 - OBJ(Sprite) Size(0=8x8, 1=8x16)
    /// Bit 1 - OBJ(Sprite) Display Enable(0=Off, 1=On)
    /// Bit 0 - BG Display(for CGB see below) (0=Off, 1=On)
    /// </summary>
    /// <seealso cref="ILcdControlRegister" />
    public class LcdControlRegister : ILcdControlRegister
    {
        private LcdControl _lcdControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="LcdControlRegister"/> class.
        /// </summary>
        public LcdControlRegister()
        {
            _lcdControl = LcdControl.None;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address => 0xff40;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "LCD Control (LCDCONT R/W)";

        /// <summary>
        /// FF40 -- LCDCONT [RW] LCD Control             | when set to 1 | when set to 0
        /// Bit7 LCD operation                           | ON            | OFF
        /// Bit6 Window Tile Map address                 | 9C00-9FFF     | 9800-9BFF
        /// Bit5 Window display                          | ON            | OFF
        /// Bit4 Tile Pattern Table address              | 8000-8FFF     | 8800-97FF
        /// Bit3 Background Tile Map address             | 9C00-9FFF     | 9800-9BFF
        /// Bit2 Sprite size                             | 8x16          | 8x8
        /// Bit1 Color #0 transparency in the window     | SOLID         | TRANSPARENT
        /// Bit0 Background display                      | ON            | OFF
        /// </summary>
        public byte Register
        {
            get { return (byte) _lcdControl; }
            set { _lcdControl = (LcdControl) value; }
        }

        /// <summary>
        /// Gets the debug view.
        /// </summary>
        /// <value>
        /// The debug view.
        /// </value>
        public string DebugView => ToString();

        /// <summary>
        /// LCD status
        /// True: On
        /// False: Off
        /// </summary>
        public bool LcdOperation => _lcdControl.HasFlag(LcdControl.LcdOperation);

        /// <summary>
        /// Sets which tile map the window uses
        /// True: 9C00-9FFF (1)
        /// False: 9800-9BFF (0)
        /// </summary>
        public bool WindowTileMap => _lcdControl.HasFlag(LcdControl.WindowTileMap);

        /// <summary>
        /// Window status
        /// True: On
        /// False: Off
        /// </summary>
        public bool WindowDisplay => _lcdControl.HasFlag(LcdControl.WindowDisplay);

        /// <summary>
        /// Sets which tile pattern table to use
        /// True: 8000-8FFF (1)
        /// False: 8800-97FF (0)
        /// </summary>
        public bool TilePatternTable => _lcdControl.HasFlag(LcdControl.TilePatternTable);

        /// <summary>
        /// Sets which tile map the background uses
        /// True: 9C00-9FFF (1)
        /// False: 9800-9BFF (0)
        /// </summary>
        public bool BackgroundTileMap => _lcdControl.HasFlag(LcdControl.BackgroundTileMap);

        /// <summary>
        /// Sets the sprite size
        /// True: 8x16
        /// False: 8x8
        /// </summary>
        public bool SpriteSize => _lcdControl.HasFlag(LcdControl.SpriteSize);

        /// <summary>
        /// Sets whether sprites are diaplayed.
        /// True: Displayed.
        /// False: Not displayed.
        /// </summary>
        public bool SpriteDisplayEnable => _lcdControl.HasFlag(LcdControl.SpriteDisplayEnable);


        /// <summary>
        /// --- LCDC.0 has different Meanings depending on Gameboy Type ---
        /// 
        /// LCDC.0 - 1) Monochrome Gameboy and SGB: BG Display
        /// When Bit 0 is cleared, the background becomes blank(white). Window and
        /// Sprites may still be displayed(if enabled in Bit 1 and/or Bit 5).
        /// 
        /// LCDC.0 - 2) CGB in CGB Mode: BG and Window Master Priority
        /// When Bit 0 is cleared, the background and window lose their priority - the
        /// sprites will be always displayed on top of background and window,
        /// independently of the priority flags in OAM and BG Map attributes.
        /// 
        /// LCDC.0 - 3) CGB in Non CGB Mode: BG and Window Display
        /// When Bit 0 is cleared, both background and window become blank(white), ie.
        /// the Window Display Bit(Bit 5) is ignored in that case. Only Sprites may still
        /// be displayed(if enabled in Bit 1).
        /// This is a possible compatibility problem - any monochrome games(if any) that
        /// disable the background, but still want to display the window wouldn't work
        /// properly on CGBs.
        /// 
        /// </summary>
        public bool BackgroundDisplay => _lcdControl.HasFlag(LcdControl.BackgroundDisplay);

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Name} ({Address}) = {Register} ({_lcdControl})";


        /// <summary>
        /// The LCD control command bitmap.
        /// </summary>
        [Flags]
        private enum LcdControl : byte
        {
            /// <summary>
            /// No operation.
            /// </summary>
            None = 0,

            /// <summary>
            /// Background display enabled.
            /// </summary>
            BackgroundDisplay = 0x01,
            
            /// <summary>
            /// Sprite display enabled.
            /// </summary>
            SpriteDisplayEnable = 0x02,

            /// <summary>
            /// The sprite size flag.
            /// Set: 8x16
            /// Unset: 8x8.
            /// </summary>
            SpriteSize = 0x04,

            /// <summary>
            /// The background tile map flag.
            /// Set: 9C00-9FFF (1)
            /// Unset: 9800-9BFF (0)
            /// </summary>
            BackgroundTileMap = 0x08,

            /// <summary>
            /// The tile pattern table flag.
            /// Set: 8000-8FFF (1)
            /// Unset: 8800-97FF (0)
            /// </summary>
            TilePatternTable = 0x10,

            /// <summary>
            /// Window display enabled.
            /// </summary>
            WindowDisplay = 0x20,

            /// <summary>
            /// The window tile map flag.
            /// True: 9C00-9FFF (1)
            /// False: 9800-9BFF (0)
            /// </summary>
            WindowTileMap = 0x40,

            /// <summary>
            /// LCD operation enabled.
            /// </summary>
            LcdOperation = 0x80
        }
    }
}