using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vima.LoggingAbstractor.Core;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Console
{
    /// <summary>
    /// Represents an instance of a logger that logs everything to a console.
    /// </summary>
    /// <seealso cref="IConsoleAbstractLogger" />
    public class ConsoleAbstractLogger : AbstractLoggerBase, IConsoleAbstractLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleAbstractLogger"/> class.
        /// </summary>
        /// <param name="minimalLoggingLevel">The minimal logging level.</param>
        public ConsoleAbstractLogger(LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(new AbstractLoggerSettings { MinimalLoggingLevel = minimalLoggingLevel })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleAbstractLogger"/> class.
        /// </summary>
        /// <param name="settings">The logger's settings.</param>
        public ConsoleAbstractLogger(AbstractLoggerSettings settings)
            : base(settings ?? throw new ArgumentNullException(nameof(settings)))
        {
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

            string date = DateTime.UtcNow.ToString("s");
            System.Console.WriteLine($"Date: {date}, LoggingLevel: {loggingLevel:G} Message: {message}");
            return Task.CompletedTask;
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

            string date = DateTime.UtcNow.ToString("s");
            System.Console.WriteLine($"Date: {date}, LoggingLevel: {loggingLevel:G} Exception: {exception.Message}\n{exception.StackTrace}");
            return Task.CompletedTask;
        }
    }
}