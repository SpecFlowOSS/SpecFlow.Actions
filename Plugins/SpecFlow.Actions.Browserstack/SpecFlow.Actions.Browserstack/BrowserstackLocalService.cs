using BrowserStack;

namespace SpecFlow.Actions.Browserstack
{
    public class BrowserstackLocalService
    {
        private static Local? _instance;
        private static readonly object Lock = new();

        private BrowserstackLocalService()
        {
            // Private constructor for singleton instance
        }

        public static Local GetInstance()
        {
            if (_instance is not null)
            {
                return _instance;
            }

            lock (Lock)
            {
                _instance ??= new Local();
            }

            return _instance;
        }
    }
}