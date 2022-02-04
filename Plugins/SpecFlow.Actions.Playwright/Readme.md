# SpecFlow.Actions.Playwright

[![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.Playwright)](https://www.nuget.org/packages/SpecFlow.Actions.Playwright/)

## Summary

This SpecFlow.Action will help you use [Playwright](https://playwright.dev/) together with SpecFlow. It handles the initialisation and lifetime of your browser, provides methods to work with your page selectors and a configuration that makes it easy to set up the browser instance.

### Setup

You will need to read through the [setup process for Playwright](https://playwright.dev/dotnet/docs/intro/) so that your environment is ready for execution.

## Included Features

- Lifetime handling of Browser
    - Supported Browsers
        - Chromium
        - Firefox
        - Edge
        - Chrome
- Configuration via `specflow.actions.json`

## Configuration

You can configure this plugin via the  `specflow.actions.json`.

``` json
{
  "playwright": {
    "browser": "edge",
    "args": [
      "arg1",
      "arg2",
      "arg3"
    ],
    "defaultTimeout": 60,
    "headless": false,
    "slowmo": 100,
    "traceDir": "traces"
  }
}
```

### browser
Supported values:
- `chrome`
- `firefox`
- `chromium`
- `edge`

### arguments

Supported broswer arguments can be defined from the configuration, these are browser specific.

### defaultTimeout

Expressed in seconds, the maximum time the browser will wait for interactions before failing.

### headless

Whether the browser should run in headless mode or not.

### slowmo

Optional. Expressed in miliseconds. If present, will reflect Playwright's slowmo functionality, which determines the time between each individual actions are made, like clicking or typing.

### traceDir

Optional. If [specified](https://playwright.dev/docs/api/class-browsertype#browser-type-launch-option-traces-dir), traces are saved into this directory. 
See also [record traces](https://playwright.dev/dotnet/docs/trace-viewer#recording-a-trace).

## How to use it

The browser is started automatically when you try to use the browser instance for the first time. It is closed after the scenario ends. You can use constructor and/or parameter injection to inject dependencies into your classes, this way the class instances are managed automatically.

### Page object

``` csharp
public class BasePage
{
    public readonly Task<IPage> _page;

    // BrowserDriver resolved automatically
    public BasePage(BrowserDriver browserDriver)
    {
        // Assignes the page instance
        _page = CreatePageAsync(browserDriver.Current);
    }

    private async Task<IPage> CreatePageAsync(Task<IBrowser> browser)
    {
        // Creates a new page instance
        return await (await browser).NewPageAsync();
    }
}

public class SomePageObjectClass : BasePage
{
    // Used to interact with the selectors
    private Interactions _interactions;

    public CalculatorPageObject(BrowserDriver browserDriver) : base(browserDriver)
    {
        // Creates the Interactions instance using the page object
        _interactions = new Interactions(_page);
    }

    public async Task FillOutSomeInput(string str)
    {
        //Enter text
        await FirstNumberFieldSelector.SendTextAsync(await _page, str);
    }
}
```

### Step implementation

``` csharp
[Binding]
public sealed class CalculatorStepDefinitions
{
    private readonly SomePageObjectClass _somePageObjectClass;

    //SomePageObjectClass resolved automatically
    public CalculatorStepDefinitions(SomePageObjectClass somePageObjectClass)
    {
        _somePageObjectClass = somePageObjectClass;
    }

    [Given("something (.*)")]
    public async Task GivenSomething(string value)
    {
        await _somePageObjectClass.FillOutSomeInput(value);
    }
}
```

### Interactions

This class has methods to perform browser interactions per page instance

Available Helper Methods:

```csharp
/// Navigates to the specified URL
public async Task GoToUrl(string url)

/// Gets the current URL
public async Task<string?> GetUrl()

/// Sends a string to the specified selector
public async Task SendTextAsync(string selector, string keys, PageFillOptions? pageFillOptions = null)

/// Sends individual keystrokes to the specified selector
public async Task SendKeystrokesAsync(string selector, string keys, PageTypeOptions? pageTypeOptions = null)

/// Sends a click to an element
public async Task ClickAsync(string selector, PageClickOptions? pageClickOptions = null)

/// Gets the value attribute of an element
public async Task<string?> GetValueAttributeAsync(string selector, PageInputValueOptions? pageInputValueOptions = null)

/// Waits for the value attribute of an element to not be empty
public async Task WaitForNonEmptyValue(string selector)

/// Waits for the value attribute of an element to be empty
public async Task WaitForEmptyValue(string selector)

/// Selects the option from a select element by its value
public async Task SelectDropdownOptionAsync(string selector, string value, PageSelectOptionOptions? pageSelectOptionOptions = null)

/// Selects the option from a select element by its index
public async Task SelectDropdownOptionAsync(string selector, int index, PageSelectOptionOptions? pageSelectOptionOptions = null)
```

## How to get it

Add the latest version of the `SpecFlow.Actions.Playwright` NuGet Package to your project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.Playwright)](https://www.nuget.org/packages/SpecFlow.Actions.Playwright/)
