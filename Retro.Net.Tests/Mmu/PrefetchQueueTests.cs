using System;
using System.Linq;
using Moq;
using Retro.Net.Memory;
using Retro.Net.Tests.Util;
using Shouldly;
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
            
            var mmu = The<IMmu>();
            mmu.Setup(x => x.ReadBytes(It.IsAny<ushort>(), It.IsAny<int>()))
                .Returns((ushort address, int length) => _bytes.Skip(address).Take(length).ToArray());
            mmu.Setup(x => x.ReadWord(It.IsAny<ushort>())).Returns((ushort address) => BitConverter.ToUInt16(_bytes, address));
            mmu.Setup(x => x.ReadByte(It.IsAny<ushort>())).Returns((ushort address) => _bytes[address]);
            Subject.ReBuildCache(Address);
        }

        [Fact]
        public void When_fetching_single_bytes()
        {
            // Read bytes
            var bytes = Enumerable.Range(0, PrefetchCount).Select(i => Subject.NextByte()).ToArray();

            var expected = _bytes.Skip(Address).Take(PrefetchCount).ToArray();

            bytes.ShouldBe(expected);
        }

        [Fact]
        public void When_fetching_arrays_of_bytes()
        {
            // Read bytes
            var bytes = Subject.NextBytes(PrefetchCount);

            var expected = _bytes.Skip(Address).Take(PrefetchCount).ToArray();

            bytes.ShouldBe(expected);
        }

        [Fact]
        public void When_fetching_a_mixture_of_single_and_arrays_of_bytes()
        {
            // Read bytes
            var bytes = Subject.NextBytes(PrefetchCount);

            var expected = _bytes.Skip(Address).Take(PrefetchCount).ToArray();

            bytes.ShouldBe(expected);

            // Now read some more bytes
            bytes = Enumerable.Range(0, PrefetchCount).Select(i => Subject.NextByte()).ToArray();

            expected = _bytes.Skip(Address + PrefetchCount).Take(PrefetchCount).ToArray();

            bytes.ShouldBe(expected);
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
            Enumerable.Range(0, PrefetchCount).Select(i => Subject.NextWord()).ShouldBe(expected);
        }
    }
}
