using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task TraceMessage(string message);

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task TraceMessage(string message, LoggingLevel loggingLevel);

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task TraceException(Exception exception);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task TraceException(Exception exception, LoggingLevel loggingLevel);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters);
    }
}
