using System;
using System.Collections.Generic;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core.Tests
{
    public class TestAbstractLoggerBase : AbstractLoggerBase
    {
        public TestAbstractLoggerBase(LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(minimalLoggingLevel)
        {
        }

        public override void TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
        }

        public override void TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
        }

        public new bool ShouldBeTraced(LoggingLevel loggingLevel)
        {
            return base.ShouldBeTraced(loggingLevel);
        }
    }
}
