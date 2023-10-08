using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;

namespace StageZero.Selenium.Browser.Implementations;

internal class EdgeBrowserDriver : BrowserDriver
{
    internal override IWebDriver CreateWebDriver()
    {
        var arguments = new List<string>();
        var options = new EdgeOptions();

        if (Options.Headless)
        {
            arguments.Add("headless");
        }

        if (!string.IsNullOrEmpty(Options.EmulatedDeviceName))
        {
            options.EnableMobileEmulation(Options.EmulatedDeviceName);
        }

        options.AddArguments(arguments);

        return new EdgeDriver(
            EdgeDriverService.CreateDefaultService(),
            options
        );
    }
}
