# StageZero

![Build](https://github.com/mitchHay/StageZero/actions/workflows/build.yml/badge.svg?branch=main)
[![CodeQL](https://github.com/mitchHay/StageZero/actions/workflows/github-code-scanning/codeql/badge.svg?branch=main)](https://github.com/mitchHay/StageZero/actions/workflows/github-code-scanning/codeql)

> **Please note**
>
> This project is a work-in-progress, some features may be missing and you may experience some bugs.
> If you do experience any issues, please raise them [here](https://github.com/mitchHay/StageZero/issues/new).

## What is StageZero?

StageZero is an automated UI testing library for the web that allows you to use whatever underlying WebDriver you wish (e.g. Selenium, or Playwright) to steer the browser.

### Why?

StageZero puts flexibility and developer experience first. Want to change to a different UI testing framework (e.g. Selenium -> Playwright)? You can with next to no refactoring required in your codebase. The less time you have to think about steering the browser, the more time you can invest in your tests. StageZero makes it easy to get started with next to no initial setup required.

## Usage

### Registering a DriverBuilder

Before writing any tests, first you have to register a `IDriverBuilder` instance globally (this only has to be done once!). For example, if you're wanting to use our `Selenium` implementation with `NUnit` you can do:

```csharp
[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public class BeforeEverything()
    {
        DriverBuilder.Register<WebDriverBuilder>();
    }
}
```

Boom. That's it, you can start writing your tests! 

### Writing a test

`StageZero` handles all of the underlying logic required to create, navigate, interact with elements, and terminate drivers. All you have to do is write your test, no more managing frameworks! Following on from the example provided above, you can write a test for the web like:

```csharp
public class Test
{
    private IDriverWeb _driver;

    [SetUp]
    public void BeforeEach()
    {
        _driver = DriverBuilder.Create(new WebDriverOptions());
    }

    [Test]
    public Task NavigateToGoogle()
    {
        await _driver.Navigate().ToUrl("https://google.com");
    }

    [TearDown]
    public Task AfterEach()
    {
        await _driver.Terminate();
    }
}
```

If you ever want to change to a different underlying driver (e.g. Playwright), all you have to do is swap out the registered builder for your desired UI testing frameworks implementation. All of your existing test logic 110% **will** work with the updated builder.
