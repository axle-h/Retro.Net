namespace Retro.Net.Util
{
    /// <summary>
    /// A message for the message bus.
    /// </summary>
    public enum Message
    {
        /// <summary>
        /// A message to request that the cpu be pasued.
        /// </summary>
        PauseCpu,

        /// <summary>
        /// A message to request that the cpu be resumed after a pause.
        /// </summary>
        ResumeCpu
    }
}