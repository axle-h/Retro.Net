namespace GameBoy.Net.Media
{
    /// <summary>
    /// GameBoy cartridge type.
    /// </summary>
    public enum CartridgeType : byte
    {
        /// <summary>
        /// ROM only. No banking, no RAM. Simple.
        /// </summary>
        ROM = 0x00,

        /// <summary>
        /// Has MBC1 (max 2MByte ROM and/or 32KByte RAM) for ROM banking only.
        /// </summary>
        MBC1 = 0x01,

        /// <summary>
        /// Has MBC1 (max 2MByte ROM and/or 32KByte RAM) for ROM and RAM banking.
        /// </summary>
        MBC1_RAM = 0x02,

        /// <summary>
        /// Has MBC1 (max 2MByte ROM and/or 32KByte RAM) for ROM and RAM banking with battery backup.
        /// </summary>
        MBC1_RAM_BATTERY = 0x03,

        /// <summary>
        /// Has MBC2 (max 256KByte ROM and 512x4 bits RAM).
        /// </summary>
        MBC2 = 0x05,

        /// <summary>
        /// Has MBC2 (max 256KByte ROM and 512x4 bits RAM) with battery backup.
        /// </summary>
        MBC2_BATTERY = 0x06,
        
        /// <summary>
        /// Has ROM and RAM. No banking supported.
        /// </summary>
        ROM_RAM = 0x08,

        /// <summary>
        /// Has ROM and RAM with battery backup. No banking supported.
        /// </summary>
        ROM_RAM_BATTERY = 0x09,
        
        /// <summary>
        /// Chinese multi-cart... I think. Probably never implement this.
        /// </summary>
        MMM01 = 0x0B,

        /// <summary>
        /// Chinese multi-cart with RAM... I think. Probably never implement this.
        /// </summary>
        MMM01_RAM = 0x0C,

        /// <summary>
        /// Chinese multi-cart with RAM and battery backup... I think. Probably never implement this.
        /// </summary>
        MMM01_RAM_BATTERY = 0x0D,

        /// <summary>
        /// Has MBC3 (max 2MByte ROM and/or 32KByte RAM and Timer) with timer and battery backup.
        /// </summary>
        MBC3_TIMER_BATTERY = 0x0F,

        /// <summary>
        /// Has MBC3 (max 2MByte ROM and/or 32KByte RAM and Timer) with RAM and battery backup.
        /// </summary>
        MBC3_TIMER_RAM_BATTERY = 0x10,

        /// <summary>
        /// Has MBC3 (max 2MByte ROM and/or 32KByte RAM and Timer).
        /// </summary>
        MBC3 = 0x11,

        /// <summary>
        /// Has MBC3 (max 2MByte ROM and/or 32KByte RAM and Timer) with RAM.
        /// </summary>
        MBC3_RAM = 0x12,

        /// <summary>
        /// Has MBC3 (max 2MByte ROM and/or 32KByte RAM and Timer) with RAM and battery backup.
        /// </summary>
        MBC3_RAM_BATTERY = 0x13,

        /// <summary>
        /// Has MBC5 (8MB ROM. 128KB RAM. DMG, SGB, CGB).
        /// </summary>
        MBC5 = 0x19,

        /// <summary>
        /// Has MBC5 (8MB ROM. 128KB RAM. DMG, SGB, CGB) with RAM.
        /// </summary>
        MBC5_RAM = 0x1A,

        /// <summary>
        /// Has MBC5 (8MB ROM. 128KB RAM. DMG, SGB, CGB) with RAM and battery backup.
        /// </summary>
        MBC5_RAM_BATTERY = 0x1B,

        /// <summary>
        /// Has MBC5 (8MB ROM. 128KB RAM. DMG, SGB, CGB) with rumble.
        /// </summary>
        MBC5_RUMBLE = 0x1C,

        /// <summary>
        /// THas MBC5 (8MB ROM. 128KB RAM. DMG, SGB, CGB) with RAM and rumble.
        /// </summary>
        MBC5_RUMBLE_RAM = 0x1D,

        /// <summary>
        /// THas MBC5 (8MB ROM. 128KB RAM. DMG, SGB, CGB) with RAM, rumble and battery backup.
        /// </summary>
        MBC5_RUMBLE_RAM_BATTERY = 0x1E,

        /// <summary>
        /// Has MBC6 (unknown operation).
        /// </summary>
        MBC6_RAM_Battery = 0x20,        /// <summary>
        /// Has MBC7 (unknown operation).
        /// </summary>
        MBC7_RAM_BATTERY_Accelerometer = 0x22,

        /// <summary>
        /// The GameBoy pocket camera.
        /// https://github.com/AntonioND/gbcam-rev-engineer
        /// </summary>
        POCKET_CAMERA = 0xFC,

        /// <summary>
        /// Has BANDAI TAMA5 controller (unknown operation).
        /// </summary>
        BANDAI_TAMA5 = 0xFD,

        /// <summary>
        /// Has HuC3 controller.
        /// No interesting games use this.
        /// </summary>
        HuC3 = 0xFE,

        /// <summary>
        /// Has HuC1 controller (MBC with Infrared Controller).
        /// Pokemon cards uses this so must implement!
        /// It's supposedly very similar to an MBC1 with the main difference being that it supports infrared LED input / output.
        /// </summary>
        HuC1_RAM_BATTERY = 0xFF
    }
}