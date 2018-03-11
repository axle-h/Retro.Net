using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class TransferTests
    {
        [Fact] public void LDD() => TestEd(PrefixEdOpCode.LDD, OpCode.TransferDecrement);

        [Fact] public void LDDR() => TestEd(PrefixEdOpCode.LDDR, OpCode.TransferDecrementRepeat);

        [Fact] public void LDI() => TestEd(PrefixEdOpCode.LDI, OpCode.TransferIncrement);

        [Fact] public void LDIR() => TestEd(PrefixEdOpCode.LDIR, OpCode.TransferIncrementRepeat);

        private static void TestEd(PrefixEdOpCode op, OpCode expected)
        {
            using (var fixture = new DecodeFixture(4, 16, PrimaryOpCode.Prefix_ED, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected);
            }
        }
    }
}
