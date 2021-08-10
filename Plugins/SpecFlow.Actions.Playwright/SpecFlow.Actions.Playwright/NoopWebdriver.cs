using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpecFlow.Actions.Playwright
{
    public class NoopWebdriver : IBrowser
    {
        public IReadOnlyList<IBrowserContext> Contexts => throw new NotImplementedException();

        public bool IsConnected => throw new NotImplementedException();

        public string Version => throw new NotImplementedException();

        public event EventHandler<IBrowser> Disconnected = null!;

        public Task CloseAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IBrowserContext> NewContextAsync(BrowserNewContextOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IPage> NewPageAsync(BrowserNewPageOptions? options = null)
        {
            throw new NotImplementedException();
        }
    }
}