using System;
using Retro.Net.Z80.Core;

namespace Retro.Net.Z80.Cache
{
    /// <summary>
    /// An instruction block cache.
    /// Implementation notice: this doesn't need to be thread safe.
    /// </summary>
    public interface IInstructionBlockCache
    {
        /// <summary>
        /// Get an instruction block from the cache at address. If not present then call getInstanceFunc and add to the cache.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="getInstanceFunc"></param>
        /// <returns></returns>
        IInstructionBlock GetOrSet(ushort address, Func<IInstructionBlock> getInstanceFunc);

        /// <summary>
        /// Invalidates all cache from address for length
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        void InvalidateCache(ushort address, ushort length);
    }
}