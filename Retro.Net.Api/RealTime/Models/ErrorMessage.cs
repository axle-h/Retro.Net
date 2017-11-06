using System.Collections.Generic;
using MessagePack;

namespace Retro.Net.Api.RealTime.Models
{
    /// <summary>
    /// An error message.
    /// </summary>
    [MessagePackObject]
    public class ErrorMessage
    {
        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        [Key("reasons")]
        public ICollection<string> Reasons { get; set; }
    }
}
