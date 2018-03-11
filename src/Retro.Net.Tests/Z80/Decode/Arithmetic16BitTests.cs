using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class Arithmetic16BitTests
    {
        [Theory]
        [InlineData(PrimaryOpCode.ADD_HL_BC, Operand.BC)]
        [InlineData(PrimaryOpCode.ADD_HL_DE, Operand.DE)]
        [InlineData(PrimaryOpCode.ADD_HL_HL, Operand.HL)]
        [InlineData(PrimaryOpCode.ADD_HL_SP, Operand.SP)]
        public void ADD_HL_s(PrimaryOpCode op, Operand s)
        {
            using (var fixture = new DecodeFixture(3, 11, op))
            {
                fixture.Expected.OpCode(OpCode.Add16).Operands(Operand.HL, s);
            }
        }

        [Theory]
        [InlineData(PrimaryOpCode.ADD_HL_BC, Operand.IX, Operand.BC)]
        [InlineData(PrimaryOpCode.ADD_HL_DE, Operand.IX, Operand.DE)]
        [InlineData(PrimaryOpCode.ADD_HL_HL, Operand.IX, Operand.IX)]
        [InlineData(PrimaryOpCode.ADD_HL_SP, Operand.IX, Operand.SP)]
        [InlineData(PrimaryOpCode.ADD_HL_BC, Operand.IY, Operand.BC)]
        [InlineData(PrimaryOpCode.ADD_HL_DE, Operand.IY, Operand.DE)]
        [InlineData(PrimaryOpCode.ADD_HL_HL, Operand.IY, Operand.IY)]
        [InlineData(PrimaryOpCode.ADD_HL_SP, Operand.IY, Operand.SP)]
        public void ADD_IXY_s(PrimaryOpCode op, Operand index, Operand s)
        {
            using (var fixture = new DecodeFixture(4, 15, index.GetZ80IndexPrefix(), op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Add16).Operands(index, s);
            }
        }

        [Theory]
        [InlineData(PrefixEdOpCode.ADC_HL_BC, Operand.BC)]
        [InlineData(PrefixEdOpCode.ADC_HL_DE, Operand.DE)]
        [InlineData(PrefixEdOpCode.ADC_HL_HL, Operand.HL)]
        [InlineData(PrefixEdOpCode.ADC_HL_SP, Operand.SP)]
        public void ADC_HL_s(PrefixEdOpCode op, Operand s)
        {
            using (var fixture = new DecodeFixture(4, 15, PrimaryOpCode.Prefix_ED, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.AddWithCarry16).Operands(Operand.HL, s);
            }
        }

        [Theory]
        [InlineData(PrefixEdOpCode.SBC_HL_BC, Operand.BC)]
        [InlineData(PrefixEdOpCode.SBC_HL_DE, Operand.DE)]
        [InlineData(PrefixEdOpCode.SBC_HL_HL, Operand.HL)]
        [InlineData(PrefixEdOpCode.SBC_HL_SP, Operand.SP)]
        public void SBC_HL_s(PrefixEdOpCode op, Operand s)
        {
            using (var fixture = new DecodeFixture(4, 15, PrimaryOpCode.Prefix_ED, op).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.SubtractWithCarry16).Operands(Operand.HL, s);
            }
        }

        [Theory]
        [InlineData(PrimaryOpCode.INC_BC, Operand.BC)]
        [InlineData(PrimaryOpCode.INC_DE, Operand.DE)]
        [InlineData(PrimaryOpCode.INC_HL, Operand.HL)]
        [InlineData(PrimaryOpCode.INC_SP, Operand.SP)]
        public void INC_ss(PrimaryOpCode op, Operand s)
        {
            using (var fixture = new DecodeFixture(1, 6, op))
            {
                fixture.Expected.OpCode(OpCode.Increment16).Operands(s);
            }
        }

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void INC_IXY(Operand index)
        {
            using (var fixture = new DecodeFixture(2, 10, index.GetZ80IndexPrefix(), PrimaryOpCode.INC_HL).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Increment16).Operands(index);
            }
        }

        [Theory]
        [InlineData(PrimaryOpCode.DEC_BC, Operand.BC)]
        [InlineData(PrimaryOpCode.DEC_DE, Operand.DE)]
        [InlineData(PrimaryOpCode.DEC_HL, Operand.HL)]
        [InlineData(PrimaryOpCode.DEC_SP, Operand.SP)]
        public void DEC_ss(PrimaryOpCode op, Operand s)
        {
            using (var fixture = new DecodeFixture(1, 6, op))
            {
                fixture.Expected.OpCode(OpCode.Decrement16).Operands(s);
            }
        }

        [Theory]
        [InlineData(Operand.IX)]
        [InlineData(Operand.IY)]
        public void DEC_IXY(Operand index)
        {
            using (var fixture = new DecodeFixture(2, 10, index.GetZ80IndexPrefix(), PrimaryOpCode.DEC_HL).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(OpCode.Decrement16).Operands(index);
            }
        }

        
    }
}
