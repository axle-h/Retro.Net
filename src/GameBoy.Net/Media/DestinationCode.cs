namespace GameBoy.Net.Media
{
    /// <summary>
    /// GameBoy cartridge header destination code.
    /// </summary>
    public enum DestinationCode : byte
    {
        /// <summary>
        /// Japanese region.
        /// </summary>
        Japanese = 0x00,

        /// <summary>
        /// Non-Japanese region.
        /// </summary>
        NonJapanese = 0x01
    }
}