using System;

namespace Vima.LoggingAbstractor.Core.Parameters
{
    /// <summary>
    /// Represents logging identity parameter.
    /// </summary>
    public class LoggingIdentityParameter : ILoggingParameter<IdentityParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingIdentityParameter"/> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="name">The name.</param>
        public LoggingIdentityParameter(string identity, string name = null)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            Value = new IdentityParameter(identity, name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingIdentityParameter"/> class.
        /// </summary>
        /// <param name="identityParameter">The identity parameter.</param>
        public LoggingIdentityParameter(IdentityParameter identityParameter)
        {
            Value = identityParameter ?? throw new ArgumentNullException(nameof(identityParameter));
        }

        /// <summary>
        /// Gets the parameter's value.
        /// </summary>
        /// <value>
        /// The parameter's value.
        /// </value>
        public IdentityParameter Value { get; }

        /// <summary>
        /// Gets the type of the logging parameter.
        /// </summary>
        /// <value>
        /// The type of the logging parameter.
        /// </value>
        public LoggingParameterType LoggingParameterType => LoggingParameterType.Identity;
    }
}