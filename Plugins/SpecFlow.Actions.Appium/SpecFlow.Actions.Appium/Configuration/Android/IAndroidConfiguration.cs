using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Appium.Configuration.Android
{
    public interface IAndroidConfiguration
    {
        Dictionary<string, string> Capabilities { get; }

        int? Timeout { get; }

        bool LocalAppiumServerRequired { get; }

        Uri? ServerAddress { get; }
    }
}