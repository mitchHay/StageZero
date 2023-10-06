using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using System;

namespace StageZero.Selenium.Browser.Implementations;

internal class SafariBrowserDriver : BrowserDriver
{
    internal override IWebDriver CreateWebDriver()
    {
        var safariOptions = new SafariOptions();
        if (Options.Headless)
        {
            Console.WriteLine(
                "Headless mode is not supported in Safari :/" + "\n" +
                "Learn more at: https://github.com/SeleniumHQ/selenium/issues/5985"
            );
        }

        return new SafariDriver(
            SafariDriverService.CreateDefaultService(),
            safariOptions
        );
    }
}
