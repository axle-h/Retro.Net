using GameBoy.Net.Devices.Graphics.Models;

namespace GameBoy.Net.Devices.Interfaces
{
    /// <summary>
    /// An external renderer.
    /// </summary>
    public interface IRenderer
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
        /// The renderer can choose to display this if required.
        /// </summary>
        /// <param name="gpuMetrics">The metrics.</param>
        void UpdateMetrics(GpuMetrics gpuMetrics);
    }
}