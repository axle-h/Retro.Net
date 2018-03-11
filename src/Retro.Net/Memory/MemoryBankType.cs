namespace Retro.Net.Memory
{
    /// <summary>
    /// A memory bank type.
    /// </summary>
    public enum MemoryBankType
    {
        /// <summary>
        /// An unused memory bank.
        /// </summary>
        Unused,

        /// <summary>
        /// A random access memory bank (RAM).
        /// </summary>
        RandomAccessMemory,

        /// <summary>
        /// A read only memory bank (ROM).
        /// </summary>
        ReadOnlyMemory,

        /// <summary>
        /// A peripheral e.g. a hardware register.
        /// </summary>
        Peripheral,

        /// <summary>
        /// A read only memory bank (ROM) with banking switching support.
        /// </summary>
        BankedReadOnlyMemory
    }
}