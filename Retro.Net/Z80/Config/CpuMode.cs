namespace Retro.Net.Z80.Config
{
    /// <summary>
    /// The CPU mode.
    /// This library can emulate some derrivatives of the Z80.
    /// </summary>
    public enum CpuMode
    {
        /// <summary>
        /// An Intel 8080 CPU.
        /// </summary>
        Intel8080,

        /// <summary>
        /// The GameBoy CPU (Sharp LR35902).
        /// This sits somewhere between an 8080 and Z80.
        /// </summary>
        GameBoy,

        /// <summary>
        /// A Zilog Z80 CPU.
        /// </summary>
        Z80
    }
}