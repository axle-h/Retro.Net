using GameBoy.Net.Devices.Interfaces;

namespace Retro.Net.Api.Services.Interfaces
{
    public interface IGameBoyContext
    {
        /// <summary>
        /// Gets the GameBoy Renderer.
        /// </summary>
        /// <returns></returns>
        IRenderer GetRenderer();

        /// <summary>
        /// Gets the GameBoy GPU.
        /// </summary>
        /// <returns></returns>
        IGpu GetGpu();
    }
}