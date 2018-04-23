using System;
using GameBoy.Net.Devices.Graphics.Models;
using GameBoy.Net.Devices.Graphics.Util;
using GameBoy.Net.Devices.Interfaces;
using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Devices.Graphics
{
    public class DistinctRenderContextObserver : IObserver<RenderContext>
    {
        private readonly IGpuRegisters _gpuRegisters;
        private readonly TileMapPointer _tileMapPointer;
        private readonly Frame _lcdBuffer;
        private readonly IRenderer _renderer;

        public DistinctRenderContextObserver(IGpuRegisters gpuRegisters, IRenderer renderer, RenderContext initialContext)
        {
            _gpuRegisters = gpuRegisters;
            _lcdBuffer = new Frame(GpuConstants.LcdWidth, GpuConstants.LcdHeight);
            _renderer = renderer;
            _tileMapPointer = new TileMapPointer(initialContext);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(RenderContext value)
        {
            _tileMapPointer.Reset(value);

            var bitmapPalette = _gpuRegisters.LcdMonochromePaletteRegister.Pallette;

            for (var y = 0; y < GpuConstants.LcdHeight; y++)
            {
                for (var x = 0; x < GpuConstants.LcdWidth; x++)
                {
                    _lcdBuffer.SetPixel(x, y, (byte) bitmapPalette[_tileMapPointer.Pixel]);

                    if (x + 1 < GpuConstants.LcdWidth)
                    {
                        _tileMapPointer.NextColumn();
                    }
                }

                if (y + 1 < GpuConstants.LcdHeight)
                {
                    _tileMapPointer.NextRow();
                }
            }

            _renderer.Paint(_lcdBuffer);
        }
    }
}
