---
type: test automation
language: CSharp
libraries_needed: SpecFlow
---

# Test Output

This code snippets shows you how to bring your custom test output to the unit test runner of your choosing. It supports NUnit, MSTest, xUnit and SpecFlow+ Runner.

## Code Snippet

### Constructor

``` csharp
public StepImplementation(ISpecFlowOutputHelper specFlowOutputHelper)
{
    _specFlowOutputHelper = specFlowOutputHelper;
}
```

### Usage
``` csharp
_specFlowOutputHelper.WriteLine("your message");
```

### Can be used in

- Hooks
    - Before/After- Scenario
    - Before/After- ScenarioBlock
    - Before/After- Step
- Step Bindings
- every class you request via Context- Injection


## Documentation

Here comes some explanation how to use the code snippet