using BoDi;
using FluentAssertions;
using Xunit;

namespace SpecFlow.Actions.Configuration.Tests
{
    public class ContainerRegistrationsTests
    {

        [Fact]
        public void CurrentTargetIdentifier_ObjectContainer_Both_Container_Resolve_Same_Instance()
        {
            var parentContainer = new ObjectContainer();
            var childContainer = new ObjectContainer(parentContainer);

            parentContainer.RegisterTypeAs<CurrentTargetIdentifier, CurrentTargetIdentifier>();

            var resolvedFromParent = parentContainer.Resolve<CurrentTargetIdentifier>();
            var resolvedFromChild = childContainer.Resolve<CurrentTargetIdentifier>();

            resolvedFromChild.Should().Be(resolvedFromParent);
        }

        [Fact]
        public void CurrentTargetIdentifier_ObjectContainer_Both_Container_Resolve_Different_Instance()
        {
            var parentContainer = new ObjectContainer();
            var childContainer = new ObjectContainer(parentContainer);

            parentContainer.RegisterTypeAs<CurrentTargetIdentifier, CurrentTargetIdentifier>();
            childContainer.RegisterTypeAs<CurrentTargetIdentifier, CurrentTargetIdentifier>();

            var resolvedFromParent = parentContainer.Resolve<CurrentTargetIdentifier>();
            var resolvedFromChild = childContainer.Resolve<CurrentTargetIdentifier>();

            resolvedFromChild.Should().NotBe(resolvedFromParent);
        }
    }
}
