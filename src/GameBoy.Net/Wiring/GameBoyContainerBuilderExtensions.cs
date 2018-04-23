using Autofac;
using GameBoy.Net.Config;
using GameBoy.Net.Devices.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Retro.Net.Wiring;
using Retro.Net.Z80.Config;

namespace GameBoy.Net.Wiring
{
    public static class GameBoyContainerBuilderExtensions
    {
        /// <summary>
        /// Registers the dependencies required to bootstrap a GameBoy.
        /// You will also ned to register a <see cref="IRenderer" />.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="gameBoyConfig">The game boy configuration.</param>
        /// <param name="coreMode">The core mode.</param>
        /// <returns></returns>
        public static ContainerBuilder RegisterGameBoy(this ContainerBuilder builder,
                                                       IHostingEnvironment environment,
                                                       IGameBoyConfig gameBoyConfig,
                                                       CoreMode coreMode = CoreMode.DynaRec)
        {
            var module = new GameBoyHardwareModule(gameBoyConfig);
            builder.RegisterModule(module);

            var runtimeConfig = new RuntimeConfig(environment.IsDevelopment(), coreMode);
            var z80Module = new Z80Module(runtimeConfig, module.PlatformConfig);
            builder.RegisterModule(z80Module);
            return builder;
        }
    }
}
