using System;
using System.Collections.Generic;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core.Tests
{
    public class TestLogger : LoggerBase
    {
        public TestLogger(LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(minimalLoggingLevel)
        {
        }

        public override void TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
        }

        public override void TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingAdditionalParameter> parameters)
        {
        }

        public new bool ShouldBeTraced(LoggingLevel loggingLevel)
        {
            return base.ShouldBeTraced(loggingLevel);
        }
    }
}
