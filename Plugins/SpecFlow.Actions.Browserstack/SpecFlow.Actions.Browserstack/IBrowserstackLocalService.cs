using System;

namespace SpecFlow.Actions.Browserstack
{
    public interface IBrowserstackLocalService : IDisposable
    {
        void Start();
    }
}