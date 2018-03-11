using System;
using System.IO;
using System.Linq;
using GameBoy.Net.Media.Interfaces;
using Retro.Net.Util;

namespace GameBoy.Net.Media
{
    /// <summary>
    /// Factory for building <see cref="ICartridge"/> instances from raw bytes.
    /// </summary>
    public class CartridgeFactory : ICartridgeFactory
    {
        private const int RomBankLength = 0x4000;
        private const ushort HeaderStart = 0x0100;

        /// <summary>
        /// Gets the cartridge from the specified bytes.
        /// </summary>
        /// <param name="cartridge">The cartridge.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Invalid cartridge length
        /// or
        /// Number of banks must be even
        /// or
        /// No valid entrypoint in cartridge
        /// or
        /// Cartridge type does not match number of ROM banks
        /// </exception>
        public ICartridge GetCartridge(byte[] cartridge)
        {
            var header = GetCartridgeHeader(cartridge);
            var numberOfBanks = cartridge.Length / RomBankLength;

            if (cartridge.Length % RomBankLength != 0)
            {
                throw new ArgumentException("Invalid cartridge length");
            }

            if (numberOfBanks % 2 != 0)
            {
                throw new ArgumentException("Number of banks must be even");
            }

            // EntryPoint should not be all 0's or ff's
            if (header.EntryPoint.All(x => x == 0 || x == 0xff))
            {
                throw new ArgumentException("No valid entrypoint in cartridge");
            }

            if (header.RomSize.NumberOfBanks() != numberOfBanks)
            {
                throw new ArgumentException($"Cartridge type {header.RomSize} does not match number of ROM banks: " + numberOfBanks);
            }

            var banks = new byte[numberOfBanks][];
            for (var n = 0; n < numberOfBanks; n++)
            {
                var bank = new byte[RomBankLength];
                Array.Copy(cartridge, n * RomBankLength, bank, 0, RomBankLength);
                banks[n] = bank;
            }

            var ramBanks = Enumerable.Range(0, header.RamSize.NumberOfBanks()).Select(i => header.RamSize.BankSize()).ToArray();

            return new Cartridge(banks, ramBanks, header);
        }

        /// <summary>
        /// Gets the cartridge header.
        /// </summary>
        /// <param name="cartridgeBytes">The cartridge bytes.</param>
        /// <returns></returns>
        private static ICartridgeHeader GetCartridgeHeader(byte[] cartridgeBytes)
        {
            using (var stream = new MemoryStream(cartridgeBytes))
            {
                stream.Seek(HeaderStart, SeekOrigin.Begin);

                var entryPoint = stream.ReadBuffer(4);
                
                // Read over copyright header.
                stream.ReadBuffer(0x30);

                var title = stream.ReadAscii(15).Trim();
                var isGameBoyColour = stream.ReadByte() == 0x80;
                var newLicenseCode = new string(stream.ReadAscii(2).Reverse().ToArray());
                var isSuperGameBoy = stream.ReadByte() == 0x03;
                var cartridgeType = stream.ReadEnum<CartridgeType>();
                var romSize = stream.ReadEnum<CartridgeRomSize>();
                var ramSize = stream.ReadEnum<CartridgeRamSize>();
                var destinationCode = stream.ReadEnum<DestinationCode>();
                var oldLicenseCode = (byte) stream.ReadByte();
                var romVersion = (byte) stream.ReadByte();
                var headerChecksum = (byte) stream.ReadByte();
                var romChecksum = stream.ReadBigEndianUInt16();

                string licenseCode = null;
                switch (oldLicenseCode)
                {
                    case 0x79:
                        licenseCode = "Accolade";
                        break;
                    case 0xa4:
                        licenseCode = "Konami";
                        break;
                    case 0x33:
                        licenseCode = newLicenseCode;
                        break;
                }

                return new CartridgeHeader(entryPoint,
                                           title,
                                           isGameBoyColour,
                                           licenseCode,
                                           isSuperGameBoy,
                                           cartridgeType,
                                           romSize,
                                           ramSize,
                                           destinationCode,
                                           romVersion,
                                           headerChecksum,
                                           romChecksum);
            }
        }
    }
}