using System;
using System.Collections.Generic;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core.NoOpLogger
{
    /// <summary>
    /// Represents an instance of a logger that performs no operations.
    /// </summary>
    /// <seealso cref="INoOpAbstractLogger" />
    public class NoOpAbstractLogger : INoOpAbstractLogger
    {
        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void TraceMessage(string message)
        {
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        public void TraceMessage(string message, LoggingLevel loggingLevel)
        {
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        public void TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        public void TraceException(Exception exception)
        {
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        public void TraceException(Exception exception, LoggingLevel loggingLevel)
        {
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        public void TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
        }
    }
}