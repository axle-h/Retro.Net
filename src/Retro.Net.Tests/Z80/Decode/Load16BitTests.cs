using Bogus;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class Load16BitTests
    {
        private static readonly Randomizer Rng = new Faker().Random;

        [Theory]
        [InlineData(PrimaryOpCode.LD_BC_nn, Operand.BC)]
        [InlineData(PrimaryOpCode.LD_DE_nn, Operand.DE)]
        [InlineData(PrimaryOpCode.LD_HL_nn, Operand.HL)]
        [InlineData(PrimaryOpCode.LD_SP_nn, Operand.SP)]
        public void LD_dd_nn(PrimaryOpCode op, Operand dd)
        {
            var literal = Rng.UShort();
            using (var fixture = new DecodeFixture(3, 10, op, literal))
            {
                fixture.Expected.OpCode(OpCode.Load16).Operands(dd, Operand.nn).WordLiteral(literal);
            }
        }

        [Theory]
        [InlineData(PrefixEdOpCode.LD_BC_mnn, Operand.BC)]
        [InlineData(PrefixEdOpCode.LD_DE_mnn, Operand.DE)]
        [InlineData(PrefixEdOpCode.LD_HL_mnn, Operand.HL)]
        [InlineData(PrefixEdOpCode.LD_SP_mnn, Operand.SP)]
        public void LD_dd_mnn(PrefixEdOpCode op, Operand dd) => TestZ80WordIndexed(op, dd, Operand.mnn);

        [Theory]
        [InlineData(PrefixEdOpCode.LD_mnn_BC, Operand.BC)]
        [InlineData(PrefixEdOpCode.LD_mnn_DE, Operand.DE)]
        [InlineData(PrefixEdOpCode.LD_mnn_HL, Operand.HL)]
        [InlineData(PrefixEdOpCode.LD_mnn_SP, Operand.SP)]
        public void LD_mnn_dd(PrefixEdOpCode op, Operand dd) => TestZ80WordIndexed(op, Operand.mnn, dd);

        [Theory]
        [InlineData(PrimaryOpCode.PUSH_BC, Operand.BC)]
        [InlineData(PrimaryOpCode.PUSH_DE, Operand.DE)]
        [InlineData(PrimaryOpCode.PUSH_HL, Operand.HL)]
        [InlineData(PrimaryOpCode.PUSH_AF, Operand.AF)]
        public void PUSH_qq(PrimaryOpCode op, Operand qq) => TestStackOperation(op, qq);

        [Theory]
        [InlineData(PrimaryOpCode.POP_BC, Operand.BC)]
        [InlineData(PrimaryOpCode.POP_DE, Operand.DE)]
        [InlineData(PrimaryOpCode.POP_HL, Operand.HL)]
        [InlineData(PrimaryOpCode.POP_AF, Operand.AF)]
        public void POP_qq(PrimaryOpCode op, Operand qq) => TestStackOperation(op, qq, OpCode.Pop, 3, 10);

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void PUSH_IXY(Operand index) => TestZ80StackOperation(PrimaryOpCode.PUSH_HL, index);

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void POP_IXY(Operand index) => TestZ80StackOperation(PrimaryOpCode.POP_HL, index, OpCode.Pop, 4, 14);

        [Fact] public void LD_HL_mnn() => TestIndex(PrimaryOpCode.LD_HL_mnn, Operand.HL, Operand.mnn);

        [Fact] public void LD_mnn_HL() => TestIndex(PrimaryOpCode.LD_mnn_HL, Operand.mnn, Operand.HL);

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void LD_IXY_mnn(Operand index) => TestZ80Index(index.GetZ80IndexPrefix(), PrimaryOpCode.LD_HL_mnn, index, Operand.mnn);

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void LD_mnn_IXY(Operand index) => TestZ80Index(index.GetZ80IndexPrefix(), PrimaryOpCode.LD_mnn_HL, Operand.mnn, index);

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void LD_IXY_nn(Operand index) => TestZ80Index(index.GetZ80IndexPrefix(), PrimaryOpCode.LD_HL_nn, index, Operand.nn, 4, 14);

        [Fact]
        public void LD_SP_HL()
        {
            using (var fixture = new DecodeFixture(1, 6, PrimaryOpCode.LD_SP_HL))
            {
                fixture.Expected.OpCode(OpCode.Load16).Operands(Operand.SP, Operand.HL);
            }
        }

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void LD_SP_IXY(Operand index)
        {
            using (var fixture = new DecodeFixture(2, 10, index.GetZ80IndexPrefix(), PrimaryOpCode.LD_SP_HL).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Load16).Operands(Operand.SP, index);
            }
        }

        private static void TestZ80WordIndexed(PrefixEdOpCode op, Operand o0, Operand o1)
        {
            var literal = Rng.UShort();
            using (var fixture = new DecodeFixture(6, 20, PrimaryOpCode.Prefix_ED, op, literal).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Load16).Operands(o0, o1).WordLiteral(literal);
            }
        }

        private static void TestStackOperation(PrimaryOpCode op, Operand qq, OpCode expected = OpCode.Push, int machineCycles = 3, int throttlineStates = 11)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlineStates, op))
            {
                fixture.Expected.OpCode(expected).Operands(qq);
            }
        }

        private static void TestZ80StackOperation(PrimaryOpCode op, Operand qq, OpCode expected = OpCode.Push, int machineCycles = 4, int throttlineStates = 15)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlineStates, qq.GetZ80IndexPrefix(), op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected).Operands(qq);
            }
        }

        private static void TestIndex(PrimaryOpCode op, Operand o0, Operand o1)
        {
            var literal = Rng.UShort();
            using (var fixture = new DecodeFixture(5, 16, op, literal).NotOnGameboy())
            {
                fixture.Expected.OpCode(OpCode.Load16).Operands(o0, o1).WordLiteral(literal);
            }
        }

        private static void TestZ80Index(PrimaryOpCode prefix, PrimaryOpCode op, Operand o0, Operand o1, int maxhineCycles = 6, int throttlineStates = 20)
        {
            var literal = Rng.UShort();
            using (var fixture = new DecodeFixture(maxhineCycles, throttlineStates, prefix, op, literal).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Load16).Operands(o0, o1).WordLiteral(literal);
            }
        }
    }
}
