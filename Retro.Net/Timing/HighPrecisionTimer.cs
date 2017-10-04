using System;
using System.Diagnostics;

namespace Retro.Net.Timing
{
    public class HighPrecisionTimer
    {
        private readonly Stopwatch _stopwatch;

        public HighPrecisionTimer()
        {
            if (!Stopwatch.IsHighResolution)
            {
                throw new InvalidOperationException("Must be running in high precision mode.");
            }

            _stopwatch = Stopwatch.StartNew();
        }
        
        public void Block(long ticks)
        {
            while (_stopwatch.ElapsedTicks < ticks)
            {
            }

            _stopwatch.Restart();
        }

        public void BlockFor(long ticks)
        {
            var stopWatch = Stopwatch.StartNew();
            while (stopWatch.ElapsedTicks < ticks)
            {
            }
        }
    }
}
