using System;

namespace Retro.Net.Api.RealTime.Extensions
{
    internal static class ArrayExtensions
    {
        /// <summary>
        /// Gets an array segment from the specified array from the beginning for the specified length.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static ArraySegment<T> Segment<T>(this T[] array, int length) => new ArraySegment<T>(array, 0, length);

        /// <summary>
        /// Gets an array segment from the specified array from the specified offset until the end.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static ArraySegment<T> EndSegment<T>(this T[] array, int offset) => new ArraySegment<T>(array, offset, array.Length - offset);
    }
}
