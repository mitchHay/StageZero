using Microsoft.Playwright;
using StageZero.Web;
using System;
using System.Threading.Tasks;

namespace StageZero.Playwright;

public class WebElement : IElementWeb
{
    private readonly ILocator _locator;

    public string ClassName => GetAttributeValue("class").Result;

    public string Id => GetAttributeValue("id").Result;

    public string Tag => _locator.EvaluateAsync<string>("e => e.tagName").Result;

    public string Text => _locator.TextContentAsync().Result;

    public WebElement(ILocator locator)
    {
        _locator = locator;
    }

    public async Task Click()
    {
        await _locator.ClickAsync();
    }

    public async Task ClickAndHold(TimeSpan duration)
    {
        await _locator.ClickAsync(new LocatorClickOptions
        {
            Delay = duration.Milliseconds
        });
    }

    public async Task DoubleClick()
    {
        await _locator.DblClickAsync();
    }

    public async Task<string> GetAttributeValue(string attributeName)
    {
        return await _locator.GetAttributeAsync(attributeName);
    }

    public Task PressKeys(Keys keys)
    {
        throw new NotImplementedException();
    }

    public async Task RightClick()
    {
        await _locator.ClickAsync(new LocatorClickOptions
        {
            Button = MouseButton.Right
        });
    }

    public async Task Type(string text)
    {
        await _locator.FillAsync(text);
    }
}
