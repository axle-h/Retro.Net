using Autofac;
using GameBoy.Net.Config;
using GameBoy.Net.Devices;
using GameBoy.Net.Devices.Graphics;
using GameBoy.Net.Devices.Interfaces;
using GameBoy.Net.Media;
using GameBoy.Net.Peripherals;
using GameBoy.Net.Registers;
using GameBoy.Net.Registers.Interfaces;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Wiring;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.State;

namespace GameBoy.Net.Wiring
{
    /// <inheritdoc />
    /// <summary>
    /// Autofac module for wiring up GameBoy hardware and config for a <see cref="T:Retro.Net.Wiring.AutofacCpuCoreFactory" />.
    /// </summary>
    /// <seealso cref="T:Autofac.Module" />
    public class GameBoyHardwareModule : Module
    {

#if DEBUG
        private const bool DebugMode = true;
#else
        private const bool DebugMode = false;
#endif

        /// <summary>
        /// Gets the runtime configuration.
        /// </summary>
        /// <value>
        /// The runtime configuration.
        /// </value>
        public static IRuntimeConfig RuntimeConfig { get; } = new RuntimeConfig(DebugMode, CoreMode.DynaRec);
        
        private readonly IGameBoyConfig _gameBoyConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoyHardwareModule"/> class.
        /// </summary>
        /// <param name="gameBoyConfig">The game boy configuration.</param>
        public GameBoyHardwareModule(IGameBoyConfig gameBoyConfig)
        {
            _gameBoyConfig = gameBoyConfig;
            PlatformConfig = new GameBoyPlatformConfig(gameBoyConfig, new CartridgeFactory());
        }

        /// <summary>
        /// Gets the platform configuration.
        /// </summary>
        /// <value>
        /// The platform configuration.
        /// </value>
        public IPlatformConfig PlatformConfig { get; }

        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_gameBoyConfig);
            builder.RegisterType<InterruptFlagsRegister>().As<IInterruptFlagsRegister>().As<IRegister>().InZ80Scope();
            builder.RegisterType<HardwareRegisters>().As<IHardwareRegisters>().InZ80Scope();

            // Named registers
            builder.RegisterType<InterruptEnableRegister>().As<IInterruptEnableRegister>().InZ80Scope();
            builder.RegisterType<JoyPad>().As<IJoyPadRegister>().As<IJoyPad>().InZ80Scope();
            builder.RegisterType<SyncSerialPort>().As<ISerialPort>().As<ISerialPortRegister>().InZ80Scope();
            builder.RegisterType<MemoryBankController1>().As<IMemoryBankController>().InZ80Scope();
            builder.RegisterType<TimerControlRegister>().As<ITimerControlRegister>().InZ80Scope();
            builder.RegisterType<TimerRegisters>().As<ITimerRegisters>().InZ80Scope();

            // GPU registers.
            builder.RegisterType<GpuRegisters>().As<IGpuRegisters>().InZ80Scope();
            builder.RegisterType<LcdControlRegister>().As<ILcdControlRegister>().InZ80Scope();
            builder.RegisterType<CurrentScanlineRegister>().As<ICurrentScanlineRegister>().InZ80Scope();
            builder.RegisterType<LcdMonochromePaletteRegister>().As<ILcdMonochromePaletteRegister>().InZ80Scope();
            builder.RegisterType<LcdStatusRegister>().As<ILcdStatusRegister>().InZ80Scope();

            // Un-named registers
            builder.RegisterType<LazyDividerRegister>().As<IRegister>().InZ80Scope();
            builder.RegisterType<LcdOamDmaTransferRegister>().As<IRegister>().InZ80Scope();

            // Peripherals, no IO mapped peripherals on GB, only memory mapped
            builder.RegisterType<GameBoyMemoryMappedIo>().As<IPeripheral>().InZ80Scope();

            // GPU
            builder.RegisterType<Gpu>().As<IGpu>().InZ80Scope();

            // Initial state.
            var initialRegisterState =
                new Intel8080RegisterState(new GeneralPurposeRegisterState(0x00, 0x13, 0x00, 0xd8, 0x01, 0x4d),
                                           new AccumulatorAndFlagsRegisterState(0x01, 0xb0),
                                           0xfffe,
                                           0x0100,
                                           true,
                                           true,
                                           InterruptMode.InterruptMode0);
            builder.RegisterInstance(initialRegisterState);
        }
    }
}
