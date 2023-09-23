using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using StageZero.Web;
using System;
using System.Threading.Tasks;

namespace StageZero.Selenium;

public class WebElement : IElementWeb
{
    private readonly IWebDriver _driver;
    private readonly IWebElement _element;
    private readonly Actions _actions;

    public WebElement(IWebDriver driver, IWebElement element)
    {
        _driver = driver;
        _element = element;
        _actions = new Actions(_driver);
    }

    /// <inheritdoc/>
    public Task Click()
    {
        return Task.Run(() => _element.Click());
    }

    /// <inheritdoc/>
    public Task ClickAndHold(TimeSpan duration)
    {
        return Task.Run(() =>
        {
            _actions.ClickAndHold(_element)
                    .Perform();
        });
    }

    /// <inheritdoc/>
    public Task DoubleClick()
    {
        return Task.Run(() =>
        {
            _actions.DoubleClick(_element)
                    .Perform();
        });
    }

    /// <inheritdoc/>
    public Task RightClick()
    {
        return Task.Run(() =>
        {
            _actions.ContextClick(_element)
                    .Perform();
        });
    }

    /// <inheritdoc/>
    public Task Type(string text)
    {
        return Task.Run(() => _element.SendKeys(text));
    }

    /// <inheritdoc/>
    public Task PressKeys(Web.Keys keys)
    {
        return Task.Run(async () => 
        {
            var keyDownActions = new Actions(_driver);
            
            // Perform KeyDown actions
            InvokeActionOnKey(keys, (key) => keyDownActions.KeyDown(WebKeysToString(key)));
            keyDownActions.Perform();

            // We should wait ~50ms so that we mimic a "user press"
            await Task.Delay(50);

            // Perform KeyUp actions
            var keyUpActions = new Actions(_driver);
            InvokeActionOnKey(keys, (key) => keyUpActions.KeyUp(WebKeysToString(key)));
            keyUpActions.Perform();
        });
    }

    private void InvokeActionOnKey(Web.Keys keys, Action<Web.Keys> actionToInvoke)
    {
        foreach (Web.Keys key in Enum.GetValues(keys.GetType()))
        {
            if (!keys.HasFlag(key))
            {
                continue;
            }

            actionToInvoke.Invoke(key);
        }
    }

    private string WebKeysToString(Web.Keys key)
    {
        return (string)typeof(OpenQA.Selenium.Keys).GetField(key.ToString()).GetValue(null);
    }
}