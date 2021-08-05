using BoDi;

namespace SpecFlow.Actions.Selenium
{
    public interface IDriverInitialiserResolver
    {
        IDriverInitialiser GetInitialiser(Browser browser);
    }

    public class DriverInitialiserResolver : IDriverInitialiserResolver
    {
        private readonly IObjectContainer _container;

        public DriverInitialiserResolver(IObjectContainer container)
        {
            _container = container;

            _container.RegisterTypeAs<EdgeDriverInitialiser, IDriverInitialiser>(Browser.Firefox.ToString());
            _container.RegisterTypeAs<FirefoxDriverInitialiser, IDriverInitialiser>(Browser.Firefox.ToString());
            _container.RegisterTypeAs<ChromeDriverInitialiser, IDriverInitialiser>(Browser.Chrome.ToString());
            _container.RegisterTypeAs<InternetExplorerDriverInitialiser, IDriverInitialiser>(Browser.InternetExplorer.ToString());
        }

        public IDriverInitialiser GetInitialiser(Browser browser)
        {
            return _container.Resolve<IDriverInitialiser>(browser.ToString());
        }
    }
}