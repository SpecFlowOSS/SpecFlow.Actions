﻿using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public interface IDriverInitialiser
    {
        Task<(IPlaywright, IBrowser)> GetChromeDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null);
        Task<(IPlaywright, IBrowser)> GetEdgeDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null);
        Task<(IPlaywright, IBrowser)> GetFirefoxDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null);
        Task<(IPlaywright, IBrowser)> GetChromiumDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null);
        Task<(IPlaywright, IBrowser)> GetWebKitDriverAsync(string[]? args = null, float? timeout = DriverInitialiser.DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null);
    }

    public class DriverInitialiser : IDriverInitialiser
    {
        public const float DEFAULT_TIMEOUT = 30f;

        public async Task<(IPlaywright, IBrowser)> GetChromeDriverAsync(string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo, traceDir);
            options.Channel = "chrome";

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<(IPlaywright, IBrowser)> GetFirefoxDriverAsync(string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo, traceDir);

            return await GetBrowserAsync(BrowserType.Firefox, options);
        }

        public async Task<(IPlaywright, IBrowser)> GetEdgeDriverAsync(string[]? args = null, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo, traceDir);
            options.Channel = "msedge";

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<(IPlaywright, IBrowser)> GetChromiumDriverAsync(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo, traceDir);

            return await GetBrowserAsync(BrowserType.Chromium, options);
        }

        public async Task<(IPlaywright, IBrowser)> GetWebKitDriverAsync(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null)
        {
            var options = GetOptions(args, timeout, headless, slowmo, traceDir);

            return await GetBrowserAsync(BrowserType.Webkit, options);
        }

        private BrowserTypeLaunchOptions GetOptions(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null, string? traceDir = null)
            => new()
            { Args = args, Timeout = ToMilliseconds(timeout), Headless = headless, SlowMo = slowmo, TracesDir = traceDir};

        private async Task<(IPlaywright,IBrowser)> GetBrowserAsync(string browserType, BrowserTypeLaunchOptions options)
        {
            IPlaywright? playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            return (playwright, await playwright[browserType].LaunchAsync(options));
        }

        private static float? ToMilliseconds(float? seconds)
        {
            return seconds * 1000;
        }
    }
}