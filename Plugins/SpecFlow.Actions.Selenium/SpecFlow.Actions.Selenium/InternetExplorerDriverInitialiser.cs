using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecFlow.Actions.Selenium
{
    public class InternetExplorerDriverInitialiser : IDriverInitialiser
    {
        private readonly Lazy<string?> _internetExplorerWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("IE_WEBDRIVER_FILE_PATH"));

        /// <summary>
        /// Gets the Internet Explorer driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public IWebDriver GetDriverInstance(Dictionary<string, string>? capabilities = null,
            string[]? args = null)
        {
            var options = new InternetExplorerOptions();

            if (capabilities?.Count != 0 && capabilities != null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            if (args != null && args?.Length != 0)
            {
                options.AddAdditionalCapability("args", args!.ToList());
            }

            return string.IsNullOrWhiteSpace(_internetExplorerWebDriverFilePath.Value)
                ? new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(), options)
                : new InternetExplorerDriver(
                    InternetExplorerDriverService.CreateDefaultService(_internetExplorerWebDriverFilePath.Value),
                    options);
        }
    }
}