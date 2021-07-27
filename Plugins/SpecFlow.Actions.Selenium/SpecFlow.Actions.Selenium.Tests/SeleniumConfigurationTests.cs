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
        public void Browser_NoSpecFlowJsonContent_ReturnsNull()
        {
            var specflowJsonContent = String.Empty;

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Browser.Should().BeNull();
        }

        [Fact]
        public void Browser_EmptyJson_ReturnsNull()
        {
            var specflowJsonContent = "{}";

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Browser.Should().BeNull();
        }

        [Fact]
        public void Browser_SeleniumNodeExists_NoBrowserSet_ReturnsNull()
        {
            var specflowJsonContent = @"{ ""selenium"": {} }";

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Browser.Should().BeNull();
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_ReturnsValue()
        {
            var specflowJsonContent = @"{ ""selenium"": { ""browser"":""Some Browser"" } }";

            var seleniumConfiguration = new SeleniumConfiguration(new MockSpecFlowJsonLoader(specflowJsonContent));

            seleniumConfiguration.Browser.Should().Be("Some Browser");
        }
    }
}