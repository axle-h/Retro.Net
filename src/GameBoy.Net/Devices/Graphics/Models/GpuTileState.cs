using System.Collections.Generic;

namespace GameBoy.Net.Devices.Graphics.Models
{
    public class GpuTileState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpuTileState"/> class.
        /// </summary>
        /// <param name="backgroundTiles">The background tiles.</param>
        /// <param name="spriteTiles">The sprite tiles.</param>
        /// <param name="sprites">The sprites.</param>
        public GpuTileState(ICollection<Tile> backgroundTiles,
                            ICollection<Tile> spriteTiles,
                            ICollection<Sprite> sprites)
        {
            BackgroundTiles = backgroundTiles;
            SpriteTiles = spriteTiles;
            Sprites = sprites;
        }

        /// <summary>
        /// Gets the background tiles.
        /// </summary>
        public ICollection<Tile> BackgroundTiles { get; }

        /// <summary>
        /// Gets the sprite tiles.
        /// </summary>
        public ICollection<Tile> SpriteTiles { get; }

        /// <summary>
        /// Gets the sprites.
        /// </summary>
        public ICollection<Sprite> Sprites { get; }
    }
}