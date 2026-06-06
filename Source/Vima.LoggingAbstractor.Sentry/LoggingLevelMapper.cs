using Sentry;
using Vima.LoggingAbstractor.Core;

namespace Vima.LoggingAbstractor.Sentry
{
    /// <summary>
    /// Responsible for mapping <see cref="LoggingLevel"/> to respective Sentry level.
    /// </summary>
    internal static class LoggingLevelMapper
    {
        /// <summary>
        /// Converts the <see cref="LoggingLevel"/> to <see cref="SentryLevel"/>.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>Sentry error level.</returns>
        internal static SentryLevel ConvertLoggingLevelToSentryLevel(LoggingLevel loggingLevel)
        {
            switch (loggingLevel)
            {
                case LoggingLevel.Verbose:
                    return SentryLevel.Debug;
                case LoggingLevel.Information:
                    return SentryLevel.Info;
                case LoggingLevel.Warning:
                    return SentryLevel.Warning;
                case LoggingLevel.Error:
                    return SentryLevel.Error;
                case LoggingLevel.Critical:
                    return SentryLevel.Fatal;
                default:
                    return SentryLevel.Debug;
            }
        }
    }
}