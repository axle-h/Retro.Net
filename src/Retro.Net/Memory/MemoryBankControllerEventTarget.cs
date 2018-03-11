namespace Retro.Net.Memory
{
    /// <summary>
    /// The target of a memory bank switch.
    /// </summary>
    public enum MemoryBankControllerEventTarget
    {
        /// <summary>
        /// A RAM bank switch has occurred.
        /// </summary>
        RamBankSwitch,

        /// <summary>
        /// A ROM bank switch has occurred.
        /// </summary>
        RomBankSwitch,

        /// <summary>
        /// RAM has been enabled.
        /// </summary>
        RamEnable
    }
}