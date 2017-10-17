namespace Retro.Net.Memory
{
    /// <summary>
    /// An MMU cache for prefetching sequential bytes.
    /// Should speed up sequential access to the segment MMU e.g. reading op-codes
    /// </summary>
    public interface IPrefetchQueue
    {
        /// <summary>
        /// Gets the base address of the prefetch cache.
        /// </summary>
        /// <value>
        /// The base address of the prefetch cache.
        /// </value>
        ushort BaseAddress { get; }

        /// <summary>
        /// Gets the total bytes read since the last rebuild.
        /// </summary>
        /// <value>
        /// The total bytes read.
        /// </value>
        int TotalBytesRead { get; }

        /// <summary>
        /// Gets the next byte in the prefetch queue.
        /// </summary>
        /// <returns>The next byte in the prefetch queue</returns>
        byte NextByte();

        /// <summary>
        /// Gets the next bytes in the prefetch queue.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>The next bytes in the prefetch queue.</returns>
        byte[] NextBytes(int length);

        /// <summary>
        /// Gets the next word in the prefetch queue.
        /// </summary>
        /// <returns>The next word in the prefetch queue.</returns>
        ushort NextWord();

        /// <summary>
        /// Re-builds the cache.
        /// </summary>
        /// <param name="newAddress">The new address.</param>
        void ReBuildCache(ushort newAddress);
    }
}