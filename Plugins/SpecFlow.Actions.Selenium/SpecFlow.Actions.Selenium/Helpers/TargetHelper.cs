using SpecFlow.Actions.Selenium.Configuration;
using System;

namespace SpecFlow.Actions.Selenium.Helpers
{
    public static class TargetHelper
    {
        // Gets the next target configuration randomly
        public static ITargetConfiguration GetNextTarget(ITargetConfiguration[] targets) => targets[new Random().Next(0, targets.Length)];
    }
}