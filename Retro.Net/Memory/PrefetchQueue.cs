using System;

namespace Retro.Net.Memory
{
    /// <summary>
    /// An MMU cache for prefetching sequential bytes.
    /// Should speed up sequential access to the segment MMU e.g. reading op-codes
    /// </summary>
    public class PrefetchQueue : IPrefetchQueue
    {
        private const int CacheSize = 64;

        private readonly IMmu _mmu;

        private byte[] _cache;
        private int _cachePointer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefetchQueue" /> class.
        /// </summary>
        /// <param name="mmu">The mmu.</param>
        public PrefetchQueue(IMmu mmu)
        {
            _mmu = mmu;
            ReBuildCache(0x0000);
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort BaseAddress { get; private set; }

        /// <summary>
        /// Gets the next byte in the prefetch queue.
        /// </summary>
        /// <returns>The next byte in the prefetch queue</returns>
        public byte NextByte()
        {
            var value = _cache[_cachePointer];

            if (++_cachePointer == CacheSize)
            {
                NudgeCache();
            }

            TotalBytesRead++;
            return value;
        }

        /// <summary>
        /// Gets the next bytes in the prefetch queue.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>The next bytes in the prefetch queue.</returns>
        public byte[] NextBytes(int length)
        {
            var bytes = new byte[length];
            var bytesRead = 0;

            while (bytesRead < length)
            {
                var bytesToRead = Math.Min(length - bytesRead, CacheSize - _cachePointer);
                Array.Copy(_cache, _cachePointer, bytes, bytesRead, bytesToRead);
                bytesRead += bytesToRead;

                _cachePointer += bytesToRead;
                if (_cachePointer == CacheSize)
                {
                    NudgeCache();
                }
            }

            TotalBytesRead += length;
            return bytes;
        }

        /// <summary>
        /// Gets the next word in the prefetch queue.
        /// </summary>
        /// <returns>The next word in the prefetch queue.</returns>
        public ushort NextWord()
        {
            if (CacheSize - _cachePointer == 1)
            {
                // If there's only one byte left then we need to read it then the next byte from the next cache
                var lsb = NextByte();
                var msb = NextByte();

                return BitConverter.ToUInt16(new[] { lsb, msb }, 0);
            }

            var value = BitConverter.ToUInt16(_cache, _cachePointer);

            _cachePointer += 2;
            if (_cachePointer == CacheSize)
            {
                NudgeCache();
            }

            TotalBytesRead += 2;
            return value;
        }

        /// <summary>
        /// Re-builds the cache.
        /// </summary>
        /// <param name="newAddress">The new address.</param>
        public void ReBuildCache(ushort newAddress)
        {
            // TODO: check if we can re-use this cache, make sure TotalBytesRead is still reset
            BaseAddress = newAddress;
            _cachePointer = 0;
            _cache = _mmu.ReadBytes(BaseAddress, CacheSize);
            TotalBytesRead = 0;
        }

        /// <summary>
        /// Gets the total bytes read since the last rebuild.
        /// </summary>
        /// <value>
        /// The total bytes read.
        /// </value>
        public int TotalBytesRead { get; private set; }
        
        /// <summary>
        /// Increments the address by <see cref="CacheSize" /> and repopulates the cache.
        /// </summary>
        private void NudgeCache()
        {
            BaseAddress = (ushort) (BaseAddress + CacheSize);
            _cachePointer = 0;
            _cache = _mmu.ReadBytes(BaseAddress, CacheSize);
        }
    }
}