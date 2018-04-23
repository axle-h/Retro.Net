using System;
using System.Threading;
using System.Threading.Tasks;

namespace Retro.Net.Util
{
    /// <summary>
    /// Simple messaging.
    /// </summary>
    public interface IMessageBus
    {
        IObservable<IObserver<object>> GetObservable(string message);

        Task<TResponse> RequestAsync<TResponse>(string message, CancellationToken cancellationToken)
            where TResponse : class;

        void FireAndForget(string message);
    }
}