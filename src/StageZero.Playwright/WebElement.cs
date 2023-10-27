using Microsoft.Playwright;
using StageZero.Web;
using System;
using System.Threading.Tasks;

namespace StageZero.Playwright;

public class WebElement : IElementWeb
{
    private readonly ILocator _locator;
    private readonly string _cssSelector;
    private readonly IPage _page;

    public string ClassName => GetAttributeValue("class").Result;

    public string Id => GetAttributeValue("id").Result;

    public string Tag 
    { 
        get
        {
            var handle = _page.WaitForSelectorAsync(_cssSelector).Result;
            var tagNameProp = handle.GetPropertyAsync("tagName").Result;
            var tagNameValue = tagNameProp.JsonValueAsync<string>().Result;

            return tagNameValue.ToLower();
        } 
    }

    public string Text => _locator.TextContentAsync().Result;

    public WebElement(ILocator locator, IPage page, string cssSelector)
    {
        _locator = locator;
        _cssSelector = cssSelector;
        _page = page;
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
        if (attributeName == "value")
        {
            return await _locator.InputValueAsync();
        }

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
