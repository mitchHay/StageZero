using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuppeteerSharp;
using StageZero.Web;

namespace StageZero.Puppeteer;

public class WebDriver : IDriverWeb
{
    public string Title => _page.GetTitleAsync().Result;

    public string Url => _page.Url;

    public event IDriverWeb.HandleAlert OnAlert;

    private IBrowser _browser;
    private IPage _page;
    private readonly object _lockObj = new();

    public WebDriver(WebDriverOptions options)
    {
        lock (_lockObj)
        {
            InitialisePuppeteer(options).Wait();
        }
    }

    private async Task InitialisePuppeteer(WebDriverOptions options)
    {
        try 
        {
            var browser = options.Browser switch
            {
                Web.Browser.Chrome => SupportedBrowser.Chrome,
                Web.Browser.Edge => SupportedBrowser.Chromium,
                Web.Browser.Firefox => SupportedBrowser.Firefox,
                _ => throw new NotSupportedException($"Provided browser {options.Browser} is not supported."),
            };

            var browserFetcher = PuppeteerSharp.Puppeteer.CreateBrowserFetcher(new BrowserFetcherOptions
            {
                Browser = browser,
            });

            await browserFetcher.DownloadAsync();

            var launchOptions = new LaunchOptions
            {
                Headless = options.Headless,
                Browser = browser,
            };

            _browser = await PuppeteerSharp.Puppeteer.LaunchAsync(launchOptions);
            _page = await _browser.NewPageAsync();

            _page.Dialog += (_, args) => 
            {
                OnAlert.Invoke(this, new PuppeteerAlert(args.Dialog));
            };
        }
        catch (PuppeteerException)
        {
            // Browser is being downloaded, retry.
            await InitialisePuppeteer(options);
        }
    }

    public IDocument Document() => new Document(_page);

    public async Task<IElementWeb> GetElement(string cssSelector)
    {
        var elementHandle = await _page.WaitForSelectorAsync(cssSelector);
        return new WebElement(elementHandle, _page, cssSelector);
    }

    public async Task<IEnumerable<IElementWeb>> GetElements(string cssSelector)
    {
        var elementHandles = await _page.QuerySelectorAllAsync(cssSelector);
        return elementHandles.Select((elementHandle) => new WebElement(elementHandle, _page, cssSelector));
    }

    public INavigate Navigate() => new PuppeteerNavigate(_page);

    public Task Refresh() => _page.ReloadAsync();

    public async Task Terminate()
    {
        await _browser.CloseAsync();
        await _browser.DisposeAsync();
    }

    public IWindow Window() => new Window(_page, _browser);
}
