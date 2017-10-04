using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Memory;
using Retro.Net.Z80.Core;

namespace Retro.Net.Z80.Cache
{
    /// <summary>
    /// An instruction block cache.
    /// </summary>
    public class InstructionBlockCache : IInstructionBlockCache
    {
        private readonly ConcurrentDictionary<ushort, ICachedInstructionBlock> _cache;

        private ICollection<AddressRange> _cachedRanges;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionBlockCache"/> class.
        /// </summary>
        public InstructionBlockCache()
        {
            _cache = new ConcurrentDictionary<ushort, ICachedInstructionBlock>();
        }

        /// <summary>
        /// Get an instruction block from the cache at address. If not present then call getInstanceFunc and add to the cache.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="getInstanceFunc"></param>
        /// <returns></returns>
        public IInstructionBlock GetOrSet(ushort address, Func<IInstructionBlock> getInstanceFunc)
        {
            ICachedInstructionBlock cachedInstructionBlock;
            if (_cache.TryGetValue(address, out cachedInstructionBlock))
            {
                cachedInstructionBlock.AccessedCount++;
            }
            else
            {
                var block = getInstanceFunc();
                var ranges = AddressRange.GetRanges(block.Address, block.Length).ToArray();
                if (ranges.Length == 1)
                {
                    cachedInstructionBlock = new NormalCachedInstructionBlock(ranges[0], block);
                }
                else
                {
                    cachedInstructionBlock = new CachedInstructionBlock(ranges, block);
                }

                _cache.TryAdd(block.Address, cachedInstructionBlock);
                AddCachedRanges(cachedInstructionBlock.AddressRanges);
            }

            return cachedInstructionBlock.InstructionBlock;
        }

        /// <summary>
        /// Invalidates all cache from address for length
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        public void InvalidateCache(ushort address, ushort length)
        {
            var ranges = AddressRange.GetRanges(address, length).ToArray();
            if (ranges.Length == 1)
            {
                var range = ranges[0];

                // Lightweight check first.
                if (!_cachedRanges.Any(x => x.Intersects(range)))
                {
                    return;
                }

                foreach (var kvp in _cache.Where(x => x.Value.Intersects(range)).ToArray())
                {
                    ICachedInstructionBlock dummy;
                    _cache.TryRemove(kvp.Key, out dummy);
                    UpdateCachedRanges();
                }
            }
            else
            {
                var range0 = ranges[0];
                var range1 = ranges[1];

                // Lightweight check first.
                if (!_cachedRanges.Any(x => x.Intersects(range0) || x.Intersects(range1)))
                {
                    return;
                }

                foreach (var key in _cache.Keys)
                {
                    var cacheItem = _cache[key];
                    if (ranges.Any(range => cacheItem.Intersects(range)))
                    {
                        _cache.TryRemove(key, out cacheItem);
                        UpdateCachedRanges();
                    }
                }
            }
        }

        /// <summary>
        /// Adds the specified cached ranges.
        /// </summary>
        /// <param name="ranges">The ranges.</param>
        private void AddCachedRanges(IEnumerable<AddressRange> ranges)
        {
            if (_cachedRanges == null)
            {
                UpdateCachedRanges();
                return;
            }

            _cachedRanges = _cachedRanges.Concat(ranges)
                                         .OrderBy(x => x.Address)
                                         .Aggregate(new List<AddressRange>(), AddAddressRange);
        }

        /// <summary>
        /// Updates the cached ranges.
        /// </summary>
        private void UpdateCachedRanges()
        {
            _cachedRanges =
                _cache.SelectMany(x => x.Value.AddressRanges)
                      .OrderBy(x => x.Address)
                      .Aggregate(new List<AddressRange>(), AddAddressRange);
        }

        /// <summary>
        /// Adds the specified address range to the list, combining ranges that are adjacent or intersecting.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        private static List<AddressRange> AddAddressRange(List<AddressRange> list, AddressRange range)
        {
            if (!list.Any())
            {
                list.Add(range);
                return list;
            }

            var last = list.Last();
            if (last.IntersectsOrAdjacent(range))
            {
                list.Remove(last);
                list.Add(last.Merge(range));
            }
            else
            {
                list.Add(range);
            }

            return list;
        }
    }
}