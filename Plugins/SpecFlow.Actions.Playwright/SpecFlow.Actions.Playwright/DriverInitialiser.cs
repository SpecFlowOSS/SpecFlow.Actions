using Microsoft.Playwright;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public interface IDriverInitialiser
    {
        Task<IBrowser> GetChromeDriverAsync(Dictionary<string, string>? capabilities = null, string[]? args = null);
        Task<IBrowser> GetEdgeDriverAsync(Dictionary<string, string>? capabilities = null, string[]? args = null);
        Task<IBrowser> GetFirefoxDriverAsync(Dictionary<string, string>? capabilities = null, string[]? args = null);
        Task<IBrowser> GetChromiumDriverAsync(Dictionary<string, string>? capabilities = null, string[]? args = null);
    }

    public class DriverInitialiser : IDriverInitialiser
    {
        /// <summary>
        /// Gets the Chrome driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public async Task<IBrowser> GetChromeDriverAsync(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new BrowserTypeLaunchOptions
            {
                Args = args,
                Channel = "chrome",
                Headless = false
            };

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            var chromium = playwright.Chromium;

            return await chromium.LaunchAsync(options);
        }

        /// <summary>
        /// Gets the Firefox driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public async Task<IBrowser> GetFirefoxDriverAsync(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new BrowserTypeLaunchOptions
            {
                Args = args
            };

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            var firefox = playwright.Firefox;

            return await firefox.LaunchAsync(options);
        }

        /// <summary>
        /// Gets the Edge driver with the desired arguments and/or capabilities
        /// </summary>
        /// <param name="capabilities"></param>
        /// <param name="args"></param>
        public async Task<IBrowser> GetEdgeDriverAsync(Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = new BrowserTypeLaunchOptions
            {
                Args = args,
                Channel = "msedge"
            };

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            var chromium = playwright.Chromium;

            return await chromium.LaunchAsync(options);
        }

        public async Task<IBrowser> GetChromiumDriverAsync(Dictionary<string, string>? capabilities, string[]? args)
        {
            var options = new BrowserTypeLaunchOptions
            {
                Args = args,
                Headless = false
            };

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            var chromium = playwright.Chromium;

            return await chromium.LaunchAsync(options);
        }
    }
}