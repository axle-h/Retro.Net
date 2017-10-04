using System;

namespace Retro.Net.Memory
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
        /// Reads bytes from the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        byte[] ReadBytes(ushort address, int length);

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
        /// Writes a collection of bytes to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="bytes">The bytes.</param>
        void WriteBytes(ushort address, byte[] bytes);

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
    }
}