using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace StageZero.Selenium.Browser.Implementations;

internal class ChromeBrowserDriver : BrowserDriver
{
    internal override IWebDriver CreateWebDriver()
    {
        var arguments = new List<string>
        {
            "--disable-gpu",
            "--disable-dev-shm-usage"
        };

        var chromeOptions = new ChromeOptions();
        if (Options.Headless)
        {
            // Use the new headless mode
            // Ref: https://developer.chrome.com/articles/new-headless/
            arguments.Add("--headless=new");
        }

        return new ChromeDriver(
            ChromeDriverService.CreateDefaultService(),
            chromeOptions
        );
    }
}
