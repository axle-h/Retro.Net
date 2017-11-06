using System.Collections.Generic;
using GameBoy.Net.Graphics;
using MessagePack;

namespace Retro.Net.Api.RealTime.Models
{
    /// <summary>
    /// Metrics for a GameBoy socket client.
    /// </summary>
    [MessagePackObject]
    public class GameBoyMetrics
    {
        /// <summary>
        /// Gets or sets the frames per second.
        /// </summary>
        /// <value>
        /// The frames per second.
        /// </value>
        [Key("framesPerSecond")]
        public int FramesPerSecond { get; set; }

        /// <summary>
        /// Gets or sets the skipped frames.
        /// </summary>
        /// <value>
        /// The skipped frames.
        /// </value>
        [Key("skippedFrames")]
        public int SkippedFrames { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        [Key("messages")]
        public ICollection<GameBoyClientMessage> Messages { get; set; }
    }
}
