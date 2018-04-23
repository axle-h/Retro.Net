using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using GameBoy.Net.Config;
using GameBoy.Net.Devices.Graphics.Models;
using GameBoy.Net.Devices.Graphics.Util;
using GameBoy.Net.Devices.Interfaces;
using GameBoy.Net.Registers.Interfaces;
using Retro.Net.Config;
using Retro.Net.Memory;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Timing;
using Retro.Net.Util;
using Retro.Net.Z80.Core.Interfaces;

namespace GameBoy.Net.Devices.Graphics
{
    /// <summary>
    /// The GameBoy GPU.
    /// </summary>
    /// <seealso cref="IGpu" />
    public class Gpu : IGpu
    {
        private const int ScanLines = 144;
        private const int VerticalBlankScanLines = 153;
        private const int VerticalBlankCycles = 4560 / (VerticalBlankScanLines - ScanLines + 1);
        private const int ReadingOamCycles = 80;
        private const int ReadingVramCycles = 172;
        private const int HorizontalBlankCycles = 204;

        private static readonly IMemoryBankConfig SpriteRamConfig =
            new SimpleMemoryBankConfig(MemoryBankType.Peripheral,
                                       null,
                                       0xfe00,
                                       0xa0);

        private static readonly IMemoryBankConfig MapRamConfig =
            new SimpleMemoryBankConfig(MemoryBankType.Peripheral,
                                       null,
                                       0x8000,
                                       0x2000);
        
        private readonly IGpuRegisters _gpuRegisters;

        private readonly IInterruptFlagsRegister _interruptFlagsRegister;
        
        /// <summary>
        /// $FE00-$FE9F	OAM - Object Attribute Memory
        /// </summary>
        private readonly ArrayBackedMemoryBank _spriteRam;

        /// <summary>
        /// $9C00-$9FFF	Tile map #1
        /// $9800-$9BFF Tile map #0 32*32
        /// $9000-$97FF Tile set #0: tiles 0-127
        /// $8800-$8FFF Tile set #1: tiles 128-255 & Tile set #0: tiles -128 to -1
        /// $8000-$87FF Tile set #1: tiles 0-127
        /// </summary>
        private readonly ArrayBackedMemoryBank _tileRam;

        private readonly ISubject<RenderContext> _renderSubject;
        private readonly IDisposable _subscriptions;
        private RenderChecksum _lastRenderedChecksum;
        
        private int _currentTimings;
        private bool _halted;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gpu" /> class.
        /// </summary>
        /// <param name="gameBoyConfig">The game boy configuration.</param>
        /// <param name="interruptFlagsRegister">The interrupt flags register.</param>
        /// <param name="gpuRegisters">The gpu registers.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="timer">The timer.</param>
        /// <param name="messageBus">The message bus.</param>
        public Gpu(IGameBoyConfig gameBoyConfig,
                   IInterruptFlagsRegister interruptFlagsRegister,
                   IGpuRegisters gpuRegisters,
                   IRenderer renderer,
                   IInstructionTimer timer,
                   IMessageBus messageBus)
        {
            _interruptFlagsRegister = interruptFlagsRegister;
            _gpuRegisters = gpuRegisters;

            _spriteRam = new ArrayBackedMemoryBank(SpriteRamConfig);
            _tileRam = new ArrayBackedMemoryBank(MapRamConfig);
            
            _gpuRegisters.GpuMode = GpuMode.VerticalBlank;
            _gpuRegisters.CurrentScanlineRegister.Scanline = 0x92;

            if (!gameBoyConfig.RunGpu)
            {
                return;
            }

            var timerSubscription = timer.Timing.Subscribe(Sync);

            var initialContext = GetCurrentRenderContext(false);
            var observer = new DistinctRenderContextObserver(gpuRegisters, renderer, initialContext);

            // Render the first frame by publishing directly to the observer.
            observer.OnNext(initialContext);
            _lastRenderedChecksum = initialContext.Checksum;

            _renderSubject = new Subject<RenderContext>();
            var renderSubscription = _renderSubject
                                     .Where(x => !_halted && x.RenderStateChange != RenderStateChange.None)
                                     .Subscribe(observer);

            var metricsSubscription = _renderSubject
                                      .Buffer(TimeSpan.FromSeconds(1))
                                      .Count()
                                      .Subscribe(fps => renderer.UpdateMetrics(new GpuMetrics(fps)));

            var getGpuStateSubscription = messageBus.GetObservable(GameBoyDeviceMessages.GetGpuState)
                                                    .Subscribe(response =>
                                                               {
                                                                   try
                                                                   {
                                                                       response.OnNext(CreateState());
                                                                       response.OnCompleted();
                                                                   }
                                                                   catch (Exception e)
                                                                   {
                                                                       response.OnError(e);
                                                                   }
                                                               });

            _subscriptions = new CompositeDisposable { timerSubscription, renderSubscription, metricsSubscription, getGpuStateSubscription };
        }

