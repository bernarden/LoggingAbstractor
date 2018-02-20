﻿using System;
using System.Collections.Generic;
using SharpRaven;
using SharpRaven.Data;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Sentry
{
    /// <summary>
    /// Represents an instance of a Sentry logger.
    /// </summary>
    public class SentryLogger : LoggerBase
    {
        private readonly RavenClient _ravenClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SentryLogger"/> class.
        /// </summary>
        /// <param name="ravenClient">The raven client.</param>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        public SentryLogger(RavenClient ravenClient, LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(minimalLoggingLevel)
        {
            _ravenClient = ravenClient ?? throw new ArgumentNullException(nameof(ravenClient));
        }

        /// <summary>
        /// Traces the message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public override void TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
            if (!ShouldBeTraced(loggingLevel))
            {
                return;
            }

            _ravenClient.Capture(new SentryEvent(message));
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <param name="parameters">The additional parameters.</param>
        public override void TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
            if (!ShouldBeTraced(loggingLevel))
            {
                return;
            }

            _ravenClient.Capture(new SentryEvent(exception));
        }
    }
}
