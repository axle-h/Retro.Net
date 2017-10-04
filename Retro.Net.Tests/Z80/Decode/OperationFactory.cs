using Retro.Net.Tests.Util;
using Retro.Net.Z80.Core.Decode;

namespace Retro.Net.Tests.Z80.Decode
{
    public class OperationFactory
    {
        private static readonly Operand[] Registers = {Operand.A, Operand.B, Operand.C, Operand.D, Operand.E, Operand.H, Operand.L};
        private static readonly Operand[] Registers16 = { Operand.AF, Operand.BC, Operand.DE, Operand.HL };

        private OpCode _opCode;
        private Operand _operand1 = Operand.None;
        private Operand _operand2 = Operand.None;
        private FlagTest _flagTest;
        private OpCodeMeta _opCodeMeta = OpCodeMeta.None;
        private byte _byteLiteral;
        private ushort _wordLiteral;
        private sbyte _displacement;
            
        public OperationFactory(ushort address)
        {
            Address = address;
        }

        protected ushort Address { get; }

        public Operation Build() => new Operation(Address, _opCode, _operand1, _operand2, _flagTest, _opCodeMeta, _byteLiteral, _wordLiteral, _displacement);

        public OperationFactory OpCode(OpCode opCode)
        {
            _opCode = opCode;
            return this;
        }

        public OperationFactory Operands(Operand operand1, Operand? operand2 = null)
        {
            _operand1 = operand1;
            if (operand2.HasValue)
            {
                _operand2 = operand2.Value;
            }
            return this;
        }

        public OperationFactory Operand2(Operand operand2)
        {
            _operand2 = operand2;
            return this;
        }

        public OperationFactory RandomRegister(out Operand o)
        {
            o = _operand1 = Rng.Pick(Registers);
            return this;
        }

        public OperationFactory Random16BitRegister(out Operand o)
        {
            o = _operand1 = Rng.Pick(Registers16);
            return this;
        }

        public OperationFactory RandomRegisters(out Operand o1, out Operand o2)
        {
            o1 = _operand1 = Rng.Pick(Registers);
            o2 = _operand2 = Rng.Pick(Registers);
            return this;
        }

        public OperationFactory FlagTest(FlagTest flagTest)
        {
            _flagTest = flagTest;
            return this;
        }

        public OperationFactory AutoCopy(bool enabled = true)
        {
            _opCodeMeta = enabled ? OpCodeMeta.AutoCopy : OpCodeMeta.None;
            return this;
        }

        public OperationFactory UseAlternativeFlagAffection(bool enabled = true)
        {
            _opCodeMeta = enabled ? OpCodeMeta.UseAlternativeFlagAffection : OpCodeMeta.None;
            return this;
        }

        public OperationFactory ByteLiteral(byte literal)
        {
            _byteLiteral = literal;
            return this;
        }

        public OperationFactory WordLiteral(ushort literal)
        {
            _wordLiteral = literal;
            return this;
        }
        
        public OperationFactory Displacement(sbyte displacement)
        {
            _displacement = displacement;
            return this;
        }

        public OperationFactory RandomLiterals()
        {
            _byteLiteral = Rng.Byte();
            _wordLiteral = Rng.Word();
            _displacement = Rng.SByte();
            return this;
        }
    }
}