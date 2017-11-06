namespace GameBoy.Net.Graphics
{
    public class GpuMetrics
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpuMetrics"/> class.
        /// </summary>
        /// <param name="framesPerSecond">The frams per second.</param>
        /// <param name="skippedFrames">The skipped frames.</param>
        public GpuMetrics(int framesPerSecond, int skippedFrames)
        {
            FramesPerSecond = framesPerSecond;
            SkippedFrames = skippedFrames;
        }

        /// <summary>
        /// Gets or sets the frames per second.
        /// </summary>
        /// <value>
        /// The frames per second.
        /// </value>
        public int FramesPerSecond { get; set; }

        /// <summary>
        /// Gets or sets the skipped frames.
        /// </summary>
        /// <value>
        /// The skipped frames.
        /// </value>
        public int SkippedFrames { get; set; }
    }
}
