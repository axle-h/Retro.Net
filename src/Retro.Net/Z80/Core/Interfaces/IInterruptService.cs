namespace Retro.Net.Z80.Core.Interfaces
{
    public interface IInterruptService
    {
        /// <summary>
        /// Interrupts the CPU, pushing all registers to the stack and setting the program counter to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        void Interrupt(ushort address);

        /// <summary>
        /// Gets the interrupted address.
        /// </summary>
        /// <value>
        /// The interrupted address.
        /// </value>
        ushort? InterruptedAddress { get; }

        /// <summary>
        /// Gets a value indicating whether [interrupts enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [interrupts enabled]; otherwise, <c>false</c>.
        /// </value>
        bool InterruptsEnabled { get; set; }

        /// <summary>
        /// Notifies the interrupted.
        /// </summary>
        void NotifyInterrupted();
    }
}