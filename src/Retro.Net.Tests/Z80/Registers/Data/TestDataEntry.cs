namespace Retro.Net.Tests.Z80.Registers.Data
{
    public class TestDataEntry<TValue, TExpected>
    {
        public TValue Value { get; set; }

        public TExpected Expected { get; set; }

        public override string ToString()
        {
            return $"{nameof(Value)}: {Value}, {nameof(Expected)}: {Expected}";
        }
    }
}
