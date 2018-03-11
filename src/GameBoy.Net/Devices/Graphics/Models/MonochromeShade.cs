namespace GameBoy.Net.Devices.Graphics.Models
{
    /// <summary>
    /// 4 shades of... gray (!)
    /// </summary>
    public enum MonochromeShade : byte
    {
        /// <summary>
        /// White.
        /// </summary>
        White = 0x0,

        /// <summary>
        /// Light gray.
        /// </summary>
        LightGray = 0x1,

        /// <summary>
        /// Dark gray.
        /// </summary>
        DarkGray = 0x2,

        /// <summary>
        /// Black.
        /// </summary>
        Black = 0x3
    }
}