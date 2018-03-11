using System.IO;
using System.Reflection;
using System.Text;

namespace Retro.Net.Tests.Util
{
    internal static class Resource
    {
        private static readonly Assembly Assembly = typeof(Resource).GetTypeInfo().Assembly;

        public static string Utf8(string name)
        {
            using (var stream = Stream(name))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream Stream(string name) => Assembly.GetManifestResourceStream(name);
    }
}
