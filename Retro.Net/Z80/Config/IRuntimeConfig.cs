namespace Retro.Net.Z80.Config
{
    /// <summary>
    /// The runtime configuration.
    /// </summary>
    public interface IRuntimeConfig
    {
        /// <summary>
        /// Gets a value indicating whether [debug mode enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug mode enabled]; otherwise, <c>false</c>.
        /// </value>
        bool DebugMode { get; }

        /// <summary>
        /// Gets the core mode.
        /// </summary>
        /// <value>
        /// The core mode.
        /// </value>
        CoreMode CoreMode { get; }
    }
}