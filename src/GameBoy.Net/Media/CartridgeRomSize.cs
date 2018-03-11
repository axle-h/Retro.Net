namespace GameBoy.Net.Media
{
    /// <summary>
    /// Type of ROM in GameBoy cartridge.
    /// </summary>
    public enum CartridgeRomSize : byte
    {
        /// <summary>
        /// 0 - 256Kbit = 32KByte = 2 banks (No MBC needed).
        /// </summary>
        Fixed32 = 0x00,

        /// <summary>
        /// 1 - 512Kbit = 64KByte = 4 banks.
        /// </summary>
        Banked64 = 0x01,

        /// <summary>
        /// 2 - 1Mbit = 128KByte = 8 banks.
        /// </summary>
        Banked128 = 0x02,

        /// <summary>
        /// 3 - 2Mbit = 256KByte = 16 banks.
        /// </summary>
        Banked256 = 0x03,

        /// <summary>
        /// 4 - 4Mbit = 512KByte = 32 banks.
        /// </summary>
        Banked512 = 0x04,

        /// <summary>
        /// 5 - 8Mbit = 1MByte = 64 banks.
        /// </summary>
        Banked1024 = 0x05,

        /// <summary>
        /// 6 - 16Mbit = 2MByte = 128 banks.
        /// </summary>
        Banked2048 = 0x06,

        /// <summary>
        /// 7 - 32Mbit = 4MByte = 256 banks.
        /// </summary>
        Banked4096 = 0x07,

        /// <summary>
        /// 8 - 64Mbit = 8MByte = 512 banks.
        /// </summary>
        Banked8192 = 0x08
    }
}