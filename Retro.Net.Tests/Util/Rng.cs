using System;
using System.Reflection;

namespace Retro.Net.Tests.Util
{
    public static class Rng
    {
        private static readonly Random Random = new Random();

        public static byte[] Bytes(int length) => InRandomLock(r =>
        {
            var buffer = new byte[length];
            r.NextBytes(buffer);
            return buffer;
        });
        
        public static long Long(long min = long.MinValue, long max = long.MaxValue)
        {
            if (max <= min)
            {
                throw new ArgumentOutOfRangeException(nameof(max), $"invalid range {min} - {max}");
            }
            
            var range = (ulong) (max - min);
            return InRandomLock(r =>
            {
                ulong result;
                var buffer = new byte[8];
                do
                {
                    r.NextBytes(buffer);
                    result = BitConverter.ToUInt64(buffer, 0);
                } while (result > ulong.MaxValue - (ulong.MaxValue % range + 1) % range);

                return (long) (result % range) + min;
            });
        }

        public static ulong UnsignedLong(ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
        {
            if (max <= min)
            {
                throw new ArgumentOutOfRangeException(nameof(max), $"invalid range {min} - {max}");
            }

            var range = max - min;
            return InRandomLock(r =>
            {
                ulong result;
                var buffer = new byte[sizeof(ulong)];
                do
                {
                    r.NextBytes(buffer);
                    result = BitConverter.ToUInt64(buffer, 0);
                } while (result > ulong.MaxValue - (ulong.MaxValue % range + 1) % range);

                return result;
            });
        }

        public static int Int(int min = int.MinValue, int max = int.MaxValue) => InRandomLock(r => r.Next(min, max));

        public static uint UnsignedInt(uint min = uint.MinValue, uint max = uint.MaxValue) => (uint) UnsignedLong(min, max);

        public static short Short(short min = short.MinValue, short max = short.MaxValue) => (short) Int(min, max);

        public static ushort Word(ushort min = ushort.MinValue, ushort max = ushort.MaxValue) => (ushort) UnsignedLong(min, max);

        public static sbyte SByte(sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue) => (sbyte) Int(min, max);

        public static byte Byte(byte min = byte.MinValue, byte max = byte.MaxValue) => (byte) Int(min, max);

        public static char Char() => (char) Short();

        public static bool Boolean() => Double() > 0.5;

        public static double Double() => InRandomLock(r => r.NextDouble());

        public static float Float() => (float) Double();
        
        public static decimal Decimal() => (decimal) (100 * Double());

        public static object Enum(Type type)
        {
            if (!type.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException("Must be an enumerated type", nameof(type));
            }

            var values = System.Enum.GetValues(type);
            return values.GetValue(Int(0, values.Length));
        }

        public static Guid Guid() => System.Guid.NewGuid();

        public static DateTime DateTime() => System.DateTime.UtcNow;

        public static DateTimeOffset DateTimeOffset() => System.DateTimeOffset.UtcNow;

        public static string String() => Guid().ToString();

        public static TItem Pick<TItem>(params TItem[] items) => items[Int(0, items.Length)];

        private static TObject InRandomLock<TObject>(Func<Random, TObject> f)
        {
            lock (Random)
            {
                return f(Random);
            }
        }
    }
}

