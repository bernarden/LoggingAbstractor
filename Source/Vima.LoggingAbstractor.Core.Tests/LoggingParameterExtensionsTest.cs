using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
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
                // Act
                var tags = new List<ILoggingParameter>().ExtractTags().ToList();

                // Assert
                tags.Should().BeEmpty();
            }

            [Fact]
            public void ShouldHandleNoTagsParameters()
            {
                // Arrange
                var loggingParameters = new List<ILoggingParameter> { new LoggingDataParameter("Data") };

                // Act
                var tags = loggingParameters.ExtractTags().ToList();

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
                var loggingParameters = new List<ILoggingParameter> { loggingTagsParameter1, loggingTagsParameter2 };

                // Act
                var tags = loggingParameters.ExtractTags().ToList();

                // Assert
                tags.Count.Should().Be(2);
                tags.Should().Contain(LoggingLevel.Critical.ToString("G"));
                tags.Should().Contain(LoggingLevel.None.ToString("G"));
            }
        }

        public sealed class ExtractData
        {
            [Fact]
            public void ShouldHandleNoParameters()
            {
                // Act
                var data = new List<ILoggingParameter>().ExtractData().ToList();

                // Assert
                data.Should().BeEmpty();
            }

            [Fact]
            public void ShouldHandleNoDataParameters()
            {
                // Arrange
                var loggingParameters = new[] { new LoggingTagsParameter(new List<string> { "CustomTag" }) };

                // Act
                var data = loggingParameters.ExtractData().ToList();

                // Assert
                data.Should().BeEmpty();
            }

            [Fact]
            public void ShouldHandleOneDataParameter()
            {
                // Arrange
                var dataValue = "Data";
                var loggingParameters = new List<ILoggingParameter> { new LoggingDataParameter(dataValue) };

                // Act
                var data = loggingParameters.ExtractData().ToList();

                // Assert
                data.Count.Should().Be(1);
                data.Should().Contain(JsonConvert.SerializeObject(dataValue));
            }

            [Fact]
            public void ShouldHandleMultipleDataParameter()
            {
                // Arrange
                var dataValue1 = "Data 1";
                var dataValue2 = "Data 2";
                var loggingParameters = new List<ILoggingParameter> { new LoggingDataParameter(dataValue1), new LoggingDataParameter(dataValue2) };

                // Act
                var data = loggingParameters.ExtractData().ToList();

                // Assert
                data.Count.Should().Be(2);
                data.Should().Contain(JsonConvert.SerializeObject(dataValue1));
                data.Should().Contain(JsonConvert.SerializeObject(dataValue2));
            }
        }

        public sealed class ExtractIdentity
        {
            [Fact]
            public void ShouldHandleNoParameters()
            {
                // Act
                var identity = new List<ILoggingParameter>().ExtractIdentity();

                // Assert
                identity.Should().BeNull();
            }

            [Fact]
            public void ShouldHandleNoIdentityParameters()
            {
                // Arrange
                var loggingParameters = new[] { new LoggingTagsParameter(new List<string> { "CustomTag" }) };

                // Act
                var identity = loggingParameters.ExtractIdentity();

                // Assert
                identity.Should().BeNull();
            }

            [Fact]
            public void ShouldHandleIdentityParameter()
            {
                // Arrange
                var identityValue = "Id";
                var identityName = "Name";
                var loggingParameters = new List<ILoggingParameter> { new LoggingIdentityParameter(identityValue, identityName) };

                // Act
                var identity = loggingParameters.ExtractIdentity();

                // Assert
                identity.Should().NotBeNull();
                identity.Identity.Should().Be(identityValue);
                identity.Name.Should().Be(identityName);
            }

            [Fact]
            public void ShouldHandleIdentityParameterWithoutName()
            {
                // Arrange
                var identityValue = "Id";
                var loggingParameters = new List<ILoggingParameter> { new LoggingIdentityParameter(identityValue) };

                // Act
                var identity = loggingParameters.ExtractIdentity();

                // Assert
                identity.Should().NotBeNull();
                identity.Identity.Should().Be(identityValue);
                identity.Name.Should().BeNull();
            }

            [Fact]
            public void ShouldHandleMultipleIdentityParameter()
            {
                // Arrange
                var identityValue = "Id";
                var identityName = "Name";
                var loggingParameters = new List<ILoggingParameter>
                {
                    new LoggingIdentityParameter(identityValue, identityName),
                    new LoggingIdentityParameter($"{identityValue}-2", $"{identityName}-2")
                };

                // Act
                var identity = loggingParameters.ExtractIdentity();

                // Assert
                identity.Should().NotBeNull();
                identity.Identity.Should().Be(identityValue);
                identity.Name.Should().Be(identityName);
            }
        }
    }
}