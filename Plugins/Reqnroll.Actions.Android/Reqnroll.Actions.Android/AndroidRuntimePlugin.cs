using Reqnroll.Actions.Android;
using Reqnroll.Actions.Android.Configuration;
using Reqnroll.Actions.Android.Driver;
using Reqnroll.Actions.Android.Server;
using Reqnroll.Actions.Appium.Configuration.Android;
using Reqnroll.Actions.Appium.Driver;
using Reqnroll.Actions.Appium.Server;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;

[assembly: RuntimePlugin(typeof(AndroidRuntimePlugin))]
namespace Reqnroll.Actions.Android
{
    class AndroidRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies!;
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<AndroidConfiguration, IAndroidConfiguration>();
            e.ObjectContainer.RegisterTypeAs<AppiumServer, IAppiumServer>();
            e.ObjectContainer.RegisterTypeAs<AndroidAppDriverOptions, IDriverOptions>();

            var configuration = e.ObjectContainer.Resolve<AndroidConfiguration>();
            var server = e.ObjectContainer.Resolve<AppiumServer>();

            if (configuration.LocalAppiumServerRequired)
            {
                server.Current.Start();
            }
        }
    }
}