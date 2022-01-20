using SpecFlow.Actions.Selenium.DriverOptions;
using System.Collections.Generic;

namespace SpecFlow.Actions.Selenium
{
    public interface IOptionsConfigurator
    {
        void Add(IOptionsWrapper optionsWrapper, Dictionary<string, string>? capabilities = null, string[]? args = null);
    }
}