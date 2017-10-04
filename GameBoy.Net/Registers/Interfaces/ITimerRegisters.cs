namespace GameBoy.Net.Registers.Interfaces
{
    /// <summary>
    /// Timer registers.
    /// </summary>
    public interface ITimerRegisters
    {
        /// <summary>
        /// Gets the timer control register.
        /// </summary>
        /// <value>
        /// The timer control register.
        /// </value>
        ITimerControlRegister TimerControlRegister { get; }

        /// <summary>
        /// Gets the timer modulo register.
        /// </summary>
        /// <value>
        /// The timer modulo register.
        /// </value>
        IRegister TimerModuloRegister { get; }

        /// <summary>
        /// Gets the timer counter register.
        /// </summary>
        /// <value>
        /// The timer counter register.
        /// </value>
        IRegister TimerCounterRegister { get; }
    }
}
