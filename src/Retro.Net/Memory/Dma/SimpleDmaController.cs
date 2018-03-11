using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Timing;

namespace Retro.Net.Memory.Dma
{
    /// <summary>
    /// A simple DMA controller.
    /// </summary>
    /// <seealso cref="IDmaController" />
    public class SimpleDmaController : IDmaController
    {
        private TaskCompletionSource<IDmaOperation> _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDmaController"/> class.
        /// </summary>
        public SimpleDmaController()
        {
            _source = new TaskCompletionSource<IDmaOperation>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose.
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
            => Task.Run(() =>
                        _source.TrySetResult(new DmaCopyOperation(sourceAddress,
                                                                  destinationAddress,
                                                                  length,
                                                                  timings,
                                                                  lockedAddressesRanges)));

        /// <summary>
        /// Gets the next DMA operation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IDmaOperation> GetNextAsync(CancellationToken cancellationToken)
        {
            var result = await Task.Run(async () => await _source.Task, cancellationToken);
            _source = new TaskCompletionSource<IDmaOperation>();
            return result;
        }
    }
}
