# SpecFlow.Actions.Android

[![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.Android)](https://www.nuget.org/packages/SpecFlow.Actions.Android/)

This SpecFlow.Action will help you use Appium together with SpecFlow to test your Android application. It handles the lifetime of your app driver and provides easy configuration options for you to set up the environment and driver.

This plugin consumes [SpecFlow.Actions.Appium](https://github.com/SpecFlowOSS/SpecFlow.Actions/tree/main/Plugins/SpecFlow.Actions.Appium)

## Included features

- Lifetime handling of the app driver
- Lifetime handling of the Appium server
- Configuration via ```specflow.actions.json```

## Configuration

You can configure this plugin via the ```specflow.actions.json``` file. Add a new json file to your project and set it to 'copy if newer' so that it is copied to the output directory.

Example:

```json
{
  "Android": {
    "Capabilities": {
      "automationName": "UiAutomator2",
      "avd": "pixel_3a_xl_r_11_0_-_api_30",
      "appPackage": "com.companyname.specflowcalculator",
      "appActivity": "crc6451aad8f0eac5360f.MainActivity"
    },
    "timeout": 60
  },
  "AppiumServer": {
    "LocalAppiumServerRequired": false,
    "ServerAddress": "http://localhost:4723/wd/hub"
  }
}
```

### Capabilities

- Capabilities are specific to the driver/device configuration you are using, so we suggest reading the [Appium documentation](https://appium.io/docs/en/writing-running-appium/caps/). In any case, these capabilities are added to the driver at runtime inside of the ```AppiumOptions``` object.

### Timeout

- The timeout property controls the driver timeout [when the driver is initalised](https://github.com/SpecFlowOSS/SpecFlow.Actions/blob/baaa372693b2d79f71e435bc1ca524f82484235f/Plugins/SpecFlow.Actions.Appium/SpecFlow.Actions.Appium/Driver/DriverFactory.cs)

### Appium Server

- The ```AppiumServer``` object is only required when the execution of the Appium server is handled externally, in which case a server address is required to connect to server successfully. Otherwise, the server is [launched programmatically](https://github.com/SpecFlowOSS/SpecFlow.Actions/blob/main/Plugins/SpecFlow.Actions.Android/SpecFlow.Actions.Android/AndroidRuntimePlugin.cs) and the lifetime is handled by the plugin.

## How to use it

The app driver is started automatically when you try to use the ```AndroidAppDriver``` the first time.
It is closed after the scenario ends.

### AndroidAppDriver

This class gives you direct access to the app driver. Request an instance via context injection and access it via the Current property.

```c#
[Binding]
public class StepImplementation
{
    private AndroidAppDriver _androidAppDriver;

    public StepImplementation(AndroidAppDriver androidAppDriver)
    {
        _androidAppDriver = androidAppDriver;
    }

    [Given("Some Step")]
    public void SomeStep()
    {
        var FirstNumberTextBox = _androidAppDriver.Current.FindElementById("[some id]");
    }
}
```

## How to get it

Add the latest version of the SpecFlow.Actions.Android NuGet Package to your project.

You can see an example of how you can use this plugin in the example folder inside this project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.Android)](https://www.nuget.org/packages/SpecFlow.Actions.Android/)