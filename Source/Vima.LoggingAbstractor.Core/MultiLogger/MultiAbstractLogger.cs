using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core.MultiLogger
{
    /// <summary>
    /// Responsible for combining multiple loggers at the same time.
    /// </summary>
    /// <seealso cref="IMultiAbstractLogger" />
    public class MultiAbstractLogger : IMultiAbstractLogger
    {
        private readonly IEnumerable<IAbstractLogger> _loggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiAbstractLogger"/> class.
        /// </summary>
        /// <param name="loggers">The loggers used to trace events.</param>
        public MultiAbstractLogger(IEnumerable<IAbstractLogger> loggers)
        {
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceMessage(string message)
        {
           return TraceMessage(message, LoggingLevel.Verbose);
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceMessage(string message, LoggingLevel loggingLevel)
        {
            return TraceMessage(message, loggingLevel, Enumerable.Empty<ILoggingParameter>());
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            IEnumerable<ILoggingParameter> loggingParameters = parameters.ToList();
            foreach (var logger in _loggers)
            {
               await logger.TraceMessage(message, loggingLevel, loggingParameters);
            }
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceException(Exception exception)
        {
            return TraceException(exception, LoggingLevel.Critical);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task TraceException(Exception exception, LoggingLevel loggingLevel)
        {
           return TraceException(exception, loggingLevel, Enumerable.Empty<ILoggingParameter>());
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            IEnumerable<ILoggingParameter> loggingParameters = parameters.ToList();
            foreach (var logger in _loggers)
            {
               await logger.TraceException(exception, loggingLevel, loggingParameters);
            }
        }
    }
}