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

    /// <inheritdoc/>
    public string ClassName
    {
        get
        {
            GetAttributeValue("class").Wait();
            return GetAttributeValue("class").Result;
        }
    }

    /// <inheritdoc/>
    public string Id
    {
        get
        {
            GetAttributeValue("id").Wait();
            return GetAttributeValue("id").Result;
        }
    }

    /// <inheritdoc/>
    public bool IsDisplayed
    {
        get
        {
            _locator.IsDisabledAsync().Wait();
            return _locator.IsDisabledAsync().Result;
        }
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public string Text
    {
        get
        {
            _locator.TextContentAsync().Wait();
            return _locator.TextContentAsync().Result;
        }
    }

    public WebElement(ILocator locator, IPage page, string cssSelector)
    {
        _locator = locator;
        _cssSelector = cssSelector;
        _page = page;
    }

    /// <inheritdoc/>
    public async Task Click()
    {
        await _locator.ClickAsync();
    }

    /// <inheritdoc/>
    public async Task ClickAndHold(TimeSpan duration)
    {
        var boundingBox = await _locator.BoundingBoxAsync();

        await _page.Mouse.MoveAsync(boundingBox.X, boundingBox.Y);
        await _page.Mouse.DownAsync();

        // Emulate a hold
        await Task.Delay(duration);

        await _page.Mouse.UpAsync();
    }

    /// <inheritdoc/>
    public async Task DoubleClick()
    {
        await _locator.DblClickAsync();
    }

    /// <inheritdoc/>
    public async Task<string> GetAttributeValue(string attributeName)
    {
        if (attributeName == "value")
        {
            return await _locator.InputValueAsync();
        }

        return await _locator.GetAttributeAsync(attributeName);
    }

    /// <inheritdoc/>
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

        await _locator.PressAsync(string.Join("+", keysToPress));
    }

    /// <inheritdoc/>
    public async Task RightClick()
    {
        await _locator.ClickAsync(new LocatorClickOptions
        {
            Button = MouseButton.Right
        });
    }

    /// <inheritdoc/>
    public async Task Type(string text)
    {
        await _locator.FillAsync(text);
    }

    /// <inheritdoc/>
    public async Task<IElementWeb> ScrollTo(string cssSelector)
    {
        var scrollToElement = _locator.Locator(cssSelector);
        await scrollToElement.ScrollIntoViewIfNeededAsync();

        return new WebElement(scrollToElement, _page, cssSelector);
    }
}
