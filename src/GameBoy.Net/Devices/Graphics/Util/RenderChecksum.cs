namespace GameBoy.Net.Devices.Graphics.Util
{
    public struct RenderChecksum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderChecksum"/> struct.
        /// </summary>
        /// <param name="context">The context.</param>
        public RenderChecksum(RenderContext context)
        {
            RenderSettings = context.RenderSettings;
            TileSet = context.TileSet.Crc32();
            BackgroundTileMap = context.BackgroundTileMap.Crc32();
            WindowTileMap = context.WindowTileMap.Crc32();
            SpriteOam = context.SpriteOam.Crc32();
            SpriteTileSet = context.SpriteTileSet.Crc32();
        }

        /// <summary>
        /// Gets the render settings checksum.
        /// </summary>
        public RenderSettings RenderSettings { get; }

        /// <summary>
        /// Gets the tile set bytes checksum.
        /// </summary>
        public uint TileSet { get; }

        /// <summary>
        /// Gets the tile map bytes checksum.
        /// </summary>
        public uint BackgroundTileMap { get; }

        /// <summary>
        /// Gets or sets the window tile map checksum.
        /// </summary>
        public uint WindowTileMap { get; }

        /// <summary>
        /// Gets the sprite bytes checksum.
        /// </summary>
        public uint SpriteOam { get; }

        /// <summary>
        /// Gets the sprite tile set bytes checksum.
        /// </summary>
        public uint SpriteTileSet { get; }

        /// <summary>
        /// Gets the render states that have changed in comparison to the specified render state.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public RenderStateChange GetRenderStateChange(RenderChecksum other)
        {
            if (other.Equals(default))
            {
                return RenderStateChange.All;
            }

            var result = RenderSettings.GetRenderStateChange(other.RenderSettings);

            if (!result.HasFlag(RenderStateChange.TileSet) && TileSet != other.TileSet)
            {
                result |= RenderStateChange.TileSet;
            }

            if (!result.HasFlag(RenderStateChange.BackgroundTileMap) && BackgroundTileMap != other.BackgroundTileMap)
            {
                result |= RenderStateChange.BackgroundTileMap;
            }

            // If window settings haven't changed and the window is not enabled then we're not bothered what happens to the window tile map.
            if (!result.HasFlag(RenderStateChange.WindowTileMap) && RenderSettings.WindowEnabled && WindowTileMap != other.WindowTileMap)
            {
                result |= RenderStateChange.WindowTileMap;
            }

            if (!result.HasFlag(RenderStateChange.Sprites) && !RenderSettings.SpritesEnabled)
            {
                return result;
            }

            // If sprite settings haven't changed and sprite are not enabled then we're not bothered what happens to sprite OAM.
            if (!result.HasFlag(RenderStateChange.SpriteOam) && SpriteOam != other.SpriteOam)
            {
                result |= RenderStateChange.SpriteOam;
            }

            if (!result.HasFlag(RenderStateChange.SpriteTileSet) && SpriteTileSet != other.SpriteTileSet)
            {
                result |= RenderStateChange.SpriteTileSet;
            }

            return result;
        }

        public bool Equals(RenderChecksum other)
        {
            return RenderSettings.Equals(other.RenderSettings)
                   && TileSet == other.TileSet
                   && BackgroundTileMap == other.BackgroundTileMap
                   && WindowTileMap == other.WindowTileMap
                   && SpriteOam == other.SpriteOam
                   && SpriteTileSet == other.SpriteTileSet;
        }

        public override bool Equals(object obj) => !(obj is null) && obj is RenderChecksum checksum && Equals(checksum);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = RenderSettings.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) TileSet;
                hashCode = (hashCode * 397) ^ (int) BackgroundTileMap;
                hashCode = (hashCode * 397) ^ (int) WindowTileMap;
                hashCode = (hashCode * 397) ^ (int) SpriteOam;
                hashCode = (hashCode * 397) ^ (int) SpriteTileSet;
                return hashCode;
            }
        }
    }
}
