using System;
using System.Collections.Generic;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core
{
    /// <summary>
    /// Represents an instance of a logger.
    /// </summary>
    public interface IAbstractLogger
    {
        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        void TraceMessage(string message);

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        void TraceMessage(string message, LoggingLevel loggingLevel);

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        void TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        void TraceException(Exception exception);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        void TraceException(Exception exception, LoggingLevel loggingLevel);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        void TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters);
    }
}
