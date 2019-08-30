using System;
using System.Collections.Generic;
using System.Linq;
using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.AspNetCore;
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
        /// <param name="raygunClient">The Raygun client.</param>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        public RaygunAbstractLogger(RaygunClient raygunClient, LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(new AbstractLoggerSettings { MinimalLoggingLevel = minimalLoggingLevel })
        {
            _raygunClient = raygunClient ?? throw new ArgumentNullException(nameof(raygunClient));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RaygunAbstractLogger"/> class.
        /// </summary>
        /// <param name="raygunClient">The Raygun client.</param>
        /// <param name="settings">The logger's settings.</param>
        public RaygunAbstractLogger(RaygunClient raygunClient, AbstractLoggerSettings settings)
            : base(settings ?? throw new ArgumentNullException(nameof(settings)))
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

            var loggingParameters = GetGlobalAndLocalLoggingParameters(parameters);
            SetIdentityParameters(loggingParameters);
            var messageException = new RaygunMessageException(message);
            var data = ExtractDataValues(loggingParameters);
            var tags = ExtractTags(loggingParameters, loggingLevel);
            _raygunClient.Send(messageException, tags, data);
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

            var loggingParameters = GetGlobalAndLocalLoggingParameters(parameters);
            SetIdentityParameters(loggingParameters);
            var data = ExtractDataValues(loggingParameters);
            var tags = ExtractTags(loggingParameters, loggingLevel);
            _raygunClient.Send(exception, tags, data);
        }

        private static List<string> ExtractTags(IEnumerable<ILoggingParameter> loggingParameters, LoggingLevel loggingLevel)
        {
            var parameters = loggingParameters.ToList();
            var extractTags = parameters.ExtractTags().ToList();

            extractTags.Add(loggingLevel.ToString("G"));

            var environment = parameters.ExtractEnvironment();
            if (!string.IsNullOrEmpty(environment))
            {
                extractTags.Add(environment);
            }

            return extractTags;
        }

        private static Dictionary<string, string> ExtractDataValues(IEnumerable<ILoggingParameter> parameters)
        {
            var dataCount = 1;
            var dataDictionary = new Dictionary<string, string>();
            foreach (string data in parameters.ExtractData())
            {
                dataDictionary.Add($"Data #{dataCount++}", data);
            }

            return dataDictionary;
        }

        private void SetIdentityParameters(IEnumerable<ILoggingParameter> loggingParameters)
        {
            var identity = loggingParameters.ExtractIdentity();
            if (identity == null || string.IsNullOrEmpty(identity.Identity))
            {
                return;
            }

            _raygunClient.UserInfo = new RaygunIdentifierMessage(identity.Identity)
            {
                FullName = identity.Name
            };
        }
    }
}