using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Retro.Net.Tests.Util
{
    public class TheorySource<TItem> : IEnumerable<object[]>
    {
        private readonly ICollection<TItem> _data;

        public TheorySource(params TItem[] data)
        {
            _data = data;
        }

        public IEnumerator<object[]> GetEnumerator() => _data.Select(x => new object[] { x }).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    public class TheorySource<TItem1, TItem2> : IEnumerable<object[]>
    {
        private readonly ICollection<TItem1> _data1;
        private ICollection<TItem2> _data2;

        public TheorySource(params TItem1[] data)
        {
            _data1 = data;
        }

        public TheorySource<TItem1, TItem2> With(params TItem2[] data)
        {
            _data2 = data;
            return this;
        }

        public IEnumerator<object[]> GetEnumerator() => _data1.SelectMany(x1 => _data2.Select(x2 => new object[] { x1, x2 })).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
