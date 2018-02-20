using System;
using System.Collections.Generic;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core.Tests
{
    public class TestLogger : LoggerBase
    {
        public TestLogger(LoggingSeverityLevel minimalLoggingLevel = LoggingSeverityLevel.Verbose)
            : base(minimalLoggingLevel)
        {
        }

        public override void TraceMessage(string message, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
        }

        public override void TraceException(Exception exception, LoggingSeverityLevel loggingSeverityLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
        }

        public new bool ShouldBeTraced(LoggingSeverityLevel loggingSeverityLevel)
        {
            return base.ShouldBeTraced(loggingSeverityLevel);
        }
    }
}
