namespace Vima.LoggingAbstractor.Core
{
    /// <summary>
    /// Represents an instance of settings.
    /// </summary>
    public class AbstractLoggerSettings
    {
        /// <summary>
        /// Gets or sets the minimal logging lever required for a log to go through.
        /// </summary>
        /// <value>
        /// The minimal logging lever required for a log to go through.
        /// </value>
        public LoggingLevel MinimalLoggingLevel { get; set; }
    }
}