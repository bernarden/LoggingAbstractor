using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
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
        /// <returns>Tag values.</returns>
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

        /// <summary>
        /// Extracts the data values.
        /// </summary>
        /// <param name="parameters">The logging parameters.</param>
        /// <returns>Data values.</returns>
        public static IEnumerable<string> ExtractData(this IEnumerable<ILoggingParameter> parameters)
        {
            List<ILoggingParameter> loggingParameters = parameters
                .Where(x => x.LoggingParameterType == LoggingParameterType.Data)
                .ToList();

            if (!loggingParameters.Any())
            {
                return new List<string>();
            }

            List<string> result = new List<string>();
            foreach (var loggingParameter in loggingParameters)
            {
                if (loggingParameter is ILoggingParameter<object> data)
                {
                    string value = JsonConvert.SerializeObject(data.Value);
                    result.Add(value);
                }
            }

            return result;
        }

        /// <summary>
        /// Extracts the identity information.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Identity value.</returns>
        public static IdentityParameter ExtractIdentity(this IEnumerable<ILoggingParameter> parameters)
        {
            ILoggingParameter loggingParameter = parameters
                .FirstOrDefault(x => x.LoggingParameterType == LoggingParameterType.Identity);

            if (loggingParameter != null && loggingParameter is ILoggingParameter<IdentityParameter> identity)
            {
                return identity.Value;
            }

            return null;
        }
    }
}