namespace GameBoy.Net.Graphics
{
    /// <summary>
    /// A renderer that does nothing.
    /// </summary>
    /// <seealso cref="IRenderer" />
    public class NullRenderer : IRenderer
    {
        /// <summary>
        /// Called every time the GB LCD is updated.
        /// The frame is updated and locked for each GPU cycle. So implementations should block as long as they are accessing pixels/buffers.
        /// Obviously the longer you block, the more frames will be skipped.
        /// </summary>
        /// <param name="frame"></param>
        public void Paint(Frame frame)
        {
        }

        /// <summary>
        /// Updates the metrics.
        /// </summary>
        /// <param name="metrics">The metrics.</param>
        public void UpdateMetrics(GpuMetrics metrics)
        {
        }
    }
}