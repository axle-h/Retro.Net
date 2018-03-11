using System;
using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Memory.Dma;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Timing;
using Retro.Net.Util;
using Retro.Net.Z80.Cache;
using Retro.Net.Z80.Cache.Interfaces;
using Retro.Net.Z80.Core.Interfaces;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.Core
{
    /// <summary>
    /// Base class for CPU cores with external api implemented and dispose wired up.
    /// </summary>
    public class CpuCore : ICpuCore
    {
        private readonly IRegisters _registers;
        private readonly IInterruptService _interruptService;
        private readonly IPeripheralManager _peripheralManager;
        private readonly IMmu _mmu;
        private readonly IInstructionTimer _instructionTimer;
        private readonly IAlu _alu;
        private readonly IOpCodeDecoder _opCodeDecoder;
        private readonly IInstructionBlockFactory _instructionBlockFactory;
        private readonly IDmaController _dmaController;
        private readonly IMessageBus _messageBus;
        private readonly IInstructionBlockCache _instructionBlockCache;
        private readonly ICpuCoreDebugger _debugger;

        private readonly SemaphoreSlim _paused;
        private readonly CancellationTokenSource _cancellation;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="CpuCore" /> class.
        /// </summary>
        /// <param name="registers">The registers.</param>
        /// <param name="interruptService">The interrupt manager.</param>
        /// <param name="peripheralManager">The peripheral manager.</param>
        /// <param name="mmu">The mmu.</param>
        /// <param name="instructionTimer">The instruction timer.</param>
        /// <param name="alu">The alu.</param>
        /// <param name="opCodeDecoder">The opcode decoder.</param>
        /// <param name="instructionBlockFactory">The instruction block decoder.</param>
        /// <param name="dmaController">The dma controller.</param>
        /// <param name="messageBus">The message bus.</param>
        /// <param name="instructionBlockCache">The instruction block cache.</param>
        /// <param name="debugger">The debugger.</param>
        public CpuCore(IRegisters registers,
                       IInterruptService interruptService,
                       IPeripheralManager peripheralManager,
                       IMmu mmu,
                       IInstructionTimer instructionTimer,
                       IAlu alu,
                       IOpCodeDecoder opCodeDecoder,
                       IInstructionBlockFactory instructionBlockFactory,
                       IDmaController dmaController,
                       IMessageBus messageBus,
                       IInstructionBlockCache instructionBlockCache,
                       ICpuCoreDebugger debugger)
        {
            CoreId = Guid.NewGuid();
            _registers = registers;
            _interruptService = interruptService;
            _peripheralManager = peripheralManager;
            _mmu = mmu;
            _instructionTimer = instructionTimer;
            _alu = alu;
            _opCodeDecoder = opCodeDecoder;
            _instructionBlockFactory = instructionBlockFactory;
            _dmaController = dmaController;
            _messageBus = messageBus;
            _instructionBlockCache = instructionBlockCache;
            _debugger = debugger;
            messageBus.RegisterHandler(Message.PauseCpu, Pause);
            messageBus.RegisterHandler(Message.ResumeCpu, Resume);
            _paused = new SemaphoreSlim(1, 1);
            _cancellation = new CancellationTokenSource();
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
        public async Task StartCoreProcessAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var (address, isInterrupt) = _interruptService.InterruptedAddress.HasValue
                                                 ? (_interruptService.InterruptedAddress.Value, true)
                                                 : (_registers.ProgramCounter, false);

                var instructionBlock = _instructionBlockCache.GetOrSet(address, () =>
                                                                                {
                                                                                    var block = _opCodeDecoder.DecodeNextBlock(address);
                                                                                    return _instructionBlockFactory.Build(block);
                                                                                });

                await _debugger.NextBlockAsync(instructionBlock);

                await ExecuteInstructionBlockAsync(instructionBlock, isInterrupt);
            }
        }

        /// <summary>
        /// Retrieve peripheral of specified type.
        /// </summary>
        /// <typeparam name="TPeripheral"></typeparam>
        /// <returns></returns>
        public TPeripheral GetPeripheralOfType<TPeripheral>() where TPeripheral : IPeripheral => _peripheralManager.PeripheralOfType<TPeripheral>();

        /// <summary>
        /// Pauses this core.
        /// </summary>
        public void Pause() => Task.Run(() => _paused.WaitAsync(_cancellation.Token));

        /// <summary>
        /// Resumes this core after a pause.
        /// </summary>
        public void Resume() => Task.Run(() =>
                                         {
                                             if (_paused.CurrentCount == 0)
                                             {
                                                 _paused.Release();
                                             }
                                         });

        /// <summary>
        /// Executes the specified instruction block.
        /// </summary>
        /// <param name="instructionBlock">The instruction block.</param>
        /// <param name="isInterrupt">if set to <c>true</c> [this block was decoded as a result of an interrupt].</param>
        /// <returns></returns>
        private async Task ExecuteInstructionBlockAsync(IInstructionBlock instructionBlock, bool isInterrupt)
        {
            try
            {
                await _paused.WaitAsync(_cancellation.Token);

                if (isInterrupt)
                {
                    // Push the program counter onto the stack
                    _registers.StackPointer = (ushort)(_registers.StackPointer - 2);
                    _mmu.WriteWord(_registers.StackPointer, _registers.ProgramCounter);

                    // Disable interrupts whilst we're... interrupting.
                    _interruptService.InterruptsEnabled = false;
                }

                var timings = instructionBlock.ExecuteInstructionBlock(_registers, _mmu, _alu, _peripheralManager);
                _instructionTimer.SyncToTimings(timings);

                if (isInterrupt)
                {
                    _interruptService.NotifyInterrupted();
                    _interruptService.InterruptsEnabled = true;
                }

                if (!instructionBlock.HaltCpu)
                {
                    return;
                }

                // We're halting.
                if (instructionBlock.HaltPeripherals)
                {
                    _peripheralManager.Signal(ControlSignal.Halt);
                }

                // Wait for next interrupt.
                _instructionTimer.NotifyHalt();
                SpinWait.SpinUntil(() =>
                                   {
                                       _interruptService.InterruptsEnabled = true;
                                       return _interruptService.InterruptedAddress.HasValue;
                                   });
                _instructionTimer.NotifyResume();

                if (instructionBlock.HaltPeripherals)
                {
                    _peripheralManager.Signal(ControlSignal.Resume);
                }
            }
            finally
            {
                _paused.Release();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            lock (_cancellation)
            {
                if (_disposed)
                {
                    return;
                }

                _disposed = true;
                _cancellation.Cancel();
                _paused.Dispose();
                _cancellation.Dispose();
            }
        }
    }
}
