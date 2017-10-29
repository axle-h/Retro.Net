using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Memory;
using Retro.Net.Memory.Dma;
using Retro.Net.Timing;
using Retro.Net.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.Core
{
    /// <summary>
    /// A simple Z80 CPU core.
    /// </summary>
    /// <seealso cref="CpuCoreBase" />
    public class CpuCore : CpuCoreBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CpuCore" /> class.
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
        /// <param name="messageBus">The message bus.</param>
        public CpuCore(IRegisters registers,
            IInterruptManager interruptManager,
            IPeripheralManager peripheralManager,
            IMmu mmu,
            IInstructionTimer instructionTimer,
            IAlu alu,
            IOpCodeDecoder opCodeDecoder,
            IInstructionBlockFactory instructionBlockFactory,
            IDmaController dmaController,
            IMessageBus messageBus)
            : base(registers, interruptManager, peripheralManager, mmu, instructionTimer, alu, opCodeDecoder, instructionBlockFactory, dmaController, messageBus, false)
        {
        }

        /// <summary>
        /// Starts the core process.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public override async Task StartCoreProcessAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var address = GetNextAddress();
                var instructionBlock = DecodeBlock(address);
                await ExecuteInstructionBlockAsync(instructionBlock).ConfigureAwait(false);
            }
        }
    }
}
