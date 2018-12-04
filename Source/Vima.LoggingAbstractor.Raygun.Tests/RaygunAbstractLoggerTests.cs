using System;
using System.Collections.Generic;
using Mindscape.Raygun4Net.AspNetCore;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Parameters;
using Xunit;

namespace Vima.LoggingAbstractor.Raygun.Tests
{
    public sealed class RaygunAbstractLoggerTests
    {
        private const string ApiKey = "";

        private static RaygunAbstractLogger CreateRaygunAbstractLogger()
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new ArgumentNullException(nameof(ApiKey));
            }

            var raygunClient = new RaygunClient(ApiKey);
            raygunClient.SendingMessage += (sender, args) => args.Message.Details.MachineName = string.Empty;
            var raygunLogger = new RaygunAbstractLogger(raygunClient);
            return raygunLogger;
        }

        public class TraceException
        {
            [Fact(Skip = "Needs a Raygun ApiKey.")]
            public void ShouldTraceExceptionWithTags()
            {
                // Arrange
                var raygunLogger = CreateRaygunAbstractLogger();
                var exception = new Exception("Test-" + DateTime.UtcNow.ToString("s"));

                LoggingTagsParameter loggingTagsParameter = new LoggingTagsParameter(new[] { "tag", "tag2" });
                var loggingParameters = new List<ILoggingParameter> { loggingTagsParameter };

                // Act
                raygunLogger.TraceException(exception, LoggingLevel.Critical, loggingParameters);

                // Assert
                // Manually validate that it was created correctly.
            }
        }
    }
}