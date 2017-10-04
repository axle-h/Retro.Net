namespace GameBoy.Net.Devices
{
    /// <summary>
    /// The GameBoy joypad.
    /// </summary>
    public interface IJoyPad
    {
        /// <summary>
        /// Gets or sets the buttons.
        /// Note: <see cref="JoyPadButton"/> is a flags register.
        /// </summary>
        /// <value>
        /// The buttons.
        /// </value>
        JoyPadButton Buttons { get; set; }
    }
}