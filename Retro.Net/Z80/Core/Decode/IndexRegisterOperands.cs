namespace Retro.Net.Z80.Core.Decode
{
    /// <summary>
    /// An index register represented by it's operands.
    /// </summary>
    internal struct IndexRegisterOperands
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexRegisterOperands"/> struct.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="index">The index.</param>
        /// <param name="lowRegister">The low register.</param>
        /// <param name="highRegister">The high register.</param>
        /// <param name="isDisplaced">if set to <c>true</c> [is displaced].</param>
        public IndexRegisterOperands(Operand register,
            Operand index,
            Operand lowRegister,
            Operand highRegister,
            bool isDisplaced) : this()
        {
            Register = register;
            Index = index;
            LowRegister = lowRegister;
            HighRegister = highRegister;
            IsDisplaced = isDisplaced;
        }

        /// <summary>
        /// Gets the register.
        /// </summary>
        /// <value>
        /// The register.
        /// </value>
        public Operand Register { get; }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public Operand Index { get; }

        /// <summary>
        /// Gets the low register.
        /// </summary>
        /// <value>
        /// The low register.
        /// </value>
        public Operand LowRegister { get; }

        /// <summary>
        /// Gets the high register.
        /// </summary>
        /// <value>
        /// The high register.
        /// </value>
        public Operand HighRegister { get; }

        /// <summary>
        /// Gets a value indicating whether this index register is displaced.
        /// </summary>
        /// <value>
        /// <c>true</c> if this index register is displaced; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisplaced { get; }
    }
}