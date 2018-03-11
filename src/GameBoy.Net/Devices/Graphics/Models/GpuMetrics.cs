namespace GameBoy.Net.Devices.Graphics.Models
{
    public struct GpuMetrics
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpuMetrics"/> class.
        /// </summary>
        /// <param name="framesPerSecond">The frames per second.</param>
        public GpuMetrics(int framesPerSecond)
        {
            FramesPerSecond = framesPerSecond;
        }

        /// <summary>
        /// Gets or sets the frames per second.
        /// </summary>
        /// <value>
        /// The frames per second.
        /// </value>
        public int FramesPerSecond { get; }
    }
}
