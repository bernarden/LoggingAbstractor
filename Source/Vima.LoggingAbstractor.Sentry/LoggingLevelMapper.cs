using SharpRaven.Data;
using Vima.LoggingAbstractor.Core;

namespace Vima.LoggingAbstractor.Sentry
{
    /// <summary>
    /// Responsible for mapping <see cref="LoggingLevel"/> to <see cref="ErrorLevel"/>.
    /// </summary>
    internal static class LoggingLevelMapper
    {
        /// <summary>
        /// Converts the <see cref="LoggingLevel"/> to <see cref="ErrorLevel"/>.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>Sentry error level.</returns>
        internal static ErrorLevel ConvertLoggingLevelToErrorLevel(LoggingLevel loggingLevel)
        {
            switch (loggingLevel)
            {
                case LoggingLevel.Verbose:
                    return ErrorLevel.Debug;
                case LoggingLevel.Information:
                    return ErrorLevel.Info;
                case LoggingLevel.Warning:
                    return ErrorLevel.Warning;
                case LoggingLevel.Error:
                    return ErrorLevel.Error;
                case LoggingLevel.Critical:
                    return ErrorLevel.Fatal;
                default:
                    return ErrorLevel.Debug;
            }
        }
    }
}