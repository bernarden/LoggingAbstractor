using System;

namespace Vima.LoggingAbstractor.Core.Parameters
{
    /// <summary>
    /// Represents logging environment parameter.
    /// </summary>
    public class LoggingEnvironmentParameter : ILoggingParameter<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingEnvironmentParameter"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public LoggingEnvironmentParameter(string environment)
        {
            Value = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        /// <summary>
        /// Gets the parameter's value.
        /// </summary>
        /// <value>
        /// The parameter's value.
        /// </value>
        public string Value { get; }

        /// <summary>
        /// Gets the type of the logging parameter.
        /// </summary>
        /// <value>
        /// The type of the logging parameter.
        /// </value>
        public LoggingParameterType LoggingParameterType => LoggingParameterType.Environment;
    }
}