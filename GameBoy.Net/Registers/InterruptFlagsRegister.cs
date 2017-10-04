using GameBoy.Net.Devices;
using GameBoy.Net.Registers.Interfaces;
using Retro.Net.Z80.Core;

namespace GameBoy.Net.Registers
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
    /// <seealso cref="IInterruptFlagsRegister" />
    public class InterruptFlagsRegister : IInterruptFlagsRegister
    {
        private const ushort VerticalBlankAddress = 0x0040;
        private const ushort LcdStatusTriggersAddress = 0x0048;
        private const ushort TimerOverflowAddress = 0x0050;
        private const ushort SerialLinkAddress = 0x0058;
        private const ushort JoyPadPressAddress = 0x0060;

        private readonly IInterruptEnableRegister _interruptEnableRegister;

        private readonly IInterruptManager _interruptManager;

        private InterruptFlag _interruptFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterruptFlagsRegister"/> class.
        /// </summary>
        /// <param name="interruptManager">The interrupt manager.</param>
        /// <param name="interruptEnableRegister">The interrupt enable register.</param>
        public InterruptFlagsRegister(IInterruptManager interruptManager, IInterruptEnableRegister interruptEnableRegister)
        {
            _interruptManager = interruptManager;
            _interruptEnableRegister = interruptEnableRegister;
            _interruptFlag = InterruptFlag.None;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address => 0xff0f;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Interrupt Flag (IF R/W)";

        /// <summary>
        /// FF0F - IF - Interrupt Flags (R/W)
        /// Bit 0: V-Blank Interrupt Request(INT 40h)  (1=Requested)
        /// Bit 1: LCD STAT Interrupt Request(INT 48h)  (1=Requested)
        /// Bit 2: Timer Interrupt Request(INT 50h)  (1=Requested)
        /// Bit 3: Serial Interrupt Request(INT 58h)  (1=Requested)
        /// Bit 4: Joypad Interrupt Request(INT 60h)  (1=Requested)
        /// </summary>
        public byte Register
        {
            get { return (byte) _interruptFlag; }
            set
            {
                var newInterruptFlag = (InterruptFlag) value;
                var changedFlags = _interruptFlag ^ newInterruptFlag;

                if (changedFlags != InterruptFlag.None)
                {
                    UpdateInterrupts(changedFlags);
                }

                _interruptFlag = newInterruptFlag;
            }
        }

        /// <summary>
        /// Gets the debug view.
        /// </summary>
        /// <value>
        /// The debug view.
        /// </value>
        public string DebugView => ToString();

        /// <summary>
        /// Updates the interrupt flags, setting any new interrupt flags in <see cref="interrupts" />.
        /// </summary>
        /// <param name="interrupts">The interrupts.</param>
        public void UpdateInterrupts(InterruptFlag interrupts)
        {
            // Combine with delayed interrupts.
            _interruptFlag |= interrupts;

            if (_interruptFlag == InterruptFlag.None)
            {
                return;
            }

            if (!_interruptManager.InterruptsEnabled)
            {
                // Interrupts disabled.
                return;
            }

            if (TryExecuteInterrupt(InterruptFlag.VerticalBlank, VerticalBlankAddress))
            {
                return;
            }

            if (TryExecuteInterrupt(InterruptFlag.LcdStatusTriggers, LcdStatusTriggersAddress))
            {
                return;
            }

            if (TryExecuteInterrupt(InterruptFlag.TimerOverflow, TimerOverflowAddress))
            {
                return;
            }

            if (TryExecuteInterrupt(InterruptFlag.SerialLink, SerialLinkAddress))
            {
                return;
            }

            TryExecuteInterrupt(InterruptFlag.JoyPadPress, JoyPadPressAddress);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Name} ({Address}) = {Register}";


        /// <summary>
        /// Tries to execute the interrupt.
        /// </summary>
        /// <param name="interrupt">The interrupt.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        private bool TryExecuteInterrupt(InterruptFlag interrupt, ushort address)
        {
            if (!_interruptFlag.HasFlag(interrupt) || !_interruptEnableRegister.InterruptEnabled(interrupt))
            {
                // Interrupt flag is not set or enabled.
                return false;
            }

            // Do interrupt.
            _interruptManager.Interrupt(address);

            // Clear the interrupt flag.
            _interruptFlag &= ~interrupt;
            return true;
        }
    }
}