using System;

namespace GameBoy.Net.Devices
{
    /// <summary>
    /// A GameBoy interrupt.
    /// </summary>
    [Flags]
    public enum InterruptFlag : byte
    {
        /// <summary>
        /// No interrupt.
        /// </summary>
        None = 0,

        /// <summary>
        /// The vertical blank interrupt.
        /// </summary>
        VerticalBlank = 0x01,

        /// <summary>
        /// The LCD status triggers interrupt.
        /// </summary>
        LcdStatusTriggers = 0x02,

        /// <summary>
        /// The timer overflow interrupt.
        /// </summary>
        TimerOverflow = 0x04,

        /// <summary>
        /// The serial link interrupt.
        /// </summary>
        SerialLink = 0x08,

        /// <summary>
        /// The joy pad press interrupt.
        /// </summary>
        JoyPadPress = 0x10
    }
}