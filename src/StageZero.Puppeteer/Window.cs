using PuppeteerSharp;
using StageZero.Web;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace StageZero.Puppeteer;

public sealed class Window(IPage page, IBrowser browser) : IWindow
{
    /// <inheritdoc/>
    public string CurrentHandle => page.GetTitleAsync().Result;

    /// <inheritdoc/>
    public IEnumerable<string> Handles => browser.PagesAsync().Result.Select(p => p.GetTitleAsync().Result);

    /// <inheritdoc/>
    public Size Size => new(page.Viewport.Width, page.Viewport.Height);

    /// <inheritdoc/>
    public async Task Fullscreen()
    {
        await page.SetViewportAsync(new ViewPortOptions
        {
            Width = 0,
            Height = 0,
        });
    }

    /// <inheritdoc/>
    public async Task SetSize(int width, int height)
    {
        await page.SetViewportAsync(new ViewPortOptions
        {
            Width = width,
            Height = height,
        });
    }
}
