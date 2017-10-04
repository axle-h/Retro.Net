using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class CompareTests
    {
        [Fact] public void CPD() => TestPrefixEd(PrefixEdOpCode.CPD, OpCode.SearchDecrement);

        [Fact] public void CPDR() => TestPrefixEd(PrefixEdOpCode.CPDR, OpCode.SearchDecrementRepeat);

        [Fact] public void CPI() => TestPrefixEd(PrefixEdOpCode.CPI, OpCode.SearchIncrement);

        [Fact] public void CPIR() => TestPrefixEd(PrefixEdOpCode.CPIR, OpCode.SearchIncrementRepeat);

        private static void TestPrefixEd(PrefixEdOpCode op, OpCode expected)
        {
            using (var fixture = new DecodeFixture(4, 16, PrimaryOpCode.Prefix_ED, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected);
            }
        }
    }
}
