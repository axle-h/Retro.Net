using MessagePack;

namespace Retro.Net.Api.RealTime.Models
{
    /// <summary>
    /// The state of a gameboy websocket connection.
    /// </summary>
    [MessagePackObject]
    public class GameBoySocketClientState
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [Key("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [metrics enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [metrics enabled]; otherwise, <c>false</c>.
        /// </value>
        [Key("metricsEnabled")]
        public bool MetricsEnabled { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() =>
            $"{nameof(DisplayName)}: {DisplayName}, {nameof(MetricsEnabled)}: {MetricsEnabled}";
    }
}