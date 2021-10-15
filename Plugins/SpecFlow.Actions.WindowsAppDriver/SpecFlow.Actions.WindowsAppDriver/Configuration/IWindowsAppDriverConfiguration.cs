using System.Collections.Generic;

namespace SpecFlow.Actions.WindowsAppDriver.Configuration
{
    public interface IWindowsAppDriverConfiguration
    {
        Dictionary<string, string>? Capabilities { get; }

        string? WindowsAppDriverPath { get; }

        bool? EnableScreenshots { get;  }
    }
}