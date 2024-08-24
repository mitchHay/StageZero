using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using StageZero.Web;
using System.Linq;
using StageZero.Puppeteer.Extensions;

namespace StageZero.Puppeteer;

public class WebElement(IElementHandle elementHandle, IPage page, string cssSelector) : IElementWeb
{
    /// <inheritdoc />
    public string Text => elementHandle.GetPropertyAsync("innerText").GetAwaiter().GetResult()
        .JsonValueAsync<string>().GetAwaiter().GetResult();

    /// <inheritdoc />
    public bool IsDisplayed => elementHandle.IsVisibleAsync().GetAwaiter().GetResult();

    /// <inheritdoc />
    public string ClassName => elementHandle.GetPropertyAsync("class").GetAwaiter().GetResult()
        .JsonValueAsync<string>().GetAwaiter().GetResult();

    /// <inheritdoc />
    public string Id => elementHandle.GetPropertyAsync("id").GetAwaiter().GetResult()
        .JsonValueAsync<string>().GetAwaiter().GetResult();

    /// <inheritdoc />
    public string Tag => elementHandle.GetPropertyAsync("tag").GetAwaiter().GetResult()
        .JsonValueAsync<string>().GetAwaiter().GetResult();
    
    /// <inheritdoc />
    public async Task PressKeys(Keys keys)
    {
        await page.Keyboard.SendKeyEvent(keys, KeyPress.Down);
        await page.Keyboard.SendKeyEvent(keys, KeyPress.Up);
    }
    
    /// <inheritdoc />
    public Task Type(string text) => elementHandle.TypeAsync(text);

    /// <inheritdoc />
    public Task Click() => elementHandle.ClickAsync();

    /// <inheritdoc />
    public Task RightClick() => elementHandle.ClickAsync(new ClickOptions
    {
        Button = MouseButton.Right,
    });

    /// <inheritdoc />
    public Task DoubleClick() => elementHandle.ClickAsync(new ClickOptions
    {
        Count = 2,
    });


    /// <inheritdoc />
    public Task ClickAndHold(TimeSpan duration) => elementHandle.ClickAsync(new ClickOptions
    {
        Delay = duration.Milliseconds,
    });

    /// <inheritdoc />
    public Task<string> GetAttributeValue(string attributeName) =>
        elementHandle.EvaluateFunctionAsync<string>($"node => node.{attributeName}");
    
    /// <inheritdoc />
    public async Task<IElementWeb> ScrollTo(string cssSelector)
    {
        var scrollToElement = (await elementHandle.QuerySelectorAllAsync(cssSelector)).FirstOrDefault();
        if (scrollToElement == null)
        {
            throw new Exception($"Failed to find any elements with the css selector {cssSelector}");
        }

        await scrollToElement.ScrollIntoViewAsync();

        return new WebElement(scrollToElement, page, cssSelector);
    }

    /// <inheritdoc />
    public Task SelectOption(string optionText) => elementHandle.SelectAsync(optionText);

    
    /// <inheritdoc />
    public async Task SelectOption(int optionIndex)
    {
        // Find all options
        var options = await elementHandle.QuerySelectorAllAsync($"{cssSelector} option");
        var optionToSelect = options.ElementAt(optionIndex);
        var optionText = await (await optionToSelect.GetPropertyAsync("value")).JsonValueAsync<string>();
        
        await elementHandle.SelectAsync(optionText);
    }
}
