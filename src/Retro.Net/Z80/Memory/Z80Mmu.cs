using System;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Config;
using Retro.Net.Memory;
using Retro.Net.Memory.Dma;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Core.Interfaces;
using Retro.Net.Z80.Peripherals;

namespace Retro.Net.Z80.Memory
{
    /// <summary>
    /// Z80 MMU builds a segment MMU taking memory bank configs from <see cref="IPlatformConfig"/> and <see cref="IPeripheralManager"/>.
    /// </summary>
    public class Z80Mmu : SegmentMmu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheAwareZ80Mmu"/> class.
        /// </summary>
        /// <param name="peripheralManager">The peripheral manager.</param>
        /// <param name="platformConfig">The platform configuration.</param>
        /// <param name="memoryBankController">The memory bank controller.</param>
        /// <param name="dmaController">The dma controller.</param>
        /// <param name="instructionTimer">The instruction timer.</param>
        public Z80Mmu(IPeripheralManager peripheralManager,
                      IPlatformConfig platformConfig,
                      IMemoryBankController memoryBankController,
                      IDmaController dmaController,
                      IInstructionTimer instructionTimer)
            : base(GetAddressSegments(peripheralManager, platformConfig, memoryBankController), dmaController, instructionTimer)
        {
        }

        /// <summary>
        /// Gets the address segments.
        /// </summary>
        /// <param name="peripheralManager">The peripheral manager.</param>
        /// <param name="platformConfig">The platform configuration.</param>
        /// <param name="memoryBankController">The memory bank controller.</param>
        /// <returns></returns>
        private static IEnumerable<IAddressSegment> GetAddressSegments(IPeripheralManager peripheralManager,
                                                                       IPlatformConfig platformConfig,
                                                                       IMemoryBankController memoryBankController)
        {
            var memoryBanks = platformConfig.MemoryBanks
                                            .GroupBy(x => x.Address)
                                            .Select(x => GetAddressSegment(x.ToArray(), memoryBankController))
                                            .ToArray();
            return memoryBanks.Concat(peripheralManager.MemoryMap);
        }

        /// <summary>
        /// Gets the address segment.
        /// </summary>
        /// <param name="configs">The configs.</param>
        /// <param name="memoryBankController">The memory bank controller.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// </exception>
        /// <exception cref="System.NotImplementedException">Banked RAM</exception>
        private static IAddressSegment GetAddressSegment(ICollection<IMemoryBankConfig> configs,
                                                         IMemoryBankController memoryBankController)
        {
            var config = configs.First();
            if (configs.Count == 1)
            {
                switch (config.Type)
                {
                    case MemoryBankType.RandomAccessMemory:
                        return new ArrayBackedMemoryBank(config);
                    case MemoryBankType.ReadOnlyMemory:
                        return new ReadOnlyMemoryBank(config);
                    case MemoryBankType.Unused:
                        return new NullMemoryBank(config);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            switch (config.Type)
            {
                case MemoryBankType.RandomAccessMemory:
                    throw new NotImplementedException("Banked RAM");
                case MemoryBankType.ReadOnlyMemory:
                    return new BankedReadOnlyMemoryBank(configs, memoryBankController);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
