namespace GameBoy.Net.Devices.Graphics.Models
{
    public class Frame
    {
        public Frame(int width, int height)
        {
            Width = width;
            Height = height;
            Buffer = new byte[Width * Height];
        }

        public void SetPixel(int x, int y, byte value) => Buffer[Index(x, y)] = value;

        public byte[] Buffer { get; }

        public int Height { get; }

        public int Width { get; }

        private int Index(int x, int y) => Width * y + x;
    }
}
