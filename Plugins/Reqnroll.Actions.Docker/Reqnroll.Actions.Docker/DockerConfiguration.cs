// unset

using Reqnroll.Actions.Configuration;

namespace Reqnroll.Actions.Docker
{
    public class DockerConfiguration : IDockerConfiguration
    {
        private readonly IReqnrollActionsConfiguration _reqnrollActionsConfiguration;

        public DockerConfiguration(IReqnrollActionsConfiguration reqnrollActionsConfiguration)
        {
            _reqnrollActionsConfiguration = reqnrollActionsConfiguration;
        }

        public string? File => _reqnrollActionsConfiguration.Get("docker:file");
    }
}