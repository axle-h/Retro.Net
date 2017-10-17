using System;
using Retro.Net.Z80.Core;

namespace Retro.Net.Wiring
{
    /// <inheritdoc />
    /// <summary>
    /// Factory for building <see cref="T:Retro.Net.Z80.Core.ICpuCore" /> instances.
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    public interface ICpuCoreFactory : IDisposable
    {
        /// <summary>
        /// Gets a new core.
        /// </summary>
        /// <returns></returns>
        ICpuCore GetNewCore();
    }
}
