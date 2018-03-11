using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.State
{
    /// <summary>
    /// The state of all Z80 registers.
    /// </summary>
    /// <seealso cref="Intel8080RegisterState" />
    public class Z80RegisterState : Intel8080RegisterState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Z80RegisterState"/> class.
        /// </summary>
        /// <param name="generalPurposeRegisterState">State of the primary general purpose registers.</param>
        /// <param name="alternativeGeneralPurposeRegisterState">State of the alternative general purpose registers.</param>
        /// <param name="accumulatorAndFlagsRegisterState">State of the primary accumulator and flags registers.</param>
        /// <param name="alternativeAccumulatorAndFlagsRegisterState">State of the alternative accumulator and flags registers.</param>
        /// <param name="isGeneralPurposeAlternative">if set to <c>true</c> [the general purpose registers are set to their alternative values].</param>
        /// <param name="isAccumulatorAndFlagsAlternative">if set to <c>true</c> [the accumulator and flags registers are set to their alternative values].</param>
        /// <param name="ix">The IX register.</param>
        /// <param name="iy">The IY register.</param>
        /// <param name="i">The I (interrupt vector) register.</param>
        /// <param name="r">The R (memory refresh) register.</param>
        /// <param name="stackPointer">The SP (stack pointer) register.</param>
        /// <param name="programCounter">The PC (program counter) register.</param>
        /// <param name="interruptFlipFlop1">if set to <c>true</c> [interrupt flip flop 1 is set].</param>
        /// <param name="interruptFlipFlop2">if set to <c>true</c> [interrupt flip flop 2 is set].</param>
        /// <param name="interruptMode">The interrupt mode.</param>
        public Z80RegisterState(GeneralPurposeRegisterState generalPurposeRegisterState,
            GeneralPurposeRegisterState alternativeGeneralPurposeRegisterState,
            AccumulatorAndFlagsRegisterState accumulatorAndFlagsRegisterState,
            AccumulatorAndFlagsRegisterState alternativeAccumulatorAndFlagsRegisterState,
            bool isGeneralPurposeAlternative,
            bool isAccumulatorAndFlagsAlternative,
            ushort ix,
            ushort iy,
            byte i,
            byte r,
            ushort stackPointer,
            ushort programCounter,
            bool interruptFlipFlop1,
            bool interruptFlipFlop2,
            InterruptMode interruptMode)
            : base(
                generalPurposeRegisterState,
                accumulatorAndFlagsRegisterState,
                stackPointer,
                programCounter,
                interruptFlipFlop1,
                interruptFlipFlop2,
                interruptMode)
        {
            AlternativeGeneralPurposeRegisterState = alternativeGeneralPurposeRegisterState;
            AlternativeAccumulatorAndFlagsRegisterState = alternativeAccumulatorAndFlagsRegisterState;
            IsGeneralPurposeAlternative = isGeneralPurposeAlternative;
            IsAccumulatorAndFlagsAlternative = isAccumulatorAndFlagsAlternative;
            IX = ix;
            IY = iy;
            I = i;
            R = r;
        }
        
        /// <summary>
        /// Gets the state of the alternative general purpose registers.
        /// This is Z80 specific.
        /// </summary>
        /// <value>
        /// The state of the alternative general purpose registers.
        /// </value>
        public GeneralPurposeRegisterState AlternativeGeneralPurposeRegisterState { get; }

        /// <summary>
        /// Gets the state of the alternative accumulator and flags registers.
        /// This is Z80 specific.
        /// </summary>
        /// <value>
        /// The state of the alternative accumulator and flags registers.
        /// </value>
        public AccumulatorAndFlagsRegisterState AlternativeAccumulatorAndFlagsRegisterState { get; }

        /// <summary>
        /// Gets a value indicating whether the general purpose registers are set to their alternative values.
        /// This is Z80 specific.
        /// </summary>
        /// <value>
        /// <c>true</c> if the general purpose registers are set to their alternative values; otherwise, <c>false</c>.
        /// </value>
        public bool IsGeneralPurposeAlternative { get; }

        /// <summary>
        /// Gets a value indicating whether the accumulator and flags registers are set to their alternative values.
        /// This is Z80 specific.
        /// </summary>
        /// <value>
        /// <c>true</c> if the accumulator and flags registers are set to their alternative values; otherwise, <c>false</c>.
        /// </value>
        public bool IsAccumulatorAndFlagsAlternative { get; }

        /// <summary>
        /// Gets the IX register.
        /// This is Z80 specific.
        /// </summary>
        /// <value>
        /// The IX register.
        /// </value>
        public ushort IX { get; }

        /// <summary>
        /// Gets the IY register.
        /// This is Z80 specific.
        /// </summary>
        /// <value>
        /// The IY register.
        /// </value>
        public ushort IY { get; }
        
        /// <summary>
        /// Gets the I (interrupt vector) register.
        /// This is Z80 specific.
        /// </summary>
        /// <value>
        /// The I (interrupt vector) register.
        /// </value>
        public byte I { get; }

        /// <summary>
        /// Gets the R (memory refresh) register.
        /// This is Z80 specific.
        /// </summary>
        /// <value>
        /// The R (memory refresh) register.
        /// </value>
        public byte R { get; }

        /// <summary>
        /// Gets the a zero state.
        /// </summary>
        /// <value>
        /// The zero state.
        /// </value>
        public static Z80RegisterState Zero
            =>
                new Z80RegisterState(new GeneralPurposeRegisterState(),
                                     new GeneralPurposeRegisterState(),
                                     new AccumulatorAndFlagsRegisterState(),
                                     new AccumulatorAndFlagsRegisterState(),
                                     false,
                                     false,
                                     0,
                                     0,
                                     0,
                                     0,
                                     0xffff,
                                     0x0000,
                                     false,
                                     false,
                                     Registers.InterruptMode.InterruptMode0);
    }
}