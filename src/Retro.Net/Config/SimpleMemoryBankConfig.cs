using System;
using Retro.Net.Memory;

namespace Retro.Net.Config
{
    /// <summary>
    /// Simple memory bank configuration.
    /// </summary>
    public class SimpleMemoryBankConfig : IMemoryBankConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMemoryBankConfig" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bankId">The bank identifier.</param>
        /// <param name="address">The address.</param>
        /// <param name="length">The length.</param>
        public SimpleMemoryBankConfig(MemoryBankType type, byte? bankId, ushort address, ushort length)
            : this(type, bankId, address, length, new byte[length])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMemoryBankConfig" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bankId">The bank identifier.</param>
        /// <param name="address">The address.</param>
        /// <param name="length">The length.</param>
        /// <param name="initialState">The initial state.</param>
        public SimpleMemoryBankConfig(MemoryBankType type, byte? bankId, ushort address, ushort length, byte[] initialState)
        {
            Type = type;
            BankId = bankId;
            Address = address;
            Length = length;
            InitialState = new byte[length];
            Array.Copy(initialState, 0, InitialState, 0, length);
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public MemoryBankType Type { get; }

        /// <summary>
        /// Gets the bank identifier.
        /// </summary>
        /// <value>
        /// The bank identifier.
        /// </value>
        public byte? BankId { get; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address { get; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public ushort Length { get; }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <value>
        /// The initial state.
        /// </value>
        public byte[] InitialState { get; }
    }
}