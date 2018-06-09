using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Devices.Graphics.Util
{
    /// <summary>
    /// GameBoy GPU render settings.
    /// </summary>
    public class RenderSettings
    {
        private static readonly TileMapAddress TileMap1Address = new TileMapAddress(0x1800, false);
        private static readonly TileMapAddress TileMap2Address = new TileMapAddress(0x1c00, true);

        private const ushort TileSet1Address = 0;
        private const ushort TileSet2Address = 0x800;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderSettings"/> class.
        /// </summary>
        /// <param name="registers">The registers.</param>
        public RenderSettings(IGpuRegisters registers)
        {
            BackgroundDisplay = registers.LcdControlRegister.BackgroundDisplay;
            BackgroundTileMapAddress = registers.LcdControlRegister.BackgroundTileMap ? TileMap2Address : TileMap1Address;
            WindowTileMapAddress = registers.LcdControlRegister.WindowTileMap ? TileMap2Address : TileMap1Address;
            WindowEnabled = registers.LcdControlRegister.WindowDisplay;

            if (registers.LcdControlRegister.TilePatternTable)
            {
                TileSetAddress = TileSet1Address;
                SpriteTileSetAddress = TileSet1Address;
            }
            else
            {
                TileSetAddress = TileSet2Address;
                SpriteTileSetAddress = TileSet1Address;
            }

            Scroll = new Coordinates(registers.ScrollXRegister.Register, registers.ScrollYRegister.Register);
            WindowPosition = new Coordinates(registers.WindowXPositionRegister.Register - 7, registers.WindowYPositionRegister.Register);
            LargeSprites = registers.LcdControlRegister.SpriteSize;
            SpritesEnabled = registers.LcdControlRegister.SpriteDisplayEnable;
        }

        /// <summary>
        /// Gets a value indicating whether [background display]. 
        /// </summary>
        /// <value>
        /// Monochrome GameBoy and SGB: BG Display
        ///   <c>true</c> if background should be displayed; otherwise, <c>false</c>.
        /// 
        /// CGB in CGB Mode: BG and Window Master Priority
        ///   <c>true</c> if background and window should lose it's priority (sprites are always rendered on top); otherwise, <c>false</c>.
        /// 
        /// CGB in Non CGB Mode: BG and Window Display
        ///   <c>true</c> if background and window should be displayed; otherwise, <c>false</c>.
        /// </value>
        public bool BackgroundDisplay { get; }

        /// <summary>
        /// Gets a value indicating whether [window enabled].
        /// </summary>
        public bool WindowEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether [sprites enabled].
        /// </summary>
        public bool SpritesEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether [large sprites are enabled].
        /// </summary>
        public bool LargeSprites { get; }

        /// <summary>
        /// Gets the tile map address.
        /// </summary>
        public TileMapAddress BackgroundTileMapAddress { get; }

        /// <summary>
        /// Gets the window tile map address.
        /// </summary>
        public TileMapAddress WindowTileMapAddress { get; }

        /// <summary>
        /// Gets the address of the background and window tile set.
        /// </summary>
        public ushort TileSetAddress { get; }

        /// <summary>
        /// Gets the address of the sprite tile set.
        /// </summary>
        public ushort SpriteTileSetAddress { get; }
        
        /// <summary>
        /// Gets the scroll coordinates.
        /// </summary>
        public Coordinates Scroll { get; }
        
        /// <summary>
        /// Gets the window position.
        /// </summary>
        public Coordinates WindowPosition { get; }
        
        /// <summary>
        /// Gets the render settings that have changed in comparison to the specified render settings.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public RenderStateChange GetRenderStateChange(RenderSettings other)
        {
            var result = RenderStateChange.None;
            if (BackgroundDisplay != other.BackgroundDisplay)
            {
                result |= RenderStateChange.BackgroundDisplay;
            }

            if (!BackgroundTileMapAddress.Equals(other.BackgroundTileMapAddress))
            {
                result |= RenderStateChange.BackgroundTileMap;
            }

            if (WindowEnabled != other.WindowEnabled || !WindowTileMapAddress.Equals(other.WindowTileMapAddress))
            {
                result |= RenderStateChange.WindowTileMap;
            }

            if (TileSetAddress != other.TileSetAddress)
            {
                result |= RenderStateChange.TileSet;
            }

            if (!Scroll.Equals(other.Scroll))
            {
                result |= RenderStateChange.Scroll;
            }
            
            if (!WindowPosition.Equals(other.WindowPosition))
            {
                result |= RenderStateChange.WindowPosition;
            }

            if (LargeSprites != other.LargeSprites)
            {
                result |= RenderStateChange.SpriteSize;
            }

            if (SpriteTileSetAddress != other.SpriteTileSetAddress)
            {
                result |= RenderStateChange.SpriteTileSet;
            }

            if (SpritesEnabled != other.SpritesEnabled)
            {
                result |= RenderStateChange.SpriteTileSet | RenderStateChange.SpriteOam;
            }
            return result;
        }
    }
}