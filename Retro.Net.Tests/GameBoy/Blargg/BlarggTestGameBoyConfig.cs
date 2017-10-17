using GameBoy.Net;
using GameBoy.Net.Config;

namespace Retro.Net.Tests.GameBoy.Blargg
{
    internal class BlarggTestGameBoyConfig : IGameBoyConfig
    {
        public BlarggTestGameBoyConfig(byte[] cartridgeData)
        {
            CartridgeData = cartridgeData;
        }
        
        public byte[] CartridgeData { get; }

        public GameBoyType GameBoyType => GameBoyType.GameBoy;

        public bool RunGpu => false;
    }
}
