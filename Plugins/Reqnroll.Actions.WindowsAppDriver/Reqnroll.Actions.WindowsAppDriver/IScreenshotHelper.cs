using Reqnroll;

namespace Reqnroll.Actions.WindowsAppDriver
{
    public interface IScreenshotHelper
    {
        void TakeScreenshot(AppDriver appDriver, FeatureContext featureContext, ScenarioContext scenarioContext);
    }
}