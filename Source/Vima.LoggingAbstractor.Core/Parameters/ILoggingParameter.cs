namespace Vima.LoggingAbstractor.Core.Parameters
{
    /// <summary>
    /// Represents logging parameter.
    /// </summary>
    /// <typeparam name="T">Parameter's value type.</typeparam>
    public interface ILoggingParameter<out T> : ILoggingParameter
    {
        /// <summary>
        /// Gets the parameter's value.
        /// </summary>
        /// <value>
        /// The parameter's value.
        /// </value>
        T Value { get; }
    }

    /// <summary>
    /// Represents logging parameter.
    /// </summary>
    public interface ILoggingParameter
    {
        /// <summary>
        /// Gets the type of the logging parameter.
        /// </summary>
        /// <value>
        /// The type of the logging parameter.
        /// </value>
        LoggingParameterType LoggingParameterType { get; }
    }
}