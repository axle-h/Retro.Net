using System;
using System.Linq;
using Retro.Net.Z80.OpCodes;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Z80
{
    public class OpCodeDistributionTests
    {
        private static void AssertOpCodes<TOpCode>()
        {
            var opCodes = Enum.GetValues(typeof(TOpCode)).Cast<byte>().OrderBy(x => x).ToArray();
            var allBytes = Enumerable.Range(0, 0x100).Select(x => (byte)x).ToArray();

            opCodes.ShouldBe(allBytes);
        }

        [Fact] public void All_prefix_CB_opcodes() => AssertOpCodes<PrefixCbOpCode>();

        [Fact] public void All_primary_opcodes() => AssertOpCodes<PrimaryOpCode>();
    }
}
