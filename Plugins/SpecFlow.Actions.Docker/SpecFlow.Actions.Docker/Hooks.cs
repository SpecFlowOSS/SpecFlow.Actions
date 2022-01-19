using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Docker
{
    [Binding]
    class Hooks
    {
        [BeforeScenario(Order = int.MinValue)]
        public void DockerComposeUp(IDockerHandling dockerHandling)
        {
            dockerHandling.DockerComposeUp();
        }

        [AfterScenario(Order = int.MaxValue)]
        public void DockerComposeDown(IDockerHandling dockerHandling)
        {
            dockerHandling.DockerComposeDown();
        }
    }
}
