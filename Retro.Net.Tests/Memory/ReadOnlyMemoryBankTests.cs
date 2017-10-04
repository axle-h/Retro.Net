using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Retro.Net.Config;
using Retro.Net.Exceptions;
using Retro.Net.Memory;
using Moq;
using Retro.Net.Tests.Util;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Memory
{
    public class ReadOnlyMemoryBankTests : WithSubject<ReadOnlyMemoryBank>
    {
        private const ushort Address = 0xf00d;
        private const ushort Length = 0x0bad;

        private readonly byte[] _byteContent;
        private readonly ushort[] _wordContent;

        public ReadOnlyMemoryBankTests()
        {
            _byteContent = Rng.Bytes(Length);
            _wordContent = new ushort[Length / 2];
            for (var i = 0; i < Length / 2; i++)
            {
                _wordContent[i] = BitConverter.ToUInt16(_byteContent, i * 2);
            }

            var memoryBankConfig = The<IMemoryBankConfig>();
            memoryBankConfig.Setup(x => x.Address).Returns(Address);
            memoryBankConfig.Setup(x => x.Length).Returns(Length);
            memoryBankConfig.Setup(x => x.InitialState).Returns(_byteContent);
        }

        [Fact]
        public void When_reading_each_byte()
            => Enumerable.Range(0, Length).Select(i => Subject.ReadByte((ushort) i)).ShouldBe(_byteContent);

        [Fact]
        public void When_reading_all_bytes() => Subject.ReadBytes(0, Length).ShouldBe(_byteContent);

        [Fact]
        public void When_reading_each_word()
            => Enumerable.Range(0, Length / 2).Select(i => Subject.ReadWord((ushort) (i * 2))).ShouldBe(_wordContent);
    }
}
