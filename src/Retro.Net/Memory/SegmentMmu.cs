using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Memory.Dma;
using Retro.Net.Memory.Extensions;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Timing;
using Retro.Net.Util;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Memory
{
    /// <summary>
    ///     A memory management unit made up of address segments.
    ///     Requests are redirected to the relevent address segments.
    ///     The entire 64k address range must be filled with address segments.
    ///     See <see cref="NullMemoryBank" /> for address ranges where no hardware should be available.
    /// </summary>
    /// <seealso cref="IMmu" />
    public class SegmentMmu : IMmu
    {
        private readonly IDmaController _dmaController;
        private readonly IInstructionTimer _instructionTimer;

        private readonly SegmentPointer<IReadableAddressSegment> _readPointer;
        private readonly SegmentPointer<IWriteableAddressSegment> _writePointer;

        private readonly List<AddressRange> _lockedAddressRanges; // TODO: respect these.
        private readonly CancellationTokenSource _dmaThreadCancellation;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SegmentMmu" /> class.
        /// </summary>
        /// <param name="addressSegments">The address segments.</param>
        /// <param name="dmaController">The dma controller.</param>
        /// <param name="instructionTimer">The instruction timer.</param>
        public SegmentMmu(IEnumerable<IAddressSegment> addressSegments,
                          IDmaController dmaController,
                          IInstructionTimer instructionTimer)
        {
            _dmaController = dmaController;
            _instructionTimer = instructionTimer;
            var sortedSegments = addressSegments.OrderBy(x => x.Address).ToArray();

            var readSegments = sortedSegments.OfType<IReadableAddressSegment>().ToArray();
            _readPointer = new SegmentPointer<IReadableAddressSegment>(readSegments);
            
            var writeSegments = sortedSegments.OfType<IWriteableAddressSegment>().ToArray();
            _writePointer = new SegmentPointer<IWriteableAddressSegment>(writeSegments, OnAddressWrite);

            _lockedAddressRanges = new List<AddressRange>();

            // Dma task.
            _dmaThreadCancellation = new CancellationTokenSource();
            Task.Factory.StartNew(DmaTask, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        ///     Reads a byte from the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public byte ReadByte(ushort address)
        {
            var (segment, offset) = _readPointer.GetOffset(address);
            return segment.ReadByte(offset);
        }

        /// <summary>
        ///     Reads a word from the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public ushort ReadWord(ushort address)
        {
            using (var stream = ReadableStream(address))
            using (var reader = new BinaryReader(stream))
            {
                return reader.ReadUInt16();
            }
        }
        
        /// <summary>
        ///     Writes a byte to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="value">The value.</param>
        public void WriteByte(ushort address, byte value)
        {
            var (segment, offset) = _writePointer.GetOffset(address);
            segment.WriteByte(offset, value);

            if (segment.Type.IsMutableApplicationMemory())
            {
                OnAddressWrite(address, 1);
            }
        }

        /// <summary>
        ///     Writes a word to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="word">The word.</param>
        public void WriteWord(ushort address, ushort word)
        {
            using (var stream = WritableStream(address))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(word);
            }
        }
        
        /// <summary>
        ///     Copies a byte from one address to another.
        /// </summary>
        /// <param name="addressFrom">The address from.</param>
        /// <param name="addressTo">The address to.</param>
        public void TransferByte(ushort addressFrom, ushort addressTo)
        {
            var b = ReadByte(addressFrom);
            WriteByte(addressTo, b);
        }

        /// <summary>
        ///     Copies bytes from one address to another.
        /// </summary>
        /// <param name="addressFrom">The address from.</param>
        /// <param name="addressTo">The address to.</param>
        /// <param name="length">The length.</param>
        public void TransferBytes(ushort addressFrom, ushort addressTo, int length)
        {
            using (var read = ReadableStream(addressFrom))
            using (var write = WritableStream(addressTo))
            {
                var buffer = read.ReadBuffer(length);
                write.Write(buffer, 0, length);
            }
        }

        /// <summary>
        /// Gets a stream that wraps this MMU.
        /// </summary>
        /// <param name="address">The address that the stream will initially seek to.</param>
        /// <param name="readable">if set to <c>true</c> [the stream will be readable].</param>
        /// <param name="writable">if set to <c>true</c> [the stream will be writable].</param>
        /// <returns></returns>
        public Stream GetStream(ushort address, bool readable = true, bool writable = true) => new SegmentStream(address, readable ? _readPointer : null, writable ? _writePointer : null);

        /// <summary>
        /// Creates a new collection of state objects representing the current state of the MMU.
        /// </summary>
        /// <returns></returns>
        public ICollection<AddressSegmentState> CreateState() => _readPointer.Segments.Select(s => s.CreateState()).ToList();

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!_dmaThreadCancellation.IsCancellationRequested)
            {
                _dmaThreadCancellation.Cancel();
            }
        }

        /// <summary>
        ///     The long running DMA task.
        /// </summary>
        private async Task DmaTask()
        {
            try
            {
                while (!_dmaThreadCancellation.IsCancellationRequested)
                {
                    var operation = await _dmaController.GetNextAsync(_dmaThreadCancellation.Token);

                    // Check if we need to lock any address ranges.
                    _lockedAddressRanges.AddRange(operation.LockedAddressesRanges);

                    // Execute the operation.
                    operation.Execute(this);

                    await _instructionTimer.DelayAsync(operation.Timings);

                    // Unlock the locked address ranges.
                    _lockedAddressRanges.Clear();
                }
            }
            catch (TaskCanceledException tce)
            {
                if (tce.InnerException != null)
                {
                    throw;
                }
            }
        }

        /// <summary>
        ///     When overridden in a derived class, registers an address write event.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        protected virtual void OnAddressWrite(ushort address, int length)
        {
        }

        private Stream ReadableStream(ushort address) => new SegmentStream(address, _readPointer);

        private Stream WritableStream(ushort address) => new SegmentStream(address, writePointer: _writePointer);
    }
}