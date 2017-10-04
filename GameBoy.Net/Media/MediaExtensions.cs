using System;

namespace GameBoy.Net.Media
{
    /// <summary>
    /// Extensions for media enum types.
    /// </summary>
    public static class MediaExtensions
    {
        /// <summary>
        /// Gets the number of ROM banks required by the specified cartridge ROM size.
        /// </summary>
        /// <param name="cartridgeRomSize">Size of the cartridge rom.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException">RomSize:  + cartridgeRomSize</exception>
        public static int NumberOfBanks(this CartridgeRomSize cartridgeRomSize)
        {
            switch (cartridgeRomSize)
            {
                case CartridgeRomSize.Fixed32:
                    return 2;
                case CartridgeRomSize.Banked64:
                    return 4;
                case CartridgeRomSize.Banked128:
                    return 8;
                case CartridgeRomSize.Banked256:
                    return 16;
                case CartridgeRomSize.Banked512:
                    return 32;
                case CartridgeRomSize.Banked1024:
                    return 64;
                case CartridgeRomSize.Banked2048:
                    return 128;
                case CartridgeRomSize.Banked4096:
                    return 256;
                case CartridgeRomSize.Banked8192:
                    return 512;
                default:
                    throw new NotSupportedException("RomSize: " + cartridgeRomSize);
            }
        }

        /// <summary>
        /// Gets the number of RAM banks required by the specified cartridge RAM size.
        /// </summary>
        /// <param name="cartridgeRamSize">Size of the cartridge ram.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException">RamSize:  + cartridgeRamSize</exception>
        public static int NumberOfBanks(this CartridgeRamSize cartridgeRamSize)
        {
            switch (cartridgeRamSize)
            {
                case CartridgeRamSize.None:
                    return 0;
                case CartridgeRamSize.Fixed2:
                    return 1;
                case CartridgeRamSize.Fixed8:
                    return 1;
                case CartridgeRamSize.Banked32:
                    return 4;
                case CartridgeRamSize.Banked128:
                    return 16;
                default:
                    throw new NotSupportedException("RamSize: " + cartridgeRamSize);
            }
        }

        /// <summary>
        /// Gets the RAM bank size value of the specified cartridge RAM size.
        /// </summary>
        /// <param name="cartridgeRamSize">Size of the cartridge ram.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException">RamSize:  + cartridgeRamSize</exception>
        public static ushort BankSize(this CartridgeRamSize cartridgeRamSize)
        {
            switch (cartridgeRamSize)
            {
                case CartridgeRamSize.None:
                    return 0;
                case CartridgeRamSize.Fixed2:
                    return 0x800;
                case CartridgeRamSize.Fixed8:
                case CartridgeRamSize.Banked32:
                case CartridgeRamSize.Banked128:
                    return 0x2000;
                default:
                    throw new NotSupportedException("RamSize: " + cartridgeRamSize);
            }
        }
    }
}