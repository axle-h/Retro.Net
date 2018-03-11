using System;
using System.Collections.Generic;
using System.Linq;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Core.Interfaces;
using Retro.Net.Z80.Timing;

namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// Core op-code decoder.
    /// This will decode blocks of raw 8080/GBCPU/Z80 operands from a prefetch queue as collections of <see cref="DecodedBlock"/>.
    /// Blocks begin at the address specified when calling <see cref="DecodeNextBlock"/> and end with an operand that could potentially change the value of the PC register.
    /// </summary>
    public partial class OpCodeDecoder : IOpCodeDecoder
    {
        private readonly CpuMode _cpuMode;
        private readonly IDictionary<IndexRegister, IndexRegisterOperands> _indexRegisterOperands;

        private readonly IPrefetchQueue _prefetch;

        private readonly IInstructionTimingsBuilder _timer;
        
        private readonly Func<object, OpCode> _undefinedInstruction;
        private byte _byteLiteral;
        private byte _displacement;
        private FlagTest _flagTest;

        private IndexRegisterOperands _index;
        private OpCodeMeta _opCodeMeta;
        private DecodeMeta _decodeMeta;

        private Operand _operand1;
        private Operand _operand2;
        private ushort _wordLiteral;

        private bool _stop, _halt;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpCodeDecoder"/> class.
        /// </summary>
        /// <param name="platformConfig">The platform configuration.</param>
        /// <param name="prefetch">The prefetch.</param>
        public OpCodeDecoder(IPlatformConfig platformConfig, IPrefetchQueue prefetch)
        {
            _cpuMode = platformConfig.CpuMode;
            _undefinedInstruction = GetUndefinedInstructionFunction(platformConfig.UndefinedInstructionBehaviour);
            _prefetch = prefetch;
            _timer = new InstructionTimingsBuilder();
            _indexRegisterOperands = new Dictionary<IndexRegister, IndexRegisterOperands>
                                     {
                                         {
                                             IndexRegister.HL,
                                             new IndexRegisterOperands(
                                                 Operand.HL,
                                                 Operand.mHL,
                                                 Operand.L,
                                                 Operand.H,
                                                 false)
                                         }
                                     };

            if (_cpuMode == CpuMode.Z80)
            {
                // Initialize Z80 specific index register operands.
                _indexRegisterOperands.Add(IndexRegister.IX,
                                           new IndexRegisterOperands(Operand.IX, Operand.mIXd, Operand.IXl, Operand.IXh, true));
                _indexRegisterOperands.Add(IndexRegister.IY,
                                           new IndexRegisterOperands(Operand.IY, Operand.mIYd, Operand.IYl, Operand.IYh, true));
            }
        }

        /// <summary>
        /// Decodes the next block.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public DecodedBlock DecodeNextBlock(ushort address)
        {
            var operations = DecodeOperations(address).ToList();
            return new DecodedBlock(address, _prefetch.TotalBytesRead, operations, _timer.GetInstructionTimings(), _halt, _stop);
        }

        /// <summary>
        /// Decodes the operations at the specific address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        private IEnumerable<Operation> DecodeOperations(ushort address)
        {
            _halt = _stop = false;
            _timer.Reset();
            _index = _indexRegisterOperands[IndexRegister.HL];
            _prefetch.Seek(address);
            var baseAddress = address;

            while (true)
            {
                // Reset
                _operand1 = Operand.None;
                _operand2 = Operand.None;
                _flagTest = FlagTest.None;
                _opCodeMeta = OpCodeMeta.None;
                _decodeMeta = DecodeMeta.None;
                _byteLiteral = 0x00;
                _wordLiteral = 0x0000;
                _displacement = 0x00;

                var opCode = DecodeNextOpCode();

                if (!opCode.HasValue)
                {
                    continue;
                }

                if (_decodeMeta.HasFlag(DecodeMeta.Displacement))
                {
                    _displacement = _prefetch.NextByte();
                }

                if (_decodeMeta.HasFlag(DecodeMeta.ByteLiteral))
                {
                    _byteLiteral = _prefetch.NextByte();
                }

                if (_decodeMeta.HasFlag(DecodeMeta.WordLiteral))
                {
                    _wordLiteral = _prefetch.NextWord();
                }

                yield return new Operation(address,
                                           opCode.Value,
                                           _operand1,
                                           _operand2,
                                           _flagTest,
                                           _opCodeMeta,
                                           _byteLiteral,
                                           _wordLiteral,
                                           (sbyte) _displacement);

                if (_decodeMeta.HasFlag(DecodeMeta.EndBlock))
                {
                    yield break;
                }

                _index = _indexRegisterOperands[IndexRegister.HL];
                address = (ushort) (baseAddress + _prefetch.TotalBytesRead);
            }
        }

        private Func<object, OpCode> GetUndefinedInstructionFunction(UndefinedInstructionBehaviour behaviour)
        {
            switch (behaviour)
            {
                case UndefinedInstructionBehaviour.Nop:
                    return o => OpCode.NoOperation;
                case UndefinedInstructionBehaviour.Halt:
                    return o => OpCode.Halt;
                case UndefinedInstructionBehaviour.Throw:
                    return o => throw new InvalidOperationException($"{o} is not defined for cpu {_cpuMode}");
                default:
                    throw new ArgumentOutOfRangeException(nameof(behaviour), behaviour, null);
            }
        }

        private IInstructionTimingsBuilder UpdateDisplacement()
        {
            if (!_index.IsDisplaced)
            {
                return _timer.Index(false);
            }

            _decodeMeta |= DecodeMeta.Displacement;
            return _timer.Index(true);
        }
    }
}