using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecFlow.Actions.Selenium.DriverInitialisers
{
    public static class DriverOptionsHelper
    {
        public static void TryToAddGlobalCapability<T>(this T options, string name, string value) where T : DriverOptions
        {
            switch (options)
            {
                case FirefoxOptions firefoxOptions:
                    firefoxOptions.AddAdditionalCapability(name, value, true);
                    break;
                case ChromeOptions chromeOptions:
                    chromeOptions.AddAdditionalCapability(name, value, true);
                    break;
                default:
                    options.AddAdditionalCapability(name, value);
                    break;
            }
        }

        public static void TryToAddArguments<T>(this T options, string[] arguments) where T : DriverOptions
        {
            switch (options)
            {
                case FirefoxOptions firefoxOptions:
                    firefoxOptions.AddArguments(arguments);
                    break;
                case ChromeOptions chromeOptions:
                    chromeOptions.AddArguments(arguments);
                    break;
                case EdgeOptions edgeOptions:
                    edgeOptions.AddAdditionalCapability("args", arguments.ToList());
                    break;
                case InternetExplorerOptions internetExplorerOptions:
                    internetExplorerOptions.AddAdditionalCapability("args", arguments.ToList());
                    break;
                case SafariOptions safariOptions:
                    safariOptions.AddAdditionalCapability("args", arguments.ToList());
                    break;
                default:
                    throw new NotImplementedException(nameof(TryToAddArguments) + " is not implemented for " +
                                                      options.GetType().Name);
            }
        }
    }
}
