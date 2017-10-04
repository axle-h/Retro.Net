namespace Retro.Net.Z80.Config
{
    /// <summary>
    /// The instruction timing sync mode.
    /// </summary>
    public enum InstructionTimingSyncMode
    {
        /// <summary>
        /// Do no timing sync, simulation will run as fast as possible
        /// </summary>
        Null,

        /// <summary>
        /// Sync to machine cycles
        /// </summary>
        MachineCycles,
    }
}