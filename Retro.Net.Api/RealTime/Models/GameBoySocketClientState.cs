using System;
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

        public bool StateChanged(GameBoySocketMessage m) => MetricsEnabledChanged(m) || DisplayNameChanged(m);

        public void Update(GameBoySocketMessage m)
        {
            if (MetricsEnabledChanged(m))
            {
                MetricsEnabled = m.EnableMetrics.GetValueOrDefault();
            }

            if (DisplayNameChanged(m))
            {
                DisplayName = m.SetDisplayName;
            }
        }

        public bool MetricsEnabledChanged(GameBoySocketMessage m) =>
            m.EnableMetrics.HasValue && m.EnableMetrics.Value != MetricsEnabled;

        public bool DisplayNameChanged(GameBoySocketMessage m) =>
            !string.IsNullOrEmpty(m.SetDisplayName) && !m.SetDisplayName.Equals(DisplayName, StringComparison.InvariantCultureIgnoreCase);
    }
}