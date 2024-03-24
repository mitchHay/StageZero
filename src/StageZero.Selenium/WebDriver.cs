using OpenQA.Selenium;
using StageZero.Selenium.Browser;
using StageZero.Selenium.Extensions;
using StageZero.Web;
using System.Threading.Tasks;

namespace StageZero.Selenium;

public class WebDriver : IDriverWeb
{
    /// <inheritdoc/>
    public string Title => _seleniumDriver.Title;

    /// <inheritdoc/>
    public string Url => _seleniumDriver.Url;


    private readonly IWebDriver _seleniumDriver;

    public WebDriver(WebDriverOptions options)
    {
        _seleniumDriver = BrowserDriverFactory
            .New
            .Build(options)
            .CreateWebDriver();
    }

    /// <inheritdoc/>
    public Task<IElementWeb> GetElement(string cssSelector)
    {
        return Task.Run(async () =>
        {
            var element = await _seleniumDriver.GetElement(cssSelector);
            return (IElementWeb)new WebElement(_seleniumDriver, element);
        });
    }

    /// <inheritdoc/>
    public INavigate Navigate()
    {
        return new SeleniumNavigate(_seleniumDriver);
    }

    /// <inheritdoc/>
    public Task Refresh()
    {
        return Task.Run(() => _seleniumDriver.Navigate().Refresh());
    }

    /// <inheritdoc/>
    public Web.IWindow Window()
    {
        return new Window(_seleniumDriver);
    }

    /// <inheritdoc/>
    public IDocument Document()
    {
        return new Document(_seleniumDriver);
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
}
