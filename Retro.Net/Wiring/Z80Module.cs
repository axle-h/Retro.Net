using System;
using Autofac;
using Retro.Net.Memory;
using Retro.Net.Memory.Dma;
using Retro.Net.Timing;
using Retro.Net.Z80.Cache;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Core.DynaRec;
using Retro.Net.Z80.Core.Interpreted;
using Retro.Net.Z80.Memory;
using Retro.Net.Z80.Peripherals;
using Retro.Net.Z80.Registers;
using Retro.Net.Z80.Timing;

namespace Retro.Net.Wiring
{
    /// <inheritdoc />
    /// <summary>
    /// Autofac module for wiring up the Z80 core components.
    /// </summary>
    /// <seealso cref="T:Autofac.Module" />
    public class Z80Module : Module
    {
        private readonly IRuntimeConfig _runtimeConfig;
        private readonly IPlatformConfig _platformConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="Z80Module"/> class.
        /// </summary>
        /// <param name="runtimeConfig">The runtime configuration.</param>
        /// <param name="platformConfig">The platform configuration.</param>
        public Z80Module(IRuntimeConfig runtimeConfig, IPlatformConfig platformConfig)
        {
            _runtimeConfig = runtimeConfig;
            _platformConfig = platformConfig;
        }

        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_runtimeConfig);
            builder.RegisterInstance(_platformConfig);
            
            builder.RegisterType<GeneralPurposeRegisterSet>().AsSelf().InZ80Scope();
            builder.RegisterType<AccumulatorAndFlagsRegisterSet>().AsSelf().InZ80Scope();
            
            switch (_platformConfig.CpuMode)
            {
                case CpuMode.Intel8080:
                    builder.RegisterType<QueuingDmaController>().As<IDmaController>().InZ80Scope();
                    builder.RegisterType<Intel8080FlagsRegister>().As<IFlagsRegister>().InZ80Scope();
                    builder.RegisterType<Z80Registers>().As<IRegisters>().InZ80Scope();
                    break;
                case CpuMode.Z80:
                    builder.RegisterType<QueuingDmaController>().As<IDmaController>().InZ80Scope();
                    builder.RegisterType<Intel8080FlagsRegister>().As<IFlagsRegister>().InZ80Scope();
                    builder.RegisterType<Intel8080Registers>().As<IRegisters>().InZ80Scope();
                    break;
                case CpuMode.GameBoy:
                    builder.RegisterType<SimpleDmaController>().As<IDmaController>().InZ80Scope();
                    builder.RegisterType<GameBoyFlagsRegister>().As<IFlagsRegister>().InZ80Scope();
                    builder.RegisterType<Intel8080Registers>().As<IRegisters>().InZ80Scope();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_platformConfig.CpuMode), _platformConfig.CpuMode, null);
            }

            builder.RegisterType<PeripheralManager>().As<IPeripheralManager>().InZ80Scope();
            builder.RegisterType<PrefetchQueue>().As<IPrefetchQueue>().InZ80Scope();
            builder.RegisterType<Alu>().As<IAlu>().InZ80Scope();
            builder.RegisterType<InstructionBlockCache>().As<IInstructionBlockCache>().InZ80Scope();
            builder.RegisterType<InterruptManager>().As<IInterruptManager>().InZ80Scope();
            builder.RegisterType<InstructionTimer>().As<IInstructionTimer>().InZ80Scope();
            builder.RegisterType<OpCodeDecoder>().As<IOpCodeDecoder>().InZ80Scope();
            
            switch (_runtimeConfig.CoreMode)
            {
                case CoreMode.Interpreted:
                    builder.RegisterType<CpuCore>().As<ICpuCore>().InZ80Scope();
                    builder.RegisterType<Interpreter>().As<IInstructionBlockFactory>().InZ80Scope();
                    builder.RegisterType<Z80Mmu>().As<IMmu>().InZ80Scope();
                    break;
                case CoreMode.DynaRec:
                    builder.RegisterType<CachingCpuCore>().As<ICpuCore>().InZ80Scope();
                    builder.RegisterType<DynaRec>().As<IInstructionBlockFactory>().InZ80Scope();
                    builder.RegisterType<CacheAwareZ80Mmu>().As<IMmu>().InZ80Scope();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_runtimeConfig.CoreMode), _runtimeConfig.CoreMode, null);
            }
        }
    }
}
