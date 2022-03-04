using BrowserStack;
using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.Browserstack
{
    public class BrowserstackLocalService
    {
        private static bool _isRunning = false;
        private static readonly object StartLock = new();

        private static Lazy<Local>? _browserstackLocal;

        private BrowserstackLocalService()
        {
            _browserstackLocal = new Lazy<Local>(() => new Local());
        }

        public static void Start(List<KeyValuePair<string, string>> capabilities)
        {
            if (_isRunning)
            {
                return;
            }

            lock (StartLock)
            {
                if (!_isRunning)
                {
                    _browserstackLocal?.Value.start(capabilities);
                    _isRunning = true;
                }
            }
        }

        public static void Stop()
        {
            if (_isRunning)
            {
                _browserstackLocal?.Value.stop(); 
            }
        }
    }
}