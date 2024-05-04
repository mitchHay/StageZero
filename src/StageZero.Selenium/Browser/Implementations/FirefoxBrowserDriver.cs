using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
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

        if (!string.IsNullOrEmpty(Options.EmulatedDeviceName))
        {
            Console.WriteLine("Mobile emulation is not supported in Firefox, please consider using an alternative supported browser (e.g. Chrome or Edge)");
        }

        firefoxOptions.AddArguments(arguments);

        return new FirefoxDriver(
            FirefoxDriverService.CreateDefaultService()
        );
    }
}
