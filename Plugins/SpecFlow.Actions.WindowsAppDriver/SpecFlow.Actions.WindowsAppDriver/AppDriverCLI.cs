using SpecFlow.Actions.Appium.Configuration.WindowsAppDriver;
using System;
using System.Diagnostics;

namespace SpecFlow.Actions.WindowsAppDriver
{
    public class AppDriverCli : IAppDriverCli
    {
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
            var path = _windowsAppDriverConfiguration.WindowsAppDriverPath ??
                       Environment.GetEnvironmentVariable("WINDOWS_APP_DRIVER_EXECUTABLE_PATH") ?? null;

            if (path != null)
            {
                _appDriverProcess = Process.Start(path, _windowsAppDriverConfiguration.WindowsAppDriverPort != null ? _windowsAppDriverConfiguration.WindowsAppDriverPort.ToString() : "");
            }
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