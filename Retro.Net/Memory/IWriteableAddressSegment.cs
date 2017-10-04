namespace Retro.Net.Memory
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
        /// Writes a word to this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="word">The word.</param>
        void WriteWord(ushort address, ushort word);

        /// <summary>
        /// Writes bytes to this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="values">The values.</param>
        void WriteBytes(ushort address, byte[] values);
    }
}