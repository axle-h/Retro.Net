using System;
using System.Collections.Generic;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Timing;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Core.Interfaces;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.Core
{
    /// <summary>
    /// An instruction block.
    /// </summary>
    /// <seealso cref="IInstructionBlock" />
    internal class InstructionBlock : IInstructionBlock
    {
        private readonly Func<IRegisters, IMmu, IAlu, IPeripheralManager, InstructionTimings> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionBlock" /> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="length">The length.</param>
        /// <param name="action">The action.</param>
        /// <param name="staticTimings">The static timings.</param>
        /// <param name="halt">if set to <c>true</c> [halt].</param>
        /// <param name="stop">if set to <c>true</c> [stop].</param>
        /// <param name="operations">The operations.</param>
        /// <param name="debugInfo">The debug information.</param>
        public InstructionBlock(ushort address,
                                int length,
                                Func<IRegisters, IMmu, IAlu, IPeripheralManager, InstructionTimings> action,
                                InstructionTimings staticTimings,
                                bool halt,
                                bool stop,
                                ICollection<Operation> operations,
                                string debugInfo)
        {
            _action = action;
            Address = address;
            StaticTimings = staticTimings;
            Length = length;
            HaltCpu = halt || stop;
            HaltPeripherals = stop;
            Operations = operations;
            DebugInfo = debugInfo;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        public ushort Address { get; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets a value indicating whether [to halt the cpu at the end of the block].
        /// </summary>
        public bool HaltCpu { get; }

        /// <summary>
        /// Gets a value indicating whether [to halt peripherals at the end of the block].
        /// </summary>
        public bool HaltPeripherals { get; }

        /// <summary>
        /// Static instruction timings, known at compile time
        /// </summary>
        public InstructionTimings StaticTimings { get; }

        /// <summary>
        /// Gets the debug information.
        /// This is only populated when debug mode is enabled in config.
        /// </summary>
        public string DebugInfo { get; }

        /// <summary>
        /// Gets the operations.
        /// This is only populated when debug mode is enabled in config.
        /// </summary>
        public ICollection<Operation> Operations { get; }

        /// <summary>
        /// Executes the instruction block.
        /// </summary>
        /// <param name="registers">The registers.</param>
        /// <param name="mmu">The mmu.</param>
        /// <param name="alu">The alu.</param>
        /// <param name="peripheralManager">The peripheral manager.</param>
        /// <returns></returns>
        public InstructionTimings ExecuteInstructionBlock(IRegisters registers,
            IMmu mmu,
            IAlu alu,
            IPeripheralManager peripheralManager) => _action(registers, mmu, alu, peripheralManager) + StaticTimings;
    }
}