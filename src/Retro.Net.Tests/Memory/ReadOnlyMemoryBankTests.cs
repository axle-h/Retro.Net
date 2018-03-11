using System;
using System.Linq;
using Retro.Net.Config;
using Retro.Net.Memory;
using Retro.Net.Tests.Util;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Memory
{
    public class ReadOnlyMemoryBankTests : WithSubject<ReadOnlyMemoryBank>
    {
        private const ushort Address = 0xf00d;
        private const ushort Length = 0x0bad;

        private readonly byte[] _byteContent;

        public ReadOnlyMemoryBankTests()
        {
            _byteContent = Rng.Bytes(Length);
            var memoryBankConfig = Mock<IMemoryBankConfig>();
            memoryBankConfig.Setup(x => x.Address).Returns(Address);
            memoryBankConfig.Setup(x => x.Length).Returns(Length);
            memoryBankConfig.Setup(x => x.InitialState).Returns(_byteContent);
        }

        [Fact]
        public void When_reading_each_byte()
            => Enumerable.Range(0, Length).Select(i => Subject.ReadByte((ushort) i)).Should().BeEquivalentTo(_byteContent);

        [Fact]
        public void When_reading_all_bytes()
        {
            var bytes = new byte[Length];
            Subject.ReadBytes(0, bytes, 0, Length);
            bytes.Should().BeEquivalentTo(_byteContent);
        }
    }
}
