using System.Collections.Generic;
using Retro.Net.Config;

namespace Retro.Net.Z80.Config
{
    /// <summary>
    /// The platform configuration.
    /// </summary>
    public interface IPlatformConfig
    {
        /// <summary>
        /// Gets the cpu mode.
        /// </summary>
        /// <value>
        /// The cpu mode.
        /// </value>
        CpuMode CpuMode { get; }

        /// <summary>
        /// Gets the memory banks.
        /// </summary>
        /// <value>
        /// The memory banks.
        /// </value>
        IEnumerable<IMemoryBankConfig> MemoryBanks { get; }

        /// <summary>
        /// Gets the machine cycle speed in MHZ.
        /// </summary>
        /// <value>
        /// The machine cycle speed in MHZ.
        /// </value>
        double MachineCycleFrequencyMhz { get; }

        /// <summary>
        /// Gets the instruction timing synchronize mode.
        /// </summary>
        /// <value>
        /// The instruction timing synchronize mode.
        /// </value>
        InstructionTimingSyncMode InstructionTimingSyncMode { get; }

        /// <summary>
        /// Gets the undefined instruction behaviour.
        /// </summary>
        /// <value>
        /// The undefined instruction behaviour.
        /// </value>
        UndefinedInstructionBehaviour UndefinedInstructionBehaviour { get; }
    }
}