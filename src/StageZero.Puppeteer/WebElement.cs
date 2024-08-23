using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using StageZero.Web;

namespace StageZero.Puppeteer;

public class WebElement(IElementHandle elementHandle) : IElementWeb
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
    public Task PressKeys(Keys keys)
    {
        throw new NotImplementedException();
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
    public Task<IElementWeb> ScrollTo(string cssSelector)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task SelectOption(string optionText) => elementHandle.SelectAsync(optionText);

    
    /// <inheritdoc />
    public Task SelectOption(int optionIndex)
    {
        throw new NotImplementedException();
    }
}
