using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceMessage(string message)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceMessage(string message, LoggingLevel loggingLevel)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceException(Exception exception)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceException(Exception exception, LoggingLevel loggingLevel)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            return Task.CompletedTask;
        }
    }
}