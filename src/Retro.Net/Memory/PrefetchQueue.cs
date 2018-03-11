using System;
using System.IO;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Util;

namespace Retro.Net.Memory
{
    /// <summary>
    /// An MMU cache for prefetching sequential bytes.
    /// Should speed up sequential access to the segment MMU e.g. reading op-codes
    /// </summary>
    public class PrefetchQueue : IPrefetchQueue
    {
        private readonly Stream _stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefetchQueue" /> class.
        /// </summary>
        /// <param name="mmu">The mmu.</param>
        public PrefetchQueue(IMmu mmu)
        {
            _stream = mmu.GetStream(0, writable: false);
        }

        /// <summary>
        /// Gets the next byte in the prefetch queue.
        /// </summary>
        /// <returns>The next byte in the prefetch queue</returns>
        public byte NextByte()
        {
            var value = (byte) _stream.ReadByte();
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
            var buffer = _stream.ReadBuffer(length);
            TotalBytesRead += length;
            return buffer;
        }

        /// <summary>
        /// Gets the next word in the prefetch queue.
        /// </summary>
        /// <returns>The next word in the prefetch queue.</returns>
        public ushort NextWord()
        {
            var buffer = NextBytes(2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Seeks to the specified address.
        /// </summary>
        /// <param name="address">The new address.</param>
        public void Seek(ushort address)
        {
            _stream.Seek(address, SeekOrigin.Begin);
            TotalBytesRead = 0;
        }

        /// <summary>
        /// Gets the total bytes read since the last rebuild.
        /// </summary>
        /// <value>
        /// The total bytes read.
        /// </value>
        public int TotalBytesRead { get; private set; }
        
    }
}