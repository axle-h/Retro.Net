using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Retro.Net.Tests.Util
{
    public class CartesianTheorySource<TItem1, TItem2> : IEnumerable<object[]>
    {
        private readonly ICollection<TItem1> _data1;
        private ICollection<TItem2> _data2;

        public CartesianTheorySource(params TItem1[] data)
        {
            _data1 = data;
        }

        public CartesianTheorySource<TItem1, TItem2> With(params TItem2[] data)
        {
            _data2 = data;
            return this;
        }

        public IEnumerator<object[]> GetEnumerator() => Enumerable.SelectMany(_data1, x1 => Enumerable.Select(_data2, x2 => new object[] { x1, x2 })).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}