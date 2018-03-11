using GameBoy.Net.Devices;
using GameBoy.Net.Devices.Interfaces;
using Retro.Net.Z80.Peripherals;

namespace GameBoy.Net.Peripherals
{
    /// <summary>
    /// The GameBoy IO interface peripheral.
    /// </summary>
    /// <seealso cref="Retro.Net.Z80.Contracts.Peripherals.IMemoryMappedPeripheral" />
    public interface IGameBoyMemoryMappedIo : IMemoryMappedPeripheral
    {
        /// <summary>
        /// Gets the hardware registers.
        /// </summary>
        /// <value>
        /// The hardware registers.
        /// </value>
        IHardwareRegisters HardwareRegisters { get; }

        /// <summary>
        /// Gets the GPU.
        /// </summary>
        /// <value>
        /// The GPU.
        /// </value>
        IGpu Gpu { get; }

        /// <summary>
        /// Gets the renderer.
        /// </summary>
        /// <value>
        /// The renderer.
        /// </value>
        IRenderer Renderer { get; }
    }
}