using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Extensions;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.AppInsights
{
    /// <summary>
    /// Represents an instance of an Application Insights logger.
    /// </summary>
    /// <seealso cref="AbstractLoggerBase" />
    /// <seealso cref="IAppInsightsAbstractLogger" />
    public class AppInsightsAbstractLogger : AbstractLoggerBase, IAppInsightsAbstractLogger
    {
        private readonly TelemetryClient _telemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInsightsAbstractLogger"/> class.
        /// </summary>
        /// <param name="telemetryClient">The Application Insights client.</param>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        public AppInsightsAbstractLogger(TelemetryClient telemetryClient, LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(new AbstractLoggerSettings { MinimalLoggingLevel = minimalLoggingLevel })
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInsightsAbstractLogger"/> class.
        /// </summary>
        /// <param name="telemetryClient">The Application Insights client.</param>
        /// <param name="settings">The logger's settings.</param>
        public AppInsightsAbstractLogger(TelemetryClient telemetryClient, AbstractLoggerSettings settings)
            : base(settings ?? throw new ArgumentNullException(nameof(settings)))
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
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

            var allParameters = GetGlobalAndLocalLoggingParameters(parameters);
            var traceTelemetry = new TraceTelemetry(message)
            {
                SeverityLevel = LoggingLevelMapper.ConvertLoggingLevelToSeverityLevel(loggingLevel)
            };
            SetIdentityParameter(traceTelemetry, allParameters);
            AddParametersToProperties(traceTelemetry, allParameters);
            _telemetryClient.Track(traceTelemetry);
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

            var allParameters = GetGlobalAndLocalLoggingParameters(parameters);
            var exceptionTelemetry = new ExceptionTelemetry(exception)
            {
                SeverityLevel = LoggingLevelMapper.ConvertLoggingLevelToSeverityLevel(loggingLevel)
            };
            SetIdentityParameter(exceptionTelemetry, allParameters);
            AddParametersToProperties(exceptionTelemetry, allParameters);
            _telemetryClient.Track(exceptionTelemetry);
        }

        private static void SetIdentityParameter(ITelemetry exceptionTelemetry, IEnumerable<ILoggingParameter> loggingParameters)
        {
            var identity = loggingParameters.ExtractIdentity();
            if (identity == null || string.IsNullOrEmpty(identity.Identity))
            {
                return;
            }

            exceptionTelemetry.Context.User.Id = identity.Identity;
        }

        private static void AddParametersToProperties(ISupportProperties telemetry, IEnumerable<ILoggingParameter> parameters)
        {
            IEnumerable<ILoggingParameter> loggingParameters = parameters.ToList();

            var tagCount = 1;
            foreach (string tag in loggingParameters.ExtractTags())
            {
                telemetry.Properties.Add($"Tag #{tagCount++}", tag);
            }

            var dataCount = 1;
            foreach (string data in loggingParameters.ExtractData())
            {
                telemetry.Properties.Add($"Data #{dataCount++}", data);
            }
        }
    }
}
