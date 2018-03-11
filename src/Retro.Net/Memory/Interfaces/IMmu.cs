using System;
using System.IO;

namespace Retro.Net.Memory.Interfaces
{
    /// <summary>
    /// A memory management unit.
    /// Used directly by the CPU for directing IO to relevent hardware e.g. RAM, ROM, registers.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IMmu : IDisposable
    {
        /// <summary>
        /// Reads a byte from the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        byte ReadByte(ushort address);

        /// <summary>
        /// Reads a word from the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        ushort ReadWord(ushort address);

        /// <summary>
        /// Writes a byte to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="value">The value.</param>
        void WriteByte(ushort address, byte value);

        /// <summary>
        /// Writes a word to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="word">The word.</param>
        void WriteWord(ushort address, ushort word);

        /// <summary>
        /// Copies a byte from one address to another.
        /// </summary>
        /// <param name="addressFrom">The address from.</param>
        /// <param name="addressTo">The address to.</param>
        void TransferByte(ushort addressFrom, ushort addressTo);

        /// <summary>
        /// Copies bytes from one address to another.
        /// </summary>
        /// <param name="addressFrom">The address from.</param>
        /// <param name="addressTo">The address to.</param>
        /// <param name="length">The length.</param>
        void TransferBytes(ushort addressFrom, ushort addressTo, int length);

        /// <summary>
        /// Gets a stream that wraps this MMU.
        /// </summary>
        /// <param name="address">The address that the stream will initially seek to.</param>
        /// <param name="readable">if set to <c>true</c> [the stream will be readable].</param>
        /// <param name="writable">if set to <c>true</c> [the stream will be writable].</param>
        /// <returns></returns>
        Stream GetStream(ushort address, bool readable = true, bool writable = true);
    }
}