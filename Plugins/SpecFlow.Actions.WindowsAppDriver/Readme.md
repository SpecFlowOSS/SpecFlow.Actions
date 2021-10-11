# SpecFlow.Actions.WindowsAppDriver

[![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.WindowsAppDriver)](https://www.nuget.org/packages/SpecFlow.Actions.WindowsAppDriver/)

This SpecFlow.Action will help you use Windows App Driver together with SpecFlow. It handles the lifetime of the Windows App Driver, and the Appium instance created to interact with the application.

## Prerequisites

- Install [Windows App Driver](https://github.com/microsoft/WinAppDriver)
- Enable [Developer Mode](https://docs.microsoft.com/en-us/windows/apps/get-started/enable-your-device-for-development) in Windows settings

## Included Features

- Lifetime handling of Windows App Driver and the application being tested
- Configuration via `specflow.actions.json`
- Automatic screenshot capture

## Configuration

You can configure this plugin by adding a json file into your project named `specflow.actions.json`. Set the file as 'content' and to 'copy if newer'.

Example:

``` json
{
  "windowsAppDriver": {
    "capabilities": {
      "app": "../SpecFlowCalculator/bin/Debug/net5.0-windows/SpecFlowCalculator.exe"
    },
    "WindowsAppDriverPath": "C:/Program Files/Windows Application Driver/WinAppDriver.exe",
    "EnableScreenshots": true
  }
}
```

1. ```windowsAppDriver.capabilities``` - The capabilities used to configure the appium instance. In this case, we have provided the path to the WinForms application being tested (path is combined with the current directory, so you must take this into consideration)

    ```csharp
    if (string.Equals(capability.Key, "app", StringComparison.OrdinalIgnoreCase))
    {
        options.AddAdditionalCapability(capability.Key, Path.Combine(Directory.GetCurrentDirectory(), capability.Value)); 
    }
    ```

2. ```WindowsAppDriverPath``` - The path to the Windows App Driver CLI (must be **absolute**, not relative)

3. ```EnableScreenshots``` - If no value is provided, then this resolves to true. If you want to disable screenshots, then set the value to false. Screenshots will be taken after each step automatically using the following folder structure: ```\TestResults\Screenshots\{yyyy-MM-dd_Hmmss}\{Feature title}\{Scenario title}\{Step name} ({execution status}).png``` e.g.

    ```text
    \TestResults\Screenshots\2021-10-11_171822\Calculator\Add two numbers\the first number is 50 (OK).png
    ```

### Important information

If you do not provide these configuration values, then the next default fallback is to check for the following environment variables:

```csharp
Environment.GetEnvironmentVariable("TEST_SUBJECT_FILE_PATH");
Environment.GetEnvironmentVariable("WINDOWS_APP_DRIVER_EXECUTABLE_PATH")
```

The path to the test subject **is required** either from the configuration value, or the environment variable.

The path to the Windows App Drive CLI is **not required**, so if the configuration value and the environment variable returns ```null``` then the Windows App Drive CLI wont launch. This gives you the oppertunity for the Windows App Driver CLI to be handled via a CI/CD pipeline instead of having to manually install it on the agent.

## How to use it

1. Inject the ```AppDriver``` class into your consuming class using constructor/parameter injection inside of your test project. This will give you access to the current instance of the AppDriver. The lifetime of this object is handled and disposed once no longer required.

    ```csharp
    public class CalculatorFormElements
    {
        private readonly AppDriver _appDriver;

        public CalculatorFormElements(AppDriver appDriver)
        {
            _appDriver = appDriver;
        }

        public WindowsElement FirstNumberTextBox =>
            _appDriver.Current.FindElementByAccessibilityId("textBox_firstNumber");

        public WindowsElement SecondNumberTextBox =>
            _appDriver.Current.FindElementByAccessibilityId("textBox_secondNumber");

        public WindowsElement AddButton =>
            _appDriver.Current.FindElementByAccessibilityId("button_add");

        public WindowsElement SubtractButton =>
            _appDriver.Current.FindElementByAccessibilityId("button_subtract");

        public WindowsElement MultiplyButton =>
            _appDriver.Current.FindElementByAccessibilityId("button_multiply");

        public WindowsElement DivideButton =>
            _appDriver.Current.FindElementByAccessibilityId("button_divide");

        public WindowsElement ResultTextBox =>
            _appDriver.Current.FindElementByAccessibilityId("textBox_result");
    }
    ```

2. The handling of the ```AppDriverCli``` class is abstracted away inside the plugin, so there is no need for you to do anything here. If you do not need the plugin to handle the Windows App Driver CLI, then do not provide the path in the configuration or the environment variable demonstrated below.

    ```csharp
    public void Start()
    {
        var path = _windowsAppDriverConfiguration.WindowsAppDriverPath ??
                  Environment.GetEnvironmentVariable("WINDOWS_APP_DRIVER_EXECUTABLE_PATH") ?? null;

        if (path != null)
        {
            _appDriverProcess = Process.Start(path); 
        }
    }
    ```

## How to get it

Add the latest version of the `SpecFlow.Actions.WindowsAppDriver` NuGet Package to your project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.WindowsAppDriver)](https://www.nuget.org/packages/SpecFlow.Actions.WindowsAppDriver/)
