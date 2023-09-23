using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using StageZero.Web;
using System;
using System.Threading.Tasks;

namespace StageZero.Selenium;

public class WebDriver : IDriverWeb
{
    public string Title => string.Empty;
    
    public string Url => _seleniumDriver.Url;

    private readonly IWebDriver _seleniumDriver;
    private readonly WebDriverWait _webDriverWait;

    public WebDriver(WebDriverOptions options)
    {
        switch(options.Browser)
        {
            case Browser.Chrome:
                var chromeService = ChromeDriverService.CreateDefaultService();
                _seleniumDriver = new ChromeDriver(chromeService);
                break;
            case Browser.Firefox:
                var firefoxService = FirefoxDriverService.CreateDefaultService();
                _seleniumDriver = new FirefoxDriver(firefoxService);
                break;
        }

        _webDriverWait = new WebDriverWait(_seleniumDriver, TimeSpan.FromSeconds(5));
    }

    public Task<IElementWeb> GetElement(string cssSelector)
    {
        return Task.Run(() =>
        {
            var element = _webDriverWait.Until(driver => driver.FindElement(By.CssSelector(cssSelector)));
            return (IElementWeb)new WebElement(_seleniumDriver, element);
        });
    }

    public Task GoTo(string url)
    {
        return Task.Run(() => 
            _seleniumDriver.Navigate().GoToUrl(url)
        );
    }

    public Task Terminate()
    {
        return Task.Run(() =>
        {
            _seleniumDriver.Close();
            _seleniumDriver.Quit();
        });
    }
}
