// unset

using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace SpecFlow.Actions.Configuration.Tests
{
    public class SpecFlowActionsConfigurationTests
    {
        [Fact]
        public void Get_PathWithDots_Works()
        {
            var content = @"{""parent"": { ""child"": ""value"" }}";


            var specflowActionJsonLoaderMock = new Mock<ISpecFlowActionJsonLoader>();
            specflowActionJsonLoaderMock.Setup(m => m.Load()).Returns(content);

            var specFlowActionsConfiguration = new SpecFlowActionsConfiguration(specflowActionJsonLoaderMock.Object);

            var actualValue = specFlowActionsConfiguration.Get("parent:child");

            actualValue.Should().Be("value");
        }


        [Fact]
        public void Get_PathNotExists_ReturnsNull()
        {
            var content = @"{""parent"": { ""child"": ""value"" }}";


            var specflowActionJsonLoaderMock = new Mock<ISpecFlowActionJsonLoader>();
            specflowActionJsonLoaderMock.Setup(m => m.Load()).Returns(content);

            var specFlowActionsConfiguration = new SpecFlowActionsConfiguration(specflowActionJsonLoaderMock.Object);

            var actualValue = specFlowActionsConfiguration.Get("parent:the-other-child");

            actualValue.Should().BeNull();
        }
    }
}