using System.Threading.Tasks;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Z80.Core.Debugger
{
    public class CpuCoreDebugger : ICpuCoreDebugger
    {
        public Task NextBlockAsync(IInstructionBlock instructionBlock)
        {
            return Task.CompletedTask;
        }
    }
}
