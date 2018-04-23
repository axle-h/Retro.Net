using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Core.Interfaces;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.State;

namespace Retro.Net.Z80.Core.Debugger
{
    public class CpuCoreDebugger<TRegisterState> : ICpuCoreDebugger<TRegisterState>, IInternalCpuCoreDebugger, IDisposable
        where TRegisterState : Intel8080RegisterState
    {
        private readonly ConcurrentDictionary<ushort, object> _breakpoints;
        private TaskCompletionSource<bool> _continue;
        private bool _disposed;
        private bool _isStepping;

        private readonly IRegisters _registers;
        private readonly IPlatformConfig _platformConfig;
        private readonly IRuntimeConfig _runtimeConfig;
        private readonly IMmu _mmu;
        private readonly ISubject<IDebuggerContext<TRegisterState>> _subject;

        public CpuCoreDebugger(IRegisters registers, IPlatformConfig platformConfig, IMmu mmu, IRuntimeConfig runtimeConfig)
        {
            _registers = registers;
            _platformConfig = platformConfig;
            _mmu = mmu;
            _runtimeConfig = runtimeConfig;
            _breakpoints = new ConcurrentDictionary<ushort, object>();
            _subject = new ReplaySubject<IDebuggerContext<TRegisterState>>(1);
        }

        /// <summary>
        /// Passes the next block to the debugger for processing.
        /// </summary>
        /// <param name="instructionBlock">The instruction block.</param>
        /// <returns></returns>
        public async Task NextBlockAsync(IInstructionBlock instructionBlock)
        {
            if (_disposed || !_runtimeConfig.DebugMode)
            {
                return;
            }

            if (!_isStepping)
            {
                if (!_breakpoints.Any())
                {
                    // we're not stepping and have no breakpoints to hit.
                    return;
                }

                // we're not stepping so make sure we've hit a breakpoint.
                var breakpoint = _breakpoints.Keys.FirstOrDefault(a => a >= instructionBlock.Address && a < instructionBlock.Address + instructionBlock.Length);
                if (breakpoint == default)
                {
                    return;
                }
            }

            _continue = new TaskCompletionSource<bool>();

            var registers = _platformConfig.CpuMode == CpuMode.Z80
                                ? _registers.GetZ80RegisterState()
                                : _registers.GetIntel8080RegisterState();
            var state = _mmu.CreateState();
            var context = new DebuggerContext(instructionBlock,
                                              () => Continue(false),
                                              () => Continue(true),
                                              (TRegisterState) registers,
                                              state);

            _subject.OnNext(context);

            _isStepping = await _continue.Task;
        }

        /// <summary>
        /// Gets an observable stream of break events.
        /// </summary>
        public IObservable<IDebuggerContext<TRegisterState>> BreakEvents => _subject.Where(x => x.Active).AsObservable();

        /// <summary>
        /// Adds the specified breakpoint.
        /// </summary>
        /// <param name="address">The address to break on.</param>
        public void AddBreakpoint(ushort address) => _breakpoints.TryAdd(address, null);

        /// <summary>
        /// Removes the specified breakpoint.
        /// </summary>
        /// <param name="address">The existing breakpoint address.</param>
        public void RemoveBreakpoint(ushort address) => _breakpoints.TryRemove(address, out _);

        /// <summary>
        /// Gets all defined breakpoints.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<ushort> GetBreakpoints() => _breakpoints.Keys.ToList().AsReadOnly();

        /// <summary>
        /// Breaks ASAP.
        /// </summary>
        /// <returns></returns>
        public void Break() => _isStepping = true;

        private void Continue(bool stepOver) => Task.Run(() => _continue.TrySetResult(stepOver));

        public void Dispose()
        {
            lock (_breakpoints)
            {
                if (_disposed)
                {
                    return;
                }

                _disposed = true;
            }

            _continue.TrySetCanceled();
        }

        private class DebuggerContext : IDebuggerContext<TRegisterState>
        {
            private readonly Action _continueAction;
            private readonly Action _stepOverAction;
            private readonly object _lock = new object();

            public DebuggerContext(IInstructionBlock block,
                                   Action continueAction,
                                   Action stepOverAction,
                                   TRegisterState registers,
                                   ICollection<AddressSegmentState> addressSpaceState)
            {
                Active = true;
                Block = block;
                _continueAction = continueAction;
                _stepOverAction = stepOverAction;
                Registers = registers;
                AddressSpaceState = addressSpaceState;
            }

            public bool Active { get; private set; }

            public TRegisterState Registers { get; }

            public ICollection<AddressSegmentState> AddressSpaceState { get; }

            public IInstructionBlock Block { get; }

            public void Continue()
            {
                Deactivate();
                _continueAction();
            }

            public void StepOver()
            {
                Deactivate();
                _stepOverAction();
            }

            private void Deactivate()
            {
                lock (_lock)
                {
                    if (!Active)
                    {
                        throw new InvalidOperationException("This debugger context is inactive");
                    }

                    Active = false;
                }
            }
        }
    }
}
