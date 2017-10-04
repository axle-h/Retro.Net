namespace GameBoy.Net.Graphics
{
    /// <summary>
    /// A render handler that does nothing.
    /// </summary>
    /// <seealso cref="IRenderHandler" />
    public class NullRenderHandler : IRenderHandler
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
        /// Updates the rendering metrics.
        /// The render handler can choose to display this if required.
        /// </summary>
        /// <param name="fps">The total frames rendered in the last second.</param>
        /// <param name="skippedFrames">The skipped frames.</param>
        public void UpdateMetrics(int fps, int skippedFrames)
        {
        }
    }
}