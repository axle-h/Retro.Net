using System.Threading.Tasks;

namespace Retro.Net.Z80.Core.Interfaces
{
    public interface IInternalCpuCoreDebugger
    {
        /// <summary>
        /// Passes the next block to the debugger for processing.
        /// </summary>
        /// <param name="instructionBlock">The instruction block.</param>
        /// <returns></returns>
        Task NextBlockAsync(IInstructionBlock instructionBlock);
    }
}