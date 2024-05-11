using Microsoft.Playwright;
using StageZero.Playwright.Extensions;
using StageZero.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using static StageZero.Web.IDriverWeb;

namespace StageZero.Playwright;

public class WebDriver : IDriverWeb
{
    public string Title => _page.TitleAsync().Result;

    public string Url => _page.Url;

    private IPlaywright _playwright;
    private IBrowserContext _browserContext;
    private IPage _page;
    private int _maxInitTries = 2;

    private readonly object lockObj = new();

    public event HandleAlert OnAlert;

    public WebDriver(WebDriverOptions options)
    {
        lock (lockObj)
        {
            Task.Run(async () =>
            {
                try
                {
                    await InitialisePlaywright(options);
                }
                // Install playwright browsers
                catch (PlaywrightException)
                {
                    if (_maxInitTries == 0)
                    {
                        throw new Exception("Failed to install Playwright browsers, surpassed maximum allowed browser initialisation attempts.");
                    }

                    _maxInitTries -= 1;

                    var exitCode = Program.Main(new[] { "install" });
                    if (exitCode != 0)
                    {
                        throw new Exception($"Playwright exited with code {exitCode}");
                    }

                    await InitialisePlaywright(options);
                }
            }).Wait();
        }
    }

    private async Task InitialisePlaywright(WebDriverOptions options)
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = options.Headless
        };

        var contextOptions = string.IsNullOrEmpty(options.EmulatedDeviceName)
            ? new BrowserNewContextOptions()
            : _playwright.Devices[options.EmulatedDeviceName];

        switch (options.Browser)
        {
            case Browser.Chrome:
                launchOptions.Channel = "chrome";

                var chromeBrowser = await _playwright.Chromium.LaunchAsync(launchOptions);
                _browserContext = await chromeBrowser.NewContextAsync(contextOptions);
                _page = await _browserContext.NewPageAsync();

                break;
            case Browser.Firefox:
                var firefoxBrowser = await _playwright.Firefox.LaunchAsync(launchOptions);
                _browserContext = await firefoxBrowser.NewContextAsync(contextOptions);
                _page = await _browserContext.NewPageAsync();

                break;
            case Browser.Safari:
                var safariBrowser = await _playwright.Webkit.LaunchAsync(launchOptions);
                _browserContext = await safariBrowser.NewContextAsync(contextOptions);
                _page = await _browserContext.NewPageAsync();

                break;
            case Browser.Edge:
                launchOptions.Channel = "msedge";

                var edgeBrowser = await _playwright.Chromium.LaunchAsync(launchOptions);
                _browserContext = await edgeBrowser.NewContextAsync(contextOptions);
                _page = await _browserContext.NewPageAsync();

                break;
            default:
                throw new NotSupportedException($"Sorry! Looks like the {options.Browser} isn't supported yet :/ - Feel free to check our github for updates!");
        }

        _page.Dialog += (_, dialog) => 
        {
            OnAlert.Invoke(this, new PlaywrightAlert(dialog));
        };

        await _browserContext.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    /// <inheritdoc/>
    public IDocument Document()
    {
        return new Document(_page);
    }

    /// <inheritdoc/>
    public Task<IElementWeb> GetElement(string cssSelector)
    {
        return Task.Run(() =>
        {
            var element = _page.Locator(cssSelector);
            return (IElementWeb)new WebElement(element, _page, cssSelector);
        });
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<IElementWeb>> GetElements(string cssSelector)
    {
        var locators = await _page.GetAllLocators(cssSelector);
        return locators.Select(locator => new WebElement(locator, _page, cssSelector));
    }

    /// <inheritdoc/>
    public INavigate Navigate()
    {
        return new PlaywrightNavigate(_page);
    }

    /// <inheritdoc/>
    public async Task Refresh()
    {
        await _page.ReloadAsync();
    }

    /// <inheritdoc/>
    public async Task Terminate()
    {
        var dateTime = DateTime.Now;
        await _browserContext.Tracing.StopAsync(new TracingStopOptions
        {
            Path = $"{dateTime:yyyy-mm-dd_hh-mm-ss.ff}_trace.zip"
        });

        await _browserContext.CloseAsync();
        _playwright.Dispose();
    }

    /// <inheritdoc/>
    public IWindow Window()
    {
        return new Window(_page);
    }
}
