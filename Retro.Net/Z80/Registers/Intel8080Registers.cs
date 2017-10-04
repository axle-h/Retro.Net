using System;
using Retro.Net.Z80.Config;
using Retro.Net.Z80.State;

namespace Retro.Net.Z80.Registers
{
    /// <summary>
    /// Intel 8080 registers.
    /// The processor has seven 8-bit registers (A, B, C, D, E, H, and L).
    /// Where A is the primary 8-bit accumulator and the other six registers can be used as either individual 8-bit registers
    /// or as three 16-bit register pairs (BC, DE, and HL) depending on the particular instruction.
    /// It also has a 16-bit stack pointer to memory (replacing the 8008's internal stack), and a 16-bit program counter.
    /// The processor maintains internal flag bits (a status register) which indicates the results of arithmetic and logical instructions.
    /// </summary>
    /// <seealso cref="IRegisters" />
    public class Intel8080Registers : IRegisters
    {
        private readonly Intel8080RegisterState _initialState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Intel8080Registers"/> class.
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        /// <param name="platformConfig">The platform configuration.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public Intel8080Registers(Intel8080RegisterState initialState, IPlatformConfig platformConfig)
        {
            _initialState = initialState;

            IFlagsRegister flagsRegister;
            switch (platformConfig.CpuMode)
            {
                case CpuMode.Intel8080:
                case CpuMode.Z80:
                    flagsRegister = new Intel8080FlagsRegister();
                    break;
                case CpuMode.GameBoy:
                    flagsRegister = new GameBoyFlagsRegister();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GeneralPurposeRegisters = new GeneralPurposeRegisterSet();
            AccumulatorAndFlagsRegisters = new AccumulatorAndFlagsRegisterSet(flagsRegister);
            Reset();
        }

        /// <summary>
        /// Gets the general purpose registers.
        /// </summary>
        /// <value>
        /// The general purpose registers.
        /// </value>
        public GeneralPurposeRegisterSet GeneralPurposeRegisters { get; }

        /// <summary>
        /// Gets the accumulator and flags registers.
        /// </summary>
        /// <value>
        /// The accumulator and flags registers.
        /// </value>
        public AccumulatorAndFlagsRegisterSet AccumulatorAndFlagsRegisters { get; }


        /// <summary>
        /// Gets or sets the stack pointer.
        /// </summary>
        /// <value>
        /// The stack pointer.
        /// </value>
        public ushort StackPointer { get; set; }

        /// <summary>
        /// Gets or sets the program counter.
        /// </summary>
        /// <value>
        /// The program counter.
        /// </value>
        public ushort ProgramCounter { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether [interrupt flip flop1].
        /// </summary>
        /// <value>
        /// <c>true</c> if [interrupt flip flop1]; otherwise, <c>false</c>.
        /// </value>
        public bool InterruptFlipFlop1 { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether [interrupt flip flop2].
        /// </summary>
        /// <value>
        /// <c>true</c> if [interrupt flip flop2]; otherwise, <c>false</c>.
        /// </value>
        public bool InterruptFlipFlop2 { get; set; }

        /// <summary>
        /// Gets or sets the interrupt mode.
        /// </summary>
        /// <value>
        /// The interrupt mode.
        /// </value>
        public InterruptMode InterruptMode { get; set; }

        /// <summary>
        /// Resets the registers to their initial state.
        /// </summary>
        public void Reset() => ResetToState(_initialState);

        /// <summary>
        /// Resets the registers to the specified state.
        /// </summary>
        /// <typeparam name="TRegisterState">The type of the register state.</typeparam>
        /// <param name="state">The state.</param>
        public void ResetToState<TRegisterState>(TRegisterState state) where TRegisterState : Intel8080RegisterState
        {
            GeneralPurposeRegisters.ResetToState(state.GeneralPurposeRegisterState);
            AccumulatorAndFlagsRegisters.ResetToState(state.AccumulatorAndFlagsRegisterState);

            StackPointer = state.StackPointer;
            ProgramCounter = state.ProgramCounter;

            InterruptFlipFlop1 = state.InterruptFlipFlop1;
            InterruptFlipFlop2 = state.InterruptFlipFlop2;
            InterruptMode = state.InterruptMode;
        }

        /// <summary>
        /// Gets the state of the registers in Intel 8080 format.
        /// </summary>
        /// <exception cref="NotSupportedException">The implementation must have Intel 8080 style registers. I.e. not be a Z80.</exception>
        /// <returns></returns>
        public Intel8080RegisterState GetIntel8080RegisterState()
            =>
                new Intel8080RegisterState(GeneralPurposeRegisters.GetRegisterState(),
                                           AccumulatorAndFlagsRegisters.GetRegisterState(),
                                           StackPointer,
                                           ProgramCounter,
                                           InterruptFlipFlop1,
                                           InterruptFlipFlop2,
                                           InterruptMode);

        /// <summary>
        /// Gets the state of the registers in Z80 format.
        /// Non-applicable state fields are filled with 0's.
        /// </summary>
        /// <returns></returns>
        public Z80RegisterState GetZ80RegisterState()
            =>
                new Z80RegisterState(GeneralPurposeRegisters.GetRegisterState(),
                                     default(GeneralPurposeRegisterState),
                                     AccumulatorAndFlagsRegisters.GetRegisterState(),
                                     default(AccumulatorAndFlagsRegisterState),
                                     false,
                                     false,
                                     0,
                                     0,
                                     0,
                                     0,
                                     StackPointer,
                                     ProgramCounter,
                                     InterruptFlipFlop1,
                                     InterruptFlipFlop2,
                                     InterruptMode);

        /// <summary>
        /// Gets or sets the IX register.
        /// </summary>
        /// <value>
        /// The IX register.
        /// </value>
        public ushort IX
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Gets or sets the IY register.
        /// </summary>
        /// <value>
        /// The IY register.
        /// </value>
        public ushort IY
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Gets or sets the lower byte of the IX register.
        /// </summary>
        /// <value>
        /// The lower byte of the IX register.
        /// </value>
        public byte IXl
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Gets or sets the upper byte of the IX register.
        /// </summary>
        /// <value>
        /// The upper byte of the IX register.
        /// </value>
        public byte IXh
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Gets or sets the lower byte of the IY register.
        /// </summary>
        /// <value>
        /// The lower byte of the IY register.
        /// </value>
        public byte IYl
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Gets or sets the upper byte of the IY register.
        /// </summary>
        /// <value>
        /// The upper byte of the IY register.
        /// </value>
        public byte IYh
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Gets or sets the I register.
        /// </summary>
        /// <value>
        /// The I register.
        /// </value>
        public byte I
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Gets or sets the R register.
        /// </summary>
        /// <value>
        /// The R register.
        /// </value>
        public byte R
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Switches to alternative general purpose registers.
        /// </summary>
        public void SwitchToAlternativeGeneralPurposeRegisters() => throw new NotSupportedException();

        /// <summary>
        /// Switches to alternative accumulator and flags registers.
        /// </summary>
        public void SwitchToAlternativeAccumulatorAndFlagsRegisters() => throw new NotSupportedException();
    }
}