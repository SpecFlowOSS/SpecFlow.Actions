# SpecFlow.Actions.Selenium

[![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.Selenium)](https://www.nuget.org/packages/SpecFlow.Actions.Selenium/)

This SpecFlow.Actions will help you by using Selenium together with SpecFlow. It handles the lifetime of your browser and provides easy configuration and helper methods to interact with Selenium.

## Included Features

- Lifetime handling of Browser
    - Supported Browser
        - Chrome
        - Firefox
        - Edge
        - Internet Explorer
- Configuration via `specflow.actions.json`
- Helper Methods for WebDriver
- Helper Extension Methods for WebElement

## Configuration

You can configure this plugin via the  `specflow.action.json`.

Example:

``` json
{
  "selenium": {
    "browser": "chrome",
    "arguments": [
      "--start-maximized",
      "--incognito"
    ],
    "capabilities" : 
    [
        "some_capability": "the value",
        "some_other_capability": "also a value"
    ],
    "defaultTimeout": 60,
    "pollingInterval": 5 
  }
}
```

### browser
Supported values:
- chrome
- firefox
- internetexplorer
- edge

## How to use it

The browser is started automatically when you try to use the WebDriver the first time.  
It is closed after the scenario ends.

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

Add the latest version of the `SpecFlow.Actions.Selenium` NuGet Package to your project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.Selenium)](https://www.nuget.org/packages/SpecFlow.Actions.Selenium/)
