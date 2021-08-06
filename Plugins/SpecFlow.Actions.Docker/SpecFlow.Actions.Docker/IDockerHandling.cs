namespace SpecFlow.Actions.Docker
{
    interface IDockerHandling
    {
        void DockerComposeUp();
        void DockerComposeDown();
    }
}