namespace SpecFlow.Actions.Selenium.DriverOptions
{
    public interface IWebDriverOptionsFactory
    {
        IOptionsWrapper GetChromeOptions();

        IOptionsWrapper GetFireFoxOptions();

        IOptionsWrapper GetEdgeOptions();

        IOptionsWrapper GetInternetExplorerOptions();

        IOptionsWrapper GetSafariOptions();
    }
}