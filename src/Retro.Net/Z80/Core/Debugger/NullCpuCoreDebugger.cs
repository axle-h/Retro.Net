using System.Threading.Tasks;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Z80.Core.Debugger
{
    /// <summary>
    /// A CPU core debugger that does no debugging.
    /// </summary>
    /// <seealso cref="Retro.Net.Z80.Core.Interfaces.ICpuCoreDebugger" />
    public class NullCpuCoreDebugger : ICpuCoreDebugger
    {
        /// <summary>
        /// Passes the next block to the debugger for processing.
        /// </summary>
        /// <param name="instructionBlock">The instruction block.</param>
        /// <returns></returns>
        public Task NextBlockAsync(IInstructionBlock instructionBlock) => Task.CompletedTask;
    }
}
