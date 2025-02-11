using PuppeteerSharp;
using StageZero.Web;
using System.Threading.Tasks;

namespace StageZero.Puppeteer;

public class Document(IPage page) : IDocument
{
    public async Task ExecuteJavaScript(string script, params object[] args)
    {
        await page.EvaluateFunctionAsync(script, args);
    }

    public async Task<TResult> ExecuteJavaScript<TResult>(string script, params object[] args)
    {
        return await page.EvaluateFunctionAsync<TResult>(script, args);
    }

    public async Task<IElementWeb> FullscreenElement()
    {
        var fullScreenElement = await ExecuteJavaScript<object>("document.fullscreenElement");
        return null;
    }
}