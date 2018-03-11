using System;
using System.Collections.Generic;
using System.Linq;
using GameBoy.Net.Media;
using GameBoy.Net.Media.Interfaces;
using Retro.Net.Config;
using Retro.Net.Memory;
using Retro.Net.Z80.Config;

namespace GameBoy.Net.Config
{
    /// <summary>
    /// GameBoy platform configuration.
    /// </summary>
    /// <seealso cref="Retro.Net.Z80.Contracts.Config.IPlatformConfig" />
    public class GameBoyPlatformConfig : IPlatformConfig
    {
        private const double CpuFrequency = 4.194304;
        private const ushort SystemMemoryBank1Address = 0xd000;
        private const ushort SystemMemoryBank1Length = 0x1000;
        private const int CgbSystemMemoryBanks = 8;

        private const ushort CartridgeRomBank0Address = 0x0000;
        private const ushort CartridgeRomBank1Address = 0x4000;
        private const ushort CartridgeRomBankLength = 0x4000;

        private const ushort CartridgeRamAddress = 0xa000;
        private const ushort CartridgeRamLength = 0x2000;

        /// <summary>
        /// $E000-$FDFF	Echo RAM - Reserved, Do Not Use
        /// TODO: Implement Echo RAM
        /// </summary>
        private static readonly IMemoryBankConfig EchoConfig = new SimpleMemoryBankConfig(MemoryBankType.Unused,
                                                                                          null,
                                                                                          0xe000,
                                                                                          0x1e00);

        /// <summary>
        /// $FF80-$FFFE	Zero Page - 127 bytes
        /// </summary>
        private static readonly IMemoryBankConfig ZeroPageConfig = new SimpleMemoryBankConfig(MemoryBankType.RandomAccessMemory,
                                                                                              null,
                                                                                              0xff80,
                                                                                              0x7f);

        /// <summary>
        /// $FEA0-$FEFF	Unusable Memory
        /// </summary>
        private static readonly IMemoryBankConfig UnusedConfig = new SimpleMemoryBankConfig(MemoryBankType.Unused,
                                                                                            null,
                                                                                            0xfea0,
                                                                                            0x60);

        /// <summary>
        /// $C000-$CFFF	Internal RAM - Bank 0 (fixed)
        /// </summary>
        private static readonly IMemoryBankConfig SystemMemoryBank0Config =
            new SimpleMemoryBankConfig(MemoryBankType.RandomAccessMemory, null, 0xc000, 0x1000);

        private readonly ICartridgeFactory _cartridgeFactory;

        private readonly IGameBoyConfig _gameBoyConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoyPlatformConfig"/> class.
        /// </summary>
        /// <param name="gameBoyConfig">The game boy configuration.</param>
        /// <param name="cartridgeFactory">The cartridge factory.</param>
        public GameBoyPlatformConfig(IGameBoyConfig gameBoyConfig, ICartridgeFactory cartridgeFactory)
        {
            _gameBoyConfig = gameBoyConfig;
            _cartridgeFactory = cartridgeFactory;
        }

        /// <summary>
        /// Gets the cpu mode.
        /// </summary>
        /// <value>
        /// The cpu mode.
        /// </value>
        public CpuMode CpuMode { get; } = CpuMode.GameBoy;

