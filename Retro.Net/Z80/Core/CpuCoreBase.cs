using System;
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
    /// Base class for CPU cores with external api implemented and dispose wired up.
    /// </summary>
    public abstract class CpuCoreBase : ICpuCore
    {
        private readonly IRegisters _registers;
        private readonly IInterruptManager _interruptManager;
        private readonly IPeripheralManager _peripheralManager;
        private readonly IMmu _mmu;
        private readonly IInstructionTimer _instructionTimer;
        private readonly IAlu _alu;
        private readonly IOpCodeDecoder _opCodeDecoder;
        private readonly IInstructionBlockFactory _instructionBlockFactory;
        private readonly IDmaController _dmaController;
        private readonly IMessageBus _messageBus;

        private TaskCompletionSource<bool> _paused;
        private ushort? _interruptAddress;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="CpuCoreBase" /> class.
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
        /// <param name="requireInstructionBlockCaching">if set to <c>true</c> [require instruction block caching].</param>
        /// <exception cref="ArgumentException">Instruction block decoder must support caching</exception>
        protected CpuCoreBase(IRegisters registers,
            IInterruptManager interruptManager,
            IPeripheralManager peripheralManager,
            IMmu mmu,
            IInstructionTimer instructionTimer,
            IAlu alu,
            IOpCodeDecoder opCodeDecoder,
            IInstructionBlockFactory instructionBlockFactory,
            IDmaController dmaController,
            IMessageBus messageBus,
            bool requireInstructionBlockCaching)
        {
            CoreId = Guid.NewGuid();
            _registers = registers;
            _interruptManager = interruptManager;
            _peripheralManager = peripheralManager;
            _mmu = mmu;
            _instructionTimer = instructionTimer;
            _alu = alu;
            _opCodeDecoder = opCodeDecoder;
            _instructionBlockFactory = instructionBlockFactory;
            _dmaController = dmaController;
            _messageBus = messageBus;
            messageBus.RegisterHandler(Message.PauseCpu, Pause);
            messageBus.RegisterHandler(Message.ResumeCpu, Resume);

            if (requireInstructionBlockCaching && !_instructionBlockFactory.SupportsInstructionBlockCaching)
            {
                throw new ArgumentException("Instruction block decoder must support caching");
            }
        }

        /// <summary>
        /// Gets the core identifier.
        /// </summary>
        /// <value>
        /// The core identifier.
        /// </value>
        public Guid CoreId { get; }

        /// <summary>
        /// Starts the core process.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public abstract Task StartCoreProcessAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve peripheral of specified type.
        /// </summary>
        /// <typeparam name="TPeripheral"></typeparam>
        /// <returns></returns>
        public TPeripheral GetPeripheralOfType<TPeripheral>() where TPeripheral : IPeripheral => _peripheralManager.PeripheralOfType<TPeripheral>();

        /// <summary>
        /// Pauses this core.
        /// </summary>
        public void Pause() => _paused = new TaskCompletionSource<bool>();

        /// <summary>
        /// Resumes this core after a pause.
        /// </summary>
        public void Resume() => Task.Run(() => _paused?.TrySetResult(true));

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _interruptManager.Dispose();
            _peripheralManager.Dispose();
            _mmu.Dispose();
            _dmaController.Dispose();
            _messageBus.Dispose();
        }

        /// <summary>
        /// Gets the next address.
        /// </summary>
        /// <returns></returns>
        protected ushort GetNextAddress() => _interruptAddress ?? _registers.ProgramCounter;

        /// <summary>
        /// Decodeds the block at the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        protected IInstructionBlock DecodeBlock(ushort address) => _instructionBlockFactory.Build(_opCodeDecoder.DecodeNextBlock(address));

        /// <summary>
        /// Executes the specified instruction block.
        /// </summary>
        /// <param name="instructionBlock">The instruction block.</param>
        /// <returns></returns>
        protected async Task ExecuteInstructionBlockAsync(IInstructionBlock instructionBlock)
        {
            if (_paused != null)
            {
                await _paused.Task.ConfigureAwait(false);
            }

            var timings = instructionBlock.ExecuteInstructionBlock(_registers, _mmu, _alu, _peripheralManager);

            if (instructionBlock.HaltCpu)
            {
                _interruptManager.Halt();
                if (instructionBlock.HaltPeripherals)
                {
                    _peripheralManager.Signal(ControlSignal.Halt);
                    _interruptManager.AddResumeTask(() => _peripheralManager.Signal(ControlSignal.Resume));
                }
            }

            if (_interruptManager.IsHalted)
            {
                // Did we request an interrupt or run a HALT opcode.
                if (_interruptManager.IsInterrupted || instructionBlock.HaltCpu)
                {
                    // Notify halt success before halting
                    _interruptManager.NotifyHalt();
                    _interruptAddress = await _interruptManager.WaitForNextInterrupt().ConfigureAwait(false);

                    // Push the program counter onto the stack
                    _registers.StackPointer = (ushort) (_registers.StackPointer - 2);
                    _mmu.WriteWord(_registers.StackPointer, _registers.ProgramCounter);
                }
                else
                {
                    // Dummy halt so we don't block threads trigerring interrupts when disabled.
                    _interruptManager.NotifyHalt();
                }

                _interruptManager.NotifyResume();
            }
            else
            {
                _interruptAddress = null;
            }

            _instructionTimer.SyncToTimings(timings);
        }
    }
}
