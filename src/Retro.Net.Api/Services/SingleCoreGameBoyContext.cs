using System;
using System.Threading;
using System.Threading.Tasks;
using GameBoy.Net.Devices.Interfaces;
using GameBoy.Net.Peripherals;
using Microsoft.Extensions.Logging;
using Retro.Net.Api.Services.Interfaces;
using Retro.Net.Wiring;
using Retro.Net.Wiring.Interfaces;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Interfaces;

namespace Retro.Net.Api.Services
{
    public class SingleCoreGameBoyContext : IDisposable, IGameBoyContext
    {
        private readonly ICpuCoreContext _coreContext;
        private readonly CancellationTokenSource _disposing;
        private Guid? _singletonCoreId;
        private readonly ILogger<SingleCoreGameBoyContext> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleCoreGameBoyContext" /> class.
        /// </summary>
        /// <param name="coreContext">The core context.</param>
        /// <param name="logger">The logger.</param>
        public SingleCoreGameBoyContext(ICpuCoreContext coreContext, ILogger<SingleCoreGameBoyContext> logger)
        {
            _coreContext = coreContext;
            _logger = logger;
            _disposing = new CancellationTokenSource();
        }

        /// <summary>
        /// Gets the GameBoy Renderer.
        /// </summary>
        /// <returns></returns>
        public IRenderer GetRenderer() => GetCore().GetPeripheralOfType<GameBoyMemoryMappedIo>().Renderer;

        /// <summary>
        /// Gets the GameBoy GPU.
        /// </summary>
        /// <returns></returns>
        public IGpu GetGpu() => GetCore().GetPeripheralOfType<GameBoyMemoryMappedIo>().Gpu;

        private ICpuCore GetCore()
        {
            lock (_coreContext)
            {
                if (_singletonCoreId.HasValue)
                {
                    _logger.LogInformation("Using existing core: " + _singletonCoreId.Value);
                    return _coreContext.GetCore(_singletonCoreId.Value);
                }
                var core = _coreContext.GetNewCore();
                _singletonCoreId = core.CoreId;
                Task.Run(async () =>
                         {
                             while (!_disposing.IsCancellationRequested)
                             {
                                 try
                                 {
                                     _logger.LogInformation("Starting new core: " + _singletonCoreId.Value);
                                     await core.StartCoreProcessAsync(_disposing.Token);
                                 }
                                 catch (TaskCanceledException)
                                 {
                                     _logger.LogWarning("Core completed: " + _singletonCoreId.Value);
                                     return;
                                 }
                                 catch (Exception e)
                                 {
                                     _logger.LogError(e, "Core failure: " + _singletonCoreId.Value);
                                 }

                                 await Task.Delay(1000);
                             }
                         }, _disposing.Token);
                return core;
            }
        }

        public void Dispose()
        {
            lock (_coreContext)
            {
                if (_disposing.IsCancellationRequested)
                {
                    return;
                }

                _disposing.Cancel();
                _disposing.Dispose();
            }
        }
    }
}
