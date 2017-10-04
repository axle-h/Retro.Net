using System;
using Retro.Net.Util;
using Retro.Net.Z80.State;

namespace Retro.Net.Z80.Registers
{
    /// <summary>
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
    /// <seealso cref="IRegisters" />
    public class Z80Registers : IRegisters
    {
        private readonly Z80RegisterState _initialState;

        private readonly object _accumulatorAndFlagsLockingContext = new object();
        private readonly AccumulatorAndFlagsRegisterSet _alternativeAccumulatorAndFlagsRegisterSet;
        private readonly GeneralPurposeRegisterSet _alternativeGeneralPurposeRegisterSet;
        private readonly object _generalPurposeLockingContext = new object();

        private readonly AccumulatorAndFlagsRegisterSet _primaryAccumulatorAndFlagsRegisterSet;
        private readonly GeneralPurposeRegisterSet _primaryGeneralPurposeRegisterSet;
        private bool _isAccumulatorAndFlagsAlternative;
        private bool _isGeneralPurposeAlternative;

        /// <summary>
        /// Initializes a new instance of the <see cref="Z80Registers"/> class.
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        public Z80Registers(Z80RegisterState initialState)
        {
            _initialState = initialState;
            _primaryGeneralPurposeRegisterSet = new GeneralPurposeRegisterSet();
            _alternativeGeneralPurposeRegisterSet = new GeneralPurposeRegisterSet();
            _primaryAccumulatorAndFlagsRegisterSet = new AccumulatorAndFlagsRegisterSet(new Intel8080FlagsRegister());
            _alternativeAccumulatorAndFlagsRegisterSet = new AccumulatorAndFlagsRegisterSet(new Intel8080FlagsRegister());

            Reset();
        }

        /// <summary>
        /// Gets the general purpose registers.
        /// </summary>
        /// <value>
        /// The general purpose registers.
        /// </value>
        public GeneralPurposeRegisterSet GeneralPurposeRegisters { get; private set; }

        /// <summary>
        /// Gets the accumulator and flags registers.
        /// </summary>
        /// <value>
        /// The accumulator and flags registers.
        /// </value>
        public AccumulatorAndFlagsRegisterSet AccumulatorAndFlagsRegisters { get; private set; }
        
        /// <summary>
        /// Gets or sets the IX register.
        /// </summary>
        /// <value>
        /// The IX register.
        /// </value>
        public ushort IX { get; set; }
        
        /// <summary>
        /// Gets or sets the IY register.
        /// </summary>
        /// <value>
        /// The IY register.
        /// </value>
        public ushort IY { get; set; }

        /// <summary>
        /// Gets or sets the lower byte of the IX register.
        /// </summary>
        /// <value>
        /// The lower byte of the IX register.
        /// </value>
        public byte IXl
        {
            get => BitConverterHelpers.GetLowOrderByte(IX);
            set => IX = BitConverterHelpers.SetLowOrderByte(IX, value);
        }

        /// <summary>
        /// Gets or sets the upper byte of the IX register.
        /// </summary>
        /// <value>
        /// The upper byte of the IX register.
        /// </value>
        public byte IXh
        {
            get => BitConverterHelpers.GetHighOrderByte(IX);
            set => IX = BitConverterHelpers.SetHighOrderByte(IX, value);
        }

        /// <summary>
        /// Gets or sets the lower byte of the IY register.
        /// </summary>
        /// <value>
        /// The lower byte of the IY register.
        /// </value>
        public byte IYl
        {
            get => BitConverterHelpers.GetLowOrderByte(IY);
            set => IY = BitConverterHelpers.SetLowOrderByte(IY, value);
        }

        /// <summary>
        /// Gets or sets the upper byte of the IY register.
        /// </summary>
        /// <value>
        /// The upper byte of the IY register.
        /// </value>
        public byte IYh
        {
            get => BitConverterHelpers.GetHighOrderByte(IY);
            set => IY = BitConverterHelpers.SetHighOrderByte(IY, value);
        }
        
        /// <summary>
        /// Gets or sets the I register.
        /// </summary>
        /// <value>
        /// The I register.
        /// </value>
        public byte I { get; set; }
        
        /// <summary>
        /// </summary>
        /// <value>
        /// The R register.
        /// </value>
        public byte R { get; set; }

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
        /// Switches to alternative general purpose registers.
        /// </summary>
        public void SwitchToAlternativeGeneralPurposeRegisters()
        {
            lock (_generalPurposeLockingContext)
            {
                _isGeneralPurposeAlternative = !_isGeneralPurposeAlternative;
                GeneralPurposeRegisters = _isGeneralPurposeAlternative
                                              ? _alternativeGeneralPurposeRegisterSet
                                              : _primaryGeneralPurposeRegisterSet;
            }
        }

