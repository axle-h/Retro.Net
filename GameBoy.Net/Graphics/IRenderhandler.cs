namespace GameBoy.Net.Graphics
{
    /// <summary>
    /// An external render handler.
    /// </summary>
    public interface IRenderHandler
    {
        /// <summary>
        /// Called every time the GB LCD is updated.
        /// The frame is updated and locked for each GPU cycle. So implementations should block as long as they are accessing pixels/buffers.
        /// Obviously the longer you block, the more frames will be skipped.
        /// </summary>
        /// <param name="frame"></param>
        void Paint(Frame frame);

        /// <summary>
        /// Updates the rendering metrics.
        /// The render handler can choose to display this if required.
        /// </summary>
        /// <param name="fps">The total frames rendered in the last second.</param>
        /// <param name="skippedFrames">The skipped frames.</param>
        void UpdateMetrics(int fps, int skippedFrames);
    }
}