        /// <summary>
        /// Gets the address segments.
        /// </summary>
        /// <value>
        /// The address segments.
        /// </value>
        public IEnumerable<IAddressSegment> AddressSegments => new[] { _spriteRam, _tileRam };

        /// <summary>
        /// Halts the GPU thread.
        /// </summary>
        public void Halt() => _halted = true;

        /// <summary>
        /// Resumes the GPU thread.
        /// </summary>
        public void Resume() => _halted = false;

        /// <summary>
        /// Synchronizes the GPU thread and associated registers according to the specified instruction timings.
        /// </summary>
        /// <param name="instructionTimings">The instruction timings.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private void Sync(InstructionTimings instructionTimings)
        {
            if (!_gpuRegisters.LcdControlRegister.LcdOperation)
            {
                return;
            }
            
            _currentTimings += instructionTimings.MachineCycles;

            switch (_gpuRegisters.GpuMode)
            {
                case GpuMode.HorizontalBlank:
                    if (_currentTimings >= HorizontalBlankCycles)
                    {
                        _gpuRegisters.IncrementScanline();
                        _gpuRegisters.GpuMode = _gpuRegisters.CurrentScanlineRegister.Scanline == ScanLines - 1
                                                    ? GpuMode.VerticalBlank
                                                    : GpuMode.ReadingOam;
                        _currentTimings -= HorizontalBlankCycles;
                    }
                    break;
                case GpuMode.VerticalBlank:
                    if (_currentTimings >= VerticalBlankCycles)
                    {
                        if (_gpuRegisters.CurrentScanlineRegister.Scanline == VerticalBlankScanLines)
                        {
                            // Paint.
                            var context = GetCurrentRenderContext(true);
                            _renderSubject.OnNext(context);
                            _lastRenderedChecksum = context.Checksum;

                            // Reset
                            _gpuRegisters.CurrentScanlineRegister.Scanline = 0x00;
                            _gpuRegisters.GpuMode = GpuMode.ReadingOam;
                            _interruptFlagsRegister.UpdateInterrupts(InterruptFlag.VerticalBlank);
                            _currentTimings -= VerticalBlankCycles;
                            break;
                        }

                        _gpuRegisters.IncrementScanline();
                        _currentTimings -= VerticalBlankCycles;
                    }
                    break;
                case GpuMode.ReadingOam:
                    if (_currentTimings >= ReadingOamCycles)
                    {
                        _gpuRegisters.GpuMode = GpuMode.ReadingVram;
                        _currentTimings -= ReadingOamCycles;
                    }
                    break;
                case GpuMode.ReadingVram:
                    if (_currentTimings >= ReadingVramCycles)
                    {
                        _gpuRegisters.GpuMode = GpuMode.HorizontalBlank;
                        _currentTimings -= ReadingVramCycles;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Creates a new state object that describes the GPU.
        /// </summary>
        /// <returns></returns>
        public GpuTileState CreateState() => new TileMapPointer(GetCurrentRenderContext(false)).GetCurrentState();

        private RenderContext GetCurrentRenderContext(bool calculateChecksum)
        {
            var renderSettings = new RenderSettings(_gpuRegisters);

            var backgroundTileMap = _tileRam.Segment(renderSettings.BackgroundTileMapAddress, 0x400);
            var tileSet = _tileRam.Segment(renderSettings.TileSetAddress, 0x1000);
            var windowTileMap = renderSettings.WindowEnabled
                                    ? _tileRam.Segment(renderSettings.WindowTileMapAddress, 0x400)
                                    : new ArraySegment<byte>(Array.Empty<byte>());

            ArraySegment<byte> spriteOam, spriteTileSet;
            if (renderSettings.SpritesEnabled)
            {
                // If the background tiles are read from the sprite pattern table then we can reuse the bytes.
                spriteTileSet = renderSettings.SpriteAndBackgroundTileSetShared ? tileSet : _tileRam.Segment(0x0, 0x1000);
                spriteOam = _spriteRam.Segment(0x0, 0xa0);
            }
            else
            {
                spriteOam = spriteTileSet = new ArraySegment<byte>(Array.Empty<byte>());
            }

            return new RenderContext(renderSettings,
                                     tileSet,
                                     backgroundTileMap,
                                     windowTileMap,
                                     spriteOam,
                                     spriteTileSet,
                                     calculateChecksum ? _lastRenderedChecksum : null as RenderChecksum?);
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}