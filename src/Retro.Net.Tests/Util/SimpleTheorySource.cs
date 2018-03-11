using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Retro.Net.Tests.Util
{
    public class SimpleTheorySource<TItem> : IEnumerable<object[]>
    {
        private readonly ICollection<TItem> _data;

        public SimpleTheorySource(params TItem[] data)
        {
            _data = data;
        }

        public IEnumerator<object[]> GetEnumerator() => _data.Select(x => new object[] { x }).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
