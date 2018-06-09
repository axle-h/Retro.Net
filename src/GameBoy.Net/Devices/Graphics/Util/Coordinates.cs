namespace GameBoy.Net.Devices.Graphics.Util
{
    /// <summary>
    /// Cartesian 2D coordinates.
    /// </summary>
    public struct Coordinates
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinates"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the x coordinate.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the y coordinate.
        /// </summary>
        public int Y { get; }

        public bool Equals(Coordinates other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) => !(obj is null) && obj is Coordinates coordinates && Equals(coordinates);

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}