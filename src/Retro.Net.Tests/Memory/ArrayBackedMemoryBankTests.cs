using System;
using Retro.Net.Config;
using Retro.Net.Exceptions;
using Retro.Net.Memory;
using Retro.Net.Tests.Util;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Memory
{
    public class ArrayBackedMemoryBankTests : WithSubject<ArrayBackedMemoryBank>
    {
        private const ushort Address = 0xf00d;
        private const ushort Length = 0x0bad;

        private byte[] InitialState { get; set; }

        public ArrayBackedMemoryBankTests()
        {
            var memoryBankConfig = Mock<IMemoryBankConfig>();
            memoryBankConfig.Setup(x => x.Address).Returns(Address);
            memoryBankConfig.Setup(x => x.Length).Returns(Length);
            memoryBankConfig.Setup(x => x.InitialState).Returns(() => InitialState);
        }

        [Fact]
        public void When_reading_and_writing_byte_arrays()
        {
            const ushort byteArrayAddress = Length / 4;
            var writtenBytes = Rng.Bytes(Length / 2);
            Subject.WriteBytes(byteArrayAddress, writtenBytes, 0, writtenBytes.Length);

            var readBytes = new byte[writtenBytes.Length];
            Subject.ReadBytes(byteArrayAddress, readBytes, 0, writtenBytes.Length);
            readBytes.Should().BeEquivalentTo(writtenBytes);
        }

        [Fact]
        public void When_reading_and_writing_single_bytes()
        {
            var readBytes = new byte[Length];
            var writtenBytes = new byte[Length];

            for (ushort i = 0; i < Length; i++)
            {
                var b = Rng.Byte();
                writtenBytes[i] = b;
                Subject.WriteByte(i, b);
                readBytes[i] = Subject.ReadByte(i);
            }

            readBytes.Should().BeEquivalentTo(writtenBytes);
        }

        [Fact]
        public void When_seting_initial_state()
        {
            InitialState = Rng.Bytes(Length);

            var readBytes = new byte[Length];
            Subject.ReadBytes(0, readBytes, 0, Length);
            readBytes.Should().BeEquivalentTo(InitialState);
        }

        [Fact]
        public void When_setting_state_array_too_big() => WhenSettingInvalidInitialState(Length + 1);

        [Fact]
        public void When_setting_state_array_too_small() => WhenSettingInvalidInitialState(Length - 1);

        private void WhenSettingInvalidInitialState(int length)
        {
            InitialState = new byte[length];
            var exception = ConstructionShouldThrow<MemoryConfigStateException>();
            exception.Adddress.Should().Be(Address);
            exception.SegmentLength.Should().Be(Length);
            exception.StateLength.Should().Be(length);
        }
    }
}
