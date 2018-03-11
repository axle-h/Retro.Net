using System;
using System.Diagnostics;
using Retro.Net.Timing;
using FluentAssertions;
using Xunit;

namespace Retro.Net.Tests.Z80
{
    public class HighPrecisionTimerTests
    {
        private const long ExpectedTicks = TimeSpan.TicksPerMillisecond * 20;
        private const double Bounds = 0.15; // not so high precision then.
        private const long ExpectedTicksLowerBound = (long) (ExpectedTicks * (1 - Bounds));
        private const long ExpectedTicksUpperBound = (long)(ExpectedTicks * (1 + Bounds));
        private readonly long _ticks;

        public HighPrecisionTimerTests()
        {
            var sw = Stopwatch.StartNew();
            var timer = new HighPrecisionTimer();
            timer.Block(ExpectedTicks);
            sw.Stop();
            _ticks = sw.ElapsedTicks;
        }

        [Fact(Skip = "Too slow for TravisCI")] public void It_should_wait_for_approx_expected_ticks() => _ticks.Should().BeInRange(ExpectedTicksLowerBound, ExpectedTicksUpperBound);
    }
}
