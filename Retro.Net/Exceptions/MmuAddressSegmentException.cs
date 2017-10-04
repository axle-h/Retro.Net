using System;

namespace Retro.Net.Exceptions
{
    /// <summary>
    /// MMU address segment exception.
    /// Thrown when two address segment overlap or form a gap i.e. do not fill the address space.
    /// </summary>
    /// <seealso cref="PlatformConfigurationException" />
    public class MmuAddressSegmentException : PlatformConfigurationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MmuAddressSegmentException" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="addressFrom">The address from.</param>
        /// <param name="addressTo">The address to.</param>
        public MmuAddressSegmentException(AddressSegmentExceptionType type, ushort addressFrom, ushort addressTo)
            : base($"{GetMessage(type)} from 0x{addressFrom:x4} to 0x{addressTo:x4}")
        {
            AddressSegmentExceptionType = type;
            AddressFrom = addressFrom;
            AddressTo = addressTo;
        }

        /// <summary>
        /// Gets the type of the address segment exception.
        /// </summary>
        /// <value>
        /// The type of the address segment exception.
        /// </value>
        public AddressSegmentExceptionType AddressSegmentExceptionType { get; }

        /// <summary>
        /// Gets the address of the first byte of overlaping or missing address ranges.
        /// </summary>
        /// <value>
        /// The address of the first byte of overlaping or missing address ranges.
        /// </value>
        public ushort AddressFrom { get; }

        /// <summary>
        /// Gets the address of the last byte of overlaping or missing address ranges.
        /// </summary>
        /// <value>
        /// The address of the last byte of overlaping or missing address ranges.
        /// </value>
        public ushort AddressTo { get; }

        private static string GetMessage(AddressSegmentExceptionType type)
        {
            switch (type)
            {
                case AddressSegmentExceptionType.Gap:
                    return "There was a gap between address segments";
                case AddressSegmentExceptionType.Overlap:
                    return "Address segments were overlapping";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    /// <summary>
    /// The type of error that caused the exception.
    /// </summary>
    public enum AddressSegmentExceptionType
    {
        /// <summary>
        /// There was a gap between address segments.
        /// </summary>
        Gap,

        /// <summary>
        /// Address segments were overlapping.
        /// </summary>
        Overlap
    }
}