// unset

using FluentAssertions;
using Moq;
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
            specflowActionJsonLoaderMock.Setup(m => m.LoadTarget()).Returns(content);

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
            specflowActionJsonLoaderMock.Setup(m => m.LoadTarget()).Returns(content);

            var specFlowActionsConfiguration = new SpecFlowActionsConfiguration(specflowActionJsonLoaderMock.Object);

            var actualValue = specFlowActionsConfiguration.Get("parent:the-other-child");

            actualValue.Should().BeNull();
        }

        [Fact]
        public void GetDictionary_ObjectDefinedInParentAndTarget_Merged()
        {
            var parentContent = @"{""parent"": { ""children"": { ""parentChild"":""a value""} }}";
            var targetContent = @"{""parent"": { ""children"": { ""childChild"":""another value""} }}";


            var specflowActionJsonLoaderMock = new Mock<ISpecFlowActionJsonLoader>();
            specflowActionJsonLoaderMock.Setup(m => m.Load()).Returns(parentContent);
            specflowActionJsonLoaderMock.Setup(m => m.LoadTarget()).Returns(targetContent);

            var specFlowActionsConfiguration = new SpecFlowActionsConfiguration(specflowActionJsonLoaderMock.Object);

            var actualValue = specFlowActionsConfiguration.GetDictionary("parent:children");

            actualValue.Should().HaveCount(2);
        }
    }
}