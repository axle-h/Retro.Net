using System;
using System.Collections.Generic;
using GameBoy.Net.Devices.Graphics.Models;
using Retro.Net.Memory.Interfaces;

namespace GameBoy.Net.Devices.Interfaces
{
    /// <summary>
    /// The GameBoy GPU.
    /// </summary>
    public interface IGpu : IDisposable
    {
        /// <summary>
        /// Gets the address segments.
        /// </summary>
        /// <value>
        /// The address segments.
        /// </value>
        IEnumerable<IAddressSegment> AddressSegments { get; }

        /// <summary>
        /// Halts the GPU thread.
        /// </summary>
        void Halt();

        /// <summary>
        /// Resumes the GPU thread.
        /// </summary>
        void Resume();

        /// <summary>
        /// Creates a new state object that describes the GPU.
        /// </summary>
        /// <returns></returns>
        GpuState CreateState();
    }
}