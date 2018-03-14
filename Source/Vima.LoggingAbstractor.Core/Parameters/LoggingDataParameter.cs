using System;

namespace Vima.LoggingAbstractor.Core.Parameters
{
    /// <summary>
    /// Represents logging data parameter.
    /// </summary>
    public class LoggingDataParameter : ILoggingParameter<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingDataParameter"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public LoggingDataParameter(object data)
        {
            Value = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        /// Gets the parameter's value.
        /// </summary>
        /// <value>
        /// The parameter's value.
        /// </value>
        public object Value { get; }

        /// <summary>
        /// Gets the type of the logging parameter.
        /// </summary>
        /// <value>
        /// The type of the logging parameter.
        /// </value>
        public LoggingParameterType LoggingParameterType => LoggingParameterType.Data;
    }
}