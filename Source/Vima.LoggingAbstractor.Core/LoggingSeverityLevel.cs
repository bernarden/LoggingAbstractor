using System;

namespace Vima.LoggingAbstractor.Core
{
    /// <summary>
    /// Represents the severity of the logged message or exception.
    /// </summary>
    [Flags]
    public enum LoggingSeverityLevel
    {
        /// <summary>
        /// Verbose.
        /// </summary>
        Verbose = 1,

        /// <summary>
        /// Information.
        /// </summary>
        Information = 2,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning = 4,

        /// <summary>
        /// Error.
        /// </summary>
        Error = 8,

        /// <summary>
        /// Critical.
        /// </summary>
        Critical = 16,

        /// <summary>
        /// None.
        /// </summary>
        None = 32
    }
}