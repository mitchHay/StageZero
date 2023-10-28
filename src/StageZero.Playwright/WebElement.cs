using Microsoft.Playwright;
using StageZero.Web;
using System;
using System.Collections.Generic;
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
        var boundingBox = await _locator.BoundingBoxAsync();

        await _page.Mouse.MoveAsync(boundingBox.X, boundingBox.Y);
        await _page.Mouse.DownAsync();

        // Emulate a hold
        await Task.Delay(duration);

        await _page.Mouse.UpAsync();
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

    public async Task PressKeys(Keys keys)
    {
        var keysToPress = new List<string>();
        foreach (Keys key in Enum.GetValues(keys.GetType()))
        {
            if (!keys.HasFlag(key) || keysToPress.Contains(key.ToString()))
            {
                continue;
            }

            keysToPress.Add(key.ToString());
        }

        // Press the keys
        foreach (var keyToPress in keysToPress)
        {
            await _page.Keyboard.DownAsync(keyToPress);
        }

        // Release the keys
        foreach (var keyToRelease in keysToPress)
        {
            await _page.Keyboard.UpAsync(keyToRelease);
        }
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
