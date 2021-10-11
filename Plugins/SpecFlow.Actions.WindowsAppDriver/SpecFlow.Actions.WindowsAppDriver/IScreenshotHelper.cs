using TechTalk.SpecFlow;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public interface IScreenshotHelper
    {
        void TakeScreenshot(FeatureContext featureContext, ScenarioContext scenarioContext);
    }
}