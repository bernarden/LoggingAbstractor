using System;
using System.Collections.Generic;
using Mindscape.Raygun4Net;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Parameters;

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
            TraceMessage(message, LoggingSeverityLevel.Verbose);
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel)
        {
            TraceMessage(message, LoggingSeverityLevel.Verbose, new List<ILoggingAdditionalParameter>());
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
            var messageException = new RaygunMessageException(message);
            _raygunClient.Send(messageException);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        public void TraceException(Exception exception)
        {
            TraceException(exception, LoggingSeverityLevel.Critical);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel)
        {
            TraceException(exception, LoggingSeverityLevel.Critical, new List<ILoggingAdditionalParameter>());
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
            _raygunClient.Send(exception);
        }
    }
}
