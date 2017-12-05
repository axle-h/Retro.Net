namespace Retro.Net.Util
{
    public static class BitConverterHelpers
    {
        public static ushort To16Bit(byte high, byte low) => (ushort) ((high << 8) | low);

        public static (byte high, byte low) To8Bit(ushort value)
        {
            var high = value >> 8;
            var low = value & 0xff;
            return ((byte) high, (byte) low);
        }

        public static byte GetLowOrderByte(ushort value) => (byte) (value & 0xff);

        public static byte GetHighOrderByte(ushort value) => (byte) (value >> 8);

        public static ushort SetLowOrderByte(ushort value, byte b) => (ushort) ((value & 0xff00) | b);

        public static ushort SetHighOrderByte(ushort value, byte b) => (ushort) ((value & 0xff) | (b << 8));
    }
}