using SpecFlow.Actions.WindowsAppDriver.Configuration;
using System;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.TestFramework;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public class ScreenshotHelper : IScreenshotHelper
    {
        private readonly ITestRunContext _testRunContext;
        private readonly string CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd_Hmmss");
        private readonly bool _enabled;

        public ScreenshotHelper(IWindowsAppDriverConfiguration windowsAppDriverConfiguration, ITestRunContext testRunContext)
        {
            _testRunContext = testRunContext;
            _enabled = windowsAppDriverConfiguration.EnableScreenshots ?? false;
        }

        public void TakeScreenshot(AppDriver appDriver, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            if (_enabled)
            {
                var path = Path.Combine(_testRunContext.GetTestDirectory(), "Screenshots", CurrentDateTime, featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title);

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