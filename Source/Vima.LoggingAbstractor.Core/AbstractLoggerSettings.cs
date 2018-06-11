using System.Collections.Generic;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core
{
    /// <summary>
    /// Represents an instance of settings.
    /// </summary>
    public class AbstractLoggerSettings
    {
        private IEnumerable<ILoggingParameter> _globalLoggingParameters;

        /// <summary>
        /// Gets or sets the minimal logging lever required for a log to go through.
        /// </summary>
        /// <value>
        /// The minimal logging lever required for a log to go through.
        /// </value>
        public LoggingLevel MinimalLoggingLevel { get; set; }

        /// <summary>
        /// Gets or sets the global logging parameters that are applied to every log.
        /// </summary>
        /// <value>
        /// The global logging parameters.
        /// </value>
        public IEnumerable<ILoggingParameter> GlobalLoggingParameters
        {
            get => _globalLoggingParameters ?? (_globalLoggingParameters = new List<ILoggingParameter>());
            set => _globalLoggingParameters = value;
        }
    }
}