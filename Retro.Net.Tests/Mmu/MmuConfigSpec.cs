using Retro.Net.Exceptions;
using Shouldly;
using Xunit;

namespace Retro.Net.Tests.Mmu
{
    public abstract class MmuConfigSpec : SegmentMmuTestBase
    {
        protected MmuConfigSpec((ushort address, ushort length) segment0Config,
            (ushort address, ushort length) segment1Config,
            (ushort address, ushort length) segment2Config)
            : base(segment0Config, segment1Config, segment2Config)
        {
        }

        protected void AssertThrows(AddressSegmentExceptionType type, ushort gapStart, ushort gapEnd)
        {
            var exception = ConstructionShouldThrow<MmuAddressSegmentException>();

            exception.ShouldSatisfyAllConditions(
                () => exception.AddressSegmentExceptionType.ShouldBe(type),
                () => exception.AddressFrom.ShouldBe(gapStart),
                () => exception.AddressTo.ShouldBe(gapEnd));
        }
    }

    public class When_leaving_gap_in_address_space : MmuConfigSpec
    {
        public When_leaving_gap_in_address_space() : base((0x0000, 0x1000), (0x2000, 0x1000), (0x3000, 0xcfff))
        {
        }

        [Fact] public void it_should_throw() => AssertThrows(AddressSegmentExceptionType.Gap, 0x1000, 0x2000);
    }

    public class When_not_filling_address_space : MmuConfigSpec
    {
        public When_not_filling_address_space() : base((0x0000, 0x1000), (0x1000, 0x1000), (0x2000, 0xd000))
        {
        }

        [Fact] public void it_should_throw() => AssertThrows(AddressSegmentExceptionType.Gap, 0xf000, 0xffff);
    }

    public class When_not_starting_address_space_at_0 : MmuConfigSpec
    {
        public When_not_starting_address_space_at_0() : base((0x1000, 0x1000), (0x2000, 0x1000), (0x3000, 0xcfff))
        {
        }

        [Fact] public void it_should_throw() => AssertThrows(AddressSegmentExceptionType.Gap, 0x0000, 0x1000);
    }

    public class When_configuring_overlapping_segments : MmuConfigSpec
    {
        public When_configuring_overlapping_segments() : base((0x0000, 0x2000), (0x1000, 0x2000), (0x3000, 0xcfff))
        {
        }

        [Fact] public void it_should_throw() => AssertThrows(AddressSegmentExceptionType.Overlap, 0x1000, 0x2000);
    }

    public class When_configuring_segment_with_0_length : MmuConfigSpec
    {
        public When_configuring_segment_with_0_length() : base((0x0000, 0x1000), (0x1000, 0x0000), (0x1000, 0xefff))
        {
        }

        [Fact] public void it_should_throw() => ConstructionShouldThrow<PlatformConfigurationException>();
    }
}
