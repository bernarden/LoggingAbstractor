using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.AppInsights
{
    /// <summary>
    /// Represents an instance of an Application Insights logger.
    /// </summary>
    public class AppInsightsLogger : LoggerBase
    {
        private readonly TelemetryClient _telemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInsightsLogger"/> class.
        /// </summary>
        /// <param name="telemetryClient">The Application Insights client.</param>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        public AppInsightsLogger(TelemetryClient telemetryClient, LoggingSeverityLevel minimalLoggingLevel = LoggingSeverityLevel.Verbose)
            : base(minimalLoggingLevel)
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public override void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
            if (!ShouldBeTraced(loggingSeverityLevel))
            {
                return;
            }

            _telemetryClient.Track(new TraceTelemetry(message));
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public override void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
            if (!ShouldBeTraced(loggingSeverityLevel))
            {
                return;
            }

            _telemetryClient.Track(new ExceptionTelemetry(exception));
        }
    }
}
