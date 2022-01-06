using SpecFlow.Actions.Selenium;
using System;
using System.Text.Json.Serialization;

namespace Selenium.Targets.Generation
{
    internal class SpecFlowActionsTargets
    {
        [JsonInclude]
        public SeleniumSpecFlowJsonPart[] Selenium { get; set; } = Array.Empty<SeleniumSpecFlowJsonPart>();
    }
}
