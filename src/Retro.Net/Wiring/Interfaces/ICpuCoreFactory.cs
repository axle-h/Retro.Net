using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Wiring.Interfaces
{
    /// <summary>
    /// Factory for building <see cref="T:Retro.Net.Z80.Core.ICpuCore" /> instances.
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    public interface ICpuCoreFactory
    {
        /// <summary>
        /// Gets a new core.
        /// </summary>
        /// <returns></returns>
        ICpuCore GetNewCore();
    }
}
