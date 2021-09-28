using SpecFlow.Actions.WindowsAppDriver;
using TechTalk.SpecFlow;

namespace Example.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private static IAppDriverCli? _appDriverCli;

        [BeforeTestRun]
        public static void BeforeTestRun(IAppDriverCli appDriverCli)
        {
            _appDriverCli = appDriverCli;

            _appDriverCli.Start();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _appDriverCli?.Dispose();
        }
    }
}