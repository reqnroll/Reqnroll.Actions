using Reqnroll.Actions.Docker;
using Reqnroll.Configuration;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;

[assembly:RuntimePlugin(typeof(DockerRuntimePlugin))]


namespace Reqnroll.Actions.Docker
{
    public class DockerRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies!;
            runtimePluginEvents.CustomizeGlobalDependencies += RuntimePluginEvents_CustomizeGlobalDependencies!;
        }

        private void RuntimePluginEvents_CustomizeGlobalDependencies(object sender, CustomizeGlobalDependenciesEventArgs e)
        {
            var reqnrollConfiguration = e.ObjectContainer.Resolve<ReqnrollConfiguration>();
            reqnrollConfiguration.AdditionalStepAssemblies.Add("Reqnroll.Actions.Docker.ReqnrollPlugin");
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<DockerHandling, IDockerHandling>();
            e.ObjectContainer.RegisterTypeAs<DockerConfiguration, IDockerConfiguration>();
        }
    }
}