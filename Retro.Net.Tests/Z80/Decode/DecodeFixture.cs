using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Moq;
using Retro.Net.Memory;
using Retro.Net.Tests.Util;
using Retro.Net.Timing;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using Shouldly;

namespace Retro.Net.Tests.Z80.Decode
{
    public class DecodeFixture : IDisposable
    {
        private readonly InstructionTimings _expectedTimings;
        private readonly object[] _data;
        private readonly ushort _address;
        private CpuMode[] _cpuModes;
        private CpuMode[] _throwOn;
        private bool _halt;
        
        public DecodeFixture(int expectedMachineCycles, int expectedThrottlingStates, params object[] data)
        {
            _expectedTimings = new InstructionTimings(expectedMachineCycles, expectedThrottlingStates);
            _data = data;
            _address = Rng.Word(0x6000, 0xaaaa);
            Expected = new OperationFactory(_address);
            _cpuModes = Enum.GetValues(typeof(CpuMode)).Cast<CpuMode>().ToArray();
            _throwOn = Array.Empty<CpuMode>();
            _halt = true;
        }

        public OperationFactory Expected { get; }

        public DecodeFixture ThrowOnGameboy(bool enabled = true)
        {
            if (enabled)
            {
                _throwOn = new[] { CpuMode.GameBoy };
            }
            return this;
        }

        public DecodeFixture ThrowOn8080()
        {
            _throwOn = new[] { CpuMode.Intel8080 };
            return this;
        }

        public DecodeFixture ThrowUnlessZ80()
        {
            _throwOn = new[] { CpuMode.Intel8080, CpuMode.GameBoy };
            return this;
        }

        public DecodeFixture OnlyGameboy()
        {
            _cpuModes = new[] { CpuMode.GameBoy };
            return this;
        }

        public DecodeFixture NotOnGameboy()
        {
            _cpuModes = new[] { CpuMode.Intel8080, CpuMode.Z80 };
            return this;
        }

        public DecodeFixture OnGameboy(bool enabled)
        {
            _cpuModes = enabled ? Enum.GetValues(typeof(CpuMode)).Cast<CpuMode>().ToArray() : new[] { CpuMode.Intel8080, CpuMode.Z80 };
            return this;
        }

        public DecodeFixture DoNotHalt()
        {
            _halt = false;
            return this;
        }

        private IEnumerable<Action> Test(CpuMode cpuMode)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var expected = Expected.Build();
                var data = _halt ? _data.Concat(new object[] { PrimaryOpCode.HALT }).ToArray() : _data;

                var config = mock.Mock<IPlatformConfig>();
                config.Setup(x => x.UndefinedInstructionBehaviour).Returns(UndefinedInstructionBehaviour.Throw);
                config.Setup(x => x.CpuMode).Returns(cpuMode);

                // Simulate a prefetch queue from the specified data.
                var queue = new Queue(data);
                var prefetch = mock.Mock<IPrefetchQueue>();
                prefetch.Setup(x => x.NextByte()).Returns(() =>
                {
                    var value = queue.Dequeue();
                    switch (value)
                    {
                        case byte b:
                            return b;
                        case sbyte b:
                            return unchecked((byte) b);
                        case PrimaryOpCode op:
                            return (byte) op;
                        case PrefixCbOpCode op:
                            return (byte) op;
                        case PrefixEdOpCode op:
                            return (byte) op;
                        case GameBoyPrimaryOpCode op:
                            return (byte) op;
                        case GameBoyPrefixCbOpCode op:
                            return (byte) op;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(value), value, $"{value.GetType()} not supported");
                    }
                });

                prefetch.Setup(x => x.NextWord()).Returns(() => (ushort) queue.Dequeue());

                var expectedWordsRead = data.Count(x => x is ushort);
                var expectedBytesRead = data.Length - expectedWordsRead; // everything else is a byte.
                var totalBytesRead = expectedWordsRead * 2 + expectedBytesRead;

                prefetch.Setup(x => x.TotalBytesRead).Returns(() => totalBytesRead);

                // Run.
                var decoder = mock.Create<OpCodeDecoder>();
                if (_throwOn.Contains(cpuMode))
                {
                    yield return () => Should.Throw<InvalidOperationException>(() => decoder.DecodeNextBlock(_address));
                    yield break;
                }


                var block = decoder.DecodeNextBlock(_address);
                yield return () => block.Address.ShouldBe(_address, nameof(block.Address));
                yield return () => block.Length.ShouldBe(totalBytesRead, nameof(block.Length));
                yield return () => block.Operations.FirstOrDefault().ShouldBe(expected);

                // Timings, adjusted for the extra HALT.
                var timings = block.Timings;
                if (_halt)
                {
                    // Remove tiumings generated by the HALT.
                    timings -= new InstructionTimings(1, 4);
                }
                yield return () => timings.ShouldBe(_expectedTimings);
                
                // Make sure the correct mix of bytes and words were read.
                yield return () => queue.Count.ShouldBe(0, () => $"Didn't read some data: {string.Join(", ", queue.ToArray())}");
                yield return () => prefetch.Verify(x => x.NextWord(), Times.Exactly(expectedWordsRead), $"Should have read {expectedWordsRead} words");
                yield return () => prefetch.Verify(x => x.NextByte(), Times.Exactly(expectedBytesRead), $"Should have read {expectedBytesRead} bytes");
                
                if (_halt)
                {
                    yield return () => block.Operations.Count.ShouldBe(2, "Should have a single operation and a HALT");
                }
                else
                {
                    yield return () => block.Operations.Count.ShouldBe(1, "Should have a single operation");
                }
            }
        }
        
        public void Dispose()
        {
            var conditions = _cpuModes.Select<CpuMode, Action>(cpu => () => cpu.ShouldSatisfyAllConditions(Test(cpu).ToArray())).ToArray();
            _data.ShouldSatisfyAllConditions(conditions);
        }
    }
}
