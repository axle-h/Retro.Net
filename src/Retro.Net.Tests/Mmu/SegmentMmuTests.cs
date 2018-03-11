using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Tests.Util;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Mmu
{
    public abstract class SegmentMmuTestBase : WithSubject<SegmentMmu>
    {
        protected readonly (ushort address, ushort length) Segment0Config;
        protected readonly (ushort address, ushort length) Segment1Config;
        protected readonly (ushort address, ushort length) Segment2Config;
        
        protected readonly Mock<IReadWriteAddressSegment> Segment0;
        protected readonly Mock<IReadableAddressSegment> SegmentReadable1;
        protected readonly Mock<IWriteableAddressSegment> SegmentWriteable1;
        protected readonly Mock<IReadWriteAddressSegment> Segment2;
        
        protected SegmentMmuTestBase((ushort address, ushort length) segment0Config,
            (ushort address, ushort length) segment1Config,
            (ushort address, ushort length) segment2Config)
        {
            Segment0Config = segment0Config;
            Segment1Config = segment1Config;
            Segment2Config = segment2Config;

            Segment0 = GetSegmentMock<IReadWriteAddressSegment>(segment0Config);
            SegmentReadable1 = GetSegmentMock<IReadableAddressSegment>(segment1Config);
            SegmentWriteable1 = GetSegmentMock<IWriteableAddressSegment>(segment1Config);
            Segment2 = GetSegmentMock<IReadWriteAddressSegment>(segment2Config);

            var segments = new IAddressSegment[] { Segment0.Object, SegmentReadable1.Object, SegmentWriteable1.Object, Segment2.Object };
            Register((IEnumerable<IAddressSegment>)segments);
        }

        private static Mock<TSegment> GetSegmentMock<TSegment>((ushort address, ushort length) config)
            where TSegment : class, IAddressSegment
        {
            var segment = new Mock<TSegment>();
            segment.Setup(x => x.Address).Returns(config.address);
            segment.Setup(x => x.Length).Returns(config.length);

            if (typeof(IReadableAddressSegment).IsAssignableFrom(typeof(TSegment)))
            {
                SetupReadableSegment(segment.As<IReadableAddressSegment>());
            }

            if (typeof(IWriteableAddressSegment).IsAssignableFrom(typeof(TSegment)))
            {
                SetupWriteableSegment(segment.As<IWriteableAddressSegment>());
            }

            return segment;
        }

        private static void SetupWriteableSegment<TSegment>(Mock<TSegment> segment)
            where TSegment : class, IWriteableAddressSegment
        {
            var length = segment.Object.Length;
            void AddressWriteCallback(ushort a)
            {
                if (a >= length)
                {
                    throw new Exception($"Tried to write outside of the segment 0x{a:x4}");
                }
            }

            segment.Setup(x => x.WriteByte(It.IsAny<ushort>(), It.IsAny<byte>()))
                   .Callback((ushort a, byte v) => AddressWriteCallback(a));

            segment.Setup(x => x.WriteBytes(It.IsAny<ushort>(), It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                   .Returns((ushort a, byte[] v, int o, int l) => Math.Min(l, length - a));
        }

        private static void SetupReadableSegment<TSegment>(Mock<TSegment> segment)
            where TSegment : class, IReadableAddressSegment
        {
            var length = segment.Object.Length;
            T AddressReadCallback<T>(ushort a, T value)
            {
                if (a >= length)
                {
                    throw new Exception($"Tried to read outside of the segment 0x{a:x4}");
                }

                return value;
            }

            segment.Setup(x => x.ReadByte(It.IsAny<ushort>())).Returns((ushort a) => AddressReadCallback(a, (byte)0x00));

            segment.Setup(x => x.ReadBytes(It.IsAny<ushort>(), It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                   .Returns((ushort a, byte[] v, int o, int l) => Math.Min(l, length - a));
        }
    }

    public class SegmentMmuTests : SegmentMmuTestBase
    {
        public SegmentMmuTests() : base((0x0000, 0x8000), (0x8000, 0x7000), (0xf000, 0x1000))
        {
        }

        private void WhenReadingAndWritingByte<TReadableSegment, TWriteableSegment>(ushort address, Mock<TReadableSegment> readSegment, Mock<TWriteableSegment> writeSegment)
            where TReadableSegment : class, IReadableAddressSegment
            where TWriteableSegment : class, IWriteableAddressSegment
        {
            readSegment.Object.Address.Should().Be(writeSegment.Object.Address);

            var segmentAddress = (ushort) (address - readSegment.Object.Address);
            var value = Rng.Byte();
            Subject.WriteByte(address, value);
            Subject.ReadByte(address);

            readSegment.Verify(x => x.ReadByte(segmentAddress));
            writeSegment.Verify(x => x.WriteByte(segmentAddress, value));
        }
        
        [Fact] public void When_constructing() => ConstructionShouldNotThrow();

        [Fact] public void When_reading_and_writing_first_byte_of_segment_0() => WhenReadingAndWritingByte(Segment0Config.address, Segment0, Segment0);

        [Fact] public void When_reading_and_writing_last_byte_of_segment_0() => WhenReadingAndWritingByte((ushort) (Segment1Config.address - 1), Segment0, Segment0);

        [Fact] public void When_reading_and_writing_first_byte_of_segment_1() => WhenReadingAndWritingByte(Segment1Config.address, SegmentReadable1, SegmentWriteable1);

        [Fact] public void When_reading_and_writing_last_byte_of_segment_1() => WhenReadingAndWritingByte((ushort) (Segment2Config.address - 1), SegmentReadable1, SegmentWriteable1);

        [Fact] public void When_reading_and_writing_first_byte_of_segment_2() => WhenReadingAndWritingByte(Segment2Config.address, Segment2, Segment2);

        [Fact] public void When_reading_and_writing_last_byte_of_segment_2() => WhenReadingAndWritingByte(0xffff, Segment2, Segment2);
        
        [Fact]
        public void When_reading_and_writing_word()
        {
            var address = (ushort) (Segment1Config.address - 1);
            var value = Rng.UShort();
            Subject.WriteWord(address, value);
            Subject.ReadWord(address);

            var bytes = BitConverter.GetBytes(value);

            Segment0.Verify(x => x.WriteBytes(address, It.Is<byte[]>(b => b.Take(2).SequenceEqual(bytes)), 0, 2)); // first tries writing all bytes to segment0
            SegmentWriteable1.Verify(x => x.WriteBytes(0, It.Is<byte[]>(b => b.Take(2).SequenceEqual(bytes)), 1, 1)); // then writes last byte to segment1
            Segment0.Verify(x => x.ReadBytes(address, It.IsAny<byte[]>(), 0, 2)); // first tries reading all bytes from segment0
            SegmentReadable1.Verify(x => x.ReadBytes(0, It.IsAny<byte[]>(), 1, 1)); // then reads last byte from segment1
        }
    }
}
