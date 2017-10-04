using System;

namespace GameBoy.Net.Devices
{
    /// <summary>
    /// A button on the GameBoy joypad.
    /// </summary>
    [Flags]
    public enum JoyPadButton : byte
    {
        /// <summary>
        /// No buttons are pressed.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// The up button.
        /// </summary>
        Up = 0x01,

        /// <summary>
        /// The down button.
        /// </summary>
        Down = 0x02,

        /// <summary>
        /// The left button.
        /// </summary>
        Left = 0x04,

        /// <summary>
        /// The right button.
        /// </summary>
        Right = 0x08,

        /// <summary>
        /// The A button.
        /// </summary>
        A = 0x10,

        /// <summary>
        /// The B button.
        /// </summary>
        B = 0x20,

        /// <summary>
        /// The start button.
        /// </summary>
        Start = 0x40,

        /// <summary>
        /// The select button.
        /// </summary>
        Select = 0x80
    }
}
