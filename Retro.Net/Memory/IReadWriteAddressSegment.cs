namespace Retro.Net.Memory
{
    /// <summary>
    /// An address segment that is both readable and writeable.
    /// </summary>
    /// <seealso cref="IReadableAddressSegment" />
    /// <seealso cref="IWriteableAddressSegment" />
    public interface IReadWriteAddressSegment : IReadableAddressSegment, IWriteableAddressSegment
    {
    }
}
