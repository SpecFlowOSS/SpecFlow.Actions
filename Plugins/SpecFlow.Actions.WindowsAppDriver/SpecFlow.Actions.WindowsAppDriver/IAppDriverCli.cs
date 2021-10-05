using System;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public interface IAppDriverCli : IDisposable
    {
        void Start();
    }
}