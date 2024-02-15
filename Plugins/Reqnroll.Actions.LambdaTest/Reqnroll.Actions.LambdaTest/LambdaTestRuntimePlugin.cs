using BoDi;
using OpenQA.Selenium;
using Reqnroll.Actions.LambdaTest;
using Reqnroll.Actions.LambdaTest.DriverInitialisers;
using Reqnroll.Actions.Selenium;
using Reqnroll.Actions.Selenium.Configuration;
using Reqnroll.Actions.Selenium.DriverInitialisers;
using Reqnroll.Actions.Selenium.Hoster;
using System;
using System.Linq;
using Reqnroll;
using Reqnroll.Plugins;
using Reqnroll.UnitTestProvider;

[assembly: RuntimePlugin(typeof(LambdaTestRuntimePlugin))]

namespace Reqnroll.Actions.LambdaTest
{
    public class LambdaTestRuntimePlugin : IRuntimePlugin
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
        }

        private void RuntimePluginTestExecutionLifecycleEventEmitter_AfterScenario(object? sender, RuntimePluginAfterScenarioEventArgs e)
        {
            var scenarioContext = e.ObjectContainer.Resolve<ScenarioContext>();
            var browserDriver = e.ObjectContainer.Resolve<BrowserDriver>();

            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
            {
                ((IJavaScriptExecutor)browserDriver.Current).ExecuteScript("lambda-status=passed");
            }
            else
            {
                ((IJavaScriptExecutor)browserDriver.Current).ExecuteScript("lambda-status=failed");
            }
        }

        private void RuntimePluginEvents_CustomizeScenarioDependencies(object? sender, CustomizeScenarioDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<LambdaTestConfiguration, ISeleniumConfiguration>();
            e.ObjectContainer.RegisterTypeAs<LambdaTestCredentialProvider, ICredentialProvider>();

            RegisterInitialisers(e.ObjectContainer);
        }

        private void RegisterInitialisers(IObjectContainer objectContainer)
        {
            objectContainer.RegisterFactoryAs<IDriverInitialiser>(container =>
            {
                var config = container.Resolve<ISeleniumConfiguration>();
                
                var credentialProvider = container.Resolve<ICredentialProvider>();
                var lambdaTestDriverInitialiser = container.Resolve<LambdaTestDriverInitialiser>();
                return config.Browser switch
                {
                    Browser.Chrome => new LambdaTestChromeDriverInitialiser(lambdaTestDriverInitialiser, config, credentialProvider),
                    Browser.Firefox => new LambdaTestFirefoxDriverInitialiser(lambdaTestDriverInitialiser, config, credentialProvider),
                    Browser.Edge => new LambdaTestEdgeDriverInitialiser(lambdaTestDriverInitialiser, config, credentialProvider),
                    Browser.InternetExplorer => new LambdaTestInternetExplorerDriverInitialiser(lambdaTestDriverInitialiser, config, credentialProvider),
                    Browser.Safari => new LambdaTestSafariDriverInitialiser(lambdaTestDriverInitialiser, config, credentialProvider),
                    _ => throw new ArgumentOutOfRangeException($"Browser {config.Browser} not implemented")
                };
            });
        }
    }
}