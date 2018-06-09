using System.Collections.Generic;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Timing;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.Core.Interfaces
{
    /// <summary>
    /// An instruction block.
    /// </summary>
    public interface IInstructionBlock
    {
        /// <summary>
        /// Gets the address.
        /// </summary>
        ushort Address { get; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets a value indicating whether [to halt the cpu at the end of the block].
        /// </summary>
        bool HaltCpu { get; }

        /// <summary>
        /// Gets a value indicating whether [to halt peripherals at the end of the block].
        /// </summary>
        bool HaltPeripherals { get; }

        /// <summary>
        /// Gets the debug information.
        /// This is only populated when debug mode is enabled in config.
        /// </summary>
        string DebugInfo { get; }

        /// <summary>
        /// Static instruction timings, known at compile time
        /// </summary>
        InstructionTimings StaticTimings { get; }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        ICollection<Operation> Operations { get; }

        /// <summary>
        /// Executes the instruction block.
        /// </summary>
        /// <param name="registers">The registers.</param>
        /// <param name="mmu">The mmu.</param>
        /// <param name="alu">The alu.</param>
        /// <param name="peripheralManager">The peripheral manager.</param>
        /// <returns></returns>
        InstructionTimings ExecuteInstructionBlock(IRegisters registers, IMmu mmu, IAlu alu, IPeripheralManager peripheralManager);
    }
}