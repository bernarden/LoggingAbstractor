using System;
using System.Collections.Generic;
using System.Linq;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core.MultiLogger
{
    /// <summary>
    /// Responsible for combining multiple loggers at the same time.
    /// </summary>
    /// <seealso cref="Vima.LoggingAbstractor.Core.MultiLogger.IMultiLogger" />
    public class MultiLogger : IMultiLogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLogger"/> class.
        /// </summary>
        /// <param name="loggers">The loggers used to trace events.</param>
        protected MultiLogger(IEnumerable<ILogger> loggers)
        {
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void TraceMessage(string message)
        {
            TraceMessage(message, LoggingLevel.Verbose);
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        public void TraceMessage(string message, LoggingLevel loggingLevel)
        {
            TraceMessage(message, loggingLevel, Enumerable.Empty<ILoggingParameter>());
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        public void TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            IEnumerable<ILoggingParameter> loggingParameters = parameters.ToList();
            foreach (var logger in _loggers)
            {
                logger.TraceMessage(message, loggingLevel, loggingParameters);
            }
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        public void TraceException(Exception exception)
        {
            TraceException(exception, LoggingLevel.Critical);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        public void TraceException(Exception exception, LoggingLevel loggingLevel)
        {
            TraceException(exception, loggingLevel, Enumerable.Empty<ILoggingParameter>());
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        public void TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            IEnumerable<ILoggingParameter> loggingParameters = parameters.ToList();
            foreach (var logger in _loggers)
            {
                logger.TraceException(exception, loggingLevel, loggingParameters);
            }
        }
    }
}