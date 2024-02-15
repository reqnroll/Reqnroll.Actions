using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using Reqnroll.Actions.BoaConstrictor;
using Reqnroll.Actions.Selenium;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;

[assembly:RuntimePlugin(typeof(BoaConstrictorRuntimePlugin))]

namespace Reqnroll.Actions.BoaConstrictor
{
    public class BoaConstrictorRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies;            
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterFactoryAs<Actor>((container) =>
            {
                var reqnrollLogger = e.ObjectContainer.Resolve<ReqnrollLogger>();
                var browserDriver = e.ObjectContainer.Resolve<BrowserDriver>();

                var actor = new Actor(logger: reqnrollLogger);
                actor.Can(BrowseTheWeb.With(browserDriver.Current));

                return actor;
            });

            e.ObjectContainer.RegisterFactoryAs<IActor>((container) => container.Resolve<Actor>());
        }
    }
}