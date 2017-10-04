using System;
using System.Threading.Tasks;

namespace Retro.Net.Z80.Core
{
    /// <summary>
    /// The interrupt manager.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IInterruptManager : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the CPU is halted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the CPU is halted; otherwise, <c>false</c>.
        /// </value>
        bool IsHalted { get; }

        /// <summary>
        /// Gets a value indicating whether the CPU is interrupted.
        /// </summary>
        /// <value>
        /// <c>true</c> if the CPU is interrupted; otherwise, <c>false</c>.
        /// </value>
        bool IsInterrupted { get; }

        /// <summary>
        /// Adds a task to run when the CPU is resumed from a halt state.
        /// </summary>
        /// <param name="task">The task.</param>
        void AddResumeTask(Action task);

        /// <summary>
        /// Notifies this interrupt manager that the CPU has accepted the halt and is halted.
        /// </summary>
        void NotifyHalt();

        /// <summary>
        /// Notifies this interrupt manager that the CPU has accepted the interrupt and is running again.
        /// </summary>
        void NotifyResume();

        /// <summary>
        /// Gets a task that will wait for next interrupt.
        /// </summary>
        /// <returns></returns>
        Task<ushort> WaitForNextInterrupt();

        /// <summary>
        /// Gets a value indicating whether [interrupts enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [interrupts enabled]; otherwise, <c>false</c>.
        /// </value>
        bool InterruptsEnabled { get; }

        /// <summary>
        /// Interrupts the CPU, pushing all registers to the stack and setting the program counter to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        void Interrupt(ushort address);

        /// <summary>
        /// Halts the CPU.
        /// </summary>
        void Halt();

        /// <summary>
        /// Resumes the CPU from a halt.
        /// </summary>
        void Resume();
    }
}