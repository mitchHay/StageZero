using Microsoft.Playwright;
using StageZero.Web;
using System;
using System.Threading.Tasks;

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
                var browser = await _playwright.Chromium.LaunchAsync(launchOptions);
                _browserContext = await browser.NewContextAsync(contextOptions);
                _page = await _browserContext.NewPageAsync();

                break;
            default:
                throw new NotSupportedException($"Sorry! Looks like the {options.Browser} isn't supported yet :/ - Feel free to check our github for updates!");
        }

        await _browserContext.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    public IDocument Document()
    {
        return new Document(_page);
    }

    public Task<IElementWeb> GetElement(string cssSelector)
    {
        return Task.Run(() =>
        {
            var element = _page.Locator(cssSelector);
            return (IElementWeb)new WebElement(element);
        });
    }

    public INavigate Navigate()
    {
        return new PlaywrightNavigate(_page);
    }

    public async Task Refresh()
    {
        await _page.ReloadAsync();
    }

    public Task<IElementWeb> ScrollToElement(string cssSelector)
    {
        throw new System.NotImplementedException();
    }

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

    public IWindow Window()
    {
        throw new System.NotImplementedException();
    }
}
