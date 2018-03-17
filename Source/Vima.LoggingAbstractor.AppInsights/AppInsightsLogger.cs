using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Extensions;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.AppInsights
{
    /// <summary>
    /// Represents an instance of an Application Insights logger.
    /// </summary>
    /// <seealso cref="Vima.LoggingAbstractor.Core.LoggerBase" />
    /// <seealso cref="Vima.LoggingAbstractor.AppInsights.IAppInsightsLogger" />
    public class AppInsightsLogger : LoggerBase, IAppInsightsLogger
    {
        private readonly TelemetryClient _telemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInsightsLogger"/> class.
        /// </summary>
        /// <param name="telemetryClient">The Application Insights client.</param>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        public AppInsightsLogger(TelemetryClient telemetryClient, LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(minimalLoggingLevel)
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

            var traceTelemetry = new TraceTelemetry(message);
            AddParametersToProperties(traceTelemetry, parameters);
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

            var exceptionTelemetry = new ExceptionTelemetry(exception);
            AddParametersToProperties(exceptionTelemetry, parameters);
            _telemetryClient.Track(exceptionTelemetry);
        }

        private static void AddParametersToProperties(ISupportProperties telemetry, IEnumerable<ILoggingParameter> parameters)
        {
            IEnumerable<ILoggingParameter> loggingParameters = parameters.ToList();

            foreach (string tag in loggingParameters.ExtractTags())
            {
                telemetry.Properties.Add(tag, tag);
            }

            var dataCount = 0;
            foreach (string data in loggingParameters.ExtractData())
            {
                telemetry.Properties.Add($"Data #{dataCount++}", data);
            }
        }
    }
}
