using System.Linq;

namespace GameBoy.Net.Graphics
{
    public class Frame
    {
        private readonly byte[][] _buffer;

        public Frame(int width, int height)
        {
            Width = width;
            Height = height;

            _buffer = new byte[height][];
            for (var r = 0; r < height; r++)
            {
                _buffer[r] = new byte[width];
            }
        }

        public byte[] GetRow(int row) => _buffer[row];

        public byte[] FlatBuffer => _buffer.SelectMany(x => x).ToArray();

        public int Height { get; }

        public int Width { get; }
    }
}
