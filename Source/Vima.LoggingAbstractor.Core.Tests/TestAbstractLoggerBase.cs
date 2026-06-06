using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vima.LoggingAbstractor.Core.Parameters;

namespace Vima.LoggingAbstractor.Core.Tests
{
    public class TestAbstractLoggerBase : AbstractLoggerBase
    {
        public TestAbstractLoggerBase(LoggingLevel minimalLoggingLevel = LoggingLevel.Verbose)
            : base(new AbstractLoggerSettings { MinimalLoggingLevel = minimalLoggingLevel })
        {
        }

        public TestAbstractLoggerBase(AbstractLoggerSettings settings)
            : base(settings)
        {
        }

        public override Task TraceMessage(string message, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            return Task.CompletedTask;
        }

        public override Task TraceException(Exception exception, LoggingLevel loggingLevel, IEnumerable<ILoggingParameter> parameters)
        {
            return Task.CompletedTask;
        }

        public new bool ShouldBeTraced(LoggingLevel loggingLevel)
        {
            return base.ShouldBeTraced(loggingLevel);
        }
    }
}
