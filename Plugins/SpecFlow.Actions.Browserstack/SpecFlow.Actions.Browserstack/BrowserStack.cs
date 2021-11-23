using System;
using System.Collections;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Browserstack
{
    internal class BrowserStack
    {
        internal static Lazy<Uri> RemoteServerUri => new(() => new Uri("https://hub-cloud.browserstack.com/wd/hub/"));
        internal static Lazy<string> Username => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        internal static Lazy<string> AccessKey => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));

        internal static string GetTestName(ScenarioContext scenarioContext)
        {
            var testName = scenarioContext.ScenarioInfo.Title;

            if (scenarioContext.ScenarioInfo.Arguments.Count > 0)
            {
                testName += ": ";
            }

            foreach (DictionaryEntry argument in scenarioContext.ScenarioInfo.Arguments)
            {
                testName += argument.Key + ":" + argument.Value + "; ";
            }

            return testName.Trim();
        }
    }
}