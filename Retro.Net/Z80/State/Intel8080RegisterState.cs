using Retro.Net.Z80.Registers;

namespace Retro.Net.Z80.State
{
    /// <summary>
    /// The state of all Intel 8080 registers.
    /// </summary>
    public class Intel8080RegisterState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intel8080RegisterState"/> class.
        /// </summary>
        /// <param name="generalPurposeRegisterState">State of the primary general purpose registers.</param>
        /// <param name="accumulatorAndFlagsRegisterState">State of the primary accumulator and flags registers.</param>
        /// <param name="stackPointer">The SP (stack pointer) register.</param>
        /// <param name="programCounter">The PC (program counter) register.</param>
        /// <param name="interruptFlipFlop1">if set to <c>true</c> [interrupt flip flop 1 is set].</param>
        /// <param name="interruptFlipFlop2">if set to <c>true</c> [interrupt flip flop 2 is set].</param>
        /// <param name="interruptMode">The interrupt mode.</param>
        public Intel8080RegisterState(GeneralPurposeRegisterState generalPurposeRegisterState,
            AccumulatorAndFlagsRegisterState accumulatorAndFlagsRegisterState,
            ushort stackPointer,
            ushort programCounter,
            bool interruptFlipFlop1,
            bool interruptFlipFlop2,
            InterruptMode interruptMode)
        {
            GeneralPurposeRegisterState = generalPurposeRegisterState;
            AccumulatorAndFlagsRegisterState = accumulatorAndFlagsRegisterState;
            StackPointer = stackPointer;
            ProgramCounter = programCounter;
            InterruptFlipFlop1 = interruptFlipFlop1;
            InterruptFlipFlop2 = interruptFlipFlop2;
            InterruptMode = interruptMode;
        }

        /// <summary>
        /// Gets the state of the primary general purpose registers.
        /// </summary>
        /// <value>
        /// The state of the primary general purpose registers.
        /// </value>
        public GeneralPurposeRegisterState GeneralPurposeRegisterState { get; }

        /// <summary>
        /// Gets the state of the primary accumulator and flags registers.
        /// </summary>
        /// <value>
        /// The state of the primary accumulator and flags registers.
        /// </value>
        public AccumulatorAndFlagsRegisterState AccumulatorAndFlagsRegisterState { get; }

        /// <summary>
        /// Gets the SP (stack pointer) register.
        /// </summary>
        /// <value>
        /// The SP (stack pointer) register.
        /// </value>
        public ushort StackPointer { get; }

        /// <summary>
        /// Gets the PC (program counter) register.
        /// </summary>
        /// <value>
        /// The PC (program counter) register.
        /// </value>
        public ushort ProgramCounter { get; }

        /// <summary>
        /// Gets a value indicating whether [interrupt flip flop 1 set].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [interrupt flip flop 1 set]; otherwise, <c>false</c>.
        /// </value>
        public bool InterruptFlipFlop1 { get; }

        /// <summary>
        /// Gets a value indicating whether [interrupt flip flop 2 set].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [interrupt flip flop 2 set]; otherwise, <c>false</c>.
        /// </value>
        public bool InterruptFlipFlop2 { get; }

        /// <summary>
        /// Gets the interrupt mode.
        /// </summary>
        /// <value>
        /// The interrupt mode.
        /// </value>
        public InterruptMode InterruptMode { get; }
    }
}