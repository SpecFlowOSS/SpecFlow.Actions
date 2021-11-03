using System.Collections.Generic;

namespace SpecFlow.Actions.Appium.Configuration.WindowsAppDriver
{
    public interface IWindowsAppDriverConfiguration
    {
        Dictionary<string, string> Capabilities { get; }

        string? WindowsAppDriverPath { get; }

        bool? EnableScreenshots { get; }
    }
}