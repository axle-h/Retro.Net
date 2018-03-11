using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Registers;

namespace Retro.Net.Tests.Z80.Registers.Data
{
    internal static class FlagsTestData
    {
        public static IEnumerable<object[]> SetUndocumentedFlags()
            => GetTestDataEntryCollectionResource<byte, Intel8080FlagsRegister>("set-undocumented-flags.json");

        public static IEnumerable<object[]> SetResultFlags()
            => GetTestDataEntryCollectionResource<byte, Intel8080FlagsRegister>("set-result-flags.json");

        public static IEnumerable<object[]> SetParityFlags()
            => GetTestDataEntryCollectionResource<byte, Intel8080FlagsRegister>("set-parity-flags.json");

        public static IEnumerable<object[]> SetFlags()
            => GetTestDataEntryCollectionResource<byte, Intel8080FlagsRegister>("set-flags.json");

        private static IEnumerable<object[]> GetTestDataEntryCollectionResource<TValue, TExpected>(string name)
        {
            var json = Resource.Utf8(GetResourceName(name));
            return JsonConvert.DeserializeObject<ICollection<TestDataEntry<TValue, TExpected>>>(json).Select(x => new[] { x });
        }

        private static string GetResourceName(string name) => $"{typeof(FlagsTestData).Namespace}.{name}";
    }
}
