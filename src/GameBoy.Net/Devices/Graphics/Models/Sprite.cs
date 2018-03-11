namespace GameBoy.Net.Devices.Graphics.Models
{
    /// <summary>
    /// Sprite structure.
    /// This is only 7 bytes so struct = good.
    /// </summary>
    public struct Sprite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="tileNumber">The tile number.</param>
        /// <param name="backgroundPriority">if set to <c>true</c> [background priority].</param>
        /// <param name="yFlip">if set to <c>true</c> [y flip].</param>
        /// <param name="xFlip">if set to <c>true</c> [x flip].</param>
        /// <param name="usePalette1">if set to <c>true</c> [use palette1].</param>
        public Sprite(byte x, byte y, byte tileNumber, bool backgroundPriority, bool yFlip, bool xFlip, bool usePalette1) : this()
        {
            X = x;
            Y = y;
            TileNumber = tileNumber;
            BackgroundPriority = backgroundPriority;
            YFlip = yFlip;
            XFlip = xFlip;
            UsePalette1 = usePalette1;
        }

        /// <summary>
        /// Gets the X-coordinate of top-left corner.
        /// </summary>
        /// <value>
        /// The X-coordinate of top-left corner.
        /// </value>
        /// <remarks>
        /// Value stored is X-coordinate minus 8.
        /// </remarks>
        public byte X { get; }

        /// <summary>
        /// Gets the Y-coordinate of top-left corner.
        /// </summary>
        /// <value>
        /// The Y-coordinate of top-left corner.
        /// </value>
        /// <remarks>
        /// Value stored is Y-coordinate minus 16.
        /// </remarks>
        public byte Y { get; }

        /// <summary>
        /// Gets the tile number.
        /// </summary>
        /// <value>
        /// The tile number.
        /// </value>
        public byte TileNumber { get; }

        /// <summary>
        /// Gets a value indicating whether the sprite is above or below the background.
        /// </summary>
        /// <value>
        /// <c>true</c> if sprite should be rendered below background (except colour 0); otherwise, <c>false</c>.
        /// </value>
        public bool BackgroundPriority { get; }

        /// <summary>
        /// Gets a value indicating whether the sprite is flipped in the vertical (y) direction.
        /// </summary>
        /// <value>
        /// <c>true</c> if the sprite is flipped in the vertical (y) direction; otherwise, <c>false</c>.
        /// </value>
        public bool YFlip { get; }

        /// <summary>
        /// Gets a value indicating whether the sprite is flipped in the horizontal (x) direction.
        /// </summary>
        /// <value>
        /// <c>true</c> if the sprite is flipped in the horizontal (x) direction; otherwise, <c>false</c>.
        /// </value>
        public bool XFlip { get; }

        /// <summary>
        /// Gets a value indicating whether the sprite should use sprite palette 1 or 0.
        /// </summary>
        /// <value>
        /// <c>true</c> if the sprite should be rendered using sprite palette 1; otherwise, <c>false</c> for sprite palette 0.
        /// </value>
        public bool UsePalette1 { get; }
    }
}