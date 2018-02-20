using System.Collections.Generic;
using System.Linq;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core.Extensions
{
    /// <summary>
    /// Responsible for containing all of the extensions for logging parameters.
    /// </summary>
    public static class LoggingParameterExtensions
    {
        /// <summary>
        /// Extracts the tags.
        /// </summary>
        /// <param name="parameters">The logging parameters.</param>
        /// <returns>Tags</returns>
        public static IEnumerable<string> ExtractTags(this IEnumerable<ILoggingParameter> parameters)
        {
            List<ILoggingParameter> loggingParameters = parameters
                .Where(x => x.LoggingParameterType == LoggingParameterType.Tags)
                .ToList();

            if (!loggingParameters.Any())
            {
                return new List<string>();
            }

            List<string> result = new List<string>();
            foreach (var loggingParameter in loggingParameters)
            {
                if (loggingParameter is ILoggingParameter<IEnumerable<string>> tags)
                {
                    result.AddRange(tags.Value);
                }
            }

            return result.Distinct();
        }
    }
}
