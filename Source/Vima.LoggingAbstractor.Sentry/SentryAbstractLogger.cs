#if SentrySDK
using System;
using System.Collections.Generic;
using System.Linq;
using Sentry;
using Sentry.Protocol;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Extensions;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Sentry
{
    /// <summary>
    /// Represents an instance of a Sentry logger.
    /// </summary>
    /// <seealso cref="AbstractLoggerBase" />
    /// <seealso cref="ISentryAbstractLogger" />
    public class SentryAbstractLogger : AbstractLoggerBase, ISentryAbstractLogger
    {
        private readonly ISentryClient _sentryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SentryAbstractLogger"/> class.
        /// </summary>
        /// <param name="sentryClient">The Sentry client.</param>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        public SentryAbstractLogger(ISentryClient sentryClient, LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(new AbstractLoggerSettings { MinimalLoggingLevel = minimalLoggingLevel })
        {
            _sentryClient = sentryClient ?? throw new ArgumentNullException(nameof(sentryClient));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SentryAbstractLogger"/> class.
        /// </summary>
        /// <param name="sentryClient">The Sentry client.</param>
        /// <param name="settings">The logger's settings.</param>
        public SentryAbstractLogger(ISentryClient sentryClient, AbstractLoggerSettings settings)
            : base(settings ?? throw new ArgumentNullException(nameof(settings)))
        {
            _sentryClient = sentryClient ?? throw new ArgumentNullException(nameof(sentryClient));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        public override void TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            if (!ShouldBeTraced(loggingLevel))
            {
                return;
            }

            var scope = CreateSentryScope(loggingLevel, parameters);
            _sentryClient.CaptureEvent(new SentryEvent { Message = message }, scope);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        public override void TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            if (!ShouldBeTraced(loggingLevel))
            {
                return;
            }

            var scope = CreateSentryScope(loggingLevel, parameters);
            _sentryClient.CaptureEvent(new SentryEvent(exception), scope);
        }

        private static Dictionary<string, string> GenerateTags(IEnumerable<ILoggingParameter> parameters)
        {
            return parameters.ExtractTags().ToDictionary(tag => tag);
        }

        private Scope CreateSentryScope(LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            var loggingParameters = GetGlobalAndLocalLoggingParameters(parameters);
            Dictionary<string, string> tags = GenerateTags(loggingParameters);

            var scope = new Scope(new SentryOptions());
            scope.SetTags(tags);
            scope.Level = LoggingLevelMapper.ConvertLoggingLevelToSentryLevel(loggingLevel);

            var identity = loggingParameters.ExtractIdentity();
            if (!string.IsNullOrEmpty(identity?.Identity))
            {
                scope.User.Id = identity.Identity;
                scope.User.Username = identity.Name;
            }

            var extraCount = 1;
            foreach (string data in loggingParameters.ExtractData())
            {
                scope.SetExtra($"Extra #{extraCount++}", data);
            }

            return scope;
        }
    }
}
#endif