using TechTalk.SpecFlow;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public interface IScreenshotHelper
    {
        void TakeScreenshot(AppDriver appDriver, FeatureContext featureContext, ScenarioContext scenarioContext);
    }
}