using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StageZero.Web;

namespace StageZero.Selenium;

public class WebDriver : IDriverWeb
{
    public string Title => string.Empty;
    public string Url => string.Empty;

    private readonly IWebDriver _seleniumDriver;

    public WebDriver(WebDriverOptions options)
    {
        switch(options.Browser)
        {
            case Browser.Chrome:
                var service = ChromeDriverService.CreateDefaultService();
                _seleniumDriver = new ChromeDriver(service);

                break;
        }
    }

    public Task<IElement> GetElement(string cssSelector)
    {
        var element = _seleniumDriver.FindElement(By.CssSelector(cssSelector));
        return null;
    }
}
