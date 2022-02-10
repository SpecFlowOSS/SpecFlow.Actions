using BrowserStack;
using System.Collections.Generic;

namespace SpecFlow.Actions.Browserstack
{
    public class BrowserstackLocalService
    {
        private static Local? _instance;
        private static bool _isRunning = false;
        private static readonly object InstanceLock = new();
        private static readonly object StartLock = new();

        private static Local BrowserstackLocal
        {
            get => GetInstance();
            set => _instance = value;
        }

        private BrowserstackLocalService()
        {
        }

        private static Local GetInstance()
        {
            if (_instance is not null)
            {
                return _instance;
            }

            lock (InstanceLock)
            {
                _instance ??= new Local();
            }

            return _instance;
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
                    //BrowserstackLocal.start(capabilities);
                    _isRunning = true;
                }
            }
        }
    }
}