using System;
using System.Collections.Generic;

namespace SpecFlow.Actions.LambdaTest
{
    public class LambdaTestLocalService
    {
        //private static bool _isRunning = false;
        //private static readonly object StartLock = new();

        //private static Lazy<Local>? _browserstackLocal;

        private LambdaTestLocalService()
        {
            //_browserstackLocal = new Lazy<Local>(() => new Local());
        }

        public static void Start(List<KeyValuePair<string, string>> capabilities)
        {
            //if (_isRunning)
            //{
            //    return;
            //}

            //lock (StartLock)
            //{
            //    if (!_isRunning)
            //    {
            //        _browserstackLocal?.Value.start(capabilities);
            //        _isRunning = true;
            //    }
            //}
        }

        public static void Stop()
        {
            //if (_isRunning)
            //{
            //    _browserstackLocal?.Value.stop(); 
            //}
        }
    }
}