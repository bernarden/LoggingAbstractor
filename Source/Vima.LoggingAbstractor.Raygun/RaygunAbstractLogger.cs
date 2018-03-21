using System;
using System.Collections.Generic;
using System.Linq;
using Mindscape.Raygun4Net;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Extensions;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Raygun
{
    /// <summary>
    /// Represents an instance of a Raygun logger.
    /// </summary>
    /// <seealso cref="AbstractLoggerBase" />
    /// <seealso cref="IRaygunAbstractLogger" />
    public class RaygunAbstractLogger : AbstractLoggerBase, IRaygunAbstractLogger
    {
        private readonly RaygunClient _raygunClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaygunAbstractLogger"/> class.
        /// </summary>
        /// <param name="raygunClient">The raygun client.</param>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        public RaygunAbstractLogger(RaygunClient raygunClient, LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(minimalLoggingLevel)
        {
            _raygunClient = raygunClient ?? throw new ArgumentNullException(nameof(raygunClient));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        public override void TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            if (!ShouldBeTraced(loggingLevel))
            {
                return;
            }

            IEnumerable<ILoggingParameter> loggingParameters = parameters.ToList();
            var messageException = new RaygunMessageException(message);
            var data = ExtractDataValues(loggingParameters);
            _raygunClient.Send(messageException, loggingParameters.ExtractTags().ToList(), data);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        public override void TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            if (!ShouldBeTraced(loggingLevel))
            {
                return;
            }

            IEnumerable<ILoggingParameter> loggingParameters = parameters.ToList();
            var data = ExtractDataValues(loggingParameters);
            _raygunClient.Send(exception, loggingParameters.ExtractTags().ToList(), data);
        }

        private Dictionary<string, string> ExtractDataValues(IEnumerable<ILoggingParameter> parameters)
        {
            var dataCount = 0;
            var dataDictionary = new Dictionary<string, string>();
            foreach (string data in parameters.ExtractData())
            {
                dataDictionary.Add($"Data #{dataCount++}", data);
            }

            return dataDictionary;
        }
    }
}
