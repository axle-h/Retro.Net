namespace GameBoy.Net.Media.Interfaces
{
    /// <summary>
    /// A GameBoy cartridge header.
    /// </summary>
    public interface ICartridgeHeader
    {
        /// <summary>
        /// Gets the entry point.
        /// </summary>
        /// <value>
        /// The entry point.
        /// </value>
        byte[] EntryPoint { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        string Title { get; }

        /// <summary>
        /// Gets a value indicating whether this cartridge supports the game boy colour.
        /// </summary>
        /// <value>
        /// <c>true</c> if this cartridge supports the game boy colour; otherwise, <c>false</c>.
        /// </value>
        bool IsGameBoyColour { get; }

        /// <summary>
        /// Gets the license code.
        /// </summary>
        /// <value>
        /// The license code.
        /// </value>
        string LicenseCode { get; }

        /// <summary>
        /// Gets a value indicating whether this cartridge supports the super game boy.
        /// </summary>
        /// <value>
        /// <c>true</c> if this cartridge supports the super game boy; otherwise, <c>false</c>.
        /// </value>
        bool IsSuperGameBoy { get; }

        /// <summary>
        /// Gets the type of the cartridge.
        /// </summary>
        /// <value>
        /// The type of the cartridge.
        /// </value>
        CartridgeType CartridgeType { get; }

        /// <summary>
        /// Gets the size of the ROM.
        /// </summary>
        /// <value>
        /// The size of the ROM.
        /// </value>
        CartridgeRomSize RomSize { get; }

        /// <summary>
        /// Gets the size of the RAM.
        /// </summary>
        /// <value>
        /// The size of the RAM.
        /// </value>
        CartridgeRamSize RamSize { get; }

        /// <summary>
        /// Gets the destination code.
        /// </summary>
        /// <value>
        /// The destination code.
        /// </value>
        DestinationCode DestinationCode { get; }

        /// <summary>
        /// Gets the ROM version.
        /// </summary>
        /// <value>
        /// The ROM version.
        /// </value>
        byte RomVersion { get; }

        /// <summary>
        /// Gets the header checksum.
        /// </summary>
        /// <value>
        /// The header checksum.
        /// </value>
        byte HeaderChecksum { get; }

        /// <summary>
        /// Gets the ROM checksum.
        /// </summary>
        /// <value>
        /// The ROM checksum.
        /// </value>
        ushort RomChecksum { get; }
    }
}