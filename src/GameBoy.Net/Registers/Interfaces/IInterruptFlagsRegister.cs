using GameBoy.Net.Devices;

namespace GameBoy.Net.Registers.Interfaces
{
    /// <summary>
    /// FF0F - IF - Interrupt Flag (R/W)
    /// 
    /// Bit 0: V-Blank Interrupt Request(INT 40h)  (1=Request)
    /// Bit 1: LCD STAT Interrupt Request(INT 48h)  (1=Request)
    /// Bit 2: Timer Interrupt Request(INT 50h)  (1=Request)
    /// Bit 3: Serial Interrupt Request(INT 58h)  (1=Request)
    /// Bit 4: Joypad Interrupt Request(INT 60h)  (1=Request)
    /// When an interrupt signal changes from low to high, then the corresponding bit in the IF register becomes set.For example, Bit 0 becomes set when the LCD controller enters into the V-Blank period.
    /// </summary>
    /// <seealso cref="IRegister" />
    public interface IInterruptFlagsRegister : IRegister
    {
        /// <summary>
        /// Updates the interrupt flags, setting any new interrupt flags in <see cref="interrupts"/>.
        /// </summary>
        /// <param name="interrupts">The interrupts.</param>
        void UpdateInterrupts(InterruptFlag interrupts);
    }
}