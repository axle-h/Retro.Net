using Retro.Net.Memory;

namespace Retro.Net.Config
{
    /// <summary>
    /// Memory bank configuration.
    /// </summary>
    public interface IMemoryBankConfig
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        MemoryBankType Type { get; }

        /// <summary>
        /// Gets the bank identifier.
        /// </summary>
        /// <value>
        /// The bank identifier.
        /// </value>
        byte? BankId { get; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        ushort Address { get; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        ushort Length { get; }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <value>
        /// The initial state.
        /// </value>
        byte[] InitialState { get; }
    }
}