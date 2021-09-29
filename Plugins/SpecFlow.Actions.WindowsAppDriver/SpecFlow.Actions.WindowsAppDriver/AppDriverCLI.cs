using SpecFlow.Actions.WindowsAppDriver.Configuration;
using System;
using System.Diagnostics;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public class AppDriverCli : IAppDriverCli
    {
        private const string Error = 
            "There was an issue launching WindowsAppDriver.exe, please make sure the correct file path is included in Specflow.Actions.json or that the 'WINDOWS_APP_DRIVER_FILE_PATH' variable is defined";

        private readonly IWindowsAppDriverConfiguration _windowsAppDriverConfiguration;

        private Process? _appDriverProcess;

        public AppDriverCli(IWindowsAppDriverConfiguration windowsAppDriverConfiguration)
        {
            _windowsAppDriverConfiguration = windowsAppDriverConfiguration;
        }

        /// <summary>
        /// Starts the WindowsAppDriver.exe process
        /// </summary>
        public void Start()
        {
            _appDriverProcess = Process.Start((
                _windowsAppDriverConfiguration.WindowsAppDriverPath 
                ?? Environment.GetEnvironmentVariable("WINDOWS_APP_DRIVER_FILE_PATH")) 
                ?? throw new InvalidOperationException(Error));
        }

        /// <summary>
        /// Disposes the WindowsAppDriver.exe process
        /// </summary>
        public void Dispose()
        {
            _appDriverProcess?.Kill();
        }
    }
}