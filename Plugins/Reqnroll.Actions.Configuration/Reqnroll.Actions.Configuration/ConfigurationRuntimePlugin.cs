using Reqnroll.Actions.Configuration;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;

[assembly:RuntimePlugin(typeof(ConfigurationRuntimePlugin))]

namespace Reqnroll.Actions.Configuration
{
    public class ConfigurationRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;
            runtimePluginEvents.RegisterGlobalDependencies += RuntimePluginEvents_RegisterGlobalDependencies;
        }

        private void RuntimePluginEvents_RegisterGlobalDependencies(object sender, RegisterGlobalDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<CurrentTargetIdentifier, CurrentTargetIdentifier>();
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<CurrentTargetIdentifier, CurrentTargetIdentifier>();
            e.ObjectContainer.RegisterTypeAs<ReqnrollActionJsonLocator, IReqnrollActionJsonLocator>();
            e.ObjectContainer.RegisterTypeAs<ReqnrollActionJsonLoader, IReqnrollActionJsonLoader>();
            e.ObjectContainer.RegisterTypeAs<ReqnrollActionsConfiguration, IReqnrollActionsConfiguration>();
        }
    }
}