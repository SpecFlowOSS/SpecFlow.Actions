using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace SpecFlow.Actions.Selenium
{
    public class NoopWebdriver : IWebDriver
    {
        public IWebElement FindElement(By @by)
        {
            throw new System.NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public void Quit()
        {
            throw new System.NotImplementedException();
        }

        public IOptions Manage()
        {
            throw new System.NotImplementedException();
        }

        public INavigation Navigate()
        {
            throw new System.NotImplementedException();
        }

        public ITargetLocator SwitchTo()
        {
            throw new System.NotImplementedException();
        }

        public string Url { get; set; }
        public string Title { get; }
        public string PageSource { get; }
        public string CurrentWindowHandle { get; }
        public ReadOnlyCollection<string> WindowHandles { get; }
    }
}