        /// <summary>
        /// Build the memory banks every time they are requested.
        /// Means we can change stuff in IGameBoyConfig at runtime and get a different system state running.
        /// </summary>
        public IEnumerable<IMemoryBankConfig> MemoryBanks
        {
            get
            {
                var gameBoyType = _gameBoyConfig.GameBoyType;
                var cartridge = _cartridgeFactory.GetCartridge(_gameBoyConfig.CartridgeData);

                // TODO: error checks
                if (cartridge.RomBanks.Length < 2)
                {
                    throw new Exception("All cartridges must have 2 rom banks");
                }

                yield return
                    new SimpleMemoryBankConfig(MemoryBankType.ReadOnlyMemory,
                                               null,
                                               CartridgeRomBank0Address,
                                               CartridgeRomBankLength,
                                               cartridge.RomBanks[0]);
                for (byte i = 1; i < cartridge.RomBanks.Length; i++)
                {
                    yield return
                        new SimpleMemoryBankConfig(MemoryBankType.ReadOnlyMemory,
                                                   i,
                                                   CartridgeRomBank1Address,
                                                   CartridgeRomBankLength,
                                                   cartridge.RomBanks[i]);
                }

                if (cartridge.RamBankLengths.Any())
                {
                    var ramBanks = cartridge.RamBankLengths.Length > 1
                                       ? cartridge.RamBankLengths.SelectMany((length, id) => GetCartridgeRamBankConfig(id, length))
                                       : GetCartridgeRamBankConfig(null, cartridge.RamBankLengths.First());

                    foreach (var bank in ramBanks)
                    {
                        yield return bank;
                    }
                }
                else
                {
                    yield return new SimpleMemoryBankConfig(MemoryBankType.Unused, null, CartridgeRamAddress, CartridgeRamLength);
                }

                yield return SystemMemoryBank0Config;

                switch (gameBoyType)
                {
                    case GameBoyType.GameBoy:
                        yield return
                            new SimpleMemoryBankConfig(MemoryBankType.RandomAccessMemory,
                                                       null,
                                                       SystemMemoryBank1Address,
                                                       SystemMemoryBank1Length);
                        break;
                    case GameBoyType.GameBoyColour:
                        for (byte i = 1; i < CgbSystemMemoryBanks; i++)
                        {
                            yield return
                                new SimpleMemoryBankConfig(MemoryBankType.RandomAccessMemory,
                                                           i,
                                                           SystemMemoryBank1Address,
                                                           SystemMemoryBank1Length);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(gameBoyType), gameBoyType, null);
                }

                yield return UnusedConfig;
                yield return EchoConfig;
                yield return ZeroPageConfig;
            }
        }

        /// <summary>
        /// Gets the machine cycle speed in MHZ.
        /// </summary>
        /// <value>
        /// The machine cycle speed in MHZ.
        /// </value>
        double IPlatformConfig.MachineCycleFrequencyMhz { get; } = CpuFrequency;


        /// <summary>
        /// Gets the instruction timing synchronize mode.
        /// </summary>
        /// <value>
        /// The instruction timing synchronize mode.
        /// </value>
        public InstructionTimingSyncMode InstructionTimingSyncMode { get; } = InstructionTimingSyncMode.MachineCycles;

        /// <summary>
        /// Gets the undefined instruction behaviour.
        /// </summary>
        /// <value>
        /// The undefined instruction behaviour.
        /// </value>
        public UndefinedInstructionBehaviour UndefinedInstructionBehaviour { get; } = UndefinedInstructionBehaviour.Nop;

        /// <summary>
        /// Gets the cartridge RAM bank configuration.
        /// </summary>
        /// <param name="bankId">The bank identifier.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Banked cartridge RAM must be  + CartridgeRamLength +  bytes</exception>
        private static IEnumerable<IMemoryBankConfig> GetCartridgeRamBankConfig(int? bankId, ushort length)
        {
            yield return
                new SimpleMemoryBankConfig(MemoryBankType.RandomAccessMemory,
                                           bankId.HasValue ? (byte?) bankId.Value : null,
                                           CartridgeRamAddress,
                                           length);
            if (length >= CartridgeRamLength)
            {
                yield break;
            }

            if (bankId.HasValue)
            {
                throw new Exception("Banked cartridge RAM must be " + CartridgeRamLength + " bytes");
            }

            yield return
                new SimpleMemoryBankConfig(MemoryBankType.Unused,
                                           null,
                                           (ushort) (CartridgeRamAddress + length),
                                           (ushort) (CartridgeRamLength - length));
        }
    }
}