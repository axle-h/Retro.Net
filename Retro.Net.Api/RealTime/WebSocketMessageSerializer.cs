using System;
using MessagePack;
using Newtonsoft.Json;
using Retro.Net.Api.RealTime.Interfaces;
using Retro.Net.Api.RealTime.Models;

namespace Retro.Net.Api.RealTime
{
    public class WebSocketMessageSerializer : IWebSocketMessageSerializer
    {
        public GameBoySocketMessage DeSerialize(ArraySegment<byte> bytes) => MessagePackSerializer.Deserialize<GameBoySocketMessage>(bytes);

        public GameBoySocketMessage DeSerialize(string json) => JsonConvert.DeserializeObject<GameBoySocketMessage>(json);
    }
}
