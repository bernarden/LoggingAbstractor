using System;
using SharpRaven;
using SharpRaven.Data;
using Vima.LoggingAbstractor.Core;

namespace Vima.LoggingAbstractor.Sentry
{
    /// <summary>
    /// Represents an instance of a Sentry logger. 
    /// </summary>
    public class SentryLogger : ILogger
    {
        private readonly RavenClient _ravenClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SentryLogger"/> class.
        /// </summary>
        /// <param name="ravenClient">The raven client.</param>
        public SentryLogger(RavenClient ravenClient)
        {
            _ravenClient = ravenClient ?? throw new ArgumentNullException(nameof(ravenClient));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void TraceMessage(string message)
        {
            _ravenClient.Capture(new SentryEvent(message));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel)
        {
            _ravenClient.Capture(new SentryEvent(message));
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void TraceException(Exception exception)
        {
            _ravenClient.Capture(new SentryEvent(exception));
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel)
        {
            _ravenClient.Capture(new SentryEvent(exception));
        }
    }
}
