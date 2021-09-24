using SpecFlow.Actions.WindowsAppDriver;
using TechTalk.SpecFlow;

namespace SpecFlowCalculator.Specs.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun(AppDriverCli appDriverCli)
        {
            appDriverCli.Start();
        }

        [AfterTestRun]
        public static void AfterTestRun(AppDriverCli appDriverCli)
        {
            appDriverCli.Dispose();
        }
    }
}
