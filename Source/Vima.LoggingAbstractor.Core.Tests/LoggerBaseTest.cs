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
                var minimumLoggingSeverityLevels = Enum.GetValues(typeof(LoggingSeverityLevel)).Cast<LoggingSeverityLevel>().ToList();
                var currentLoggingSeverityLevels = Enum.GetValues(typeof(LoggingSeverityLevel)).Cast<LoggingSeverityLevel>().ToList();

                foreach (var minimumLoggingSeverityLevel in minimumLoggingSeverityLevels)
                {
                    foreach (var currentLoggingSeverityLevel in currentLoggingSeverityLevels)
                    {
                        // Arrange
                        TestLogger logger = new TestLogger(minimumLoggingSeverityLevel);
                        var expectedResult = ShouldClientLogTrace(minimumLoggingSeverityLevel, currentLoggingSeverityLevel);

                        // Act
                        var result = logger.ShouldBeTraced(currentLoggingSeverityLevel);

                        // Assert
                        result.Should().Be(expectedResult, $"current logging level is '{currentLoggingSeverityLevel:G}' and minimal logging level is '{minimumLoggingSeverityLevel.ToString()}'");
                    }
                }
            }

            private static bool ShouldClientLogTrace(LoggingSeverityLevel currentLoggingSeverityLevel, LoggingSeverityLevel minimumLoggingSeverityLevel)
            {
                Dictionary<LoggingSeverityLevel, List<LoggingSeverityLevel>> allowedLoggingLevelsForMinimumLoggingLevel =
                    new Dictionary<LoggingSeverityLevel, List<LoggingSeverityLevel>>
                    {
                    { LoggingSeverityLevel.Verbose, new List<LoggingSeverityLevel> { LoggingSeverityLevel.Verbose } },
                    { LoggingSeverityLevel.Information, new List<LoggingSeverityLevel> { LoggingSeverityLevel.Verbose, LoggingSeverityLevel.Information } },
                    { LoggingSeverityLevel.Warning, new List<LoggingSeverityLevel> { LoggingSeverityLevel.Verbose, LoggingSeverityLevel.Information, LoggingSeverityLevel.Warning } },
                    { LoggingSeverityLevel.Error, new List<LoggingSeverityLevel> { LoggingSeverityLevel.Verbose, LoggingSeverityLevel.Information, LoggingSeverityLevel.Warning, LoggingSeverityLevel.Error } },
                    { LoggingSeverityLevel.Critical, new List<LoggingSeverityLevel> { LoggingSeverityLevel.Verbose, LoggingSeverityLevel.Information, LoggingSeverityLevel.Warning, LoggingSeverityLevel.Error, LoggingSeverityLevel.Critical } },
                    { LoggingSeverityLevel.None, new List<LoggingSeverityLevel>() }
                    };

                return allowedLoggingLevelsForMinimumLoggingLevel[minimumLoggingSeverityLevel].Contains(currentLoggingSeverityLevel);
            }
        }
    }
}
