using Retro.Net.Timing;

namespace Retro.Net.Z80.Timing
{
    /// <summary>
    /// Builder class for <see cref="InstructionTimings"/>.
    /// This is just to clean up incrementing the cycle counters per instruction. Calls to Add 'should' be inlined by JIT.
    /// </summary>
    internal class InstructionTimingsBuilder : IInstructionTimingsBuilder
    {
        private const int NopMachineCycles = 1;
        private const int NopThrottlingStates = 4;

        private const int PrefetchMachineCycles = 1;
        private const int PrefetchThrottlingStates = 3;

        private const int PrefetchWordMachineCycles = 2;
        private const int PrefetchWordThrottlingStates = 6;

        private const int IndexMachineCycles = 1;
        private const int IndexThrottlingStates = 3;

        private const int DisplacedIndexMachineCycles = 3;
        private const int DisplacedIndexThrottlingStates = 11;

        private const int Arithmetic16MachineCycles = 2;
        private const int Arithmetic16ThrottlingStates = 7;

        private const int IoMachineCycles = 1;
        private const int IoThrottlingStates = 4;

        private const int ApplyDisplacementMachineCycles = 1;
        private const int ApplyDisplacementThrottlingStates = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionTimingsBuilder"/> class.
        /// </summary>
        public InstructionTimingsBuilder()
        {
            Reset();
        }

        /// <summary>
        /// Gets the machine cycles.
        /// </summary>
        /// <value>
        /// The machine cycles.
        /// </value>
        public int MachineCycles { get; private set; }

        /// <summary>
        /// Gets the throttling states.
        /// </summary>
        /// <value>
        /// The throttling states.
        /// </value>
        public int ThrottlingStates { get; private set; }

        /// <summary>
        /// Adds the specified timings.
        /// </summary>
        /// <param name="mCycles">The machine cycles.</param>
        /// <param name="tStates">The throttling states.</param>
        public void Add(int mCycles, int tStates)
        {
            MachineCycles += mCycles;
            ThrottlingStates += tStates;
        }

        /// <summary>
        /// Adds standard NOP timings.
        /// </summary>
        /// <returns></returns>
        public IInstructionTimingsBuilder Nop()
        {
            MachineCycles += NopMachineCycles;
            ThrottlingStates += NopThrottlingStates;
            return this;
        }

        /// <summary>
        /// Adds standard MMU single byte access timings.
        /// </summary>
        /// <returns></returns>
        public IInstructionTimingsBuilder MmuByte()
        {
            MachineCycles += PrefetchMachineCycles;
            ThrottlingStates += PrefetchThrottlingStates;
            return this;
        }

        /// <summary>
        /// Adds standard MMU word access timings.
        /// </summary>
        /// <returns></returns>
        public IInstructionTimingsBuilder MmuWord()
        {
            MachineCycles += PrefetchWordMachineCycles;
            ThrottlingStates += PrefetchWordThrottlingStates;
            return this;
        }

        /// <summary>
        /// Adds standard index access timings.
        /// </summary>
        /// <param name="isDisplaced">if set to <c>true</c> [the index is displaced].</param>
        /// <returns></returns>
        public IInstructionTimingsBuilder Index(bool isDisplaced)
        {
            MachineCycles += isDisplaced ? DisplacedIndexMachineCycles : IndexMachineCycles;
            ThrottlingStates += isDisplaced ? DisplacedIndexThrottlingStates : IndexThrottlingStates;
            return this;
        }

        /// <summary>
        /// Adds standard index and MMU single byte access timings.
        /// </summary>
        /// <param name="isDisplaced">if set to <c>true</c> [the index is displaced].</param>
        /// <returns></returns>
        public IInstructionTimingsBuilder IndexAndMmuByte(bool isDisplaced)
        {
            // Only add on prefetch timings when not an indexed register
            MachineCycles += isDisplaced ? DisplacedIndexMachineCycles : IndexMachineCycles + PrefetchMachineCycles;
            ThrottlingStates += isDisplaced ? DisplacedIndexThrottlingStates : IndexThrottlingStates + PrefetchThrottlingStates;
            return this;
        }

        /// <summary>
        /// Adds standard index and MMU word access timings.
        /// </summary>
        /// <returns></returns>
        public IInstructionTimingsBuilder IndexAndMmuWord()
        {
            // Only add on prefetch timings when not an indexed register
            MachineCycles += 2 * IndexMachineCycles + PrefetchWordMachineCycles;
            ThrottlingStates += 2 * IndexThrottlingStates + PrefetchWordThrottlingStates;
            return this;
        }

        /// <summary>
        /// Adds the specified throttling states.
        /// </summary>
        /// <param name="tStates">The t states.</param>
        /// <returns></returns>
        public IInstructionTimingsBuilder Extend(int tStates)
        {
            ThrottlingStates += tStates;
            return this;
        }

        /// <summary>
        /// Adds standard 16-bit arithmetic timings.
        /// </summary>
        /// <returns></returns>
        public IInstructionTimingsBuilder Arithmetic16()
        {
            MachineCycles += Arithmetic16MachineCycles;
            ThrottlingStates += Arithmetic16ThrottlingStates;
            return this;
        }

        /// <summary>
        /// Adds standard IO timings.
        /// </summary>
        /// <returns></returns>
        public IInstructionTimingsBuilder Io()
        {
            MachineCycles += IoMachineCycles;
            ThrottlingStates += IoThrottlingStates;
            return this;
        }

        /// <summary>
        /// Adds displacement application timings.
        /// </summary>
        /// <returns></returns>
        public IInstructionTimingsBuilder ApplyDisplacement()
        {
            MachineCycles += ApplyDisplacementMachineCycles;
            ThrottlingStates += ApplyDisplacementThrottlingStates;
            return this;
        }

        /// <summary>
        /// Gets the instruction timings.
        /// </summary>
        /// <returns></returns>
        public InstructionTimings GetInstructionTimings() => new InstructionTimings(MachineCycles, ThrottlingStates);

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            MachineCycles = 0;
            ThrottlingStates = 0;
        }
    }
}