using Microsoft.Playwright;
using StageZero.Web;
using System;
using System.Threading.Tasks;

namespace StageZero.Playwright;

public class Document : IDocument
{
    private readonly IPage _page;

    public Document(IPage page)
    {
        _page = page;
    }

    public async Task ExecuteJavaScript(string script)
    {
        await _page.EvaluateAsync(script);
    }

    public async Task<TResult> ExecuteJavaScript<TResult>(string script)
    {
        return await _page.EvaluateAsync<TResult>(script);
    }

    public async Task<IElementWeb> FullscreenElement()
    {
        var fullScreenElement = await ExecuteJavaScript<object>("document.fullscreenElement");
        return null;
    }
}
