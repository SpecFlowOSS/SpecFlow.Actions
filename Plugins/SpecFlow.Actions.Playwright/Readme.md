# SpecFlow.Actions.Playwright

[![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.Playwright)](https://www.nuget.org/packages/SpecFlow.Actions.Playwright/)

## Summary

This SpecFlow.Action will help you use [Playwright](https://playwright.dev/) together with SpecFlow. It handles the initialisation and lifetime of your browser, provides some extension methods to work with your page selectors and a configuration that makes it easy to set up the browser instance.

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

Example:

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
    "headless": false
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

## How to use it

The browser is started automatically when you try to use the browser instance for the first time. It is closed after the scenario ends. You can use constructor and/or parameter injection to inject dependencies into your classes, this way the class instances are managed automatically.

### Page object

``` csharp
[Binding]
public class SomePageObjectClass
{
    // Used to interact with the selectors
    private readonly Task<IPage> _page;

    //BrowserDriver resolved automatically
    public SomePageObjectClass(BrowserDriver browserDriver)
    {
        _page = CreatePageAsync(browserDriver.Current);
    }

    private async Task<IPage> CreatePageAsync(Task<IBrowser> browser)
    {
        // Creates a new page instance from the browser driver
        return await (await browser).NewPageAsync();
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
    //SomePageObjectClass resolved automatically
    private readonly SomePageObjectClass _somePageObjectClass;

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

### SelectorExtensions

This static class has extension methods to perform browser interactions

Available Helper Methods:

```csharp
/// Sends a string to the specified selector
public static async Task SendTextAsync(this string selector, IPage page, string keys, PageFillOptions? pageFillOptions = null)

/// Sends individual keystrokes to the specified selector
public static async Task SendKeystrokesAsync(this string selector, IPage page, string keys, PageTypeOptions? pageTypeOptions = null)

/// Sends a click to an element
public static async Task ClickAsync(this string selector, IPage page, PageClickOptions? pageClickOptions = null)

/// Gets the value attribute of an element
public static async Task<string?> GetValueAttributeAsync(this string selector, IPage page, PageInputValueOptions? pageInputValueOptions = null)

/// Waits for the value attribute of an element to not be empty
public static async Task WaitForNonEmptyValue(this string selector, IPage page)

/// Waits for the value attribute of an element to be empty
public static async Task WaitForEmptyValue(this string selector, IPage page)

/// Selects the option from a select element by its value
public static async Task SelectDropdownOptionAsync(this string selector, IPage page, string value, PageSelectOptionOptions? pageSelectOptionOptions = null)

/// Selects the option from a select element by its index
public static async Task SelectDropdownOptionAsync(this string selector, IPage page, int index, PageSelectOptionOptions? pageSelectOptionOptions = null)

```

## How to get it

Add the latest version of the `SpecFlow.Actions.Playwright` NuGet Package to your project.

Latest version: [![Nuget](https://img.shields.io/nuget/v/SpecFlow.Actions.Playwright)](https://www.nuget.org/packages/SpecFlow.Actions.Playwright/)