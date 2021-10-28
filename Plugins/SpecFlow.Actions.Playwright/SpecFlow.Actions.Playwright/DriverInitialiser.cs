using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public interface IDriverInitialiser
    {
        Task<IBrowser> GetChromeDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null);
        Task<IBrowser> GetEdgeDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null);
        Task<IBrowser> GetFirefoxDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null);
        Task<IBrowser> GetChromiumDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null);
        Task<IBrowser> GetWebKitDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null);
    }

    public class DriverInitialiser : IDriverInitialiser
    {
        public const float DEFAULT_TIMEOUT = 30f;

        public async Task<IBrowser> GetChromeDriverAsync(string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo);
            options.Channel = "chrome";

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<IBrowser> GetFirefoxDriverAsync(string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo);

            return await GetBrowserAsync(BrowserType.Firefox, options);
        }

        public async Task<IBrowser> GetEdgeDriverAsync(string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo);
            options.Channel = "msedge";

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<IBrowser> GetChromiumDriverAsync(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo);

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<IBrowser> GetWebKitDriverAsync(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo);

            return await GetBrowserAsync(BrowserType.Webkit, options);
        }

        private BrowserTypeLaunchOptions GetOptions(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null)
            => new()
            { Args = args, Timeout = ToMilliseconds(timeout), Headless = headless, SlowMo = slowmo };

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