using System;
using Sentry;
using Vima.LoggingAbstractor.Core;
using Xunit;

namespace Vima.LoggingAbstractor.Sentry.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // var client = new SentryClient(new SentryOptions { Dsn = new Dsn(""), BeforeSend = BeforeSend });

            // client.CaptureEvent(new SentryEvent() { Message = "Hi" });
            // var logger = new SentryAbstractLogger(client);

            // logger.TraceException(new AccessViolationException());
            // logger.TraceMessage("Test4");

            // logger.TraceMessage("Hello2");
            // logger.TraceMessage("Test3");
            // client.Dispose();
        }

        // private SentryEvent BeforeSend(SentryEvent arg)
        // {
        //    return arg;
        // }
    }
}