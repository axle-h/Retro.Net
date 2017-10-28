using MessagePack;

namespace Retro.Net.Api.RealTime
{
    /// <summary>
    /// A message from a conencted Gameboy client.
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
        public int Button { get; set; }
    }
}