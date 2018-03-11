namespace Retro.Net.Memory.Interfaces
{
    /// <summary>
    /// An readable address segment.
    /// </summary>
    /// <seealso cref="IAddressSegment" />
    public interface IReadableAddressSegment : IAddressSegment
    {
        /// <summary>
        /// Reads a byte from this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        byte ReadByte(ushort address);

        /// <summary>
        /// Reads bytes from this address segment into the specified buffer.
        /// This does not wrap the segment.
        /// i.e. if address + count is larger than the segment length then this will return a value less than count.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset to start writing to in the buffer.</param>
        /// <param name="address">The segment address to start reading from.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read into the buffer.</returns>
        int ReadBytes(ushort address, byte[] buffer, int offset, int count);
    }
}