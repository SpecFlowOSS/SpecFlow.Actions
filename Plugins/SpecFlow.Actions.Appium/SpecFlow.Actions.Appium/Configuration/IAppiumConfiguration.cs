using System.Collections.Generic;

namespace SpecFlow.Actions.Appium.Configuration
{
    internal interface IAppiumConfiguration
    {
        Dictionary<string, string>? Capabilities { get; }
        bool LocalAppiumServerRequired { get; }
    }
}