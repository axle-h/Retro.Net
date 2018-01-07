using System;
using Retro.Net.Api.RealTime.Models;

namespace Retro.Net.Api.RealTime.Interfaces
{
    public interface IWebSocketMessageSerializer
    {
        GameBoySocketMessage DeSerialize(ArraySegment<byte> bytes);

        GameBoySocketMessage DeSerialize(string json);
    }
}