using BoDi;
using OpenQA.Selenium;
using Reqnroll.Actions.Browserstack;
using Reqnroll.Actions.Browserstack.DriverInitialisers;
using Reqnroll.Actions.Selenium;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;
using System;
using System.Linq;
using Reqnroll;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;

[assembly: RuntimePlugin(typeof(BrowserstackRuntimePlugin))]

namespace Reqnroll.Actions.Browserstack
{
    public class BrowserstackRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeScenarioDependencies += RuntimePluginEvents_CustomizeScenarioDependencies!;
            runtimePluginEvents.CustomizeGlobalDependencies += RuntimePluginEvents_CustomizeGlobalDependencies!;
        }

        private void RuntimePluginEvents_CustomizeGlobalDependencies(object? sender, CustomizeGlobalDependenciesEventArgs e)
        { 
            var runtimePluginTestExecutionLifecycleEventEmitter = e.ObjectContainer.Resolve<RuntimePluginTestExecutionLifecycleEvents>();
            runtimePluginTestExecutionLifecycleEventEmitter.AfterScenario += RuntimePluginTestExecutionLifecycleEventEmitter_AfterScenario!;
            runtimePluginTestExecutionLifecycleEventEmitter.AfterTestRun += RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun!;
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterTestRun(object sender, RuntimePluginAfterTestRunEventArgs e)
        {
            BrowserstackLocalService.Stop();
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterScenario(object? sender, RuntimePluginAfterScenarioEventArgs e)
        {
            var scenarioContext = e.ObjectContainer.Resolve<ScenarioContext>();
            var browserDriver = e.ObjectContainer.Resolve<BrowserDriver>();

            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
            {

                ((IJavaScriptExecutor)browserDriver.Current).ExecuteScript(BrowserstackTestResultExecutor.GetResultExecutor("passed"));
            }
            else
            {
                ((IJavaScriptExecutor)browserDriver.Current).ExecuteScript(BrowserstackTestResultExecutor.GetResultExecutor("failed", scenarioContext.TestError.Message));
            }
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<BrowserstackConfiguration, ISeleniumConfiguration>();
            e.ObjectContainer.RegisterTypeAs<BrowserstackCredentialProvider, ICredentialProvider>();
            RegisterInitialisers(e.ObjectContainer);
        }

        private void RegisterInitialisers(IObjectContainer objectContainer)
        {
            objectContainer.RegisterFactoryAs<IDriverInitialiser>(container =>
            {
                var config = container.Resolve<ISeleniumConfiguration>();

                if (((BrowserstackConfiguration)config).BrowserstackLocalRequired)
                {
                    BrowserstackLocalService.Start(
                        ((BrowserstackConfiguration)config).BrowserstackLocalCapabilities.ToList());
                }

                var browserstackDriverInitialiser = container.Resolve<BrowserstackDriverInitialiser>();
                var credentialProvider = container.Resolve<ICredentialProvider>();
                return config.Browser switch
                {
                    Browser.Chrome => new BrowserstackChromeDriverInitialiser(browserstackDriverInitialiser, config, credentialProvider),
                    Browser.Firefox => new BrowserstackFirefoxDriverInitialiser(browserstackDriverInitialiser, config, credentialProvider),
                    Browser.Edge => new BrowserstackEdgeDriverInitialiser(browserstackDriverInitialiser, config, credentialProvider),
                    Browser.InternetExplorer => new BrowserstackInternetExplorerDriverInitialiser(browserstackDriverInitialiser, config, credentialProvider),
                    Browser.Safari => new BrowserstackSafariDriverInitialiser(browserstackDriverInitialiser, config, credentialProvider),
                    _ => throw new ArgumentOutOfRangeException($"Browser {config.Browser} not implemented")
                };
            });
        }
    }
}