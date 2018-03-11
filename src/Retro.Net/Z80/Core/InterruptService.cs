using Retro.Net.Z80.Core.Interfaces;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.Core
{
    public class InterruptService : IInterruptService
    {
        private readonly IRegisters _registers;


        /// <summary>
        /// Initializes a new instance of the <see cref="InterruptService"/> class.
        /// </summary>
        /// <param name="registers">The registers.</param>
        public InterruptService(IRegisters registers)
        {
            _registers = registers;
        }

        /// <summary>
        /// Interrupts the CPU, pushing all registers to the stack and setting the program counter to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        public void Interrupt(ushort address)
        {
            if (InterruptsEnabled)
            {
                InterruptedAddress = address;
            }
        }

        /// <summary>
        /// Notifies the interrupted.
        /// </summary>
        public void NotifyInterrupted() => InterruptedAddress = null;

        /// <summary>
        /// Gets the interrupted address.
        /// </summary>
        /// <value>
        /// The interrupted address.
        /// </value>
        public ushort? InterruptedAddress { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [interrupts enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [interrupts enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool InterruptsEnabled
        {
            get => _registers.InterruptFlipFlop1;
            set => _registers.InterruptFlipFlop1 = value;
        }
    }
}
