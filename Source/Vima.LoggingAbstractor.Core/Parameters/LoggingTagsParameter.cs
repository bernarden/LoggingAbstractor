using System;
using System.Collections.Generic;
using System.Linq;

namespace Vima.LoggingAbstractor.Core.Parameters
{
    /// <summary>
    /// Represents logging tags parameter.
    /// </summary>
    public class LoggingTagsParameter : ILoggingParameter<IEnumerable<string>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingTagsParameter"/> class.
        /// </summary>
        /// <param name="tags">The tags.</param>
        public LoggingTagsParameter(IEnumerable<string> tags)
        {
            Value = tags ?? throw new ArgumentNullException(nameof(tags));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingTagsParameter"/> class.
        /// </summary>
        /// <param name="tags">The tags.</param>
        public LoggingTagsParameter(IEnumerable<Enum> tags)
        {
            IEnumerable<Enum> enumTags = tags ?? throw new ArgumentNullException(nameof(tags));
            Value = enumTags.Select(x => x.ToString("G"));
        }

        /// <summary>
        /// Gets the parameter's value.
        /// </summary>
        /// <value>
        /// The parameter's value.
        /// </value>
        public IEnumerable<string> Value { get; }

        /// <summary>
        /// Gets the type of the logging parameter.
        /// </summary>
        /// <value>
        /// The type of the logging parameter.
        /// </value>
        public LoggingParameterType LoggingParameterType => LoggingParameterType.Tags;
    }
}