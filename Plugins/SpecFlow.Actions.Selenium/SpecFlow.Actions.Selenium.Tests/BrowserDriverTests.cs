using System;
using FluentAssertions;
using Moq;
using OpenQA.Selenium;
using Xunit;
using SpecFlow.Actions.Selenium.Configuration;
using SpecFlow.Actions.Selenium.Factories;
using SpecFlow.Actions.Selenium.Enums;

namespace SpecFlow.Actions.Selenium.Tests
{
    public class BrowserDriverTests
    {
        public class BrowserDriverAccessor : BrowserDriver
        {
            public Lazy<IWebDriver> CurrentWebDriverLazy => _currentWebDriverLazy;

            public BrowserDriverAccessor(ISeleniumConfiguration seleniumConfiguration, IDriverFactory browserFactory) : base(seleniumConfiguration, browserFactory)
            {
            }
        }

        [Fact]
        public void Current_NotInstantiatedAfterCreation()
        {
            var seleniumConfigurationMock = new Mock<ISeleniumConfiguration>();
            var driverFactoryMock = new Mock<IDriverFactory>();

            var target = new BrowserDriverAccessor(seleniumConfigurationMock.Object, driverFactoryMock.Object);

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeFalse();
        }

        [Fact]
        public void Current_AfterAccessing_Instantiated()
        {
            var seleniumConfigurationMock = new Mock<ISeleniumConfiguration>();
            seleniumConfigurationMock.Setup(m => m.Targets).Returns(new TargetConfiguration[] { new TargetConfiguration() });

            var driverFactoryMock = new Mock<IDriverFactory>();
            driverFactoryMock.Setup(m => m.GetDriver(seleniumConfigurationMock.Object.Targets[0])).Returns(new NoopWebdriver());

            var target = new BrowserDriverAccessor(seleniumConfigurationMock.Object, driverFactoryMock.Object);


            var webdriver = target.Current;

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeTrue();
            webdriver.Should().NotBeNull();
        }
    }
}