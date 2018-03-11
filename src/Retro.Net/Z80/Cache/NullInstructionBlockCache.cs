using System;
using Retro.Net.Z80.Cache.Interfaces;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Z80.Cache
{
    /// <summary>
    /// An instruction block cache that does nothing.
    /// </summary>
    /// <seealso cref="Retro.Net.Z80.Cache.Interfaces.IInstructionBlockCache" />
    public class NullInstructionBlockCache : IInstructionBlockCache
    {
        /// <summary>
        /// Get an instruction block from the cache at address. If not present then call getInstanceFunc and add to the cache.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="getInstanceFunc"></param>
        /// <returns></returns>
        public IInstructionBlock GetOrSet(ushort address, Func<IInstructionBlock> getInstanceFunc) => getInstanceFunc();

        /// <summary>
        /// Invalidates all cache from address for length
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        public void InvalidateCache(ushort address, int length)
        {
            // Nothing to invalidate.
        }
    }
}
