using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using StageZero.Selenium.Extensions;
using StageZero.Web;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordsToNumbers;

namespace StageZero.Selenium;

public class WebElement : IElementWeb
{
    private readonly IWebDriver _driver;
    private readonly IWebElement _element;
    private readonly Actions _actions;

    /// <inheritdoc/>
    public string ClassName => _element.GetAttribute("class");

    /// <inheritdoc/>
    public string Id => _element.GetAttribute("id");

    /// <inheritdoc/>
    public string Tag => _element.TagName;

    /// <inheritdoc/>
    public string Text => _element.Text;

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
        return Task.Run(async () =>
        {
            _actions.ClickAndHold(_element)
                    .Perform();

            await Task.Delay(duration);

            _actions.Release()
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

    /// <inheritdoc/>
    public Task<string> GetAttributeValue(string attributeName)
    {
        return Task.Run(() => _element.GetAttribute(attributeName));
    }

    /// <inheritdoc/>
    public Task<IElementWeb> ScrollTo(string cssSelector)
    {
        return Task.Run(async () =>
        {
            var scrollToElement = await _driver.GetElement(cssSelector);
            
            ((IJavaScriptExecutor)_driver).ExecuteScript(
                "arguments[0].scrollTop = arguments[1].offsetTop",
                _element,
                scrollToElement
            );

            return (IElementWeb)new WebElement(_driver, scrollToElement);
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
        var isQwerty = new Regex("[A-Z]").IsMatch(key.ToString()) && key.ToString().Length == 1;
        if (isQwerty)
        {
            return key.ToString();
        }

        // FUNCTIONAL KEYS
        try
        {
            var keysType = typeof(OpenQA.Selenium.Keys);
            var fieldInfo = keysType.GetField(key.ToString());

            return (string)fieldInfo.GetValue(null);
        }
        // NUMERIC KEYS
        catch(Exception)
        {
            var converter = new SimpleReplacementStrategy();
            return converter.ConvertWordsToNumbers(key.ToString());
        }
    }
}