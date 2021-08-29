using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public interface IDriverInitialiser
    {
        Task<IBrowser> GetChromeDriverAsync(string[]? args = null, float? timeout = 30, bool? headless = true);
        Task<IBrowser> GetEdgeDriverAsync(string[]? args = null, float? timeout = 30, bool? headless = true);
        Task<IBrowser> GetFirefoxDriverAsync(string[]? args = null, float? timeout = 30, bool? headless = true);
        Task<IBrowser> GetChromiumDriverAsync(string[]? args = null, float? timeout = 30, bool? headless = true);
    }

    public class DriverInitialiser : IDriverInitialiser
    {
        public async Task<IBrowser> GetChromeDriverAsync(string[]? args = null, float? timeout = 30, bool? headless = true)
        {
            var options = new BrowserTypeLaunchOptions { Args = args, Channel = "chrome", Timeout = ToMilliseconds(timeout), Headless = headless };

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            return await playwright.Chromium.LaunchAsync(options);
        }

        public async Task<IBrowser> GetFirefoxDriverAsync(string[]? args = null, float? timeout = 30, bool? headless = true)
        {
            var options = new BrowserTypeLaunchOptions { Args = args, Timeout = ToMilliseconds(timeout), Headless = headless };

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            return await playwright.Firefox.LaunchAsync(options);
        }

        public async Task<IBrowser> GetEdgeDriverAsync(string[]? args = null, float? timeout = 30, bool? headless = true)
        {
            var options = new BrowserTypeLaunchOptions { Args = args, Channel = "msedge", Timeout = ToMilliseconds(timeout), Headless = headless };

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            return await playwright.Chromium.LaunchAsync(options);
        }

        public async Task<IBrowser> GetChromiumDriverAsync(string[]? args, float? timeout = 30, bool? headless = true)
        {
            var options = new BrowserTypeLaunchOptions { Args = args, Timeout = ToMilliseconds(timeout), Headless = headless };

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            return await playwright.Chromium.LaunchAsync(options);
        }

        private static float? ToMilliseconds(float? seconds)
        {
            return seconds * 1000;
        }
    }
}