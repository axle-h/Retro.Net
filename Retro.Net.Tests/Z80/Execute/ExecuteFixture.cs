using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Castle.Components.DictionaryAdapter;
using Retro.Net.Tests.Util;
using Retro.Net.Tests.Z80.Decode;
using Retro.Net.Timing;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Core;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.Core.DynaRec;
using Retro.Net.Z80.Core.Interpreted;
using Retro.Net.Z80.State;
using Shouldly;

namespace Retro.Net.Tests.Z80.Execute
{
    public class ExecuteFixture : IDisposable
    {
        private readonly ushort _address;
        private readonly List<Action<ExecutionContext>> _setups;
        private readonly List<Action<ExecutionContext>> _assertions;
        private readonly Func<GeneralPurposeRegisterState> _registersFactory;
        private readonly Func<AccumulatorAndFlagsRegisterState> _accumulatorFactory;
        private InstructionTimings _runtimeTimings;
        private bool _halt;

        public ExecuteFixture()
        {
            _setups = new List<Action<ExecutionContext>>();
            _assertions = new EditableList<Action<ExecutionContext>>();
            _halt = true;

            _address = Rng.Word(0x6000, 0xaaaa);
            Operation = new OperationFactory(_address);
            _registersFactory = RngFactory.Build<GeneralPurposeRegisterState>();
            _accumulatorFactory = RngFactory.Build<AccumulatorAndFlagsRegisterState>();
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
            bool? halfCarry = null, bool? parityOverflow = null, bool? subtract = null, bool? carry = null)
        {
            if (result != null)
            {
                Assert(c => c.Flags.Verify(x => x.SetUndocumentedFlags(result(c))));
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
                operations.Add(new Operation(Rng.Word(operation.Address), OpCode.Halt, Operand.None, Operand.None, FlagTest.None, OpCodeMeta.None, 0, 0, 0));
            }
            var decodedBlock = new DecodedBlock(_address, Rng.Int(10, 20), operations, new InstructionTimings(Rng.Int(10, 20), Rng.Int(10, 20)), _halt, false);

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRuntimeConfig>().Setup(x => x.DebugMode).Returns(true);
                mock.Mock<IPlatformConfig>().Setup(x => x.CpuMode).Returns(CpuMode.Z80);
                
                var context = new ExecutionContext(mock, operation, decodedBlock.Length, _registersFactory(), _accumulatorFactory());
                foreach (var setup in _setups)
                {
                    setup(context);
                }
                
                var block = mock.Create<TInstructionBlockFactory>().Build(decodedBlock);
                var assertions = RunAndYieldAssertions(block, decodedBlock, context);
                block.ShouldSatisfyAllConditions($"{typeof(TInstructionBlockFactory)}: {block.DebugInfo}", assertions.ToArray());
            }
        }

        private IEnumerable<Action> RunAndYieldAssertions(IInstructionBlock block, DecodedBlock decodedBlock, ExecutionContext context)
        {
            yield return () => block.Address.ShouldBe(decodedBlock.Address, nameof(block.Address));
            yield return () => block.Length.ShouldBe(decodedBlock.Length, nameof(block.Length));
            yield return () => block.DebugInfo.ShouldNotBeNullOrEmpty(nameof(block.DebugInfo));
            yield return () => block.HaltCpu.ShouldBe(block.HaltCpu, nameof(block.HaltCpu));
            yield return () => block.HaltPeripherals.ShouldBe(block.HaltPeripherals, nameof(block.HaltPeripherals));

            var timings = block.ExecuteInstructionBlock(context.MockRegisters.Object, context.Mmu.Object, context.Alu.Object, context.Peripherals.Object);
            var expectedTimings = decodedBlock.Timings + _runtimeTimings;
            yield return () => timings.ShouldBe(expectedTimings);

            // TODO: also assert I & R on Z80
            if (_halt)
            {
                yield return () => context.MockRegisters.VerifySet(x => x.ProgramCounter = context.SyncedProgramCounter);
            }

            foreach (var assertion in _assertions)
            {
                yield return () => assertion(context);
            }
        }

        public void Dispose() => "All cores".ShouldSatisfyAllConditions("", Test<DynaRec>, Test<Interpreter>);

        public static IEnumerable<object[]> Registers8Bit() =>
            new SimpleTheorySource<Operand>(Operand.A, Operand.B, Operand.C, Operand.D, Operand.E, Operand.H, Operand.L);

        public static IEnumerable<object[]> Registers16Bit() => new SimpleTheorySource<Operand>(Operand.AF, Operand.BC, Operand.DE, Operand.HL, Operand.IX, Operand.IY);
        
        public static IEnumerable<object[]> Operands8Bit() =>
            new SimpleTheorySource<Operand>(Operand.A, Operand.B, Operand.C, Operand.D, Operand.E, Operand.H, Operand.L, Operand.n, Operand.mHL, Operand.mIXd, Operand.mIYd, Operand.mnn);

        public static IEnumerable<object[]> Operands16Bit() =>
            new SimpleTheorySource<Operand>(Operand.AF, Operand.BC, Operand.DE, Operand.HL, Operand.IX, Operand.IY, Operand.nn);
    }
}
