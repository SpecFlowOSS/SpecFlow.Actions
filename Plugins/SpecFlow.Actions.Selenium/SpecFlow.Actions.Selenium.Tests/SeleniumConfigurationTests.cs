using FluentAssertions;
using Moq;
using SpecFlow.Actions.Configuration;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Enums;
using System;
using System.Text.Json;
using Xunit;

namespace SpecFlow.Actions.Selenium.Tests
{
    public class SeleniumConfigurationTests
    {
        private static SeleniumConfiguration GetSeleniumConfiguration(string specflowJsonContent)
        {
            var specflowActionJsonLoader = new Mock<ISpecFlowActionJsonLoader>();
            specflowActionJsonLoader.Setup(m => m.Load()).Returns(specflowJsonContent);

            return new SeleniumConfiguration(specflowActionJsonLoader.Object);
        }

        [Fact]
        public void Targets_NoSpecFlowJsonContent_ReturnsEmpty()
        {
            var specflowJsonContent = string.Empty;

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Targets.Should().BeEmpty();
        }

        [Fact]
        public void Targets_EmptyJson_ReturnsEmpty()
        {
            var specflowJsonContent = "{}";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Targets.Should().BeEmpty();
        }

        [Fact]
        public void Targets_SeleniumNodeExists_NoTargetsSet_ReturnsEmpty()
        {
            var specflowJsonContent = @"{ ""selenium"": {} }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Targets.Should().BeEmpty();
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_ReturnsValue()
        {
            var specflowJsonContent = @"{ ""Selenium"": { ""Targets"": [ { ""Browser"":""Chrome"" } ] } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Targets[0].Browser.Should().Be(Browser.Chrome);
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_NoMatchingEnum_ReturnsException()
        {
            Browser GetBrowser(SeleniumConfiguration seleniumConfiguration)
            {
                return seleniumConfiguration.Targets[0].Browser;
            }

            var specflowJsonContent = @"{ ""selenium"": { ""targets"": [ { ""browser"":""NoMatchingBrowser"" } ] } }";

            Action action = () => GetBrowser(GetSeleniumConfiguration(specflowJsonContent));

            action.Should().Throw<JsonException>();
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_LowerCase_ReturnsValue()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""targets"": [ { ""browser"":""Chrome"" } ] } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Targets[0].Browser.Should().Be(Browser.Chrome);
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsNotSet_ArgumentsReturnsNull()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""targets"": [ { ""browser"":""Chrome"" } ] } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Targets[0].Arguments.Should().BeNull();
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsNotSet_ArgumentsReturnsEmpty()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""targets"": [ { ""browser"":""Chrome"", ""arguments"": [] } ] } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Targets[0].Arguments.Should().BeEmpty();
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsSet_ReturnsValue()
        {
            var specflowJsonContent =
                @"{ ""selenium"": { ""targets"": [ { ""browser"":""Chrome"", ""arguments"": [ ""--argument-one"", ""--argument-two"" ] } ] } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Targets[0].Arguments.Should().Equal("--argument-one", "--argument-two");
        }
    }
}