using System;

namespace Retro.Net.Memory.Extensions
{
    /// <summary>
    /// Extensions for <see cref="MemoryBankType"/>.
    /// </summary>
    public static class MemoryBankTypeExtensions
    {
        /// <summary>
        /// Determines whether the specified memory bank type is used for application memory and is mutable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [is mutable application memory] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMutableApplicationMemory(this MemoryBankType type)
        {
            switch (type)
            {
                case MemoryBankType.Unused:
                case MemoryBankType.ReadOnlyMemory:
                case MemoryBankType.Peripheral:
                case MemoryBankType.BankedReadOnlyMemory:
                    return false;

                case MemoryBankType.RandomAccessMemory:
                    return true;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
