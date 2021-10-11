using SpecFlow.Actions.WindowsAppDriver.Configuration;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public class ScreenshotHelper : IScreenshotHelper
    {
        private readonly string CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd_Hmmss");
        private readonly bool _enabled;

        public ScreenshotHelper(IWindowsAppDriverConfiguration windowsAppDriverConfiguration)
        {
            _enabled = windowsAppDriverConfiguration.EnableScreenshots ?? true;
        }

        public void TakeScreenshot(AppDriver appDriver, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            if (_enabled)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots", CurrentDateTime, featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var screenshot = appDriver.Current.GetScreenshot();
                screenshot.SaveAsFile(Path.Combine(path, $"{scenarioContext.StepContext.StepInfo.Text} ({scenarioContext.ScenarioExecutionStatus}).png")); 
            }
        }
    }
}