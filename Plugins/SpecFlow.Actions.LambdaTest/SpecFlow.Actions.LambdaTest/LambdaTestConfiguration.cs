using SpecFlow.Actions.Configuration;
using SpecFlow.Actions.Selenium.Configuration;

namespace SpecFlow.Actions.LambdaTest;

public class LambdaTestConfiguration : SeleniumConfiguration
{
    private readonly ISpecFlowActionsConfiguration _specFlowActionsConfiguration;

    public LambdaTestConfiguration(ISpecFlowActionsConfiguration specFlowActionsConfiguration) : base(
        specFlowActionsConfiguration)
    {
        _specFlowActionsConfiguration = specFlowActionsConfiguration;
    }

    public string Url =>
        _specFlowActionsConfiguration.Get("selenium:lambdatest:url",
            "http://hub.lambdatest.com/wd/hub/");
}