using System;

namespace Retro.Net.Util
{
    public static class BitConverterHelpers
    {
        public static ushort To16Bit(byte rH, byte rL) => (ushort) ((rH << 8) | rL);

        public static byte[] To8Bit(ushort r0) => BitConverter.GetBytes(r0);

        public static byte GetLowOrderByte(ushort r0) => (byte) (r0 & 0xff);

        public static byte GetHighOrderByte(ushort r0) => (byte) (r0 >> 8);

        public static ushort SetLowOrderByte(ushort r0, byte b) => (ushort) ((r0 & 0xff00) | b);

        public static ushort SetHighOrderByte(ushort r0, byte b) => (ushort) ((r0 & 0xff) | (b << 8));
    }
}