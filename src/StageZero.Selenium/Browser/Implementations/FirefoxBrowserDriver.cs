using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace StageZero.Selenium.Browser.Implementations;

internal class FirefoxBrowserDriver : BrowserDriver
{
    internal override IWebDriver CreateWebDriver()
    {
        var arguments = new List<string>();
        var firefoxOptions = new FirefoxOptions();

        if (Options.Headless)
        {
            arguments.Add("-headless");
        }

        firefoxOptions.AddArguments(arguments);

        return new FirefoxDriver(
            FirefoxDriverService.CreateDefaultService()
        );
    }
}
