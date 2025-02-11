using PuppeteerSharp;
using PuppeteerSharp.Input;
using StageZero.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StageZero.Puppeteer;

public class WebElement(IElementHandle elementHandle, IPage page, string cssSelector) : IElementWeb
{
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
            elementHandle.IsVisibleAsync().Wait();
            return elementHandle.IsVisibleAsync().Result;
        }
    }

    /// <inheritdoc/>
    public string Tag 
    { 
        get
        {
            var handle = page.WaitForSelectorAsync(cssSelector).Result;
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
            elementHandle.GetPropertyAsync("innerText");
            return elementHandle.GetPropertyAsync("innerText").Result.RemoteObject.Value.ToString();
        }
    }


    /// <inheritdoc/>
    public Task Click() => elementHandle.ClickAsync();

    /// <inheritdoc/>
    public async Task ClickAndHold(TimeSpan duration)
    {
        var boundingBox = await elementHandle.BoundingBoxAsync();

        await page.Mouse.MoveAsync(boundingBox.X, boundingBox.Y);
        await page.Mouse.DownAsync();

        // Emulate a hold
        await Task.Delay(duration);

        await page.Mouse.UpAsync();
    }

    /// <inheritdoc/>
    public Task DoubleClick() => elementHandle.ClickAsync(new ClickOptions
    {
        Count = 2,
        Delay = 150,
    });

    /// <inheritdoc/>
    public async Task<string> GetAttributeValue(string attributeName) 
    {
        var value = await elementHandle.GetPropertyAsync(attributeName);
        return value.RemoteObject.Value.ToString();
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

        await elementHandle.PressAsync(string.Join("+", keysToPress));
    }

    /// <inheritdoc/>
    public async Task RightClick()
    {
        await elementHandle.ClickAsync(new ClickOptions
        {
            Button = MouseButton.Right,
        });
    }

    /// <inheritdoc/>
    public Task Type(string text) => elementHandle.TypeAsync(text);

    /// <inheritdoc/>
    public async Task<IElementWeb> ScrollTo(string cssSelector)
    {
        var scrollToElement = await elementHandle.QuerySelectorAsync(cssSelector);
        await scrollToElement.ScrollIntoViewAsync();

        return new WebElement(scrollToElement, page, cssSelector);
    }

    /// <inheritdoc/>
    public async Task SelectOption(string optionText)
    {
        await elementHandle.SelectAsync([optionText]);
    }

    /// <inheritdoc/>
    public async Task SelectOption(int optionIndex)
    {
        var selectElement = await page.QuerySelectorAsync(cssSelector);
        var options = await selectElement.QuerySelectorAllAsync("option");
        
        // Validate index
        if (optionIndex < 0 || optionIndex >= options.Length)
        {
            throw new ArgumentException($"Index {optionIndex} is out of bounds!");
        }
        
        var optionValue = await options[optionIndex].EvaluateFunctionAsync<string>("option => option.value");
        await selectElement.SelectAsync(optionValue);
    }
}
