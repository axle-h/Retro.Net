using MessagePack;

namespace Retro.Net.Api.RealTime.Models
{
    /// <summary>
    /// A message to a client GameBoy.
    /// </summary>
    [MessagePackObject]
    public class GameBoyClientMessage
    {
        /// <summary>
        /// Gets or sets the user associated with this message.
        /// </summary>
        /// <value>
        /// The user associated with this message.
        /// </value>
        [Key("user")]
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [Key("date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Key("message")]
        public string Message { get; set; }
    }
}