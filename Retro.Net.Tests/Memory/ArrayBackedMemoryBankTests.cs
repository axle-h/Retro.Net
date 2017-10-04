using Retro.Net.Config;
using Retro.Net.Exceptions;
using Retro.Net.Memory;
using Retro.Net.Tests.Util;
using Shouldly;
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
            var memoryBankConfig = The<IMemoryBankConfig>();
            memoryBankConfig.Setup(x => x.Address).Returns(Address);
            memoryBankConfig.Setup(x => x.Length).Returns(Length);
            memoryBankConfig.Setup(x => x.InitialState).Returns(() => InitialState);
        }

        [Fact]
        public void When_reading_and_writing_byte_arrays()
        {
            const ushort byteArrayAddress = Length / 4;
            var writtenBytes = Rng.Bytes(Length / 2);
            Subject.WriteBytes(byteArrayAddress, writtenBytes);

            Subject.ReadBytes(byteArrayAddress, writtenBytes.Length).ShouldBe(writtenBytes);
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

            readBytes.ShouldBe(writtenBytes);
        }

        [Fact]
        public void When_reading_and_writing_single_words()
        {
            var readWords = new ushort[Length];
            var writtenWords = new ushort[Length];

            for (ushort i = 0; i < Length - 1; i++)
            {
                var w = Rng.Word();
                writtenWords[i] = w;
                Subject.WriteWord(i, w);
                readWords[i] = Subject.ReadWord(i);
            }

            readWords.ShouldBe(writtenWords);
        }

        [Fact]
        public void When_seting_initial_state()
        {
            InitialState = Rng.Bytes(Length);
            Subject.ReadBytes(0, Length).ShouldBe(InitialState);
        }

        [Fact]
        public void When_setting_state_array_too_big() => WhenSettingInvalidInitialState(Length + 1);

        [Fact]
        public void When_setting_state_array_too_small() => WhenSettingInvalidInitialState(Length - 1);

        private void WhenSettingInvalidInitialState(int length)
        {
            InitialState = new byte[length];
            var exception = ConstructionShouldThrow<MemoryConfigStateException>();
            exception.Adddress.ShouldBe(Address);
            exception.SegmentLength.ShouldBe(Length);
            exception.StateLength.ShouldBe(length);
        }
    }
}
