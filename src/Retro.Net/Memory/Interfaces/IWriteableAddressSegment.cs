namespace Retro.Net.Memory.Interfaces
{
    /// <summary>
    /// A writeable address segment.
    /// </summary>
    /// <seealso cref="IAddressSegment" />
    public interface IWriteableAddressSegment : IAddressSegment
    {
        /// <summary>
        /// Writes a byte to this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="value">The value.</param>
        void WriteByte(ushort address, byte value);

        /// <summary>
        /// Writes the bytes in the specified buffer to this address segment.
        /// This does not wrap the segment.
        /// i.e. if address + count is larger than the segment length then this will return a value less than count.
        /// </summary>
        /// <param name="address">The segment address to start writing to.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset to start reading from in the buffer.</param>
        /// <param name="count">The number of bytes to write.</param>
        /// <returns>The number of bytes written into this segment.</returns>
        int WriteBytes(ushort address, byte[] buffer, int offset, int count);
    }
}