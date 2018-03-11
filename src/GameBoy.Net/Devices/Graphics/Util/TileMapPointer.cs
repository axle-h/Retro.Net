using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameBoy.Net.Devices.Graphics.Models;

namespace GameBoy.Net.Devices.Graphics.Util
{
    /// <summary>
    /// Gameboy GPU tile map pointer.
    /// </summary>
    internal class TileMapPointer
    {
        private RenderSettings _renderSettings;
        
        private int _column;
        private int _row;

        private int _lcdColumn;
        private int _lcdRow;

        private TileCache _tiles;

        private IList<byte> _backgroundTileMap;
        private Tile _currentBackgroundTile;
        private int _backgroundTileMapColumn;
        private int _backgroundTileMapRow;
        private int _backgroundTileColumn;
        private int _backgroundTileRow;

        private IList<byte> _windowTileMap;
        private Tile _currentWindowTile;
        private int _windowTileColumn;
        private int _windowTileRow;

        private TileCache _spriteTiles;
        private Sprite[] _allSprites;
        private Sprite[] _currentSprites;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileMapPointer"/> class.
        /// </summary>
        /// <param name="renderContext">State of the render.</param>
        public TileMapPointer(RenderContext renderContext)
        {
            Reset(renderContext);
        }

        /// <summary>
        /// Resets the specified render state.
        /// </summary>
        /// <param name="renderContext">State of the render.</param>
        public void Reset(RenderContext renderContext)
        {
            _renderSettings = renderContext.RenderSettings;

            _lcdColumn = 0;
            _lcdRow = 0;

            _column = renderContext.RenderSettings.ScrollX;
            _row = renderContext.RenderSettings.ScrollY;

            _backgroundTileMap = renderContext.BackgroundTileMap;
            _backgroundTileMapColumn = renderContext.RenderSettings.ScrollX / 8;
            _backgroundTileMapRow = renderContext.RenderSettings.ScrollY / 8;
            _backgroundTileColumn = renderContext.RenderSettings.ScrollX % 8;
            _backgroundTileRow = renderContext.RenderSettings.ScrollY % 8;

            if (_renderSettings.WindowEnabled)
            {
                _windowTileMap = renderContext.WindowTileMap;
            }
            else
            {
                _windowTileMap = null;
                _currentWindowTile = null;
            }
            
            // Can use existing cache if the tile set has not changed.
            if (renderContext.RenderStateChange.HasFlag(RenderStateChange.TileSet))
            {
                _tiles = new TileCache(renderContext.TileSet);
            }

            if (_renderSettings.SpritesEnabled)
            {
                if (_spriteTiles == null || renderContext.RenderStateChange.HasFlag(RenderStateChange.SpriteTileSet))
                {
                    _spriteTiles = renderContext.RenderSettings.SpriteAndBackgroundTileSetShared
                                       ? _tiles
                                       : new TileCache(renderContext.SpriteTileSet);
                }

                if (_allSprites == null || renderContext.RenderStateChange.HasFlag(RenderStateChange.SpriteOam))
                {
                    _allSprites = GetAllSprites(renderContext.SpriteOam).ToArray();
                }

                UpdateRowSprites();
            }
            else
            {
                _spriteTiles = null;
                _allSprites = null;
                _currentSprites = null;
            }
            
            UpdateCurrentTiles();
        }

        /// <summary>
        /// Gets the current pixel under the pointer.
        /// </summary>
        /// <value>
        /// The current pixel under the pointer.
        /// </value>
        public Palette Pixel
        {
            get
            {
                // Check window first as it overlays background.
                var pixel = _currentWindowTile?.Get(_windowTileRow, _windowTileColumn) ??
                            _currentBackgroundTile.Get(_backgroundTileRow, _backgroundTileColumn);
                
                if (!_renderSettings.SpritesEnabled)
                {
                    return pixel;
                }

                foreach (var sprite in _currentSprites.Where(s => _column >= s.X && _column < s.X + 8))
                {
                    // TODO: 8x16 sprites.
                    // TODO: Background priority sprites.
                    var spriteTile = _spriteTiles.GetTile(sprite.TileNumber);
                    return spriteTile.Get(_row - sprite.Y, _column - sprite.X);
                }

                return pixel;
            }
        }

