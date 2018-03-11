namespace GameBoy.Net.Devices.Graphics
{
    /// <summary>
    /// The GPU mode.
    /// </summary>
    public enum GpuMode
    {
        /// <summary>
        /// The LCD controller is in the H-Blank period and the CPU can access both the display RAM (8000h-9FFFh) and OAM
        /// (FE00h-FE9Fh).
        /// </summary>
        HorizontalBlank = 0,

        /// <summary>
        /// The LCD controller is in the V-Blank period (or the display is disabled) and the CPU can access both the display
        /// RAM (8000h-9FFFh) and OAM (FE00h-FE9Fh).
        /// </summary>
        VerticalBlank = 1,

        /// <summary>
        /// The LCD controller is reading from OAM memory. The CPU cannot access OAM memory (FE00h-FE9Fh) during this period.
        /// </summary>
        ReadingOam = 2,

        /// <summary>
        /// The LCD controller is reading from both OAM and VRAM, The CPU cannot access OAM and VRAM during this period. CGB
        /// Mode: Cannot access Palette Data (FF69, FF6B) either.
        /// </summary>
        ReadingVram = 3
    }
}