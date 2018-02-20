using System;
using System.Collections.Generic;
#if !NET20
using System.Linq;
#endif
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core
{
    /// <summary>
    /// Represents an instance of a logger.
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
        private readonly LoggingLevel _minimalLoggingLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerBase"/> class.
        /// </summary>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        protected LoggerBase(LoggingLevel minimalLoggingLevel)
        {
            _minimalLoggingLevel = minimalLoggingLevel;
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public virtual void TraceMessage(string message)
        {
            TraceMessage(message, LoggingLevel.Verbose);
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        public virtual void TraceMessage(string message, LoggingLevel loggingLevel)
        {
#if NET20
            TraceMessage(message, loggingLevel, new List<ILoggingAdditionalParameter>());
#else
            TraceMessage(message, loggingLevel, Enumerable.Empty<ILoggingAdditionalParameter>());
#endif
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public abstract void TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingAdditionalParameter> parameters);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        public virtual void TraceException(Exception exception)
        {
            TraceException(exception, LoggingLevel.Critical);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        public virtual void TraceException(Exception exception, LoggingLevel loggingLevel)
        {
#if NET20
            TraceException(exception, loggingLevel,  new List<ILoggingAdditionalParameter>());
#else
            TraceException(exception, loggingLevel, Enumerable.Empty<ILoggingAdditionalParameter>());
#endif
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public abstract void TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingAdditionalParameter> parameters);

        /// <summary>
        /// Determines whether tracing should be performed.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>Value indicating whether tracing should be performed</returns>
        protected bool ShouldBeTraced(LoggingLevel loggingLevel)
        {
            if (loggingLevel == LoggingLevel.None)
            {
                return false;
            }

            return loggingLevel >= _minimalLoggingLevel;
        }
    }
}