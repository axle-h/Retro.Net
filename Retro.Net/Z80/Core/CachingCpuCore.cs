using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Memory;
using Retro.Net.Memory.Dma;
using Retro.Net.Timing;
using Retro.Net.Z80.Cache;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.Core
{
    /// <summary>
    /// A Z80 CPU core that caches decoded instruction blocks.
    /// </summary>
    public class CachingCpuCore : CpuCoreBase
    {
        private readonly IInstructionBlockCache _instructionBlockCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingCpuCore" /> class.
        /// </summary>
        /// <param name="registers">The registers.</param>
        /// <param name="interruptManager">The interrupt manager.</param>
        /// <param name="peripheralManager">The peripheral manager.</param>
        /// <param name="mmu">The mmu.</param>
        /// <param name="instructionTimer">The instruction timer.</param>
        /// <param name="alu">The alu.</param>
        /// <param name="opCodeDecoder">The opcode decoder.</param>
        /// <param name="instructionBlockFactory">The instruction block decoder.</param>
        /// <param name="dmaController">The dma controller.</param>
        /// <param name="instructionBlockCache">The instruction block cache.</param>
        public CachingCpuCore(IRegisters registers,
            IInterruptManager interruptManager,
            IPeripheralManager peripheralManager,
            IMmu mmu,
            IInstructionTimer instructionTimer,
            IAlu alu,
            IOpCodeDecoder opCodeDecoder,
            IInstructionBlockFactory instructionBlockFactory,
            IDmaController dmaController,
            IInstructionBlockCache instructionBlockCache)
            : base(registers, interruptManager, peripheralManager, mmu, instructionTimer, alu, opCodeDecoder, instructionBlockFactory, dmaController, true)
        {
            _instructionBlockCache = instructionBlockCache;
        }

        /// <summary>
        /// Starts the core process.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">interruptManager</exception>
        /// <exception cref="System.Exception">Instruction block decoder must support caching</exception>
        public override async Task StartCoreProcessAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var address = GetNextAddress();
                var instructionBlock = _instructionBlockCache.GetOrSet(address, () => DecodeBlock(address));
                await ExecuteInstructionBlockAsync(instructionBlock).ConfigureAwait(false);
            }
        }
    }
}