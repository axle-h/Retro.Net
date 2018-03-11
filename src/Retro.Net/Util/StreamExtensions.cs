using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Retro.Net.Util
{
    /// <summary>
    /// Extensions for <see cref="Stream"/>.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads ASCII characters from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string ReadAscii(this Stream stream, int length)
        {
            return 0 != length ? Encoding.ASCII.GetString(stream.ReadBuffer(length).TakeWhile(b => 0 != b).ToArray()) : string.Empty;
        }

        /// <summary>
        /// Reads bytes from the specified stream into a new buffer.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static byte[] ReadBuffer(this Stream stream, int length)
        {
            var bytesRead = 0;
            var buffer = new byte[length];
            while (bytesRead != length)
            {
                var read = stream.Read(buffer, bytesRead, length - bytesRead);
                if (read == 0)
                {
                    // TODO: Very specific to memory streams.
                    throw new ArgumentException("Stream too short", nameof(stream));
                }

                bytesRead += read;
            }
            return buffer;
        }

        /// <summary>
        /// Reads the big endian unsigned 16-bit integer from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static ushort ReadBigEndianUInt16(this Stream stream)
        {
            var msb = stream.ReadByte();
            var lsb = stream.ReadByte();
            return (ushort) ((msb << 8) | lsb);
        }

        /// <summary>
        /// Reads an enum of the specified type from the specified stream.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static TEnum ReadEnum<TEnum>(this Stream stream)
        {
            var value = (byte) stream.ReadByte();
            if (!Enum.IsDefined(typeof (TEnum), value))
            {
                throw new Exception($"Bad {typeof (TEnum)}: 0x{value:x2}");
            }

            return (TEnum) Enum.ToObject(typeof (TEnum), value);
        }
    }
}