        /// <summary>
        /// Switches to alternative accumulator and flags registers.
        /// </summary>
        public void SwitchToAlternativeAccumulatorAndFlagsRegisters()
        {
            lock (_accumulatorAndFlagsLockingContext)
            {
                _isAccumulatorAndFlagsAlternative = !_isAccumulatorAndFlagsAlternative;
                AccumulatorAndFlagsRegisters = _isAccumulatorAndFlagsAlternative
                                                   ? _alternativeAccumulatorAndFlagsRegisterSet
                                                   : _primaryAccumulatorAndFlagsRegisterSet;
            }
        }

        /// <summary>
        /// Resets the registers to their initial state.
        /// </summary>
        public void Reset() => ResetToState(_initialState);

        /// <summary>
        /// Resets the registers to the specified state.
        /// </summary>
        /// <typeparam name="TRegisterState">The type of the register state.</typeparam>
        /// <exception cref="ArgumentException">The register state type must be compatible.</exception>
        /// <param name="state">The state.</param>
        public void ResetToState<TRegisterState>(TRegisterState state) where TRegisterState : Intel8080RegisterState
        {
            var z80State = state as Z80RegisterState;
            if (z80State == null)
            {
                throw new ArgumentException("The register state type must be compatible.");
            }

            ResetToState(z80State);
        }

        /// <summary>
        /// Gets the state of the registers in Intel 8080 format.
        /// </summary>
        /// <exception cref="NotSupportedException">The implementation must have Intel 8080 style registers. I.e. not be a Z80.</exception>
        /// <returns></returns>
        public Intel8080RegisterState GetIntel8080RegisterState()
        {
            throw new NotSupportedException("The implementation must have Intel 8080 style registers. I.e. not be a Z80.");
        }

        /// <summary>
        /// Gets the state of the registers in Z80 format.
        /// </summary>
        /// <returns></returns>
        public Z80RegisterState GetZ80RegisterState()
            =>
                new Z80RegisterState(GeneralPurposeRegisters.GetRegisterState(),
                                     _alternativeGeneralPurposeRegisterSet.GetRegisterState(),
                                     AccumulatorAndFlagsRegisters.GetRegisterState(),
                                     _alternativeAccumulatorAndFlagsRegisterSet.GetRegisterState(),
                                     _isGeneralPurposeAlternative,
                                     _isAccumulatorAndFlagsAlternative,
                                     IX,
                                     IY,
                                     I,
                                     R,
                                     StackPointer,
                                     ProgramCounter,
                                     InterruptFlipFlop1,
                                     InterruptFlipFlop2,
                                     InterruptMode);
        
        /// <summary>
        /// Resets the registers to the specified Z80 state.
        /// </summary>
        /// <param name="state">The state.</param>
        private void ResetToState(Z80RegisterState state)
        {
            _primaryGeneralPurposeRegisterSet.ResetToState(state.GeneralPurposeRegisterState);
            _alternativeGeneralPurposeRegisterSet.ResetToState(state.AlternativeGeneralPurposeRegisterState);
            GeneralPurposeRegisters = state.IsGeneralPurposeAlternative
                                          ? _alternativeGeneralPurposeRegisterSet
                                          : _primaryGeneralPurposeRegisterSet;
            _isGeneralPurposeAlternative = state.IsGeneralPurposeAlternative;

            _primaryAccumulatorAndFlagsRegisterSet.ResetToState(state.AccumulatorAndFlagsRegisterState);
            _alternativeAccumulatorAndFlagsRegisterSet.ResetToState(state.AlternativeAccumulatorAndFlagsRegisterState);
            AccumulatorAndFlagsRegisters = state.IsAccumulatorAndFlagsAlternative
                                               ? _alternativeAccumulatorAndFlagsRegisterSet
                                               : _primaryAccumulatorAndFlagsRegisterSet;
            _isAccumulatorAndFlagsAlternative = state.IsAccumulatorAndFlagsAlternative;

            IX = state.IX;
            IY = state.IY;
            I = state.I;
            R = state.R;
            StackPointer = state.StackPointer;
            ProgramCounter = state.ProgramCounter;

            InterruptFlipFlop1 = state.InterruptFlipFlop1;
            InterruptFlipFlop2 = state.InterruptFlipFlop2;
            InterruptMode = state.InterruptMode;
        }
    }
}