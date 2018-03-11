namespace GameBoy.Net.Media.Interfaces
{
    /// <summary>
    /// A GameBoy cartridge.
    /// </summary>
    public interface ICartridge
    {
        /// <summary>
        /// Gets the ROM banks.
        /// </summary>
        /// <value>
        /// The ROM banks.
        /// </value>
        byte[][] RomBanks { get; }

        /// <summary>
        /// Gets the RAM bank lengths.
        /// </summary>
        /// <value>
        /// The RAM bank lengths.
        /// </value>
        ushort[] RamBankLengths { get; }

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        ICartridgeHeader Header { get; }
    }
}