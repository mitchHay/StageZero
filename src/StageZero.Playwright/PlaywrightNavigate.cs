using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace StageZero.Playwright;

public class PlaywrightNavigate : INavigate
{
    private readonly IPage _page;

    public PlaywrightNavigate(IPage page)
    {
        _page = page;
    }

    public async Task Back()
    {
        await _page.GoBackAsync();
    }

    public async Task Forward()
    {
        await _page.GoForwardAsync();
    }

    public async Task ToUrl(string url)
    {
        await _page.GotoAsync(url);
    }

    public async Task ToUrl(Uri uri)
    {
        await ToUrl(uri.ToString());
    }
}

