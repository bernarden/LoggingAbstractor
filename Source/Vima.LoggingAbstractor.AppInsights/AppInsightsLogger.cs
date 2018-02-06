using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Vima.LoggingAbstractor.Core;

namespace Vima.LoggingAbstractor.AppInsights
{
    /// <summary>
    /// Represents an instance of an Application Insights logger.
    /// </summary>
    public class AppInsightsLogger : ILogger
    {
        private readonly TelemetryClient _telemetryClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppInsightsLogger"/> class.
        /// </summary>
        /// <param name="telemetryClient">The Application Insights client.</param>
        public AppInsightsLogger(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void TraceMessage(string message)
        {
            _telemetryClient.Track(new TraceTelemetry(message));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel)
        {
            _telemetryClient.Track(new TraceTelemetry(message));
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void TraceException(Exception exception)
        {
            _telemetryClient.Track(new ExceptionTelemetry(exception));
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="loggingSeverityLevel">The logging severity level.</param>
        public void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel)
        {
            _telemetryClient.Track(new ExceptionTelemetry(exception));
        }
    }
}
