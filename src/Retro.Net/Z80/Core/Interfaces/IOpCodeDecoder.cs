using Retro.Net.Z80.Core.Decode;

namespace Retro.Net.Z80.Core.Interfaces
{
    /// <summary>
    /// Core op-code decoder.
    /// This will decode blocks of raw 8080/GBCPU/Z80 operands from a prefetch queue as collections of <see cref="DecodedBlock"/>.
    /// Blocks begin at the address specified when calling <see cref="DecodeNextBlock"/> and end with an operand that could potentially change the value of the PC register.
    /// </summary>
    public interface IOpCodeDecoder
    {
        /// <summary>
        /// Decodes the next block.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        DecodedBlock DecodeNextBlock(ushort address);
    }
}