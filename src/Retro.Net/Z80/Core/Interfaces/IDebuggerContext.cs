using System.Collections.Generic;
using Retro.Net.Memory;
using Retro.Net.Z80.State;

namespace Retro.Net.Z80.Core.Interfaces
{
    public interface IDebuggerContext<TRegisterState>
        where TRegisterState : Intel8080RegisterState
    {
        bool Active { get; }

        TRegisterState Registers { get; }

        ICollection<AddressSegmentState> AddressSpaceState { get; }

        IInstructionBlock Block { get; }

        void Continue();

        void StepOver();
    }
}