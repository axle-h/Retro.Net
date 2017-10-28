using System;
using System.Threading;
using System.Threading.Tasks;
using GameBoy.Net.Peripherals;
using Retro.Net.Api.RealTime.Interfaces;
using Retro.Net.Wiring;
using Retro.Net.Z80.Core;

namespace Retro.Net.Api.RealTime
{
    public class SingleCoreWebSocketContext : IDisposable, IWebSocketContext
    {
        private readonly ICpuCoreContext _coreContext;
        private readonly CancellationTokenSource _disposing;
        private Guid? _singletonCoreId;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleCoreWebSocketContext"/> class.
        /// </summary>
        /// <param name="coreContext">The core context.</param>
        public SingleCoreWebSocketContext(ICpuCoreContext coreContext)
        {
            _coreContext = coreContext;
            _disposing = new CancellationTokenSource();
        }

        public IWebSocketRenderer GetRenderer(Guid id) =>
            GetCore().GetPeripheralOfType<GameBoyMemoryMappedIo>().Renderer as IWebSocketRenderer;

        public Guid GetNewCoreId() => GetCore().CoreId;

        private ICpuCore GetCore()
        {
            lock (_coreContext)
            {
                if (_singletonCoreId.HasValue)
                {
                    return _coreContext.GetCore(_singletonCoreId.Value);
                }
                var core = _coreContext.GetNewCore();
                _singletonCoreId = core.CoreId;
                Task.Run(() => core.StartCoreProcessAsync(_disposing.Token), _disposing.Token);
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
