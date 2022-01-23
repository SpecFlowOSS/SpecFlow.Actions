using System;
using FluentAssertions;
using Moq;
using OpenQA.Selenium;
using Xunit;
using SpecFlow.Actions.Selenium.Driver;

namespace SpecFlow.Actions.Selenium.Tests
{
    public class BrowserDriverTests
    {
        public class BrowserDriverAccessor : BrowserDriver
        {
            public new Lazy<IWebDriver> CurrentWebDriverLazy => base.CurrentWebDriverLazy;

            public BrowserDriverAccessor(IDriverInitialiser driverInitialiser) : base(driverInitialiser)
            {
            }
        }

        [Fact]
        public void Current_NotInstantiatedAfterCreation()
        {
            var InitialiserMock = new Mock<IDriverInitialiser>();

            InitialiserMock.Setup(m => m.Initialise()).Returns(new NoopDriver());

            var target = new BrowserDriverAccessor(InitialiserMock.Object);

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeFalse();
        }

        [Fact]
        public void Current_AfterAccessing_Instantiated()
        {
            var InitialiserMock = new Mock<IDriverInitialiser>();

            InitialiserMock.Setup(m => m.Initialise()).Returns(new NoopDriver());

            var target = new BrowserDriverAccessor(InitialiserMock.Object);

            var webdriver = target.Current;

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeTrue();
            webdriver.Should().NotBeNull();
        }
    }
}