# SpecFlow.Actions.LambdaTest

[![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.LambdaTest)](https://www.nuget.org/packages/SpecFlow.Actions.LambdaTest/)

This SpecFlow.Action will help you use Browserstack and Selenium together with SpecFlow. As an extension of [SpecFlow.Actions.Selenium](https://github.com/SpecFlowOSS/SpecFlow.Actions/tree/main/Plugins/SpecFlow.Actions.Selenium), It handles the lifetime of your browser and provides seamless integration with the Browserstack Automate feature.

## Included Features

- Lifetime handling of Browser
    - Supported Browsers
        - Chrome
        - Firefox
        - Edge
        - Internet Explorer
        - Safari
- Configuration via `specflow.actions.json`

## Configuration

You can configure this plugin via the  `specflow.actions.json`.

Example:

``` json
{
  "selenium": {
    "defaultTimeout": 60,
    "pollingInterval": 5,
    "browser": "firefox",
    "capabilities": {
      "platform": "Windows 10"
    },
    "lambdatest": {
      "url": "http://hub.lambdatest.com/wd/hub/"
    }
  }
}
```

### browser
Supported values:
- `chrome`
- `firefox`
- `internetexplorer`
- `edge`
- 'safari'

### LambdaTest specific configuration

There is no need to specify the browser in the Selenium capabilities. It is taken from the browser configuration value.

#### url

Optional. Can be used if you have another hub Url, than the default one

### Multiple target configurations

This plugin supports tagreting of multiple configurations at runtime. For each configuration you provide, a class will be generated in your feature's code behind file when you build the project. This means that for any given test, the test will be executed against each target.

Example:

```specflow.actions.osxmoterey.safari.json```

``` json
{
  "selenium": {
    "browser": "Safari",
    "capabilities": {
      "platform": "macOS Monterey"
    }
  }
}
```

```specflow.actions.windows11.chrome.json```

``` json
{
  "selenium": {
    "browser": "Chrome",
    "capabilities": {
      "platform": "Windows 11"
    }
  }
}
```

For the example shown above, all tests will execute against both Safari and Chrome. The configuration format of these targets follows the same structure as if you provide a single config in ```specflow.actions.json```

### capabilities
See https://www.lambdatest.com/capabilities-generator/

Capabilities are Selenium 3 Json Wire protocol capabilities.

## How to use it

The browser is started automatically when you try to use the WebDriver the first time.  
It is closed after the scenario ends.

The test result and (in case of failure) reason is sent to browserstack automatically during the test execution.

### BrowserInteractions

This class gives you helper methods to work with the Webdriver. If necessary, it is waiting for completness of the action.

Available Helper Methods:

- `IWebElement WaitAndReturnElement(By elementLocator)`  
  Waits for the element to exist and returns it using the specified element localisation
- `IEnumerable<IWebElement> WaitAndReturnElements(By elementLocator)`  
  Waits for the element(s) to exist and returns them using the specified element localisation
- `void GoToUrl(string url)`  
  Goes to the specified url
- `string GetUrl()`  
  Gets the current URL
- `T? WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted)`  
  Helper method to wait until the expected result is available on the UI
  
Usage:

``` csharp
[Binding]
public class StepImplementation
{
    private IBrowserInteractions _browserInteractions;

    public StepImplementation(IBrowserInteractions browserInteractions)
    {
        _browserInteractions = browserInteractions;
    }

    [Given("")]
    public void SomeStep()
    {
        var currentUrl = _browserInteractions.GetUrl();
    }
}
```

### BrowserDriver

This class gives you direct access to the WebDriver. Request an instance via context injection and access it via the `Current` property.

``` csharp
[Binding]
public class StepImplementation
{
    private BrowserDriver _browserDriver;

    public StepImplementation(BrowserDriver browserDriver)
    {
        _browserDriver = browserDriver;
    }

    [Given("")]
    public void SomeStep()
    {
        var currentUrl = _browserDriver.Current.Url;
    }
}
```

### WebElementExtensions

This class gives to helper extension methods on the WebElements

Available Helper Methods:


- `public static void SendKeysWithClear(this IWebElement webElement, string keys)`  
  Clears and sends keystrokes to the specified web element
- `public static void ClickWithRetry(this IWebElement webElement, int retryCount = 3)`  
  Attempts to click the specified web element, ignoring intercepted clicks for a number of attempts
- `public static SelectElement GetSelectElement(this IWebElement webElement)`  
  Returns a new select element
- `public static void SelectDropdownOptionByIndex(this IWebElement webElement, int index)`  
  Selects an option from a select element by index value
- `public static void SelectDropdownOptionByText(this IWebElement webElement, string text)`  
  Selects an option from a select element by text
- `public static void SelectDropdownOptionByValue(this IWebElement webElement, string value)`  
  Selects an option from a select element by its value
- `public static void SelectRandomDropdownOption(this IWebElement webElement)`  
  Selects a random dropdown option
- `public static IEnumerable<IWebElement> WhereElementsHaveClass(this IEnumerable<IWebElement> webElements, string className)`  
  Finds and returns web elements that contain the specified class name
- `public static IEnumerable<IWebElement> WhereElementsHaveValue(this IEnumerable<IWebElement> webElements, string value)`  
  Finds and returns web elements that have the specified value
- `public static bool HasClass(this IWebElement webElement, string className)`  
  Checks if the web element contains a specified class name
- `public static bool HasValue(this IWebElement webElement, string value)`  
  Checks if the web element contains a specified value from its value attribute
- `public static IEnumerable<IWebElement> WhereElementsAreDisplayed(this IEnumerable<IWebElement> webElements)`  
  Returns only web elements that are displayed 
- `public static IEnumerable<IWebElement> WhereElementsHavePropertyValue(this IEnumerable<IWebElement> webElements, string propertyName, string value)`  
  Returns web elements that have the expected value from a specified property
    

## How to get it

Add the latest version of the `SpecFlow.Actions.LambdaTest` NuGet Package to your project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.LambdaTest)](https://www.nuget.org/packages/SpecFlow.Actions.LambdaTest/)
