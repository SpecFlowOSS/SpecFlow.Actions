using System;
using FluentAssertions;
using Xunit;

namespace SpecFlow.Actions.Selenium.Tests
{
    public class SeleniumConfigurationTests
    {
        class MockSpecFlowJsonLoader : ISpecFlowJsonLoader
        {
            private readonly string _content;

            public MockSpecFlowJsonLoader(string content)
            {
                _content = content;
            }

            public string Load()
            {
                return _content;
            }
        }

        [Fact]
        public void Browser_NoSpecFlowJsonContent_ReturnsNone()
        {
            var specflowJsonContent = String.Empty;

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Browser.Should().Be(Browser.None);
        }

        [Fact]
        public void Browser_EmptyJson_ReturnsNone()
        {
            var specflowJsonContent = "{}";

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Browser.Should().Be(Browser.None);
        }

        [Fact]
        public void Browser_SeleniumNodeExists_NoBrowserSet_ReturnsNone()
        {
            var specflowJsonContent = @"{ ""selenium"": {} }";

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Browser.Should().Be(Browser.None);
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_ReturnsValue()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"" } }";

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Browser.Should().Be(Browser.Chrome);
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsNotSet_ArgumentsReturnsNull()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"" } }";

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Arguments.Should().BeNull();
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsNotSet_ArgumentsReturnsEmpty()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"", ""arguments"": [] } }";

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Arguments.Should().BeEmpty();
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsSet_ReturnsValue()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"", ""arguments"": [ ""--argument-one"", ""--argument-two"" ] } }";

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Arguments.Should().Equal(new[] { "--argument-one", "--argument-two" });
        }
    }
}