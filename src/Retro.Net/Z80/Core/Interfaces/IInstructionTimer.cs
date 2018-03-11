using System;
using System.Threading.Tasks;
using Retro.Net.Timing;

namespace Retro.Net.Z80.Core.Interfaces
{
    /// <summary>
    /// An instruction timer.
    /// </summary>
    public interface IInstructionTimer
    {

        /// <summary>
        /// Gets the timing synchronize observable.
        /// Devices should subscribe to this that wish to be synchronized to the Z80 CPU.
        /// </summary>
        IObservable<InstructionTimings> Timing { get; }

        /// <summary>
        /// Uses the configured instruction timings to sync real time to the CPU.
        /// </summary>
        /// <param name="timings">The timings.</param>
        void SyncToTimings(InstructionTimings timings);

        /// <summary>
        /// Returns a task that will complete in an amount of time according to the specified timings.
        /// </summary>
        /// <param name="timings">The timings.</param>
        /// <returns></returns>
        Task DelayAsync(InstructionTimings timings);

        /// <summary>
        /// Notifies the instruction timer that the CPU has accepted the halt and is halted.
        /// I.e. we'll need to stop waiting for instruction blocks to trigger timing sync events and generate our own fake ones.
        /// </summary>
        void NotifyHalt();

        /// <summary>
        /// Notifies the instruction timer that the CPU has accepted the interrupt and is running again.
        /// I.e. we'll need to stop generating fake timing sync events.
        /// </summary>
        void NotifyResume();
    }
}