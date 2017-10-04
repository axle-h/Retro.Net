using System.Linq;

namespace GameBoy.Net.Devices
{
    /// <summary>
    /// The GameBoy GPU render state.
    /// </summary>
    internal class RenderState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderState"/> class.
        /// </summary>
        /// <param name="renderSettings">The render settings.</param>
        /// <param name="tileSet">The tile set.</param>
        /// <param name="backgroundTileMap">The background tile map.</param>
        /// <param name="windowTileMap">The window tile map.</param>
        /// <param name="spriteOam">The sprite oam.</param>
        /// <param name="spriteTileSet">The sprite tile set.</param>
        public RenderState(RenderSettings renderSettings,
            byte[] tileSet,
            byte[] backgroundTileMap,
            byte[] windowTileMap,
            byte[] spriteOam,
            byte[] spriteTileSet)
        {
            RenderSettings = renderSettings;
            TileSet = tileSet;
            BackgroundTileMap = backgroundTileMap;
            WindowTileMap = windowTileMap;
            SpriteOam = spriteOam;
            SpriteTileSet = spriteTileSet;
        }

        /// <summary>
        /// Gets the render settings.
        /// </summary>
        /// <value>
        /// The render settings.
        /// </value>
        public RenderSettings RenderSettings { get; }

        /// <summary>
        /// Gets the tile set bytes.
        /// </summary>
        /// <value>
        /// The tile set bytes.
        /// </value>
        public byte[] TileSet { get; }

        /// <summary>
        /// Gets the tile map bytes.
        /// </summary>
        /// <value>
        /// The tile map bytes.
        /// </value>
        public byte[] BackgroundTileMap { get; }

        /// <summary>
        /// Gets or sets the window tile map.
        /// </summary>
        /// <value>
        /// The window tile map.
        /// </value>
        public byte[] WindowTileMap { get; }

        /// <summary>
        /// Gets the sprite bytes.
        /// </summary>
        /// <value>
        /// The sprite bytes.
        /// </value>
        public byte[] SpriteOam { get; }

        /// <summary>
        /// Gets the sprite tile set bytes.
        /// </summary>
        /// <value>
        /// The sprite tile set bytes.
        /// </value>
        public byte[] SpriteTileSet { get; }

        /// <summary>
        /// Checks if this render state equals the specified render state.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        protected bool Equals(RenderState other)
        {
            return Equals(RenderSettings, other.RenderSettings) && TileSet.SequenceEqual(other.TileSet) &&
                   BackgroundTileMap.SequenceEqual(other.BackgroundTileMap) && WindowTileMap.SequenceEqual(other.WindowTileMap) &&
                   SpriteOam.SequenceEqual(other.SpriteOam) && SpriteTileSet.SequenceEqual(other.SpriteTileSet);
        }

        /// <summary>
        /// Gets the render states that have changed in comparison to the specified render state.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public RenderStateChange GetRenderStateChange(RenderState other)
        {
            if (other == null)
            {
                return RenderStateChange.All;
            }

            var result = RenderSettings.GetRenderStateChange(other.RenderSettings);

            if (!result.HasFlag(RenderStateChange.TileSet) && !TileSet.SequenceEqual(other.TileSet))
            {
                result |= RenderStateChange.TileSet;
            }

            if (!result.HasFlag(RenderStateChange.BackgroundTileMap) && !BackgroundTileMap.SequenceEqual(other.BackgroundTileMap))
            {
                result |= RenderStateChange.BackgroundTileMap;
            }

            // If window settings haven't changed and the window is not enabled then we're not bothered what happens to the window tile map.
            if (!result.HasFlag(RenderStateChange.WindowTileMap) && RenderSettings.WindowEnabled && !WindowTileMap.SequenceEqual(other.WindowTileMap))
            {
                result |= RenderStateChange.WindowTileMap;
            }

            if (!result.HasFlag(RenderStateChange.Sprites) && !RenderSettings.SpritesEnabled)
            {
                return result;
            }

            // If sprite settings haven't changed and sprite are not enabled then we're not bothered what happens to sprite OAM.
            if (!result.HasFlag(RenderStateChange.SpriteOam) && !SpriteOam.SequenceEqual(other.SpriteOam))
            {
                result |= RenderStateChange.SpriteOam;
            }

            if (!result.HasFlag(RenderStateChange.SpriteTileSet) && !SpriteTileSet.SequenceEqual(other.SpriteTileSet))
            {
                result |= RenderStateChange.SpriteTileSet;
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((RenderState) obj);
        }

        /// <summary>
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = RenderSettings.GetHashCode();
                hashCode = (hashCode * 397) ^ (TileSet?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (BackgroundTileMap?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SpriteOam?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SpriteTileSet?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
