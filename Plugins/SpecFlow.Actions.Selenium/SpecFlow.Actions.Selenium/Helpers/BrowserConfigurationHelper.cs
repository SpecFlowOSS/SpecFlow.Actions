using SpecFlow.Actions.Selenium.Configuration;
using System;

namespace SpecFlow.Actions.Selenium.Helpers
{
    public static class BrowserConfigurationHelper
    {
        // Gets the next target configuration randomly
        internal static IBrowserConfiguration GetRandomConfiguration(IBrowserConfiguration[] targets) => targets[new Random().Next(0, targets.Length)];
    }
}