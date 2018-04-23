using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Retro.Net.Util
{
    /// <inheritdoc />
    /// <summary>
    /// Simple messaging.
    /// </summary>
    /// <seealso cref="T:Retro.Net.Util.IMessageBus" />
    public class ReactiveMessageBus : IMessageBus
    {
        private readonly ISubject<(string message, IObserver<object> responseObserver)> _subject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveMessageBus"/> class.
        /// </summary>
        public ReactiveMessageBus()
        {
            _subject = new Subject<(string message, IObserver<object> responseObserver)>();
        }

        public IObservable<IObserver<object>> GetObservable(string message) =>
            _subject.Where(x => x.message == message).Select(x => x.responseObserver).AsObservable();

        public async Task<TResponse> RequestAsync<TResponse>(string message, CancellationToken cancellationToken)
            where TResponse : class
        {
            var resultSubject = new AsyncSubject<object>();

            _subject.OnNext((message, resultSubject));

            using (var cancellation = new Subject<object>())
            {
                cancellationToken.Register(() => cancellation.OnNext(null));
                var result = await resultSubject.TakeUntil(cancellation).FirstAsync();

                cancellationToken.ThrowIfCancellationRequested();

                switch (result)
                {
                    case null:
                        return null;

                    case TResponse response:
                        return response;

                    default:
                        throw new ArgumentException($"Incorrect response type: expecting {typeof(TResponse)}, observed {result.GetType()}", nameof(TResponse));
                }
            }
        }

        public void FireAndForget(string message) => _subject.OnNext((message, Observer.Create<object>(_ => { })));
    }
}
