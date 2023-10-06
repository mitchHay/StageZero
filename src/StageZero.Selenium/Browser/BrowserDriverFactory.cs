using StageZero.Selenium.Browser.Implementations;
using StageZero.Web;
using System;

namespace StageZero.Selenium.Browser;

internal class BrowserDriverFactory
{
    internal static BrowserDriverFactory New => new();

    internal BrowserDriver Build(WebDriverOptions options)
    {
        var browser = options.Browser;
        BrowserDriver browserDriver;

        switch (browser)
        {
            case Web.Browser.Chrome:
                browserDriver = new ChromeBrowserDriver();
                break;
            case Web.Browser.Firefox:
                browserDriver = new FirefoxBrowserDriver();
                break;
            case Web.Browser.Edge:
                browserDriver = new EdgeBrowserDriver();
                break;
            case Web.Browser.Safari:
                browserDriver = new SafariBrowserDriver();
                break;
            default:
                throw new NotImplementedException(
                    $"Uh oh. Looks like the {browser} browser isn't supported!" + "\n" +
                    "If you'd like supported added, feel free to either contribute to StageZero with an issue and/or PR :)"
                );
        }

        browserDriver.Options = options;
        return browserDriver;
    }
}
