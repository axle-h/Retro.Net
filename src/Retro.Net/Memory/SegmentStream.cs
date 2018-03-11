using System;
using System.IO;
using Retro.Net.Memory.Extensions;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Memory
{
    public class SegmentStream : Stream
    {
        private readonly SegmentStreamPointer<IReadableAddressSegment> _readPointer;
        private readonly SegmentStreamPointer<IWriteableAddressSegment> _writePointer;

        public SegmentStream(ushort address,
                             SegmentPointer<IReadableAddressSegment> readPointer = null,
                             SegmentPointer<IWriteableAddressSegment> writePointer = null)
        {
            if (readPointer == null && writePointer == null)
            {
                throw new ArgumentException("Must provide at least one non-null segment pointer");
            }

            _readPointer = readPointer == null ? null : new SegmentStreamPointer<IReadableAddressSegment>(address, readPointer);
            _writePointer = writePointer == null ? null : new SegmentStreamPointer<IWriteableAddressSegment>(address, writePointer);
        }

        public override bool CanRead => _readPointer != null;

        public override bool CanSeek { get; } = true;

        public override bool CanWrite => _writePointer != null;

        public override long Length { get; } = ushort.MaxValue;

        public override long Position
        {
            get => _readPointer?.Address ?? _writePointer.Address;
            set => Seek(value, SeekOrigin.Begin);
        }

        public override void Flush()
        {
        }

        public override void SetLength(long value) => throw new NotSupportedException(nameof(SetLength));

        public override int Read(byte[] buffer, int offset, int count)
        {
            var remaining = count;
            do
            {
                var read = _readPointer.Segment.ReadBytes(_readPointer.Offset, buffer, offset, remaining);
                _readPointer.Increment(read);
                offset += read;
                remaining -= read;
            } while (remaining > 0);
            _writePointer?.Increment(count);
            return count;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var remaining = count;
            do
            {
                var written = _writePointer.Segment.WriteBytes(_writePointer.Offset, buffer, offset, remaining);
                if (_writePointer.Segment.Type.IsMutableApplicationMemory())
                {
                    _writePointer.OnWrite(_writePointer.Offset, written);
                }

                _writePointer.Increment(written);
                offset += written;
                remaining -= written;
            } while (remaining > 0);
            _readPointer?.Increment(count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    break;
                case SeekOrigin.Current:
                    offset += Position;
                    break;
                case SeekOrigin.End:
                    offset += Length;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(origin), origin, null);
            }

            var address = (ushort) (offset % ushort.MaxValue);
            _readPointer?.Seek(address);
            _writePointer?.Seek(address);
            return address;
        }

        private class SegmentStreamPointer<TAddressSegment> where TAddressSegment : IAddressSegment
        {
            private readonly SegmentPointer<TAddressSegment> _pointer;

            public SegmentStreamPointer(ushort address, SegmentPointer<TAddressSegment> pointer)
            {
                _pointer = pointer;
                Seek(address);
            }

            public ushort Offset { get; private set; }

            public TAddressSegment Segment { get; private set; }

            public ushort Address => (ushort)(Segment.Address + Offset);

            public void OnWrite(ushort address, int count) => _pointer.OnWrite(address, count);

            public void Increment(int count)
            {
                var offset = Offset + count;
                if (offset < 0 || offset >= Segment.Length)
                {
                    Seek((ushort)(Segment.Address + offset));
                }
                else
                {
                    Offset = (ushort)offset;
                }
            }

            public void Seek(ushort address) => (Segment, Offset) = _pointer.GetOffset(address);
        }
    }
}
