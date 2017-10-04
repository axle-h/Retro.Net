using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Timing;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.Core
{
    /// <summary>
    /// </summary>
    /// <seealso cref="IInterruptManager" />
    public class InterruptManager : IInterruptManager
    {
        private readonly IRegisters _registers;

        private TaskCompletionSource<bool> _haltTaskSource;
        private Task<ushort> _interruptTask;

        private TaskCompletionSource<ushort> _interruptTaskSource;

        private TaskCompletionSource<ushort> _nextInterruptSource;
        private readonly CancellationTokenSource _cancellationSource;
        private readonly IInstructionTimer _instructionTimer;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="InterruptManager"/> class.
        /// </summary>
        /// <param name="registers">The registers.</param>
        /// <param name="instructionTimer">The instruction timer.</param>
        public InterruptManager(IRegisters registers, IInstructionTimer instructionTimer)
        {
            _registers = registers;
            _instructionTimer = instructionTimer;
            _haltTaskSource = new TaskCompletionSource<bool>();
            _nextInterruptSource = new TaskCompletionSource<ushort>();
            _cancellationSource = new CancellationTokenSource();
            Task.Factory.StartNew(InterruptTask, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Gets a value indicating whether [interrupts enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [interrupts enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool InterruptsEnabled => _registers.InterruptFlipFlop1;

        /// <summary>
        /// Interrupts the CPU, pushing all registers to the stack and setting the program counter to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        public void Interrupt(ushort address) => _nextInterruptSource.TrySetResult(address);

        /// <summary>
        /// Halts the CPU.
        /// </summary>
        public void Halt()
        {
            _interruptTaskSource = new TaskCompletionSource<ushort>();
            IsHalted = true;
            _interruptTask = _interruptTaskSource.Task;
        }

        /// <summary>
        /// Resumes the CPU from a halt.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">CPU must be halted first.</exception>
        public void Resume()
        {
            if (!IsHalted)
            {
                throw new InvalidOperationException("CPU must be halted first.");
            }

            // Interrupt back to program counter...
            Interrupt(_registers.ProgramCounter);
        }

        /// <summary>
        /// Gets a value indicating whether the CPU is halted.
        /// </summary>
        /// <value>
        /// <c>true</c> if the CPU is halted; otherwise, <c>false</c>.
        /// </value>
        public bool IsHalted { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the CPU is interrupted.
        /// </summary>
        /// <value>
        /// <c>true</c> if the CPU is interrupted; otherwise, <c>false</c>.
        /// </value>
        public bool IsInterrupted { get; private set; }

        /// <summary>
        /// Adds a task to run when the CPU is resumed from a halt state.
        /// </summary>
        /// <param name="task">The task.</param>
        public void AddResumeTask(Action task)
        {
            _interruptTask = _interruptTask.ContinueWith(x =>
                                                         {
                                                             task();
                                                             return x.Result;
                                                         });
        }

        /// <summary>
        /// Notifies this interrupt manager that the CPU has accepted the halt and is halted.
        /// </summary>
        public void NotifyHalt()
        {
            _instructionTimer.NotifyHalt();
            Task.Run(() => _haltTaskSource.TrySetResult(true));
        }

        /// <summary>
        /// Notifies this interrupt manager that the CPU has accepted the interrupt and is running again.
        /// </summary>
        public void NotifyResume()
        {
            _instructionTimer.NotifyResume();
            IsHalted = false;
        } 

        /// <summary>
        /// Gets a task that will wait for next interrupt.
        /// </summary>
        /// <returns></returns>
        public async Task<ushort> WaitForNextInterrupt() => await _interruptTask.ConfigureAwait(false);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!_cancellationSource.IsCancellationRequested)
            {
                _cancellationSource.Cancel();
            }
        }

        /// <summary>
        /// The interrupt background task.
        /// </summary>
        /// <returns></returns>
        private async Task InterruptTask()
        {
            try
            {
                while (!_cancellationSource.IsCancellationRequested)
                {
                    var address = await _nextInterruptSource.Task.ConfigureAwait(false);
                    _nextInterruptSource = new TaskCompletionSource<ushort>();

                    if (!_registers.InterruptFlipFlop1)
                    {
                        // Interrupts disabled. Discard this interrupt.
                        continue;
                    }

                    IsInterrupted = true;

                    // Disable interrupts whilst we're... interrupting.
                    _registers.InterruptFlipFlop1 = false;

                    // Halt the CPU if not already halted
                    if (!IsHalted)
                    {
                        Halt();
                    }

                    // Wait for the halt to be confirmed
                    await _haltTaskSource.Task.ConfigureAwait(false);
                    _haltTaskSource = new TaskCompletionSource<bool>();

                    // Resume the CPU with the program counter set to address
                    var task = Task.Run(() => _interruptTaskSource.TrySetResult(address));
                    IsInterrupted = false;
                }
            }
            catch (TaskCanceledException tce)
            {
                if (tce.InnerException != null)
                {
                    throw;
                }
            }
            catch (AggregateException ae)
            {
                var taskCanceledException = ae.InnerExceptions.OfType<TaskCanceledException>().FirstOrDefault();
                if (taskCanceledException == null || taskCanceledException.InnerException != null)
                {
                    throw;
                }
            }
        }
    }
}