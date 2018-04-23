using System;
using System.Collections.Generic;
using Retro.Net.Z80.State;

namespace Retro.Net.Z80.Core.Interfaces
{
    public interface ICpuCoreDebugger<TRegisterState>
        where TRegisterState : Intel8080RegisterState
    {
        /// <summary>
        /// Gets an observable stream of break events.
        /// </summary>
        IObservable<IDebuggerContext<TRegisterState>> BreakEvents { get; }

        /// <summary>
        /// Adds the specified breakpoint.
        /// </summary>
        /// <param name="address">The address to break on.</param>
        void AddBreakpoint(ushort address);

        /// <summary>
        /// Removes the specified breakpoint.
        /// </summary>
        /// <param name="address">The existing breakpoint address.</param>
        void RemoveBreakpoint(ushort address);

        /// <summary>
        /// Gets all defined breakpoints.
        /// </summary>
        /// <returns></returns>
        IReadOnlyCollection<ushort> GetBreakpoints();

        /// <summary>
        /// Breaks ASAP.
        /// </summary>
        /// <returns></returns>
        void Break();
    }
}
