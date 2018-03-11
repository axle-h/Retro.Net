namespace GameBoy.Net.Media.Interfaces
{
    /// <summary>
    /// Factory for building <see cref="ICartridge"/> instances from raw bytes.
    /// </summary>
    public interface ICartridgeFactory
    {
        /// <summary>
        /// Gets the cartridge from the specified bytes.
        /// </summary>
        /// <param name="cartridge">The cartridge.</param>
        /// <returns></returns>
        ICartridge GetCartridge(byte[] cartridge);
    }
}