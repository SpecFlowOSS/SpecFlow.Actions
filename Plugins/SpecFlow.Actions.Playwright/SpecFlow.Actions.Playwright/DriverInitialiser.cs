using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public interface IDriverInitialiser
    {
        Task<IBrowser> GetChromeDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true);
        Task<IBrowser> GetEdgeDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true);
        Task<IBrowser> GetFirefoxDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true);
        Task<IBrowser> GetChromiumDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true);
        Task<IBrowser> GetWebKitDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true);
    }

    public class DriverInitialiser : IDriverInitialiser
    {
        public const float DEFAULT_TIMEOUT = 30f;

        public async Task<IBrowser> GetChromeDriverAsync(string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true)
        {
            var options = GetOptions(args, timeout, headless);
            options.Channel = "chrome";

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<IBrowser> GetFirefoxDriverAsync(string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true)
        {
            var options = GetOptions(args, timeout, headless);

            return await GetBrowserAsync(BrowserType.Firefox, options);
        }

        public async Task<IBrowser> GetEdgeDriverAsync(string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true)
        {
            var options = GetOptions(args, timeout, headless);
            options.Channel = "msedge";

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<IBrowser> GetChromiumDriverAsync(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true)
        {
            var options = GetOptions(args, timeout, headless);

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<IBrowser> GetWebKitDriverAsync(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true)
        {
            var options = GetOptions(args, timeout, headless);

            return await GetBrowserAsync(BrowserType.Webkit, options);
        }

        private BrowserTypeLaunchOptions GetOptions(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true)
            => new()
            { Args = args, Timeout = ToMilliseconds(timeout), Headless = headless };

        private async Task<IBrowser> GetBrowserAsync(string browserType, BrowserTypeLaunchOptions options)
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            return await playwright[browserType].LaunchAsync(options);
        }

        private static float? ToMilliseconds(float? seconds)
        {
            return seconds * 1000;
        }
    }
}