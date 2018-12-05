using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Parameters;
using Xunit;

namespace Vima.LoggingAbstractor.AppInsights.Tests
{
    public sealed class AppInsightsAbstractLoggerTests
    {
        private const string ApiKey = "";

        private static AppInsightsAbstractLogger CreateAppInsightsAbstractLogger(out TelemetryClient telemetryClient)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new ArgumentNullException(nameof(ApiKey));
            }

            telemetryClient = new TelemetryClient(new TelemetryConfiguration(ApiKey));
            var appInsightsLogger = new AppInsightsAbstractLogger(telemetryClient);
            return appInsightsLogger;
        }

        public class TraceException
        {
            [Fact(Skip = "Needs an Application Insights ApiKey.")]
            public void ShouldTraceExceptionWithTags()
            {
                // Arrange
                var appInsightsLogger = CreateAppInsightsAbstractLogger(out var telemetryClient);
                var exception = new Exception("Test-" + DateTime.UtcNow.ToString("s"));

                var loggingTagsParameter = new LoggingTagsParameter(new[] { "tag", "tag2" });
                var loggingIdentityParameter = new LoggingIdentityParameter("identity", "name");
                var loggingParameters = new List<ILoggingParameter> { loggingTagsParameter, loggingIdentityParameter };

                // Act
                appInsightsLogger.TraceException(exception, LoggingLevel.Critical, loggingParameters);
                telemetryClient.Flush();

                // Assert
                // Manually validate that it was created correctly.
            }
        }
    }
}