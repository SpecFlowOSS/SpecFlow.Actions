using System;
using FluentAssertions;
using OpenQA.Selenium;
using Xunit;

namespace SpecFlow.Actions.Selenium.Tests
{
    public class BrowserDriverTests
    {
        class MockSeleniumConfiguration : ISeleniumConfiguration
        {

            public string Browser { get; set; }
        }


        public class BrowserDriverAccessor : BrowserDriver
        {
            public Lazy<IWebDriver> CurrentWebDriverLazy => _currentWebDriverLazy;

            public BrowserDriverAccessor(ISeleniumConfiguration seleniumConfiguration) : base(seleniumConfiguration)
            {
            }
        }

        [Fact]
        public void Current_NotInstantiatedAfterCreation()
        {
            var target = new BrowserDriverAccessor(new MockSeleniumConfiguration());

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeFalse();
        }


        [Fact]
        public void Current_AfterAccessing_Instantiated()
        {
            var target = new BrowserDriverAccessor(new MockSeleniumConfiguration(){Browser = "noop"});

            var webdriver = target.Current;

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeTrue();
            webdriver.Should().NotBeNull();
        }
    }
}