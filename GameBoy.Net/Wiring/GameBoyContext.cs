using System;
using GameBoy.Net.Config;
using GameBoy.Net.Graphics;
using Retro.Net.Wiring;

namespace GameBoy.Net.Wiring
{
    /// <summary>
    /// The primary entrypoint for bootstrapping a GameBoy, GameBoy peripherals and a Z80 core.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class GameBoyContext : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoyContext"/> class.
        /// </summary>
        /// <param name="gameBoyConfig">The game boy configuration.</param>
        /// <param name="renderHandlerFactory">The render handler factory.</param>
        public GameBoyContext(IGameBoyConfig gameBoyConfig, Func<IRenderHandler> renderHandlerFactory)
        {
            var module = new GameBoyHardwareModule(gameBoyConfig, renderHandlerFactory);
            var factory = new AutofacCpuCoreFactory(GameBoyHardwareModule.RuntimeConfig, module.PlatformConfig).RegisterModule(module);
            CoreContext = new CpuCoreContext(factory);
        }

        /// <summary>
        /// Gets the core context.
        /// </summary>
        /// <value>
        /// The core context.
        /// </value>
        public ICpuCoreContext CoreContext { get; }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() => CoreContext?.Dispose();
    }
}
