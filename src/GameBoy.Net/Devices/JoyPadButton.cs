using System;

namespace GameBoy.Net.Devices
{
    public enum JoyPadButton
    {
        /// <summary>
        /// The up button.
        /// </summary>
        Up = 1,

        /// <summary>
        /// The down button.
        /// </summary>
        Down = 2,

        /// <summary>
        /// The left button.
        /// </summary>
        Left = 3,

        /// <summary>
        /// The right button.
        /// </summary>
        Right = 4,

        /// <summary>
        /// The A button.
        /// </summary>
        A = 5,

        /// <summary>
        /// The B button.
        /// </summary>
        B = 6,

        /// <summary>
        /// The start button.
        /// </summary>
        Start = 7,

        /// <summary>
        /// The select button.
        /// </summary>
        Select = 8
    }

    internal static class JoyPadButtonExtensions
    {
        /// <summary>
        /// Gets the <see cref="JoyPadButtonFlags"/> for the specified <see cref="JoyPadButton"/>.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns></returns>
        public static JoyPadButtonFlags GetFlag(this JoyPadButton button)
        {
            switch (button)
            {
                case JoyPadButton.Up: return JoyPadButtonFlags.Up;
                case JoyPadButton.Down: return JoyPadButtonFlags.Down;
                case JoyPadButton.Left: return JoyPadButtonFlags.Left;
                case JoyPadButton.Right: return JoyPadButtonFlags.Right;
                case JoyPadButton.A: return JoyPadButtonFlags.A;
                case JoyPadButton.B: return JoyPadButtonFlags.B;
                case JoyPadButton.Start: return JoyPadButtonFlags.Start;
                case JoyPadButton.Select: return JoyPadButtonFlags.Select;
                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }
    }
}
