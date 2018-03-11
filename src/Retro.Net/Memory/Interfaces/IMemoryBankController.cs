using System;

namespace Retro.Net.Memory.Interfaces
{
    /// <summary>
    /// A memory bank controller.
    /// </summary>
    /// <seealso cref="IWriteableAddressSegment" />
    public interface IMemoryBankController : IWriteableAddressSegment
    {
        /// <summary>
        /// Gets a value indicating whether [ram enable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ram enable]; otherwise, <c>false</c>.
        /// </value>
        bool RamEnable { get; }

        /// <summary>
        /// Gets the rom bank number.
        /// </summary>
        /// <value>
        /// The rom bank number.
        /// </value>
        byte RomBankNumber { get; }

        /// <summary>
        /// Gets the ram bank number.
        /// </summary>
        /// <value>
        /// The ram bank number.
        /// </value>
        byte RamBankNumber { get; }

        /// <summary>
        /// Occurs when [memory bank switch].
        /// </summary>
        event Action<MemoryBankControllerEventTarget> MemoryBankSwitch;
    }
}