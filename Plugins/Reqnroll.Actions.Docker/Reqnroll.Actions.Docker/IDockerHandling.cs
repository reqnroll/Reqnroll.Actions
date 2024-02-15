namespace Reqnroll.Actions.Docker
{
    interface IDockerHandling
    {
        void DockerComposeUp();
        void DockerComposeDown();
    }
}