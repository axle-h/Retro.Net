namespace GameBoy.Net.Graphics
{
    public class GpuMetrics
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpuMetrics"/> class.
        /// </summary>
        /// <param name="framsPerSecond">The frams per second.</param>
        /// <param name="skippedFrames">The skipped frames.</param>
        public GpuMetrics(int framsPerSecond, int skippedFrames)
        {
            FramsPerSecond = framsPerSecond;
            SkippedFrames = skippedFrames;
        }

        /// <summary>
        /// Gets or sets the frams per second.
        /// </summary>
        /// <value>
        /// The frams per second.
        /// </value>
        public int FramsPerSecond { get; set; }

        /// <summary>
        /// Gets or sets the skipped frames.
        /// </summary>
        /// <value>
        /// The skipped frames.
        /// </value>
        public int SkippedFrames { get; set; }
    }
}
