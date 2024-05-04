using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;

namespace StageZero.Selenium.Browser.Implementations;

internal class SafariBrowserDriver : BrowserDriver
{
    internal override IWebDriver CreateWebDriver()
    {
        var safariOptions = new SafariOptions();
        if (Options.Headless)
        {
            Console.WriteLine("Headless mode is not supported in Safari, learn more at: https://github.com/SeleniumHQ/selenium/issues/5985");
        }

        if (!string.IsNullOrEmpty(Options.EmulatedDeviceName))
        {
            Console.WriteLine("Mobile emulation is not supported in Firefox, please consider using an alternative supported browser (e.g. Chrome or Edge)");
        }

        return new SafariDriver(
            SafariDriverService.CreateDefaultService(),
            safariOptions
        );
    }
}
