namespace Retro.Net.Z80.Registers
{
    /// <summary>
    /// The interrupt mode.
    /// The Z80 microprocessor supports three interrupts modes; 0, 1, and 2.
    /// </summary>
    public enum InterruptMode : byte
    {
        /// <summary>
        /// Interrupt mode 0.
        /// In interrupt mode 0 the Z80 gets an instruction from the data bus given by the peripheral and executes it.
        /// This instruction is normally RST0 -> RST7 which resets the Z80 to a specific location given by that instruction.
        /// </summary>
        InterruptMode0 = 0x00,

        /// <summary>
        /// Interrupt mode 1.
        /// In interrupt mode 1 the Z80 jumps to address 0038h where it runs a routine that the programmer implements.
        /// </summary>
        InterruptMode1 = 0x01,

        /// <summary>
        /// Interrupt mode 2.
        /// In interrupt mode 2 the Z80 gets a byte from the data bus and jumps to the 16-bit address formed by combining the ‘I’ (interrupt vector) register and the supplied byte from the peripheral.
        /// </summary>
        InterruptMode2 = 0x02
    }
}