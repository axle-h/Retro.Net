using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Retro.Net.Tests.Util
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddXunit(this ILoggerFactory factory, ITestOutputHelper outputHelper)
        {
            factory.AddProvider(new XunitLoggerProvider(outputHelper));
            return factory;
        }
    }

    public class XunitLoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper _outputHelper;

        public XunitLoggerProvider(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName) => new XunitLogger(_outputHelper, categoryName);

        private class XunitLogger : ILogger
        {
            private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
            private readonly ITestOutputHelper _outputHelper;
            private readonly string _name;

            public XunitLogger(ITestOutputHelper outputHelper, string name)
            {
                _outputHelper = outputHelper;
                _name = name;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) =>
                _outputHelper.WriteLine($"[{_stopwatch.Elapsed}] [{_name}] [{logLevel}] {formatter(state, exception)}");

            public bool IsEnabled(LogLevel logLevel) => true;

            public IDisposable BeginScope<TState>(TState state) => Disposable.Empty;
        }
    }
}
