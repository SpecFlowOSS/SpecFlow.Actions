using System;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Docker
{
    [Binding]
    public class Hooks
    {
        [BeforeTestRun(Order = int.MinValue)]
        public static void DockerComposeUp(IDockerHandling dockerHandling)
        {
            dockerHandling.DockerComposeUp();
        }

        [AfterTestRun(Order = int.MaxValue)]
        public static void DockerComposeDown(IDockerHandling dockerHandling)
        {
            dockerHandling.DockerComposeDown();
        }
    }
}
