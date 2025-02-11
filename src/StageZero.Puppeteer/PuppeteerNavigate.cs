using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace StageZero.Puppeteer;

public class PuppeteerNavigate(IPage page) : INavigate
{
    public Task Back() => page.GoBackAsync();

    public Task Forward() => page.GoForwardAsync();

    public Task ToUrl(string url) => page.GoToAsync(url);

    public Task ToUrl(Uri uri) => page.GoToAsync(uri.ToString());
}