using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Retro.Net.Api.RealTime.Util
{
    internal static class LinqExtensions
    {
        /// <summary>
        /// Safely empties the specified queue.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="queue">The queue.</param>
        /// <returns></returns>
        public static IEnumerable<TElement> TakeAll<TElement>(this IProducerConsumerCollection<TElement> queue)
        {
            for (var i = 0; i < queue.Count; i++)
            {
                if (!queue.TryTake(out var element))
                {
                    yield break;
                }
                yield return element;
            }
        }
    }
}
