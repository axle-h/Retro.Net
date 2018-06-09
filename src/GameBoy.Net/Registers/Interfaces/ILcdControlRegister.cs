namespace GameBoy.Net.Registers.Interfaces
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
    /// <seealso cref="IRegister" />
    public interface ILcdControlRegister : IRegister
    {
        /// <summary>
        /// LCD status
        /// True: On
        /// False: Off
        /// </summary>
        bool LcdOperation { get; }

        /// <summary>
        /// Sets which tile map the window uses
        /// True: 9C00-9FFF (1)
        /// False: 9800-9BFF (0)
        /// </summary>
        bool WindowTileMap { get; }

        /// <summary>
        /// Window status
        /// True: On
        /// False: Off
        /// </summary>
        bool WindowDisplay { get; }

        /// <summary>
        /// Sets which tile pattern table to use
        /// True: 8000-8FFF (1)
        /// False: 8800-97FF (0)
        /// </summary>
        bool TilePatternTable { get; }

        /// <summary>
        /// Sets which tile map the background uses
        /// True: 9C00-9FFF (1)
        /// False: 9800-9BFF (0)
        /// </summary>
        bool BackgroundTileMap { get; }

        /// <summary>
        /// Sets the sprite size
        /// True: 8x16
        /// False: 8x8
        /// </summary>
        bool SpriteSize { get; }

        /// <summary>
        /// Sets whether sprites are displayed.
        /// True: Displayed.
        /// False: Not displayed.
        /// </summary>
        bool SpriteDisplayEnable { get; }

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
        bool BackgroundDisplay { get; }
    }
}