using OpenQA.Selenium;
using StageZero.Web;
using System.Threading.Tasks;

namespace StageZero.Selenium;

public sealed class Document : IDocument
{
    private readonly IWebDriver _driver;
    private readonly IJavaScriptExecutor _jsExecutor;

    public Document(IWebDriver driver)
    {
        _driver = driver;
        _jsExecutor = (IJavaScriptExecutor)driver;
    }

    /// <inheritdoc/>
    public Task ExecuteJavaScript(string script, params object[] args)
    {
        return Task.Run(() => _jsExecutor.ExecuteScript(script, args));
    }

    /// <inheritdoc/>
    public Task<TResult> ExecuteJavaScript<TResult>(string script, params object[] args)
    {
        return Task.Run(async () =>
        {
            // If the desired return type is a WebElement or IElementWeb, first we need to
            // resolve it as a Selenium element and return a new WebElement object.
            // Otherwise it will not cast successfully.
            if (typeof(TResult) == typeof(WebElement) || typeof(TResult) == typeof(IElementWeb))
            {
                var webElement = await GetElementFromScript(script);
                return (TResult)webElement;
            }

            return (TResult)_jsExecutor.ExecuteScript(script, args);
        });
    }

    /// <inheritdoc/>
    public Task<IElementWeb> FullscreenElement()
    {
        return GetElementFromScript("return document.fullscreenElement");
    }

    private Task<IElementWeb> GetElementFromScript(string script)
    {
        return Task.Run(async () =>
        {
            OpenQA.Selenium.WebElement element = null;

            var tryCount = 2;
            while (tryCount > 0 && element == null)
            {
                element = await ExecuteJavaScript<OpenQA.Selenium.WebElement>(script);
                tryCount -= 1;

                if (element != null)
                {
                    break;
                }

                await Task.Delay(50);
            }

            if (element == null)
            {
                return null;
            }

            return (IElementWeb)new WebElement(_driver, element);
        });
    }
}

