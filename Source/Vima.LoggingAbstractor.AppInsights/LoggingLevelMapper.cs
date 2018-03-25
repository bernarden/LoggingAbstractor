using Microsoft.ApplicationInsights.DataContracts;
using Vima.LoggingAbstractor.Core;

namespace Vima.LoggingAbstractor.AppInsights
{
    /// <summary>
    /// Responsible for mapping <see cref="LoggingLevel"/> to <see cref="SeverityLevel"/>.
    /// </summary>
    internal class LoggingLevelMapper
    {
        /// <summary>
        /// Converts the <see cref="LoggingLevel"/> to <see cref="SeverityLevel"/>.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <returns>Application Insights severity level.</returns>
        internal static SeverityLevel ConvertLoggingLevelToSeverityLevel(LoggingLevel loggingLevel)
        {
            switch (loggingLevel)
            {
                case LoggingLevel.Verbose:
                    return SeverityLevel.Verbose;
                case LoggingLevel.Information:
                    return SeverityLevel.Information;
                case LoggingLevel.Warning:
                    return SeverityLevel.Warning;
                case LoggingLevel.Error:
                    return SeverityLevel.Error;
                case LoggingLevel.Critical:
                    return SeverityLevel.Critical;
                default:
                    return SeverityLevel.Verbose;
            }
        }
    }
}