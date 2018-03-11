using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Timing;

namespace Retro.Net.Memory.Dma
{
    /// <summary>
    /// DMA controller implemented with a blocking queue so that overlapping DMA operations are not lost.
    /// This is only required when the DMA hardware supports this feature, e.g. the GameBoy DMA is single threaded so this is unnecessary.
    /// </summary>
    public class QueuingDmaController : IDmaController
    {
        private const int Timeout = 500;
        private readonly object _disposingContext = new object();
        private readonly BlockingCollection<IDmaOperation> _dmaOperations;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueuingDmaController" /> class.
        /// </summary>
        public QueuingDmaController()
        {
            _dmaOperations = new BlockingCollection<IDmaOperation>();
        }

        /// <summary>
        /// Creates a DMA copy operation.
        /// </summary>
        /// <param name="sourceAddress">The source address.</param>
        /// <param name="destinationAddress">The destination address.</param>
        /// <param name="length">The length.</param>
        /// <param name="timings">The cpu cycles required to execute this operation.</param>
        /// <param name="lockedAddressesRanges">The address ranges to lock during the copy operation.</param>
        public void Copy(ushort sourceAddress,
            ushort destinationAddress,
            int length,
            InstructionTimings timings,
            IEnumerable<AddressRange> lockedAddressesRanges)
            => _dmaOperations.Add(new DmaCopyOperation(sourceAddress, destinationAddress, length, timings, lockedAddressesRanges));

        /// <summary>
        /// Gets the next DMA operation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<IDmaOperation> GetNextAsync(CancellationToken cancellationToken)
        {
            IDmaOperation operation;
            while (!_dmaOperations.TryTake(out operation, Timeout, cancellationToken))
            {
            }
            return Task.FromResult(operation);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            lock (_disposingContext)
            {
                if (_disposed)
                {
                    return;
                }

                _disposed = true;
            }

            _dmaOperations.CompleteAdding();
            
            var timeout = Task.Delay(Timeout * 10);
            while (_dmaOperations.Any())
            {
                var iteration = Task.Delay(100);
                var completedTask = Task.WhenAny(timeout, iteration).Result;

                if (completedTask == timeout)
                {
                    throw new Exception("Cannot dispose");
                }
            }
            
            _dmaOperations.Dispose();
        }
    }
}