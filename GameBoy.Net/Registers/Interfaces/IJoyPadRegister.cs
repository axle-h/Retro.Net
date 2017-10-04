using GameBoy.Net.Devices;

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