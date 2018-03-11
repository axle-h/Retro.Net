using System;

namespace Retro.Net.Z80.Util
{
    /// <summary>
    /// Extensions for bytes.
    /// </summary>
    internal static class ByteExtensions
    {
        private static readonly bool[] EvenParityTable = P8(true);

        /// <summary>
        /// Determines whether the specified byte [is even parity].
        /// </summary>
        /// <param name="b">The byte.</param>
        /// <returns></returns>
        public static bool IsEvenParity(this byte b) => EvenParityTable[b];

        /// <summary>
        /// Gets the 2-bit parity table.
        /// </summary>
        /// <param name="even">if set to <c>true</c> [the table should be even].</param>
        /// <returns></returns>
        private static bool[] P2(bool even) => new[] { even, !even, !even, even };

        /// <summary>
        /// Gets the 4-bit parity table.
        /// </summary>
        /// <param name="even">if set to <c>true</c> [the table should be even].</param>
        /// <returns></returns>
        private static bool[] P4(bool even) => P0110(P2(even), P2(!even));

        /// <summary>
        /// Gets the 6-bit parity table.
        /// </summary>
        /// <param name="even">if set to <c>true</c> [the table should be even].</param>
        /// <returns></returns>
        private static bool[] P6(bool even) => P0110(P4(even), P4(!even));
        
        /// <summary>
        /// Gets the 8-bit parity table.
        /// </summary>
        /// <param name="even">if set to <c>true</c> [the table should be even].</param>
        /// <returns></returns>
        private static bool[] P8(bool even) => P0110(P6(even), P6(!even));

        /// <summary>
        /// Builds an array containing
        /// 1. all elements from <see cref="p0"/>
        /// 2. all elements from <see cref="p1"/>
        /// 3. a copy of all elements from <see cref="p1"/>
        /// 4. a copy all elements from <see cref="p0"/>
        /// </summary>
        /// <param name="p0">The p0.</param>
        /// <param name="p1">The p1.</param>
        /// <returns></returns>
        private static bool[] P0110(bool[] p0, bool[] p1)
        {
            var ans = new bool[p0.Length * 4];

            Array.Copy(p0, 0, ans, 0, p0.Length);
            Array.Copy(p1, 0, ans, p0.Length, p0.Length);
            Array.Copy(p1, 0, ans, p0.Length * 2, p0.Length);
            Array.Copy(p0, 0, ans, p0.Length * 3, p0.Length);

            return ans;
        }
    }
}