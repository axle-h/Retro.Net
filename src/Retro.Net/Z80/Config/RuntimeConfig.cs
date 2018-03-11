namespace Retro.Net.Z80.Config
{
    /// <summary>
    /// Simple runtime configuration.
    /// </summary>
    /// <seealso cref="IRuntimeConfig" />
    public class RuntimeConfig : IRuntimeConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeConfig"/> class.
        /// </summary>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        /// <param name="coreMode">The core mode.</param>
        public RuntimeConfig(bool debugMode, CoreMode coreMode)
        {
            DebugMode = debugMode;
            CoreMode = coreMode;
        }

        /// <summary>
        /// Gets a value indicating whether [debug mode enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [debug mode enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugMode { get; }

        /// <summary>
        /// Gets the core mode.
        /// </summary>
        /// <value>
        /// The core mode.
        /// </value>
        public CoreMode CoreMode { get; }
    }
}