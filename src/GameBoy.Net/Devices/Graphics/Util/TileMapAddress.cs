namespace GameBoy.Net.Devices.Graphics.Util
{
    /// <summary>
    /// The address of a tile map.
    /// </summary>
    public struct TileMapAddress
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileMapAddress"/> struct.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="isSigned">if set to <c>true</c> [is signed].</param>
        public TileMapAddress(ushort address, bool isSigned)
        {
            Address = address;
            IsSigned = isSigned;
        }

        /// <summary>
        /// Gets the tile map address.
        /// </summary>
        public ushort Address { get; }

        /// <summary>
        /// Gets a value indicating whether the tile map is signed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the tile map is signed; otherwise, <c>false</c>.
        /// </value>
        public bool IsSigned { get; }

        public bool Equals(TileMapAddress other) => Address == other.Address && IsSigned == other.IsSigned;

        public override bool Equals(object obj) => !(obj is null) && (obj is TileMapAddress address && Equals(address));

        public override int GetHashCode()
        {
            unchecked
            {
                return (Address.GetHashCode() * 397) ^ IsSigned.GetHashCode();
            }
        }
    }
}