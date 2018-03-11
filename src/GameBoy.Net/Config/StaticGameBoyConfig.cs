namespace GameBoy.Net.Config
{
    /// <summary>
    /// Immutable gameboy config.
    /// </summary>
    /// <seealso cref="IGameBoyConfig" />
    public class StaticGameBoyConfig : IGameBoyConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticGameBoyConfig" /> class.
        /// </summary>
        /// <param name="cartridge">The cartridge.</param>
        /// <param name="type">The type.</param>
        /// <param name="runGpu">if set to <c>true</c> [run gpu].</param>
        public StaticGameBoyConfig(byte[] cartridge, GameBoyType type, bool runGpu)
        {
            CartridgeData = cartridge;
            GameBoyType = type;
            RunGpu = runGpu;
        }

        /// <summary>
        /// Gets the cartridge data.
        /// </summary>
        /// <value>
        /// The cartridge data.
        /// </value>
        public byte[] CartridgeData { get; }

        /// <summary>
        /// Gets the type of the game boy.
        /// </summary>
        /// <value>
        /// The type of the game boy.
        /// </value>
        public GameBoyType GameBoyType { get; }

        /// <summary>
        /// Gets a value indicating whether [run gpu].
        /// </summary>
        /// <value>
        /// <c>true</c> if [run gpu]; otherwise, <c>false</c>.
        /// </value>
        public bool RunGpu { get; }
    }
}