using GameBoy.Net.Media.Interfaces;

namespace GameBoy.Net.Media
{
    /// <summary>
    /// A GameBoy cartridge.
    /// </summary>
    /// <seealso cref="ICartridge" />
    internal class Cartridge : ICartridge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cartridge"/> class.
        /// </summary>
        /// <param name="romBanks">The rom banks.</param>
        /// <param name="ramBankLengths">The ram bank lengths.</param>
        /// <param name="header">The header.</param>
        public Cartridge(byte[][] romBanks, ushort[] ramBankLengths, ICartridgeHeader header)
        {
            RomBanks = romBanks;
            RamBankLengths = ramBankLengths;
            Header = header;
        }

        /// <summary>
        /// Gets the ROM banks.
        /// </summary>
        /// <value>
        /// The ROM banks.
        /// </value>
        public byte[][] RomBanks { get; }

        /// <summary>
        /// Gets the RAM bank lengths.
        /// </summary>
        /// <value>
        /// The RAM bank lengths.
        /// </value>
        public ushort[] RamBankLengths { get; }

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public ICartridgeHeader Header { get; }
    }
}