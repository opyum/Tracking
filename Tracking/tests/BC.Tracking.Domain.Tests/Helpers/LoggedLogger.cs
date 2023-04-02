using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace Connexity.BC.Tracking.Domain.Tests.Helpers
{
    public class LoggedLogger<T> : ILogger<T>
    {
        /// <summary>
        /// The output
        /// </summary>
        private readonly ITestOutputHelper output;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggedLogger<typeparamref name="T"/>"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public LoggedLogger(ITestOutputHelper output)
        {
            this.output = output;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            this.output.WriteLine($"Scope begin for state : {state}");
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = state.ToString();
            if (formatter != null && exception != null)
            {
                message = formatter(state, exception);
            }
            this.output.WriteLine($"{logLevel},{eventId},{message}");
        }
    }
}