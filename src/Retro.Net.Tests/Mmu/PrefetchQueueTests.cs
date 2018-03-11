using System;
using System.IO;
using System.Linq;
using Moq;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Tests.Util;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Mmu
{
    public class PrefetchQueueTests : WithSubject<PrefetchQueue>
    {
        private const int PrefetchCount = 10;
        private const int Address = 0x1000;
        private readonly byte[] _bytes;
        
        public PrefetchQueueTests()
        {
            _bytes = Rng.Bytes(ushort.MaxValue);
            
            var mmu = Mock<IMmu>();
            mmu.Setup(x => x.GetStream(0, true, false)).Returns(new MemoryStream(_bytes));
            Subject.Seek(Address);
        }

        [Fact]
        public void When_fetching_single_bytes()
        {
            // Read bytes
            var bytes = Enumerable.Range(0, PrefetchCount).Select(i => Subject.NextByte()).ToArray();

            var expected = _bytes.Skip(Address).Take(PrefetchCount).ToArray();

            bytes.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_fetching_arrays_of_bytes()
        {
            // Read bytes
            var bytes = Subject.NextBytes(PrefetchCount);

            var expected = _bytes.Skip(Address).Take(PrefetchCount).ToArray();

            bytes.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_fetching_a_mixture_of_single_and_arrays_of_bytes()
        {
            // Read bytes
            var bytes = Subject.NextBytes(PrefetchCount);

            var expected = _bytes.Skip(Address).Take(PrefetchCount).ToArray();

            bytes.Should().BeEquivalentTo(expected);

            // Now read some more bytes
            bytes = Enumerable.Range(0, PrefetchCount).Select(i => Subject.NextByte()).ToArray();

            expected = _bytes.Skip(Address + PrefetchCount).Take(PrefetchCount).ToArray();

            bytes.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_fetching_words()
        {
            var expected = new ushort[PrefetchCount];
            for (var i = 0; i < expected.Length; i++)
            {
                expected[i] = BitConverter.ToUInt16(_bytes, Address + i * 2);
            }

            // Read words
            Enumerable.Range(0, PrefetchCount).Select(i => Subject.NextWord()).Should().BeEquivalentTo(expected);
        }
    }
}