        /// <summary>
        /// Moves the pointer to the next column.
        /// </summary>
        public void NextColumn()
        {
            _column++;
            _lcdColumn++;
            _backgroundTileColumn = (_backgroundTileColumn + 1) % 8;
            if (_backgroundTileColumn == 0)
            {
                _backgroundTileMapColumn++;
                UpdateCurrentTiles();
            }
        }

        /// <summary>
        /// Moves the pointer to the next row.
        /// </summary>
        public void NextRow()
        {
            _row++;
            _lcdRow++;
            _backgroundTileRow = (_backgroundTileRow + 1) % 8;
            if (_backgroundTileRow == 0)
            {
                _backgroundTileMapRow++;
            }

            UpdateRowSprites();

            // Reset column.
            _column = _renderSettings.ScrollX;
            _lcdColumn = 0;
            _backgroundTileMapColumn = _renderSettings.ScrollX / 8;
            _backgroundTileColumn = _renderSettings.ScrollX % 8;
            UpdateCurrentTiles();
        }

        /// <summary>
        /// Updates the row sprites.
        /// </summary>
        private void UpdateRowSprites()
        {
            if (!_renderSettings.SpritesEnabled)
            {
                return;
            }

            // TODO: Multiple sprite priority.
            // TODO: Max 10 sprites per scan.
            // TODO: 8x16 sprites.
            _currentSprites = _allSprites.Where(s => _row >= s.Y && _row < s.Y + 8).ToArray();
        }

        /// <summary>
        /// Updates the background current tile.
        /// </summary>
        private void UpdateCurrentTiles()
        {
            var tileMapIndex = _backgroundTileMapRow * 32 + _backgroundTileMapColumn;

            _currentBackgroundTile = GetTile(_backgroundTileMap[tileMapIndex]);
            _currentWindowTile = null; // reset now just in case we don't set this later.

            if (!_renderSettings.WindowEnabled)
            {
                return;
            }

            var windowX = _lcdColumn - _renderSettings.WindowXPosition;
            var windowY = _lcdRow - _renderSettings.WindowYPosition;

            if (windowX < 0 || windowY < 0)
            {
                // Window only covers to bottom right of background.
                return;
            }

            var windowTileMapColumn = windowX / 8;
            var windowTileMapRow = windowY / 8;

            _windowTileColumn = windowX % 8;
            _windowTileRow = windowY % 8;
            _currentWindowTile = GetTile(_windowTileMap[windowTileMapRow * 32 + windowTileMapColumn]);
        }

        private Tile GetTile(byte tileMapValue)
        {
            int index;
            if (_renderSettings.TileSetIsSigned)
            {
                var signedTileMapValue = (sbyte) tileMapValue;
                //index = signedTileMapValue < 0 ? Math.Abs(signedTileMapValue) - 1 : signedTileMapValue + 128;
                index = signedTileMapValue + 128;
            }
            else
            {
                index = tileMapValue;
            }

            return _tiles.GetTile(index);
        }

        /// <summary>
        /// Gets all sprites.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        private static IEnumerable<Sprite> GetAllSprites(IList<byte> buffer)
        {
            using (var enumerator = buffer.GetEnumerator())
            {
                byte GetByte()
                {
                    enumerator.MoveNext();
                    return enumerator.Current;
                }

                for (var i = 0; i < 40; i++)
                {
                    var y = GetByte() - 16;
                    var x = GetByte() - 8;
                    var n = GetByte();
                    var flags = GetByte();

                    if (x <= 0 || x >= GpuConstants.LcdWidth || y <= 0 || y >= GpuConstants.LcdHeight)
                    {
                        // Off screen sprite.
                        continue;
                    }

                    yield return new Sprite((byte) x,
                                            (byte) y,
                                            n,
                                            (flags & 0x08) > 0,
                                            (flags & 0x04) > 0,
                                            (flags & 0x02) > 0,
                                            (flags & 0x01) > 0);
                }
            }
        }
    }
}