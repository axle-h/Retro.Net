using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// FF05 - TIMA - Timer counter (R/W)
    /// This timer is incremented by a clock frequency specified by the TAC register($FF07).
    /// When the value overflows(gets bigger than FFh) then it will be reset to the value specified in TMA(FF06), and an interrupt will be requested.
    /// </summary>
    /// <seealso cref="ITimerControlRegister" />
    public class TimerControlRegister : ITimerControlRegister
    {
        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address => 0xff07;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Timer Control (TAC R/W)";

        /// <summary>
        /// Gets or sets the raw register value.
        /// </summary>
        /// <value>
        /// The raw register value.
        /// </value>
        public byte Register
        {
            get
            {
                var value = 0x00;

                switch (TimerFrequency)
                {
                    case 262144:
                        value |= 1;
                        break;
                    case 65536:
                        value |= 2;
                        break;
                    case 16384:
                        value |= 3;
                        break;
                }

                if (TimerEnabled)
                {
                    value |= 4;
                }

                return (byte) value;
            }
            set
            {
                switch (value & 0x3)
                {
                    case 0:
                        TimerFrequency = 4096; // ~4194 Hz SGB
                        break;
                    case 1:
                        TimerFrequency = 262144; // ~268400 Hz SGB
                        break;
                    case 2:
                        TimerFrequency = 65536; // ~67110 Hz SGB
                        break;
                    case 3:
                        TimerFrequency = 16384; // ~16780 Hz SGB
                        break;
                }

                TimerEnabled = (value & 4) == 4;
            }
        }

        /// <summary>
        /// Gets the debug view.
        /// </summary>
        /// <value>
        /// The debug view.
        /// </value>
        public string DebugView => ToString();

        /// <summary>
        /// Gets a value indicating whether [timer enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [timer enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool TimerEnabled { get; private set; }

        /// <summary>
        /// Gets the timer frequency.
        /// </summary>
        /// <value>
        /// The timer frequency.
        /// </value>
        public int TimerFrequency { get; private set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString() => $"{Name} ({Address}) = {Register} ({TimerEnabled}, {TimerFrequency} Hz)";
    }
}
