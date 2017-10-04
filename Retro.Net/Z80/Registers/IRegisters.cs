using Retro.Net.Z80.State;

namespace Retro.Net.Z80.Registers
{
    /// <summary>
    /// Intel 8080 registers:
    /// The processor has seven 8-bit registers (A, B, C, D, E, H, and L).
    /// Where A is the primary 8-bit accumulator and the other six registers can be used as either individual 8-bit registers
    /// or as three 16-bit register pairs (BC, DE, and HL) depending on the particular instruction.
    /// It also has a 16-bit stack pointer to memory (replacing the 8008's internal stack), and a 16-bit program counter.
    /// The processor maintains internal flag bits (a status register) which indicates the results of arithmetic and logical instructions.
    /// 
    /// Z80 registers which extend the original Intel 8080 registers with:
    /// 
    /// IX: 16-bit index or base register for 8-bit immediate offsets
    /// IY: 16-bit index or base register for 8-bit immediate offsets
    /// I: interrupt vector base register, 8 bits
    /// R: DRAM refresh counter, 8 bits(msb does not count)
    /// AF': alternate (or shadow) accumulator and flags (toggled in and out with EX AF,AF' )
    /// BC', DE' and HL': alternate (or shadow) registers (toggled in and out with EXX)
    /// Four bits of interrupt status and interrupt mode status
    /// 
    /// There is no direct access to the alternate registers;
    /// instead, two special instructions, EX AF,AF' and EXX, each toggles one of two multiplexer flip-flops;
    /// this enables fast context switches for interrupt service routines: EX AF, AF'.
    /// </summary>
    public interface IRegisters
    {
        /// <summary>
        /// Gets the general purpose registers.
        /// </summary>
        /// <value>
        /// The general purpose registers.
        /// </value>
        GeneralPurposeRegisterSet GeneralPurposeRegisters { get; }

        /// <summary>
        /// Gets the accumulator and flags registers.
        /// </summary>
        /// <value>
        /// The accumulator and flags registers.
        /// </value>
        AccumulatorAndFlagsRegisterSet AccumulatorAndFlagsRegisters { get; }

        /// <summary>
        /// Gets or sets the stack pointer.
        /// </summary>
        /// <value>
        /// The stack pointer.
        /// </value>
        ushort StackPointer { get; set; }

        /// <summary>
        /// Gets or sets the program counter.
        /// </summary>
        /// <value>
        /// The program counter.
        /// </value>
        ushort ProgramCounter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [interrupt flip flop1].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [interrupt flip flop1]; otherwise, <c>false</c>.
        /// </value>
        bool InterruptFlipFlop1 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [interrupt flip flop2].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [interrupt flip flop2]; otherwise, <c>false</c>.
        /// </value>
        bool InterruptFlipFlop2 { get; set; }

        /// <summary>
        /// Gets or sets the interrupt mode.
        /// </summary>
        /// <value>
        /// The interrupt mode.
        /// </value>
        InterruptMode InterruptMode { get; set; }

        /// <summary>
        /// Resets the registers to their initial state.
        /// </summary>
        void Reset();

        /// <summary>
        /// Resets the registers to the specified state.
        /// </summary>
        /// <typeparam name="TRegisterState">The type of the register state.</typeparam>
        /// <param name="state">The state.</param>
        void ResetToState<TRegisterState>(TRegisterState state) where TRegisterState : Intel8080RegisterState;

        /// <summary>
        /// Gets the state of the registers in Intel 8080 format.
        /// </summary>
        /// <returns></returns>
        Intel8080RegisterState GetIntel8080RegisterState();
        
        /// <summary>
        /// Gets the state of the registers in Z80 format.
        /// </summary>
        /// <returns></returns>
        Z80RegisterState GetZ80RegisterState();

        /// <summary>
        /// Gets or sets the IX register.
        /// </summary>
        /// <value>
        /// The IX register.
        /// </value>
        ushort IX { get; set; }

        /// <summary>
        /// Gets or sets the IY register.
        /// </summary>
        /// <value>
        /// The IY register.
        /// </value>
        ushort IY { get; set; }

        /// <summary>
        /// Gets or sets the lower byte of the IX register.
        /// </summary>
        /// <value>
        /// The lower byte of the IX register.
        /// </value>
        byte IXl { get; set; }

        /// <summary>
        /// Gets or sets the upper byte of the IX register.
        /// </summary>
        /// <value>
        /// The upper byte of the IX register.
        /// </value>
        byte IXh { get; set; }

        /// <summary>
        /// Gets or sets the lower byte of the IY register.
        /// </summary>
        /// <value>
        /// The lower byte of the IY register.
        /// </value>
        byte IYl { get; set; }

        /// <summary>
        /// Gets or sets the upper byte of the IY register.
        /// </summary>
        /// <value>
        /// The upper byte of the IY register.
        /// </value>
        byte IYh { get; set; }

        /// <summary>
        /// Gets or sets the I register.
        /// </summary>
        /// <value>
        /// The I register.
        /// </value>
        byte I { get; set; }

        /// <summary>
        /// Gets or sets the R register.
        /// </summary>
        /// <value>
        /// The R register.
        /// </value>
        byte R { get; set; }

        /// <summary>
        /// Switches to alternative general purpose registers.
        /// </summary>
        void SwitchToAlternativeGeneralPurposeRegisters();

        /// <summary>
        /// Switches to alternative accumulator and flags registers.
        /// </summary>
        void SwitchToAlternativeAccumulatorAndFlagsRegisters();
    }
}