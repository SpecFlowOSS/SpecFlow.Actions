using System;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;
using Xunit;

namespace SpecFlow.Actions.Selenium.Tests
{
    public class BrowserDriverTests
    {
        class MockSeleniumConfiguration : ISeleniumConfiguration
        {
            public Browser Browser { get; set; }

            public string[]? Arguments { get; set; }
        }

        class MockSpecFlowOutputHelper : ISpecFlowOutputHelper
        {
            public void WriteLine(string message)
            {
                
            }

            public void WriteLine(string format, params object[] args)
            {
                
            }

            public void AddAttachment(string filePath)
            {
                
            }
        }

        public class BrowserDriverAccessor : BrowserDriver
        {
            public Lazy<IWebDriver> CurrentWebDriverLazy => _currentWebDriverLazy;

            public BrowserDriverAccessor(ISeleniumConfiguration seleniumConfiguration, ISpecFlowOutputHelper specFlowOutputHelper) : base(seleniumConfiguration, specFlowOutputHelper)
            {
            }
        }

        [Fact]
        public void Current_NotInstantiatedAfterCreation()
        {
            var target = new BrowserDriverAccessor(new MockSeleniumConfiguration(), new MockSpecFlowOutputHelper());

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeFalse();
        }

        [Fact]
        public void Current_AfterAccessing_Instantiated()
        {
            var target = new BrowserDriverAccessor(new MockSeleniumConfiguration(){Browser = Browser.Noop}, new MockSpecFlowOutputHelper());

            var webdriver = target.Current;

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeTrue();
            webdriver.Should().NotBeNull();
        }
    }
}