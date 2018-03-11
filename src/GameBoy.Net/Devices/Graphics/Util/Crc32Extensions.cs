using System;
using System.Linq;

namespace GameBoy.Net.Devices.Graphics.Util
{
    public static class Crc32Extensions
    {
        private const uint DefaultPolynomial = 0xedb88320u;
        private const uint DefaultSeed = 0xffffffffu;

        private static uint[] CrcTable = GetTable(DefaultPolynomial);

        public static uint Crc32(this ArraySegment<byte> buffer, uint seed = DefaultSeed)
        {
            return buffer.Aggregate(seed, (hash, b) => (hash >> 8) ^ CrcTable[b ^ hash & 0xff]);
        }

        private static uint[] GetTable(uint polynomial)
        {
            var table = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                var entry = i;
                for (var j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ polynomial;
                    }
                    else
                    {
                        entry = entry >> 1;
                    }
                }
                    
                table[i] = entry;
            }
            return table;
        }
    }
}
