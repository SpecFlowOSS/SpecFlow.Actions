using BoDi;
using System;
using FluentAssertions;
using Moq;
using OpenQA.Selenium;
using Xunit;
using SpecFlow.Actions.Selenium.Configuration;

namespace SpecFlow.Actions.Selenium.Tests
{
    public class BrowserDriverTests
    {
        //public class BrowserDriverAccessor : BrowserDriver
        //{
        //    public Lazy<IWebDriver> CurrentWebDriverLazy => _currentWebDriverLazy;

        //    public BrowserDriverAccessor(ISeleniumConfiguration seleniumConfiguration, IObjectContainer objectContainer) : base(seleniumConfiguration, objectContainer)
        //    {
        //    }
        //}

        [Fact]
        public void Current_NotInstantiatedAfterCreation()
        {
            //var seleniumConfigurationMock = new Mock<ISeleniumConfiguration>();
            //var objectContainerMock = new Mock<IObjectContainer>();

            //var target = new BrowserDriverAccessor(seleniumConfigurationMock.Object, objectContainerMock.Object);

            //target.CurrentWebDriverLazy.IsValueCreated.Should().BeFalse();
        }

        [Fact]
        public void Current_AfterAccessing_Instantiated()
        {
            //var seleniumConfigurationMock = new Mock<ISeleniumConfiguration>();
            //seleniumConfigurationMock.Setup(m => m.Browser).Returns(Browser.Noop);

            //var objectContainerMock = new Mock<IObjectContainer>();

            //var target = new BrowserDriverAccessor(seleniumConfigurationMock.Object, objectContainerMock.Object);


            //var webdriver = target.Current;

            //target.CurrentWebDriverLazy.IsValueCreated.Should().BeTrue();
            //webdriver.Should().NotBeNull();
        }
    }
}