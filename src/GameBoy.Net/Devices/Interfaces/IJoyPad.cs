namespace GameBoy.Net.Devices.Interfaces
{
    /// <summary>
    /// The GameBoy joypad.
    /// </summary>
    public interface IJoyPad
    {
        /// <summary>
        /// Gets or sets the buttons.
        /// Note: <see cref="JoyPadButtonFlags"/> is a flags register.
        /// </summary>
        /// <value>
        /// The buttons.
        /// </value>
        JoyPadButtonFlags Buttons { get; set; }

        /// <summary>
        /// Releases all buttons and presses the specified button.
        /// </summary>
        /// <param name="button">The button to press.</param>
        void PressOne(JoyPadButton button);

        /// <summary>
        /// Releases all buttons.
        /// </summary>
        void ReleaseAll();
    }
}