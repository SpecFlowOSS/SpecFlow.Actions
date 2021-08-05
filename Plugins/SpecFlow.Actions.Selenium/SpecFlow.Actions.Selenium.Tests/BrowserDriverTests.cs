using System;
using System.Collections.Generic;
using FluentAssertions;
using OpenQA.Selenium;
using Xunit;

namespace SpecFlow.Actions.Selenium.Tests
{
    public class BrowserDriverTests
    {
        class MockSeleniumConfiguration : ISeleniumConfiguration
        {
            public Browser Browser { get; set; }

            public string[]? Arguments { get; set; }

            public Dictionary<string, string>? Capabilities { get; set; }

            double? ISeleniumConfiguration.DefaultTimeout => throw new NotImplementedException();

            double? ISeleniumConfiguration.PollingInterval => throw new NotImplementedException();
        }

        class MockDriverInitialiserResolver : IDriverInitialiserResolver
        {
            public IDriverInitialiser GetInitialiser(Browser browser)
            {
                throw new NotImplementedException();
            }
        }

        public class BrowserDriverAccessor : BrowserDriver
        {
            public Lazy<IWebDriver> CurrentWebDriverLazy => _currentWebDriverLazy;

            public BrowserDriverAccessor(ISeleniumConfiguration seleniumConfiguration, IDriverInitialiserResolver driverInitialiserResolver) : base(seleniumConfiguration, driverInitialiserResolver)
            {
            }
        }

        [Fact]
        public void Current_NotInstantiatedAfterCreation()
        {
            var target = new BrowserDriverAccessor(new MockSeleniumConfiguration(), new MockDriverInitialiserResolver());

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeFalse();
        }

        [Fact]
        public void Current_AfterAccessing_Instantiated()
        {
            var target = new BrowserDriverAccessor(new MockSeleniumConfiguration() { Browser = Browser.Noop }, new MockDriverInitialiserResolver());

            var webdriver = target.Current;

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeTrue();
            webdriver.Should().NotBeNull();
        }
    }
}