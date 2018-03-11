using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Bogus;
using Moq;
using Retro.Net.Memory.Interfaces;
using Retro.Net.Timing;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.Core.Decode;
using Retro.Net.Z80.OpCodes;
using FluentAssertions;

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
            _address = new Faker().Random.UShort(0x6000, 0xaaaa);
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

        private void Test(CpuMode cpuMode)
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

                string Because(string msg) => $"[{cpuMode}] {msg}";

                // Run.
                var decoder = mock.Create<OpCodeDecoder>();
                if (_throwOn.Contains(cpuMode))
                {
                    Action act = () => decoder.DecodeNextBlock(_address);
                    act.Should().Throw<InvalidOperationException>(Because("this CPU does not support "));
                    return;
                }

                var block = decoder.DecodeNextBlock(_address);
                block.Address.Should().Be(_address, Because(nameof(block.Address)));
                block.Length.Should().Be(totalBytesRead, Because(nameof(block.Length)));
                block.Operations.FirstOrDefault().Should().Be(expected, Because("expected operation"));

                // Timings, adjusted for the extra HALT.
                var timings = block.Timings;
                if (_halt)
                {
                    // Remove tiumings generated by the HALT.
                    timings -= new InstructionTimings(1, 4);
                }
                timings.Should().Be(_expectedTimings, Because("expected timings"));
                
                // Make sure the correct mix of bytes and words were read.
                queue.Should().HaveCount(0, Because($"Didn't read some data: {string.Join(", ", queue.ToArray())}"));
                prefetch.Verify(x => x.NextWord(), Times.Exactly(expectedWordsRead), Because($"Should have read {expectedWordsRead} words"));
                prefetch.Verify(x => x.NextByte(), Times.Exactly(expectedBytesRead), Because($"Should have read {expectedBytesRead} bytes"));
                
                if (_halt)
                {
                   block.Operations.Count.Should().Be(2, Because("Should have a single operation and a HALT"));
                }
                else
                {
                    block.Operations.Count.Should().Be(1, Because("Should have a single operation"));
                }
            }
        }
        
        public void Dispose()
        {
            foreach (var cpu in _cpuModes)
            {
                Test(cpu);
            }
        }
    }
}
