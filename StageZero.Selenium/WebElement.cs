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

    public Task Click()
    {
        return Task.Run(() =>
            _element.Click()
        );
    }

    public Task ClickAndHold(TimeSpan duration)
    {
        return Task.Run(() =>
        {
            _actions.ClickAndHold(_element)
                    .Perform();
        });
    }

    public Task DoubleClick()
    {
        return Task.Run(() =>
        {
            _actions.DoubleClick(_element)
                    .Perform();
        });
    }

    public Task RightClick()
    {
        return Task.Run(() =>
        {
            _actions.ContextClick(_element)
                    .Perform();
        });
    }

    public Task Type(string text)
    {
        return Task.Run(() => _element.SendKeys(text));
    }
}