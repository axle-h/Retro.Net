namespace GameBoy.Net.Registers.Interfaces
{
    /// <summary>
    /// FF05 - TIMA - Timer counter (R/W)
    /// This timer is incremented by a clock frequency specified by the TAC register($FF07).
    /// When the value overflows(gets bigger than FFh) then it will be reset to the value specified in TMA(FF06), and an interrupt will be requested.
    /// </summary>
    /// <seealso cref="IRegister" />
    public interface ITimerControlRegister : IRegister
    {
        /// <summary>
        /// Gets a value indicating whether [timer enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [timer enabled]; otherwise, <c>false</c>.
        /// </value>
        bool TimerEnabled { get; }

        /// <summary>
        /// Gets the timer frequency.
        /// </summary>
        /// <value>
        /// The timer frequency.
        /// </value>
        int TimerFrequency { get; }
    }
}
