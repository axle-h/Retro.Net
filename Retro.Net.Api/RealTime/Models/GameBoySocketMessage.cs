using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Retro.Net.Api.RealTime.Models
{
    /// <summary>
    /// A message from a connected Gameboy client.
    /// </summary>
    [MessagePackObject]
    public class GameBoySocketMessage
    {
        /// <summary>
        /// Gets the requested joypad button.
        /// </summary>
        /// <value>
        /// The requested joypad button.
        /// </value>
        [Key("button")]
        public int? Button { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to [enable gameboy metrics].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [gameboy metrics enabled]; otherwise, <c>false</c>.
        /// </value>
        [Key("enableMetrics")]
        public bool? EnableMetrics { get; set; }

        /// <summary>
        /// Gets or sets the display name that the client has requested.
        /// </summary>
        /// <value>
        /// The display name that the client has requested.
        /// </value>
        [Key("setDisplayName")]
        public string SetDisplayName { get; set; }

        /// <summary>
        /// Determines whether this message [is a heart beat].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this message [is a heart beat]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsHeartBeat() => !Button.HasValue && !EnableMetrics.HasValue && string.IsNullOrEmpty(SetDisplayName);
        
        public override string ToString()
        {
            IEnumerable<string> Yield()
            {
                if (Button.HasValue)
                {
                    yield return $"{nameof(Button)}: {Button}";
                }

                if (EnableMetrics.HasValue)
                {
                    yield return $"{nameof(EnableMetrics)}: {EnableMetrics}";
                }

                if (!string.IsNullOrEmpty(SetDisplayName))
                {
                    yield return $"{nameof(SetDisplayName)}: {SetDisplayName}";
                }
            }

            var tokens = Yield().ToArray();
            return tokens.Any() ? string.Join(", ", Yield()) : "empty message";
        }
    }
}