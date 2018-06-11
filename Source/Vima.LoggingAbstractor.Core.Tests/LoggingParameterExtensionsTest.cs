using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Vima.LoggingAbstractor.Core.Extensions;
using Vima.LoggingAbstractor.Core.Parameters;
using Xunit;

namespace Vima.LoggingAbstractor.Core.Tests
{
    public sealed class LoggingParameterExtensionsTest
    {
        public sealed class ExtractTags
        {
            [Fact]
            public void ShouldHandleNoParameters()
            {
                // Arrange
                var loggingParameters = new List<ILoggingParameter> { new LoggingDataParameter(string.Empty) };

                // Act
                var tags = loggingParameters.ExtractTags().ToList();

                // Assert
                tags.Should().BeEmpty();
            }

            [Fact]
            public void ShouldHandleNoTagsParameters()
            {
                // Act
                var tags = new List<ILoggingParameter>().ExtractTags().ToList();

                // Assert
                tags.Should().BeEmpty();
            }

            [Fact]
            public void ShouldHandleOneTagsParameter()
            {
                // Arrange
                string tag = "CustomTag";
                var loggingTagsParameter = new LoggingTagsParameter(new List<string> { tag });

                // Act
                var tags = new List<ILoggingParameter> { loggingTagsParameter }.ExtractTags().ToList();

                // Assert
                tags.Count.Should().Be(1);
                tags.Should().Contain(tag);
            }

            [Fact]
            public void ShouldHandleMultipleTagsParameter()
            {
                // Arrange
                var loggingTagsParameter1 = new LoggingTagsParameter(new List<Enum> { LoggingLevel.Critical });
                var loggingTagsParameter2 = new LoggingTagsParameter(new List<Enum> { LoggingLevel.None });

                // Act
                var tags = new List<ILoggingParameter> { loggingTagsParameter1, loggingTagsParameter2 }.ExtractTags().ToList();

                // Assert
                tags.Count.Should().Be(2);
                tags.Should().Contain(LoggingLevel.Critical.ToString("G"));
                tags.Should().Contain(LoggingLevel.None.ToString("G"));
            }
        }
    }
}