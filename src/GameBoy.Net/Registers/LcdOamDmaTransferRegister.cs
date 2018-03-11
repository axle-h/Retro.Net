using GameBoy.Net.Registers.Interfaces;
using Retro.Net.Memory;
using Retro.Net.Memory.Dma;
using Retro.Net.Timing;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// FF46 - DMA - DMA Transfer and Start Address (W)
    /// Writing to this register launches a DMA transfer from ROM or RAM to OAM memory (sprite attribute table). The
    /// written value specifies the transfer source address divided by 100h, ie. source & destination are:
    /// Source:      XX00-XX9F; XX in range from 00-F1h
    /// Destination: FE00-FE9F
    /// It takes 160 microseconds until the transfer has completed (80 microseconds in CGB Double Speed Mode), during this
    /// time the CPU can access only HRAM (memory at FF80-FFFE).
    /// </summary>
    public class LcdOamDmaTransferRegister : IRegister
    {
        // During OAM DMA the CPU can only access HRAM (memory at FF80-FFFE).
        private static readonly AddressRange[] LockedAddresses = { new AddressRange(0, 0xff79), new AddressRange(0xffff, 0xffff) };

        private readonly IDmaController _dmaController;

        /// <summary>
        /// Initializes a new instance of the <see cref="LcdOamDmaTransferRegister" /> class.
        /// </summary>
        /// <param name="dmaController">The dma controller.</param>
        public LcdOamDmaTransferRegister(IDmaController dmaController)
        {
            _dmaController = dmaController;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public ushort Address => 0xff46;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "DMA Transfer and Start Address (W)";

        /// <summary>
        /// Gets or sets the raw register value.
        /// </summary>
        /// <value>
        /// The raw register value.
        /// </value>
        public byte Register
        {
            get { return 0x00; }
            set
            {
                var address = (ushort) (value << 8);

                // Sync the timings to 755 machine cycles, which is approx 160us on DGB and 80ms on CGB.
                _dmaController.Copy(address, 0xfe00, 0x9f, new InstructionTimings(755), LockedAddresses);
            }
        }

        /// <summary>
        /// Gets the debug view.
        /// </summary>
        /// <value>
        /// The debug view.
        /// </value>
        public string DebugView => ToString();

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString() => $"{Name} ({Address}) = {Register}";
    }
}