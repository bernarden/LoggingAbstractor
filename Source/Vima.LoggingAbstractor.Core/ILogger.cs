using System;

namespace Vima.LoggingAbstractor.Core
{
    /// <summary>
    /// Represents an instance of a logger. 
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void TraceMessage(string message);

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void TraceException(Exception exception);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel);
    }
}
