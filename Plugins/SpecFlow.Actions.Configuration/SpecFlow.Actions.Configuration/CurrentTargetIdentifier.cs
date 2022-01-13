using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Configuration;

public class CurrentTargetIdentifier
{
    private readonly ScenarioContext _scenarioContext;

    public CurrentTargetIdentifier(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    public string? Name
    {
        get
        {
            if (_scenarioContext.ContainsKey("__SpecFlowActionsConfigurationTarget"))
            {
                return (string)_scenarioContext["__SpecFlowActionsConfigurationTarget"];
            }

            return null;
        }
    }
}