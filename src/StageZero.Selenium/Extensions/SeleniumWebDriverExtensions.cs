using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;

namespace StageZero.Selenium.Extensions;

internal static class SeleniumWebDriverExtensions
{
    internal static Task<IWebElement> GetElement(this IWebDriver driver, string cssSelector)
    {
        var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        return Task.Run(() =>
            webDriverWait.Until(driver => driver.FindElement(By.CssSelector(cssSelector)))
        );
    }
}

