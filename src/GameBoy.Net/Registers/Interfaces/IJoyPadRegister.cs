using GameBoy.Net.Devices;
using GameBoy.Net.Devices.Interfaces;

namespace GameBoy.Net.Registers.Interfaces
{
    /// <summary>
    /// The GameBoy joypad register.
    /// </summary>
    /// <seealso cref="IJoyPad" />
    /// <seealso cref="IRegister" />
    public interface IJoyPadRegister : IJoyPad, IRegister
    {
    }
}