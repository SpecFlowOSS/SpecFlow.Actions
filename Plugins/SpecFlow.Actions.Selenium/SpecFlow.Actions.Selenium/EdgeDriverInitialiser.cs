using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecFlow.Actions.Selenium
{
    public class EdgeDriverInitialiser : IDriverInitialiser
    {
        private readonly Lazy<string?> _edgeWebDriverFilePath = new(() => Environment.GetEnvironmentVariable("EDGE_WEBDRIVER_FILE_PATH"));

        /// <summary>
        /// Gets the Edge driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public OpenQA.Selenium.IWebDriver GetDriverInstance(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new OpenQA.Selenium.Edge.EdgeOptions();

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

            return string.IsNullOrWhiteSpace(_edgeWebDriverFilePath.Value)
                ? new EdgeDriver(EdgeDriverService.CreateDefaultService(), options)
                : new EdgeDriver(EdgeDriverService.CreateDefaultService(_edgeWebDriverFilePath.Value), options);
        }
    }
}