using System;
using System.Collections.Generic;

namespace Vima.LoggingAbstractor.Core.Parameters
{
    /// <summary>
    /// Represents logging tags parameter.
    /// </summary>
    public class LoggingTagsParameter : ILoggingAdditionalParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingTagsParameter"/> class.
        /// </summary>
        /// <param name="tags">The tags.</param>
        public LoggingTagsParameter(IEnumerable<string> tags)
        {
            Tags = tags ?? throw new ArgumentNullException(nameof(tags));
        }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public IEnumerable<string> Tags { get; }

        /// <summary>
        /// Gets the type of the logging parameter type.
        /// </summary>
        /// <value>
        /// The type of the logging parameter type.
        /// </value>
        internal LoggingParameterType LoggingParameterType => LoggingParameterType.Tags;
    }
}