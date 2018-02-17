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
        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public virtual void TraceMessage(string message)
        {
            TraceMessage(message, LoggingSeverityLevel.Verbose);
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public virtual void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel)
        {
#if NET20
            TraceMessage(message, loggingSeverityLevel, new List<ILoggingAdditionalParameter>());
#else
            TraceMessage(message, loggingSeverityLevel, Enumerable.Empty<ILoggingAdditionalParameter>());
#endif
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public abstract void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        public virtual void TraceException(Exception exception)
        {
            TraceException(exception, LoggingSeverityLevel.Critical);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public virtual void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel)
        {
#if NET20
            TraceException(exception, loggingSeverityLevel,  new List<ILoggingAdditionalParameter>());
#else
            TraceException(exception, loggingSeverityLevel, Enumerable.Empty<ILoggingAdditionalParameter>());
#endif
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public abstract void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters);
    }
}