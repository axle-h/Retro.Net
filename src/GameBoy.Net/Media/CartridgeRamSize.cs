namespace GameBoy.Net.Media
{
    /// <summary>
    /// Type of RAM in GameBoy cartridge.
    /// </summary>
    public enum CartridgeRamSize : byte
    {
        /// <summary>
        /// 0 - No cartridge RAM.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// 1 - 16kBit = 2kB = 1 bank.
        /// </summary>
        Fixed2 = 0x01,

        /// <summary>
        /// 2 - 64kBit = 8kB = 1 bank.
        /// </summary>
        Fixed8 = 0x02,

        /// <summary>
        /// 3 - 256kBit = 32kB = 4 banks.
        /// </summary>
        Banked32 = 0x03,

        /// <summary>
        /// 4 - 1MBit = 128kB = 16 banks.
        /// </summary>
        Banked128 = 0x04,

        /// <summary>
        /// 5 - 512Kb = 64KB = 8 banks. Used by Pokemon Crystal.
        /// </summary>
        Banked64 = 0x05
    }
}