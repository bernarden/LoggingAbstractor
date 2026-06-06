using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core
{
    /// <summary>
    /// Represents an instance of a logger.
    /// </summary>
    public abstract class AbstractLoggerBase : IAbstractLogger
    {
        private readonly AbstractLoggerSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractLoggerBase"/> class.
        /// </summary>
        /// <param name="settings">The logger's settings.</param>
        protected AbstractLoggerBase(AbstractLoggerSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual Task TraceMessage(string message)
        {
            return TraceMessage(message, LoggingLevel.Verbose);
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual Task TraceMessage(string message, LoggingLevel loggingLevel)
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
        public abstract Task TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters);

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual Task TraceException(Exception exception)
        {
            return TraceException(exception, LoggingLevel.Critical);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual Task TraceException(Exception exception, LoggingLevel loggingLevel)
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
        public abstract Task TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters);

        /// <summary>
        /// Determines whether tracing should be performed.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>Value indicating whether tracing should be performed.</returns>
        protected bool ShouldBeTraced(LoggingLevel loggingLevel)
        {
            if (loggingLevel == LoggingLevel.None)
            {
                return false;
            }

            return loggingLevel >= _settings.MinimalLoggingLevel;
        }

        /// <summary>
        /// Combines global and specified local parameters.
        /// </summary>
        /// <param name="localParameters">The logging parameters that are explicitly specified by the user on logging call.</param>
        /// <returns>All parameters to be logged.</returns>
        protected IList<ILoggingParameter> GetGlobalAndLocalLoggingParameters(IEnumerable<ILoggingParameter> localParameters)
        {
            var globalAndLocalParameters = localParameters.Concat(_settings.GlobalLoggingParameters).ToList();

            var globalIdentityParameter = _settings.IdentityProvider?.Invoke();
            if (globalIdentityParameter != null)
            {
                globalAndLocalParameters.Add(new LoggingIdentityParameter(globalIdentityParameter));
            }

            return globalAndLocalParameters;
        }
    }
}