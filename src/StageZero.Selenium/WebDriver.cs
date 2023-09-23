using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using StageZero.Web;
using System;
using System.Threading.Tasks;

namespace StageZero.Selenium;

public class WebDriver : IDriverWeb
{
    /// <inheritdoc/>
    public string Title => _seleniumDriver.Title;

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public Task<IElementWeb> GetElement(string cssSelector)
    {
        return Task.Run(async () =>
        {
            var element = await GetSeleniumElement(cssSelector);
            return (IElementWeb)new WebElement(_seleniumDriver, element);
        });
    }

    /// <inheritdoc/>
    public Task<IElementWeb> ScrollToElement(string cssSelector)
    {
        return Task.Run(async () =>
        {
            await PerformScroll(cssSelector);
            return await GetElement(cssSelector);
        });
    }

    /// <inheritdoc/>
    public INavigate Navigate()
    {
        return new SeleniumNavigate(_seleniumDriver);
    }

    /// <inheritdoc/>
    public Task Terminate()
    {
        return Task.Run(() =>
        {
            _seleniumDriver.Close();
            _seleniumDriver.Quit();
        });
    }

    private Task<IWebElement> GetSeleniumElement(string cssSelector)
    {
        return Task.Run(() => 
            _webDriverWait.Until(driver => driver.FindElement(By.CssSelector(cssSelector)))
        );
    }

    private Task PerformScroll(string scrollToElementSelector)
    {
        return Task.Run(async () =>
        {
            var scrollToElement = await GetSeleniumElement(scrollToElementSelector);
            new Actions(_seleniumDriver)
                .ScrollToElement(scrollToElement)
                .Perform();
        });
    }
}
