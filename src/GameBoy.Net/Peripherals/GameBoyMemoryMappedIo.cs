using System;
using System.Collections.Generic;
using System.Linq;
using GameBoy.Net.Devices;
using GameBoy.Net.Devices.Interfaces;
using GameBoy.Net.Registers.Interfaces;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Z80.Peripherals;

namespace GameBoy.Net.Peripherals
{
    /// <summary>
    /// The GameBoy IO interface peripheral.
    /// </summary>
    /// <seealso cref="IGameBoyMemoryMappedIo" />
    public class GameBoyMemoryMappedIo : IGameBoyMemoryMappedIo
    {
        private readonly IInterruptEnableRegister _interruptRegister;

        private readonly IMemoryBankController _memoryBankController;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoyMemoryMappedIo" /> class.
        /// </summary>
        /// <param name="hardwareRegisters">The hardware registers.</param>
        /// <param name="interruptRegister">The interrupt register.</param>
        /// <param name="gpu">The gpu.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="memoryBankController">The memory bank controller.</param>
        public GameBoyMemoryMappedIo(IHardwareRegisters hardwareRegisters,
            IInterruptEnableRegister interruptRegister,
            IGpu gpu,
            IRenderer renderer,
            IMemoryBankController memoryBankController)
        {
            HardwareRegisters = hardwareRegisters;
            _interruptRegister = interruptRegister;
            Gpu = gpu;
            Renderer = renderer;
            _memoryBankController = memoryBankController;
        }

        /// <summary>
        /// Sends the specified signal to the peripheral.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">null</exception>
        public void Signal(ControlSignal signal)
        {
            switch (signal)
            {
                case ControlSignal.Halt:
                    Gpu.Halt();
                    break;
                case ControlSignal.Resume:
                    Gpu.Resume();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(signal), signal, null);
            }
        }

        /// <summary>
        /// Gets the address segments.
        /// </summary>
        /// <value>
        /// The address segments.
        /// </value>
        public IEnumerable<IAddressSegment> AddressSegments
            =>
                new IAddressSegment[] { HardwareRegisters, _interruptRegister, _memoryBankController }.Concat(Gpu.AddressSegments)
                                                                                                      .ToArray();

        /// <summary>
        /// Gets the hardware registers.
        /// </summary>
        /// <value>
        /// The hardware registers.
        /// </value>
        public IHardwareRegisters HardwareRegisters { get; }

        /// <summary>
        /// Gets the GPU.
        /// </summary>
        /// <value>
        /// The GPU.
        /// </value>
        public IGpu Gpu { get; }

        /// <summary>
        /// Gets the renderer.
        /// </summary>
        /// <value>
        /// The renderer.
        /// </value>
        public IRenderer Renderer { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => Gpu.Dispose();
    }
}