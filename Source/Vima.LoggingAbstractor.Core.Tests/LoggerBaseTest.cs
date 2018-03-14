using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Vima.LoggingAbstractor.Core.Tests
{
    public sealed class LoggerBaseTest
    {
        public sealed class ShouldBeTraced
        {
            [Fact]
            public void ShouldReturnCorrectValueInAllCombinationsOfInputs()
            {
                var loggingLevels = Enum.GetValues(typeof(LoggingLevel)).Cast<LoggingLevel>().ToList();

                foreach (var minimalLoggingLevel in loggingLevels)
                {
                    foreach (var currentLoggingLevel in loggingLevels)
                    {
                        // Arrange
                        TestLoggerBase loggerBase = new TestLoggerBase(minimalLoggingLevel);
                        var expectedResult = ShouldClientLogTrace(minimalLoggingLevel, currentLoggingLevel);

                        // Act
                        var result = loggerBase.ShouldBeTraced(currentLoggingLevel);

                        // Assert
                        result.Should().Be(expectedResult, $"current logging level is '{currentLoggingLevel:G}' and minimal logging level is '{minimalLoggingLevel.ToString()}'");
                    }
                }
            }

            private static bool ShouldClientLogTrace(LoggingLevel currentLoggingLevel, LoggingLevel minimumLoggingLevel)
            {
                Dictionary<LoggingLevel, List<LoggingLevel>> allowedLoggingLevelsForMinimumLoggingLevel =
                    new Dictionary<LoggingLevel, List<LoggingLevel>>
                    {
                    { LoggingLevel.Verbose, new List<LoggingLevel> { LoggingLevel.Verbose } },
                    { LoggingLevel.Information, new List<LoggingLevel> { LoggingLevel.Verbose, LoggingLevel.Information } },
                    { LoggingLevel.Warning, new List<LoggingLevel> { LoggingLevel.Verbose, LoggingLevel.Information, LoggingLevel.Warning } },
                    { LoggingLevel.Error, new List<LoggingLevel> { LoggingLevel.Verbose, LoggingLevel.Information, LoggingLevel.Warning, LoggingLevel.Error } },
                    { LoggingLevel.Critical, new List<LoggingLevel> { LoggingLevel.Verbose, LoggingLevel.Information, LoggingLevel.Warning, LoggingLevel.Error, LoggingLevel.Critical } },
                    { LoggingLevel.None, new List<LoggingLevel>() }
                    };

                return allowedLoggingLevelsForMinimumLoggingLevel[minimumLoggingLevel].Contains(currentLoggingLevel);
            }
        }
    }
}
