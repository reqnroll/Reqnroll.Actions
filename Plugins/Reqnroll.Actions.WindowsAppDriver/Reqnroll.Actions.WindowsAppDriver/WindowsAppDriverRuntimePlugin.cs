using Reqnroll.Actions.Appium;
using Reqnroll.Actions.Appium.Configuration.WindowsAppDriver;
using Reqnroll.Actions.Appium.Driver;
using Reqnroll.Actions.WindowsAppDriver;
using Reqnroll.Actions.WindowsAppDriver.Configuration;
using Reqnroll;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;

[assembly: RuntimePlugin(typeof(WindowsAppDriverRuntimePlugin))]
namespace Reqnroll.Actions.WindowsAppDriver
{
    public class WindowsAppDriverRuntimePlugin : IRuntimePlugin
    {
        private IAppDriverCli? _appDriverCli;
        private IScreenshotHelper? _screenshotHelper;

        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.RegisterGlobalDependencies += RuntimePluginEvents_RegisterGlobalDependencies;
        }

        private void RuntimePluginEvents_RegisterGlobalDependencies(object sender, RegisterGlobalDependenciesEventArgs e)
        {
            var runtimePluginTestExecutionLifecycleEventEmitter = e.ObjectContainer.Resolve<RuntimePluginTestExecutionLifecycleEvents>();
            runtimePluginTestExecutionLifecycleEventEmitter.BeforeTestRun += RuntimePluginTestExecutionLifecycleEventEmitter_BeforeTestRun;
            runtimePluginTestExecutionLifecycleEventEmitter.AfterTestRun += RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun;
            runtimePluginTestExecutionLifecycleEventEmitter.AfterStep += RuntimePluginTestExecutionLifecycleEventEmitter_AfterStep;

            e.ObjectContainer.RegisterTypeAs<WindowsAppDriverConfiguration, IWindowsAppDriverConfiguration>();
            e.ObjectContainer.RegisterTypeAs<WindowsAppDriverOptions, IDriverOptions>();
            e.ObjectContainer.RegisterTypeAs<AppDriverCli, IAppDriverCli>();
            e.ObjectContainer.RegisterTypeAs<ScreenshotHelper, IScreenshotHelper>();
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterStep(object sender, RuntimePluginAfterStepEventArgs e)
        {
            var scenarioContext = e.ObjectContainer.Resolve<ScenarioContext>();
            var featureContext = e.ObjectContainer.Resolve<FeatureContext>();
            var appDriver = e.ObjectContainer.Resolve<AppDriver>();

            _screenshotHelper?.TakeScreenshot(appDriver, featureContext, scenarioContext);
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_BeforeTestRun(object sender, RuntimePluginBeforeTestRunEventArgs e)
        {
            _screenshotHelper = e.ObjectContainer.Resolve<ScreenshotHelper>();
            _appDriverCli = e.ObjectContainer.Resolve<AppDriverCli>();

            _appDriverCli.Start();
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun(object sender, RuntimePluginAfterTestRunEventArgs e)
        {
            _appDriverCli?.Dispose();
        }
    }
}