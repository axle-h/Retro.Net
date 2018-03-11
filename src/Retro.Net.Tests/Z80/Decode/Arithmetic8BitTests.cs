using Bogus;
using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Xunit;

namespace Retro.Net.Tests.Z80.Decode
{
    public class Arithmetic8BitTests
    {


        [Theory]
        [InlineData(PrimaryOpCode.ADD_A_A, Operand.A)]
        [InlineData(PrimaryOpCode.ADD_A_B, Operand.B)]
        [InlineData(PrimaryOpCode.ADD_A_C, Operand.C)]
        [InlineData(PrimaryOpCode.ADD_A_D, Operand.D)]
        [InlineData(PrimaryOpCode.ADD_A_E, Operand.E)]
        [InlineData(PrimaryOpCode.ADD_A_H, Operand.H)]
        [InlineData(PrimaryOpCode.ADD_A_L, Operand.L)]
        public void ADD_A_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.Add);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void ADD_A_mIXYd(Operand index) => Z80Index(PrimaryOpCode.ADD_A_mHL, index, OpCode.Add);

        [Theory]
        [InlineData(PrimaryOpCode.ADC_A_A, Operand.A)]
        [InlineData(PrimaryOpCode.ADC_A_B, Operand.B)]
        [InlineData(PrimaryOpCode.ADC_A_C, Operand.C)]
        [InlineData(PrimaryOpCode.ADC_A_D, Operand.D)]
        [InlineData(PrimaryOpCode.ADC_A_E, Operand.E)]
        [InlineData(PrimaryOpCode.ADC_A_H, Operand.H)]
        [InlineData(PrimaryOpCode.ADC_A_L, Operand.L)]
        public void ADC_A_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.AddWithCarry);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void ADC_A_mIXYd(Operand index) => Z80Index(PrimaryOpCode.ADC_A_mHL, index, OpCode.AddWithCarry);

        [Theory]
        [InlineData(PrimaryOpCode.SUB_A_A, Operand.A)]
        [InlineData(PrimaryOpCode.SUB_A_B, Operand.B)]
        [InlineData(PrimaryOpCode.SUB_A_C, Operand.C)]
        [InlineData(PrimaryOpCode.SUB_A_D, Operand.D)]
        [InlineData(PrimaryOpCode.SUB_A_E, Operand.E)]
        [InlineData(PrimaryOpCode.SUB_A_H, Operand.H)]
        [InlineData(PrimaryOpCode.SUB_A_L, Operand.L)]
        public void SUB_A_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.Subtract);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void SUB_A_mIXYd(Operand index) => Z80Index(PrimaryOpCode.SUB_A_mHL, index, OpCode.Subtract);

        [Theory]
        [InlineData(PrimaryOpCode.SBC_A_A, Operand.A)]
        [InlineData(PrimaryOpCode.SBC_A_B, Operand.B)]
        [InlineData(PrimaryOpCode.SBC_A_C, Operand.C)]
        [InlineData(PrimaryOpCode.SBC_A_D, Operand.D)]
        [InlineData(PrimaryOpCode.SBC_A_E, Operand.E)]
        [InlineData(PrimaryOpCode.SBC_A_H, Operand.H)]
        [InlineData(PrimaryOpCode.SBC_A_L, Operand.L)]
        public void SBC_A_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.SubtractWithCarry);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void SBC_A_mIXYd(Operand index) => Z80Index(PrimaryOpCode.SBC_A_mHL, index, OpCode.SubtractWithCarry);

        [Theory]
        [InlineData(PrimaryOpCode.AND_A, Operand.A)]
        [InlineData(PrimaryOpCode.AND_B, Operand.B)]
        [InlineData(PrimaryOpCode.AND_C, Operand.C)]
        [InlineData(PrimaryOpCode.AND_D, Operand.D)]
        [InlineData(PrimaryOpCode.AND_E, Operand.E)]
        [InlineData(PrimaryOpCode.AND_H, Operand.H)]
        [InlineData(PrimaryOpCode.AND_L, Operand.L)]
        public void AND_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.And);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void AND_mIXYd(Operand index) => Z80Index(PrimaryOpCode.AND_mHL, index, OpCode.And);

        [Theory]
        [InlineData(PrimaryOpCode.OR_A, Operand.A)]
        [InlineData(PrimaryOpCode.OR_B, Operand.B)]
        [InlineData(PrimaryOpCode.OR_C, Operand.C)]
        [InlineData(PrimaryOpCode.OR_D, Operand.D)]
        [InlineData(PrimaryOpCode.OR_E, Operand.E)]
        [InlineData(PrimaryOpCode.OR_H, Operand.H)]
        [InlineData(PrimaryOpCode.OR_L, Operand.L)]
        public void OR_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.Or);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void OR_mIXYd(Operand index) => Z80Index(PrimaryOpCode.OR_mHL, index, OpCode.Or);

        [Theory]
        [InlineData(PrimaryOpCode.XOR_A, Operand.A)]
        [InlineData(PrimaryOpCode.XOR_B, Operand.B)]
        [InlineData(PrimaryOpCode.XOR_C, Operand.C)]
        [InlineData(PrimaryOpCode.XOR_D, Operand.D)]
        [InlineData(PrimaryOpCode.XOR_E, Operand.E)]
        [InlineData(PrimaryOpCode.XOR_H, Operand.H)]
        [InlineData(PrimaryOpCode.XOR_L, Operand.L)]
        public void XOR_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.Xor);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void XOR_mIXYd(Operand index) => Z80Index(PrimaryOpCode.XOR_mHL, index, OpCode.Xor);

        [Theory]
        [InlineData(PrimaryOpCode.CP_A, Operand.A)]
        [InlineData(PrimaryOpCode.CP_B, Operand.B)]
        [InlineData(PrimaryOpCode.CP_C, Operand.C)]
        [InlineData(PrimaryOpCode.CP_D, Operand.D)]
        [InlineData(PrimaryOpCode.CP_E, Operand.E)]
        [InlineData(PrimaryOpCode.CP_H, Operand.H)]
        [InlineData(PrimaryOpCode.CP_L, Operand.L)]
        public void CP_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.Compare);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void CP_mIXYd(Operand index) => Z80Index(PrimaryOpCode.CP_mHL, index, OpCode.Compare);

        [Theory]
        [InlineData(PrimaryOpCode.INC_A, Operand.A)]
        [InlineData(PrimaryOpCode.INC_B, Operand.B)]
        [InlineData(PrimaryOpCode.INC_C, Operand.C)]
        [InlineData(PrimaryOpCode.INC_D, Operand.D)]
        [InlineData(PrimaryOpCode.INC_E, Operand.E)]
        [InlineData(PrimaryOpCode.INC_H, Operand.H)]
        [InlineData(PrimaryOpCode.INC_L, Operand.L)]
        public void INC_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.Increment);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void INC_mIXYd(Operand index) => Z80Index(PrimaryOpCode.INC_mHL, index, OpCode.Increment, 6, 23);

        [Theory]
        [InlineData(PrimaryOpCode.DEC_A, Operand.A)]
        [InlineData(PrimaryOpCode.DEC_B, Operand.B)]
        [InlineData(PrimaryOpCode.DEC_C, Operand.C)]
        [InlineData(PrimaryOpCode.DEC_D, Operand.D)]
        [InlineData(PrimaryOpCode.DEC_E, Operand.E)]
        [InlineData(PrimaryOpCode.DEC_H, Operand.H)]
        [InlineData(PrimaryOpCode.DEC_L, Operand.L)]
        public void DEC_r(PrimaryOpCode op, Operand r) => Test(op, r, OpCode.Decrement);

        [Theory]
        [InlineData(Operand.mIXd)]
        [InlineData(Operand.mIYd)]
        public void DEC_mIXYd(Operand index) => Z80Index(PrimaryOpCode.DEC_mHL, index, OpCode.Decrement, 6, 23);


        [Fact] public void ADD_A_mHL() => IndexTest(PrimaryOpCode.ADD_A_mHL, OpCode.Add);

        [Fact] public void ADD_A_n() => LiteralTest(PrimaryOpCode.ADD_A_n, OpCode.Add);

        [Fact] public void ADC_A_mHL() => IndexTest(PrimaryOpCode.ADC_A_mHL, OpCode.AddWithCarry);
        
        [Fact] public void ADC_A_n() => LiteralTest(PrimaryOpCode.ADC_A_n, OpCode.AddWithCarry);
        
        [Fact] public void SUB_A_mHL() => IndexTest(PrimaryOpCode.SUB_A_mHL, OpCode.Subtract);

        [Fact] public void SUB_A_n() => LiteralTest(PrimaryOpCode.SUB_A_n, OpCode.Subtract);

        [Fact] public void SBC_A_mHL() => IndexTest(PrimaryOpCode.SBC_A_mHL, OpCode.SubtractWithCarry);

        [Fact] public void SBC_A_n() => LiteralTest(PrimaryOpCode.SBC_A_n, OpCode.SubtractWithCarry);

        [Fact] public void AND_mHL() => IndexTest(PrimaryOpCode.AND_mHL, OpCode.And);

        [Fact] public void AND_n() => LiteralTest(PrimaryOpCode.AND_n, OpCode.And);

        [Fact] public void OR_mHL() => IndexTest(PrimaryOpCode.OR_mHL, OpCode.Or);

        [Fact] public void OR_n() => LiteralTest(PrimaryOpCode.OR_n, OpCode.Or);

        [Fact] public void XOR_mHL() => IndexTest(PrimaryOpCode.XOR_mHL, OpCode.Xor);

        [Fact] public void XOR_n() => LiteralTest(PrimaryOpCode.XOR_n, OpCode.Xor);

        [Fact] public void CP_mHL() => IndexTest(PrimaryOpCode.CP_mHL, OpCode.Compare);

        [Fact] public void CP_n() => LiteralTest(PrimaryOpCode.CP_n, OpCode.Compare);

        [Fact] public void DEC_mHL() => IndexTest(PrimaryOpCode.DEC_mHL, OpCode.Decrement, 3, 11);

        [Fact] public void INC_mHL() => IndexTest(PrimaryOpCode.INC_mHL, OpCode.Increment, 3, 11);

        private static void Test(PrimaryOpCode op, Operand r, OpCode expected, int machineCycles = 1, int throttlingStates = 4)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op))
            {
                fixture.Expected.OpCode(expected).Operands(r);
            }
        }

        private static void IndexTest(PrimaryOpCode op, OpCode expected, int machineCycles = 2, int throttlingStates = 7)
        {
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op))
            {
                fixture.Expected.OpCode(expected).Operands(Operand.mHL);
            }
        }

        private static void LiteralTest(PrimaryOpCode op, OpCode expected, int machineCycles = 2, int throttlingStates = 7)
        {
            var literal = new Faker().Random.Byte();
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, op, literal))
            {
                fixture.Expected.OpCode(expected).Operands(Operand.n).ByteLiteral(literal);
            }
        }

        private static void Z80Index(PrimaryOpCode op, Operand index, OpCode expected, int machineCycles = 5, int throttlingStates = 19)
        {
            var displacement = new Faker().Random.SByte();
            using (var fixture = new DecodeFixture(machineCycles, throttlingStates, index.GetZ80IndexPrefix(), op, displacement).ThrowUnlessZ80())
            {
                fixture.Expected.OpCode(expected).Operands(index).Displacement(displacement);
            }
        }
    }
}
