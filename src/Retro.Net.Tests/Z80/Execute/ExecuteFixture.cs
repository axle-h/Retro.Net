using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using AutoFixture;
using Bogus;
using Castle.Components.DictionaryAdapter;
using Retro.Net.Tests.Util;
using Retro.Net.Tests.Z80.Decode;
using Retro.Net.Timing;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Core.DynaRec;
using Retro.Net.Z80.Core.Interfaces;
using Retro.Net.Z80.Core.Interpreted;
using Retro.Net.Z80.State;
using FluentAssertions;

namespace Retro.Net.Tests.Z80.Execute
{
    public class ExecuteFixture : IDisposable
    {
        private static readonly Randomizer Rng = new Faker().Random;

        private readonly ushort _address;
        private readonly List<Action<ExecutionContext>> _setups;
        private readonly List<Action<ExecutionContext>> _assertions;
        private InstructionTimings _runtimeTimings;
        private bool _halt;

        public ExecuteFixture()
        {
            _setups = new List<Action<ExecutionContext>>();
            _assertions = new EditableList<Action<ExecutionContext>>();
            _halt = true;

            _address = Rng.UShort(0x6000, 0xaaaa);
            Operation = new OperationFactory(_address);
        }

        public OperationFactory Operation { get; }

        public ExecuteFixture DoNotHalt()
        {
            _halt = false;
            return this;
        }

        public ExecuteFixture RuntimeTiming(int machineCycles, int throttlingStates)
        {
            _runtimeTimings = new InstructionTimings(machineCycles, throttlingStates);
            return this;
        }

        public ExecuteFixture With(params Action<ExecutionContext>[] setups)
        {
            _setups.AddRange(setups);
            return this;
        }

        public ExecuteFixture Assert(params Action<ExecutionContext>[] assertions)
        {
            _assertions.AddRange(assertions);
            return this;
        }

        public ExecuteFixture AssertFlags(Func<ExecutionContext, byte> result = null, bool? sign = null, bool? zero = null,
            bool? halfCarry = null, bool? parityOverflow = null, bool? subtract = null, bool? carry = null, bool setResult = false)
        {
            if (result != null)
            {
                if (setResult)
                {
                    Assert(c => c.Flags.Verify(x => x.SetResultFlags(result(c))));
                }
                else
                {
                    Assert(c => c.Flags.Verify(x => x.SetUndocumentedFlags(result(c))));
                }
            }

            Assert(c => c.VerifyFlag(x => x.Sign, sign));
            Assert(c => c.VerifyFlag(x => x.Zero, zero));
            Assert(c => c.VerifyFlag(x => x.HalfCarry, halfCarry));
            Assert(c => c.VerifyFlag(x => x.ParityOverflow, parityOverflow));
            Assert(c => c.VerifyFlag(x => x.Subtract, subtract));
            Assert(c => c.VerifyFlag(x => x.Carry, carry));
            return this;
        }

        private void Test<TInstructionBlockFactory>() where TInstructionBlockFactory : IInstructionBlockFactory
        {
            var operation = Operation.Build();
            var operations = new List<Operation> { operation };
            if (_halt)
            {
                operations.Add(new Operation(Rng.UShort(operation.Address), OpCode.Halt, Operand.None, Operand.None, FlagTest.None, OpCodeMeta.None, 0, 0, 0));
            }
            var decodedBlock = new DecodedBlock(_address, Rng.Int(10, 20), operations, new InstructionTimings(Rng.Int(10, 20), Rng.Int(10, 20)), _halt, false);

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRuntimeConfig>().Setup(x => x.DebugMode).Returns(true);
                mock.Mock<IPlatformConfig>().Setup(x => x.CpuMode).Returns(CpuMode.Z80);
                
                var autoFixture = new Fixture();
                var registers = autoFixture.Create<GeneralPurposeRegisterState>();
                var accumulator = autoFixture.Create<AccumulatorAndFlagsRegisterState>();

                var context = new ExecutionContext(mock, operation, decodedBlock.Length, registers, accumulator);
                foreach (var setup in _setups)
                {
                    setup(context);
                }
                
                var block = mock.Create<TInstructionBlockFactory>().Build(decodedBlock);

                block.Address.Should().Be(decodedBlock.Address, nameof(block.Address));
                block.Length.Should().Be(decodedBlock.Length, nameof(block.Length));
                block.DebugInfo.Should().NotBeNull(nameof(block.DebugInfo));
                block.HaltCpu.Should().Be(block.HaltCpu, nameof(block.HaltCpu));
                block.HaltPeripherals.Should().Be(block.HaltPeripherals, nameof(block.HaltPeripherals));

                var timings = block.ExecuteInstructionBlock(context.MockRegisters.Object, context.Mmu.Object, context.Alu.Object, context.Io.Object);
                var expectedTimings = decodedBlock.Timings + _runtimeTimings;
                timings.Should().Be(expectedTimings);

                // TODO: also assert I & R on Z80
                if (_halt)
                {
                    context.MockRegisters.VerifySet(x => x.ProgramCounter = context.SyncedProgramCounter);
                }

                foreach (var assertion in _assertions)
                {
                    assertion(context);
                }
            }
        }

        public void Dispose()
        {
            Test<DynaRec>();
            Test<Interpreter>();
        }
    }
}
