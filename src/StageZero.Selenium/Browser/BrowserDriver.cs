using OpenQA.Selenium;
using StageZero.Web;

namespace StageZero.Selenium.Browser;

internal abstract class BrowserDriver
{
    internal WebDriverOptions Options { get; set; }

    internal abstract IWebDriver CreateWebDriver();
}
