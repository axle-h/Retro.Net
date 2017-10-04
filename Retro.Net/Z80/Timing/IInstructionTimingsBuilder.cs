using Retro.Net.Timing;

namespace Retro.Net.Z80.Timing
{
    /// <summary>
    /// Builder class for <see cref="InstructionTimings"/>.
    /// This is just to clean up incrementing the cycle counters per instruction. Calls to Add 'should' be inlined by JIT.
    /// </summary>
    public interface IInstructionTimingsBuilder
    {
        /// <summary>
        /// Gets the machine cycles.
        /// </summary>
        /// <value>
        /// The machine cycles.
        /// </value>
        int MachineCycles { get; }

        /// <summary>
        /// Gets the throttling states.
        /// </summary>
        /// <value>
        /// The throttling states.
        /// </value>
        int ThrottlingStates { get; }

        /// <summary>
        /// Adds the specified timings.
        /// </summary>
        /// <param name="mCycles">The machine cycles.</param>
        /// <param name="tStates">The throttling states.</param>
        void Add(int mCycles, int tStates);

        /// <summary>
        /// Gets the instruction timings.
        /// </summary>
        /// <returns></returns>
        InstructionTimings GetInstructionTimings();

        /// <summary>
        /// Resets this instance.
        /// </summary>
        void Reset();

        /// <summary>
        /// Adds standard NOP timings.
        /// </summary>
        /// <returns></returns>
        IInstructionTimingsBuilder Nop();

        /// <summary>
        /// Adds standard MMU single byte access timings.
        /// </summary>
        /// <returns></returns>
        IInstructionTimingsBuilder MmuByte();

        /// <summary>
        /// Adds standard index access timings.
        /// </summary>
        /// <param name="isDisplaced">if set to <c>true</c> [the index is displaced].</param>
        /// <returns></returns>
        IInstructionTimingsBuilder Index(bool isDisplaced);

        /// <summary>
        /// Adds standard index and MMU single byte access timings.
        /// </summary>
        /// <param name="isDisplaced">if set to <c>true</c> [the index is displaced].</param>
        /// <returns></returns>
        IInstructionTimingsBuilder IndexAndMmuByte(bool isDisplaced);

        /// <summary>
        /// Adds standard MMU word access timings.
        /// </summary>
        /// <returns></returns>
        IInstructionTimingsBuilder MmuWord();

        /// <summary>
        /// Adds standard index and MMU word access timings.
        /// </summary>
        /// <returns></returns>
        IInstructionTimingsBuilder IndexAndMmuWord();

        /// <summary>
        /// Adds the specified throttling states.
        /// </summary>
        /// <param name="tStates">The t states.</param>
        /// <returns></returns>
        IInstructionTimingsBuilder Extend(int tStates);

        /// <summary>
        /// Adds standard 16-bit arithmetic timings.
        /// </summary>
        /// <returns></returns>
        IInstructionTimingsBuilder Arithmetic16();

        /// <summary>
        /// Adds standard IO timings.
        /// </summary>
        /// <returns></returns>
        IInstructionTimingsBuilder Io();

        /// <summary>
        /// Adds displacement application timings.
        /// </summary>
        /// <returns></returns>
        IInstructionTimingsBuilder ApplyDisplacement();
    }
}