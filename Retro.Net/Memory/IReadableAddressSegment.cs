namespace Retro.Net.Memory
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
        /// Reads a word from this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        ushort ReadWord(ushort address);

        /// <summary>
        /// Reads bytes from this address segment into a new buffer.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        byte[] ReadBytes(ushort address, int length);

        /// <summary>
        /// Reads bytes from this address segment into the specified buffer.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="buffer">The buffer.</param>
        void ReadBytes(ushort address, byte[] buffer);
    }
}