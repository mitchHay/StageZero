# StageZero

## What is StageZero?

StageZero is an automated UI testing library for the web that allows you to use whatever underlying WebDriver you wish (e.g. Selenium, or Playwright) to steer the browser.

### Why?

StageZero puts flexibility and developer experience first. Want to change to a different UI testing framework (e.g. Selenium -> Playwright)? You can with next to no refactoring required in your codebase. The less time you have to think about steering the browser, the more time you can invest in your tests. StageZero makes it easy to get started with next to no initial setup required.


## Usage

```csharp
public class Test
{
    private IDriverWeb _driver;

    [SetUp]
    public void BeforeEach()
    {
        _driver = DriverBuilder.Initialise(new DriverOptions());
    }
}
```