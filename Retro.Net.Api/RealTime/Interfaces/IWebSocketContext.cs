using System;

namespace Retro.Net.Api.RealTime.Interfaces
{
    public interface IWebSocketContext
    {
        IWebSocketRenderer GetRenderer(Guid id);
        Guid GetNewCoreId();
    }
}