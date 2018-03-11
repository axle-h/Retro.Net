using System;

namespace GameBoy.Net.Devices.Graphics.Util
{
    /// <summary>
    /// A render state change.
    /// </summary>
    [Flags]
    public enum RenderStateChange
    {
        /// <summary>
        /// The entire render state is unchanged.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The background tile map has changed.
        /// </summary>
        BackgroundTileMap = 0x1,

        /// <summary>
        /// The window tile map has changed.
        /// This could also be triggered when the window is enabled or disabled.
        /// </summary>
        WindowTileMap = 0x2,

        /// <summary>
        /// The tile set has changed.
        /// </summary>
        TileSet = 0x4,

        /// <summary>
        /// A sprite has changed.
        /// This could also be triggered when sprites are enabled or disabled.
        /// </summary>
        SpriteOam = 0x8,

        /// <summary>
        /// The sprite tile set has changed.
        /// This could also be triggered when sprites are enabled or disabled.
        /// </summary>
        SpriteTileSet = 0x10,

        /// <summary>
        /// The sprite size has changed.
        /// </summary>
        SpriteSize = 0x20,

        /// <summary>
        /// The background scroll has moved.
        /// </summary>
        Scroll = 0x40,

        /// <summary>
        /// Sprites have changed.
        /// </summary>
        Sprites = SpriteOam | SpriteTileSet | SpriteSize,

        /// <summary>
        /// Everything has changed.
        /// </summary>
        All = BackgroundTileMap | WindowTileMap | TileSet | SpriteOam | SpriteTileSet | Scroll | SpriteSize
    }
}