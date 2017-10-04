namespace GameBoy.Net.Config
{
    /// <summary>
    /// GameBoy specific config.
    /// </summary>
    public interface IGameBoyConfig
    {
        /// <summary>
        /// Gets the cartridge data.
        /// </summary>
        /// <value>
        /// The cartridge data.
        /// </value>
        byte[] CartridgeData { get; }

        /// <summary>
        /// Gets the type of the game boy.
        /// </summary>
        /// <value>
        /// The type of the game boy.
        /// </value>
        GameBoyType GameBoyType { get; }

        /// <summary>
        /// Gets a value indicating whether [run gpu].
        /// </summary>
        /// <value>
        /// <c>true</c> if [run gpu]; otherwise, <c>false</c>.
        /// </value>
        bool RunGpu { get; }
    }
}