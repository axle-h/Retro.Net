using GameBoy.Net.Media.Interfaces;

namespace GameBoy.Net.Media
{
    /// <summary>
    /// A GameBoy cartridge header.
    /// </summary>
    /// <seealso cref="ICartridgeHeader" />
    internal class CartridgeHeader : ICartridgeHeader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartridgeHeader"/> class.
        /// </summary>
        /// <param name="entryPoint">The entry point.</param>
        /// <param name="title">The title.</param>
        /// <param name="isGameBoyColour">if set to <c>true</c> [supports game boy colour].</param>
        /// <param name="licenseCode">The license code.</param>
        /// <param name="isSuperGameBoy">if set to <c>true</c> [supports super game boy].</param>
        /// <param name="cartridgeType">Type of the cartridge.</param>
        /// <param name="romSize">Size of the rom.</param>
        /// <param name="ramSize">Size of the ram.</param>
        /// <param name="destinationCode">The destination code.</param>
        /// <param name="romVersion">The rom version.</param>
        /// <param name="headerChecksum">The header checksum.</param>
        /// <param name="romChecksum">The rom checksum.</param>
        public CartridgeHeader(byte[] entryPoint,
            string title,
            bool isGameBoyColour,
            string licenseCode,
            bool isSuperGameBoy,
            CartridgeType cartridgeType,
            CartridgeRomSize romSize,
            CartridgeRamSize ramSize,
            DestinationCode destinationCode,
            byte romVersion,
            byte headerChecksum,
            ushort romChecksum)
        {
            EntryPoint = entryPoint;
            Title = title;
            IsGameBoyColour = isGameBoyColour;
            LicenseCode = licenseCode;
            IsSuperGameBoy = isSuperGameBoy;
            CartridgeType = cartridgeType;
            RomSize = romSize;
            RamSize = ramSize;
            DestinationCode = destinationCode;
            RomVersion = romVersion;
            HeaderChecksum = headerChecksum;
            RomChecksum = romChecksum;
        }

        /// <summary>
        /// Gets the entry point.
        /// </summary>
        /// <value>
        /// The entry point.
        /// </value>
        public byte[] EntryPoint { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; }

        /// <summary>
        /// Gets a value indicating whether this cartridge supports the game boy colour.
        /// </summary>
        /// <value>
        /// <c>true</c> if this cartridge supports the game boy colour; otherwise, <c>false</c>.
        /// </value>
        public bool IsGameBoyColour { get; }

        /// <summary>
        /// Gets the license code.
        /// </summary>
        /// <value>
        /// The license code.
        /// </value>
        public string LicenseCode { get; }

        /// <summary>
        /// Gets a value indicating whether this cartridge supports the super game boy.
        /// </summary>
        /// <value>
        /// <c>true</c> if this cartridge supports the super game boy; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuperGameBoy { get; }

        /// <summary>
        /// Gets the type of the cartridge.
        /// </summary>
        /// <value>
        /// The type of the cartridge.
        /// </value>
        public CartridgeType CartridgeType { get; }

        /// <summary>
        /// Gets the size of the ROM.
        /// </summary>
        /// <value>
        /// The size of the ROM.
        /// </value>
        public CartridgeRomSize RomSize { get; }

        /// <summary>
        /// Gets the size of the RAM.
        /// </summary>
        /// <value>
        /// The size of the RAM.
        /// </value>
        public CartridgeRamSize RamSize { get; }

        /// <summary>
        /// Gets the destination code.
        /// </summary>
        /// <value>
        /// The destination code.
        /// </value>
        public DestinationCode DestinationCode { get; }

        /// <summary>
        /// Gets the ROM version.
        /// </summary>
        /// <value>
        /// The ROM version.
        /// </value>
        public byte RomVersion { get; }

        /// <summary>
        /// Gets the header checksum.
        /// </summary>
        /// <value>
        /// The header checksum.
        /// </value>
        public byte HeaderChecksum { get; }

        /// <summary>
        /// Gets the ROM checksum.
        /// </summary>
        /// <value>
        /// The ROM checksum.
        /// </value>
        public ushort RomChecksum { get; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
            =>
                $"Title: {Title}, IsGameBoyColour: {IsGameBoyColour}, LicenseCode: {LicenseCode}, IsSuperGameBoy: {IsSuperGameBoy}, CartridgeType: {CartridgeType}, RomSize: {RomSize}, RamSize: {RamSize}, DestinationCode: {DestinationCode}, RomVersion: {RomVersion}, HeaderChecksum: {HeaderChecksum}, RomChecksum: {RomChecksum}";
    }
}