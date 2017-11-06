using System.Collections;
using System.Collections.Generic;

namespace Retro.Net.Api.RealTime.Util
{
    public class DropOutStack<TElement> : IEnumerable<TElement>
    {
        private readonly TElement[] _data;
        private int _pointer;
        private int _count;

        public DropOutStack(int length)
        {
            _data = new TElement[length];
        }

        public void Push(TElement element)
        {
            if (_count < _data.Length)
            {
                _count++;
            }
            _data[_pointer] = element;
            _pointer = (_pointer + 1) % _data.Length;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            var index = _pointer;
            for (var i = 0; i < _count; i++)
            {
                index = (index - 1) % _data.Length;
                yield return _data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
