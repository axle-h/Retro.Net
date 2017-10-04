namespace Retro.Net.Z80.Config
{
    /// <summary>
    /// What shall the decoder do when handling an undefined instruction?
    /// </summary>
    public enum UndefinedInstructionBehaviour
    {
        /// <summary>
        /// Decode to NOP operation.
        /// </summary>
        Nop = 1,

        /// <summary>
        /// Decode to HALT operation.
        /// </summary>
        Halt = 2,

        /// <summary>
        /// Throw an exception.
        /// </summary>
        Throw = 3
    }
}
