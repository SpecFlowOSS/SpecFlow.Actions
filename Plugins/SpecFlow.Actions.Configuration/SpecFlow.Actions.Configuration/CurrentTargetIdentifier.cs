using BoDi;
using System;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Configuration
{
    public class CurrentTargetIdentifier
    {
        private readonly ObjectContainer _objectContainer;
        
        public CurrentTargetIdentifier(ObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            
        }

        private ObjectContainer? Container
        {
            get
            {
                try
                {
                    return _objectContainer.Resolve<ObjectContainer>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        public string? Name
        {
            get
            {
                var scenarioContext = Container?.Resolve<ScenarioContext>();

                if (scenarioContext is not null && scenarioContext.ContainsKey("__SpecFlowActionsConfigurationTarget"))
                {
                    return (string)scenarioContext["__SpecFlowActionsConfigurationTarget"];
                }

                return null;
            }
        }
    }
}