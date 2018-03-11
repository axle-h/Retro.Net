using System;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// Lazy i.e. will calculate the value of the register on read. As opposed to tracking the timings in real time.
    /// It also assumes the CPU is running real time.
    /// TODO: implement SGB & CGB increment speeds.
    /// </summary>
    public class LazyDividerRegister : DividerRegisterBase
    {
        private const int RegisterIncrementRate = 16384;

        private DateTime _dateLastSet;

        private byte _registerValue;

        /// <summary>
        /// This register is incremented 16384 (~16779 on SGB) times a second.
        /// Writing any value sets it to $00.
        /// It is also affected by CGB double speed.
        /// </summary>
        public override byte Register
        {
            get
            {
                var timeSinceLastSet = DateTime.UtcNow - _dateLastSet;
                var totalIncrements = timeSinceLastSet.TotalSeconds * RegisterIncrementRate % RegisterIncrementRate;

                _registerValue = (byte) (_registerValue + totalIncrements);
                _dateLastSet = DateTime.UtcNow;

                return _registerValue;
            }
            set
            {
                // Writing anything to this register resets it
                _registerValue = 0x00;
                _dateLastSet = DateTime.UtcNow;
            }
        }
    }
}