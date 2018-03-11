using System;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Exceptions;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Memory
{
    public class SegmentPointer<TAddressSegment> where TAddressSegment : IAddressSegment
    {
        private readonly IList<TAddressSegment> _segments;
        private readonly Action<ushort, int> _onWrite;
        private readonly ushort[] _addresses;

        public SegmentPointer(IList<TAddressSegment> segments, Action<ushort, int> onWrite = null)
        {
            CheckSegments(segments);
            _segments = segments;
            _onWrite = onWrite;
            _addresses = segments.Select(x => x.Address).ToArray();
        }
        
        public (TAddressSegment segment, ushort offset) GetOffset(ushort address)
        {
            var index = Array.BinarySearch(_addresses, address);

            // If the index is negative, it represents the bitwise 
            // complement of the next larger element in the array. 
            if (index < 0)
            {
                index = ~index - 1;
            }

            var segment = _segments[index];
            var offset = (ushort) (address - segment.Address);
            return (segment, offset);
        }

        public void OnWrite(ushort address, int count) => _onWrite?.Invoke(address, count);
        
        private static void CheckSegments(IEnumerable<TAddressSegment> segments)
        {
            uint lastAddress = 0x0000;
            foreach (var segment in segments)
            {
                if (segment.Length < 1)
                {
                    throw new PlatformConfigurationException($"Segment length is less than 1 at 0x{segment.Address:x4}");
                }

                if (segment.Address > lastAddress)
                {
                    throw new MmuAddressSegmentException(AddressSegmentExceptionType.Gap, (ushort)lastAddress, segment.Address);
                }

                if (segment.Address < lastAddress)
                {
                    throw new MmuAddressSegmentException(AddressSegmentExceptionType.Overlap, segment.Address, (ushort)lastAddress);
                }

                lastAddress += segment.Length;
            }

            if (lastAddress < ushort.MaxValue + 1)
            {
                throw new MmuAddressSegmentException(AddressSegmentExceptionType.Gap, (ushort)lastAddress, ushort.MaxValue);
            }
        }
    }
}