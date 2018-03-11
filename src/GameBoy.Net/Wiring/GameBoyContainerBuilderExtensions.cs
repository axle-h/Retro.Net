using System;
using Autofac;
using GameBoy.Net.Config;
using GameBoy.Net.Devices.Interfaces;
using Retro.Net.Wiring;

namespace GameBoy.Net.Wiring
{
    public static class GameBoyContainerBuilderExtensions
    {
        /// <summary>
        /// Registers the dependencies required to bootstrap a GameBoy.
        /// You will also ned to register a <see cref="IRenderer"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="gameBoyConfig">The game boy configuration.</param>
        /// <returns></returns>
        public static ContainerBuilder RegisterGameBoy(this ContainerBuilder builder, IGameBoyConfig gameBoyConfig)
        {
            var module = new GameBoyHardwareModule(gameBoyConfig);
            builder.RegisterModule(module);
            var z80Module = new Z80Module(GameBoyHardwareModule.RuntimeConfig, module.PlatformConfig);
            builder.RegisterModule(z80Module);
            return builder;
        }
    }
}
