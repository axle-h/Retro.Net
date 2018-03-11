using System;

namespace Retro.Net.Timing
{
    /// <summary>
    /// Instruction timings measured by coarse machine cycles and fine throttling states.
    /// </summary>
    public struct InstructionTimings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionTimings" /> struct.
        /// </summary>
        /// <param name="machineCycles">The machine cycles.</param>
        /// <param name="throttlingStates">The throttling states.</param>
        public InstructionTimings(int machineCycles, int throttlingStates) : this()
        {
            MachineCycles = machineCycles;
            ThrottlingStates = throttlingStates;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionTimings" /> struct.
        /// </summary>
        /// <param name="machineCycles">The machine cycles.</param>
        public InstructionTimings(int machineCycles) : this(machineCycles, (int) Math.Round(machineCycles / 4.0))
        {
        }

        /// <summary>
        /// Gets the machine cycles.
        /// </summary>
        /// <value>
        /// The machine cycles.
        /// </value>
        public int MachineCycles { get; }

        /// <summary>
        /// Gets the throttling states.
        /// </summary>
        /// <value>
        /// The throttling states.
        /// </value>
        public int ThrottlingStates { get; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{nameof(MachineCycles)}: {MachineCycles}, {nameof(ThrottlingStates)}: {ThrottlingStates}";

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="t0">The t0.</param>
        /// <param name="t1">The t1.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static InstructionTimings operator +(InstructionTimings t0, InstructionTimings t1)
        {
            return new InstructionTimings(t0.MachineCycles + t1.MachineCycles, t0.ThrottlingStates + t1.ThrottlingStates);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="t0">The t0.</param>
        /// <param name="t1">The t1.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static InstructionTimings operator -(InstructionTimings t0, InstructionTimings t1)
        {
            return new InstructionTimings(t0.MachineCycles - t1.MachineCycles, t0.ThrottlingStates - t1.ThrottlingStates);
        }
    }
}