using System;
using System.Collections.Generic;

namespace Retro.Net.Tests.Util
{
    public class SimplePoco
    {
        public long Long { get; set; }

        public int Int { get; set; }

        public short Short { get; set; }

        public sbyte SByte { get; set; }

        public ulong ULong { get; set; }

        public uint UInt { get; set; }

        public ushort UShort { get; set; }

        public byte Byte { get; set; }

        public byte[] Bytes { get; set; }

        public DayOfWeek Enum { get; set; }

        public Guid Guid { get; set; }

        public DateTime DateTime { get; set; }

        public DateTimeOffset DateTimeOffset { get; set; }
    }

    public class SimplePocoWithConstructor
    {
        public SimplePocoWithConstructor(long l, int i, short s, sbyte sByte, ulong uLong, uint uInt, ushort uShort, byte b,
            byte[] bytes, DayOfWeek @enum, Guid guid, DateTime dateTime, DateTimeOffset dateTimeOffset)
        {
            Long = l;
            Int = i;
            Short = s;
            SByte = sByte;
            ULong = uLong;
            UInt = uInt;
            UShort = uShort;
            Byte = b;
            Bytes = bytes;
            Enum = @enum;
            Guid = guid;
            DateTime = dateTime;
            DateTimeOffset = dateTimeOffset;
        }

        public long Long { get; }

        public int Int { get; }

        public short Short { get; }

        public sbyte SByte { get; }

        public ulong ULong { get; }

        public uint UInt { get; }

        public ushort UShort { get; }

        public byte Byte { get; }

        public byte[] Bytes { get; }

        public DayOfWeek Enum { get; }

        public Guid Guid { get; }

        public DateTime DateTime { get; }

        public DateTimeOffset DateTimeOffset { get; }
    }

    public class PocoWithCollections
    {
        public string[] ArrayOfStrings { get; set; }

        public IEnumerable<string> EnumerableOfStrings { get; set; }

        public ICollection<string> CollectionOfStrings { get; set; }

        public IDictionary<string, string> DictionaryOfStrings { get; set; }

        public Dictionary<string, string> ConcreteDictionaryOfStrings { get; set; }

        public IList<string> ListOfStrings { get; set; }

        public List<string> ConcreteListOfStrings { get; set; }
    }
}