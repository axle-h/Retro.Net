using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Devices
{
    /// <summary>
    /// GameBoy GPU render settings.
    /// </summary>
    internal struct RenderSettings
    {
        public static ushort TileMap1Address = 0x1800;
        public static ushort TileMap2Address = 0x1c00;

        public static ushort TileSet1Address = 0;
        public static ushort TileSet2Address = 0x800;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderSettings"/> struct.
        /// </summary>
        /// <param name="registers">The registers.</param>
        public RenderSettings(IGpuRegisters registers)
        {
            BackgroundTileMapAddress = registers.LcdControlRegister.BackgroundTileMap ? TileMap2Address : TileMap1Address;
            WindowTileMapAddress = registers.LcdControlRegister.WindowTileMap ? TileMap2Address : TileMap1Address;
            WindowEnabled = registers.LcdControlRegister.WindowDisplay;
            WindowAndBackgroundTileMapShared = BackgroundTileMapAddress == WindowTileMapAddress;

            if (registers.LcdControlRegister.TilePatternTable)
            {
                SpriteAndBackgroundTileSetShared = true;
                TileSetAddress = TileSet1Address;
                TileSetIsSigned = false;
            }
            else
            {
                SpriteAndBackgroundTileSetShared = false;
                TileSetAddress = TileSet2Address;
                TileSetIsSigned = true;
            }

            ScrollX = registers.ScrollXRegister.Register;
            ScrollY = registers.ScrollYRegister.Register;
            WindowXPosition = registers.WindowXPositionRegister.Register - 7;
            WindowYPosition = registers.WindowYPositionRegister.Register;
            SpriteHeight = (byte)(registers.LcdControlRegister.SpriteSize ? 16 : 8);
            SpritesEnabled = registers.LcdControlRegister.SpriteDisplayEnable;
        }

        /// <summary>
        /// Gets the tile map address.
        /// </summary>
        /// <value>
        /// The tile map address.
        /// </value>
        public ushort BackgroundTileMapAddress { get; }

        /// <summary>
        /// Gets the window tile map address.
        /// </summary>
        /// <value>
        /// The window tile map address.
        /// </value>
        public ushort WindowTileMapAddress { get; }

        /// <summary>
        /// Gets a value indicating whether [window enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [window enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool WindowEnabled { get; }

        /// <summary>
        /// Gets the tile set address.
        /// </summary>
        /// <value>
        /// The tile set address.
        /// </value>
        public ushort TileSetAddress { get; }

        /// <summary>
        /// Gets a value indicating whether [sprite and background tile set shared].
        /// </summary>
        /// <value>
        /// <c>true</c> if [sprite and background tile set shared]; otherwise, <c>false</c>.
        /// </value>
        public bool SpriteAndBackgroundTileSetShared { get; }

        /// <summary>
        /// Gets a value indicating whether [window and background tile map shared].
        /// </summary>
        /// <value>
        /// <c>true</c> if [window and background tile map shared]; otherwise, <c>false</c>.
        /// </value>
        public bool WindowAndBackgroundTileMapShared { get; }

        /// <summary>
        /// Gets a value indicating whether [tile set is signed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [tile set is signed]; otherwise, <c>false</c>.
        /// </value>
        public bool TileSetIsSigned { get; }

        /// <summary>
        /// Gets the scroll x.
        /// </summary>
        /// <value>
        /// The scroll x.
        /// </value>
        public byte ScrollX { get; }

        /// <summary>
        /// Gets the scroll y.
        /// </summary>
        /// <value>
        /// The scroll y.
        /// </value>
        public byte ScrollY { get; }

        /// <summary>
        /// Gets the window x position.
        /// </summary>
        /// <value>
        /// The window x position.
        /// </value>
        public int WindowXPosition { get; }

        /// <summary>
        /// Gets the window y position.
        /// </summary>
        /// <value>
        /// The window y position.
        /// </value>
        public byte WindowYPosition { get; }

        /// <summary>
        /// Gets the height of the sprite.
        /// </summary>
        /// <value>
        /// The height of the sprite.
        /// </value>
        public byte SpriteHeight { get; }

        /// <summary>
        /// Gets a value indicating whether [sprites enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sprites enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool SpritesEnabled { get; }

        public bool Equals(RenderSettings other)
        {
            return BackgroundTileMapAddress == other.BackgroundTileMapAddress && TileSetAddress == other.TileSetAddress &&
                   ScrollX == other.ScrollX && ScrollY == other.ScrollY && SpriteHeight == other.SpriteHeight &&
                   SpritesEnabled == other.SpritesEnabled && WindowEnabled == other.WindowEnabled &&
                   WindowTileMapAddress == other.WindowTileMapAddress;
        }

        /// <summary>
        /// Gets the render settings that have changed in comparison to the specified render settings.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public RenderStateChange GetRenderStateChange(RenderSettings other)
        {
            var result = RenderStateChange.None;
            if (BackgroundTileMapAddress != other.BackgroundTileMapAddress)
            {
                result |= RenderStateChange.BackgroundTileMap;
            }

            if (WindowEnabled != other.WindowEnabled || WindowTileMapAddress != other.WindowTileMapAddress)
            {
                result |= RenderStateChange.WindowTileMap;
            }

            if (TileSetAddress != other.TileSetAddress)
            {
                result |= RenderStateChange.TileSet;
            }

            if (ScrollX != other.ScrollX || ScrollY != other.ScrollY)
            {
                result |= RenderStateChange.Scroll;
            }

            if (SpriteHeight != other.SpriteHeight)
            {
                result |= RenderStateChange.SpriteSize;
            }

            if (SpritesEnabled != other.SpritesEnabled)
            {
                result |= RenderStateChange.SpriteTileSet | RenderStateChange.SpriteOam;
            }
            return result;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current instance. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is RenderSettings && Equals((RenderSettings) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = BackgroundTileMapAddress.GetHashCode();
                hashCode = (hashCode * 397) ^ TileSetAddress.GetHashCode();
                hashCode = (hashCode * 397) ^ ScrollX.GetHashCode();
                hashCode = (hashCode * 397) ^ ScrollY.GetHashCode();
                hashCode = (hashCode * 397) ^ SpriteHeight.GetHashCode();
                hashCode = (hashCode * 397) ^ SpritesEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ WindowEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ WindowTileMapAddress.GetHashCode();
                return hashCode;
            }
        }
    }
}