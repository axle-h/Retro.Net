using System;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Config;
using Retro.Net.Exceptions;
using Retro.Net.Memory.Interfaces;

namespace Retro.Net.Memory
{
    /// <summary>
    /// A read only memory segment (ROM) that consumes an instance of <see cref="IMemoryBankController" /> to switch banks.
    /// </summary>
    /// <seealso cref="IReadableAddressSegment" />
    public class BankedReadOnlyMemoryBank : IReadableAddressSegment
    {
        private byte[] _bank;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankedReadOnlyMemoryBank" /> class.
        /// </summary>
        /// <param name="bankConfigs">The bank configs.</param>
        /// <param name="memoryBankController">The memory bank controller.</param>
        /// <exception cref="System.ArgumentException">
        /// Must provide at least one bank configuration, all must have a bank id and must be unique.
        /// or
        /// All bank configurations must have same address and length - this address segment is banked, remember.
        /// </exception>
        /// <exception cref="MemoryConfigStateException"></exception>
        public BankedReadOnlyMemoryBank(ICollection<IMemoryBankConfig> bankConfigs, IMemoryBankController memoryBankController)
        {
            if (bankConfigs == null || !bankConfigs.Any() || !bankConfigs.All(x => x.BankId.HasValue) ||
                bankConfigs.Select(x => x.BankId.Value).Distinct().Count() != bankConfigs.Count)
            {
                throw new ArgumentException(
                    "Must provide at least one bank configuration, all must have a bank id and must be unique.",
                    nameof(bankConfigs));
            }

            var distinct = bankConfigs.Select(x => new { x.Address, x.Length }).Distinct().ToArray();
            if (distinct.Length > 1)
            {
                throw new ArgumentException(
                    "All bank configurations must have same address and length - this address segment is banked, remember.",
                    nameof(bankConfigs));
            }

            Address = distinct[0].Address;
            Length = distinct[0].Length;

            var badBank = bankConfigs.FirstOrDefault(x => x.InitialState == null || x.Length != Length);
            if (badBank != null)
            {
                throw new MemoryConfigStateException(Address, Length, badBank.InitialState?.Length ?? 0);
            }

            IDictionary<byte, byte[]> banks = bankConfigs.ToDictionary(x => x.BankId.Value,
                x =>
                {
                    var memory = new byte[Length];
                    Array.Copy(x.InitialState, 0, memory, 0, Length);
                    return memory;
                });

            _bank = banks[memoryBankController.RomBankNumber];

            memoryBankController.MemoryBankSwitch += target =>
                                                     {
                                                         if (target != MemoryBankControllerEventTarget.RomBankSwitch)
                                                         {
                                                             return;
                                                         }

                                                         _bank = banks[memoryBankController.RomBankNumber];
                                                     };
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public MemoryBankType Type => MemoryBankType.BankedReadOnlyMemory;

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
        /// Reads a byte from this address segment.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public byte ReadByte(ushort address) => _bank[address];
        
        /// <summary>
        /// Reads bytes from this address segment into the specified buffer.
        /// This does not wrap the segment.
        /// i.e. if address + count is larger than the segment length then this will return a value less than count.
        /// </summary>
        /// <param name="address">The segment address to start reading from.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset to start writing to in the buffer.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>
        /// The number of bytes read into the buffer.
        /// </returns>
        public int ReadBytes(ushort address, byte[] buffer, int offset, int count)
        {
            count = Math.Min(count, Length - address);
            Array.Copy(_bank, address, buffer, offset, count);
            return count;
        }
        
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Type}: 0x{Address:x4} - 0x{Address + Length - 1:x4}";
    }
}