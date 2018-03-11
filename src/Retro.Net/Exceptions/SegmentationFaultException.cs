using System;

namespace Retro.Net.Exceptions
{
    /// <summary>
    /// A segmentation fault exception.
    /// Thrown when attempting to write to a locked address.
    /// Essentially crashes the CPU.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SegmentationFaultException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentationFaultException"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        public SegmentationFaultException(ushort address) : base("Cannot write to address: " + address)
        {
            Address = address;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address { get; }
    }
}