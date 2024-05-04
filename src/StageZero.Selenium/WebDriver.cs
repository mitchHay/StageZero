using OpenQA.Selenium;
using StageZero.Selenium.Browser;
using StageZero.Selenium.Extensions;
using StageZero.Web;
using System.Threading.Tasks;
using static StageZero.Web.IDriverWeb;

namespace StageZero.Selenium;

public class WebDriver : IDriverWeb
{
    /// <inheritdoc/>
    public string Title => _seleniumDriver.Title;

    /// <inheritdoc/>
    public string Url => _seleniumDriver.Url;


    private readonly IWebDriver _seleniumDriver;

    private bool _alertOpen;

    public event HandleAlert OnAlert;

    public WebDriver(WebDriverOptions options)
    {
        _seleniumDriver = BrowserDriverFactory
            .New
            .Build(options)
            .CreateWebDriver();

        // Run in another thread
        Task.Run(() =>
        {
            // Listen for alerts
            while (true)
            {
                try
                {
                    var alert = _seleniumDriver.SwitchTo().Alert();
                    _alertOpen = true;

                    // Invoke event listener here
                    OnAlert.Invoke(this, new SeleniumAlert(alert));

                    // Switch back to the default page content
                    _seleniumDriver.SwitchTo().DefaultContent();
                }
                // No alert found
                catch (NoAlertPresentException)
                {
                }
                finally
                {
                    _alertOpen = false;
                }
            }
        });
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
        return Task.Run(async () =>
        {
            try
            {
                await WaitForAlertToClose();
            } 
            finally
            {
                _seleniumDriver.Close();
                _seleniumDriver.Quit();
            }
        });
    }

    private async Task WaitForAlertToClose()
    {
        var timeoutMs = 1500;
        var maxAttempts = 5;
        var delayIntervalMs = timeoutMs / maxAttempts;

        while (_alertOpen)
        {
            await Task.Delay(delayIntervalMs);

            if (!_alertOpen || maxAttempts <= 0)
            {
                break;
            }

            maxAttempts -= 1;
        }
    }
}
