using System;
using System.Collections.Generic;
using Sentry;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Parameters;
using Xunit;

namespace Vima.LoggingAbstractor.Sentry.Tests
{
    public sealed class SentryAbstractLoggerTests
    {
        private const string Dsn = "";

        private static SentryAbstractLogger CreateSentryAbstractLogger(out SentryClient sentryClient)
        {
            if (string.IsNullOrEmpty(Dsn))
            {
                throw new ArgumentNullException(nameof(Dsn));
            }

            sentryClient = new SentryClient(new SentryOptions { Dsn = Dsn });
            var sentryLogger = new SentryAbstractLogger(sentryClient);
            return sentryLogger;
        }

        public class TraceException
        {
            [Fact(Skip = "Needs a Sentry Dsn.")]
            public void ShouldTraceExceptionWithTags()
            {
                // Arrange
                var sentryLogger = CreateSentryAbstractLogger(out var sentryClient);
                var exception = new Exception("Test-" + DateTime.UtcNow.ToString("s"));

                var loggingTagsParameter = new LoggingTagsParameter(new[] { "tag", "tag2" });
                var loggingIdentityParameter = new LoggingIdentityParameter("identity", "name");
                var loggingParameters = new List<ILoggingParameter> { loggingTagsParameter, loggingIdentityParameter };

                // Act
                sentryLogger.TraceException(exception, LoggingLevel.Critical, loggingParameters);
                sentryClient.Flush();

                // Assert
                // Manually validate that it was created correctly.
            }
        }
    }
}