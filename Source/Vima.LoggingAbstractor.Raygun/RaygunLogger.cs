using System;
using Mindscape.Raygun4Net;
using Vima.LoggingAbstractor.Core;

namespace Vima.LoggingAbstractor.Raygun
{
    /// <summary>
    /// Represents an instance of a Raygun logger.
    /// </summary>
    public class RaygunLogger : ILogger
    {
        private readonly RaygunClient _raygunClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaygunLogger"/> class.
        /// </summary>
        /// <param name="raygunClient">The raygun client.</param>
        public RaygunLogger(RaygunClient raygunClient)
        {
            _raygunClient = raygunClient ?? throw new ArgumentNullException(nameof(raygunClient));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void TraceMessage(string message)
        {
            var messageException = new RaygunMessageException(message);
            _raygunClient.Send(messageException);
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel)
        {
            var messageException = new RaygunMessageException(message);
            _raygunClient.Send(messageException);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void TraceException(Exception exception)
        {
            _raygunClient.Send(exception);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel)
        {
            _raygunClient.Send(exception);
        }
    }
}
