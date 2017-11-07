using System;
using System.Diagnostics;
using System.Threading;

namespace Retro.Net.Timing
{
    public class HighPrecisionTimer
    {
        private readonly Stopwatch _stopwatch;
        private long _lastTicks;

        public HighPrecisionTimer()
        {
            if (!Stopwatch.IsHighResolution)
            {
                throw new InvalidOperationException("Must be running in high precision mode.");
            }

            _lastTicks = 0;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Block(long ticks)
        {
            var nextTicks = ticks + _lastTicks;
            SpinWait.SpinUntil(() => _stopwatch.ElapsedTicks >= nextTicks);
            _lastTicks = _stopwatch.ElapsedTicks;
        }
    }
}
