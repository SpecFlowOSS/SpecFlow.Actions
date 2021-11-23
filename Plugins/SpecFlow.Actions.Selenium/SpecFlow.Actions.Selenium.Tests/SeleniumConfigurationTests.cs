using FluentAssertions;
using Moq;
using SpecFlow.Actions.Configuration;
using SpecFlow.Actions.Selenium.Configuration;
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
        public void Browser_NoSpecFlowJsonContent_ReturnsNone()
        {
            var specflowJsonContent = string.Empty;

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Browser.Should().Be(Browser.None);
        }

        [Fact]
        public void Browser_EmptyJson_ReturnsNone()
        {
            var specflowJsonContent = "{}";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Browser.Should().Be(Browser.None);
        }

        [Fact]
        public void Browser_SeleniumNodeExists_NoBrowserSet_ReturnsNone()
        {
            var specflowJsonContent = @"{ ""selenium"": {} }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Browser.Should().Be(Browser.None);
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_ReturnsValue()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"" } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Browser.Should().Be(Browser.Chrome);
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_NoMatchingEnum_ReturnsException()
        {
            Browser GetBrowser(SeleniumConfiguration seleniumConfiguration)
            {
                return seleniumConfiguration.Browser;
            }

            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""NoMatchingBrowser"" } }";

            Action action = () => GetBrowser(GetSeleniumConfiguration(specflowJsonContent));

            action.Should().Throw<JsonException>();
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_LowerCase_ReturnsValue()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""chrome"" } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Browser.Should().Be(Browser.Chrome);
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsNotSet_ArgumentsReturnsNull()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"" } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Arguments.Should().BeNull();
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsNotSet_ArgumentsReturnsEmpty()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"", ""arguments"": [] } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Arguments.Should().BeEmpty();
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsSet_ReturnsValue()
        {
            var specflowJsonContent =
                @"{ ""selenium"": { ""browser"":""Chrome"", ""arguments"": [ ""--argument-one"", ""--argument-two"" ] } }";

            var seleniumConfiguration = GetSeleniumConfiguration(specflowJsonContent);

            seleniumConfiguration.Arguments.Should().Equal("--argument-one", "--argument-two");
        }
    }
}