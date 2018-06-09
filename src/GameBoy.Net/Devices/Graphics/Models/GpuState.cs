using System.Collections.Generic;
using GameBoy.Net.Devices.Graphics.Util;

namespace GameBoy.Net.Devices.Graphics.Models
{
    public class GpuState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpuState" /> class.
        /// </summary>
        /// <param name="renderSettings">The render settings.</param>
        /// <param name="backgroundTiles">The background tiles.</param>
        /// <param name="spriteTiles">The sprite tiles.</param>
        /// <param name="sprites">The sprites.</param>
        public GpuState(RenderSettings renderSettings,
                        ICollection<Tile> backgroundTiles,
                        ICollection<Tile> spriteTiles,
                        ICollection<Sprite> sprites)
        {
            RenderSettings = renderSettings;
            BackgroundTiles = backgroundTiles;
            SpriteTiles = spriteTiles;
            Sprites = sprites;
        }

        /// <summary>
        /// Gets the render settings.
        /// </summary>
        public RenderSettings RenderSettings { get; }

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