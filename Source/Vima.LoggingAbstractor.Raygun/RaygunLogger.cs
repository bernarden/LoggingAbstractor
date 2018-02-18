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
    public class RaygunLogger : LoggerBase
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

        private static LoggingSeverityLevel MinimalLoggingLevel => LoggingSeverityLevel.Verbose;

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public override void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
            if (loggingSeverityLevel < MinimalLoggingLevel)
            {
                return;
            }

            var messageException = new RaygunMessageException(message);
            _raygunClient.Send(messageException);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public override void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
            if (loggingSeverityLevel < MinimalLoggingLevel)
            {
                return;
            }

            _raygunClient.Send(exception);
        }
    }
}
