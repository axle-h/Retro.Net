using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// A simple GameBoy memory mapped register.
    /// </summary>
    /// <seealso cref="IRegister" />
    internal class SimpleRegister : IRegister
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleRegister" /> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="name">The name.</param>
        public SimpleRegister(ushort address, string name)
        {
            Address = address;
            Name = name;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the raw register value.
        /// </summary>
        /// <value>
        /// The raw register value.
        /// </value>
        public byte Register { get; set; }

        /// <summary>
        /// Gets the debug view.
        /// </summary>
        /// <value>
        /// The debug view.
        /// </value>
        public string DebugView => ToString();

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Name} ({Address}) = {Register}";
    }
}