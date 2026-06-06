using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly RaygunClientBase _raygunClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaygunAbstractLogger"/> class.
        /// </summary>
        /// <param name="raygunClient">The Raygun client.</param>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        public RaygunAbstractLogger(RaygunClientBase raygunClient, LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(new AbstractLoggerSettings { MinimalLoggingLevel = minimalLoggingLevel })
        {
            _raygunClient = raygunClient ?? throw new ArgumentNullException(nameof(raygunClient));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RaygunAbstractLogger"/> class.
        /// </summary>
        /// <param name="raygunClient">The Raygun client.</param>
        /// <param name="settings">The logger's settings.</param>
        public RaygunAbstractLogger(RaygunClientBase raygunClient, AbstractLoggerSettings settings)
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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            if (!ShouldBeTraced(loggingLevel))
            {
                return Task.CompletedTask;
            }

            var loggingParameters = GetGlobalAndLocalLoggingParameters(parameters);
            var messageException = new RaygunMessageException(message);
            var tags = ExtractTags(loggingParameters, loggingLevel);
            var data = ExtractDataValues(loggingParameters);
            var userInfo = GetIdentityParameters(loggingParameters);
            return _raygunClient.SendAsync(messageException, tags, data, userInfo);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The logging parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            if (!ShouldBeTraced(loggingLevel))
            {
                return Task.CompletedTask;
            }

            var loggingParameters = GetGlobalAndLocalLoggingParameters(parameters);
            var tags = ExtractTags(loggingParameters, loggingLevel);
            var data = ExtractDataValues(loggingParameters);
            var userInfo = GetIdentityParameters(loggingParameters);
            return _raygunClient.SendAsync(exception, tags, data, userInfo);
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

        private static RaygunIdentifierMessage GetIdentityParameters(IEnumerable<ILoggingParameter> loggingParameters)
        {
            var identity = loggingParameters.ExtractIdentity();
            if (identity == null || string.IsNullOrEmpty(identity.Identity))
            {
                return null;
            }

            return new RaygunIdentifierMessage(identity.Identity)
            {
                FullName = identity.Name
            };
        }
    }
}