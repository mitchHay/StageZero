using Microsoft.Playwright;
using StageZero.Web;
using System.Threading.Tasks;

namespace StageZero.Playwright;

public class Document : IDocument
{
    private readonly IPage _page;

    public Document(IPage page)
    {
        _page = page;
    }

    public async Task ExecuteJavaScript(string script, params object[] args)
    {
        await _page.EvaluateAsync(ConvertToPlaywrightJS(script), args);
    }

    public async Task<TResult> ExecuteJavaScript<TResult>(string script, params object[] args)
    {
        return await _page.EvaluateAsync<TResult>(ConvertToPlaywrightJS(script), args);
    }

    public async Task<IElementWeb> FullscreenElement()
    {
        var fullScreenElement = await ExecuteJavaScript<object>("document.fullscreenElement");
        return null;
    }

    private string ConvertToPlaywrightJS(string script)
    {
        return "async () => {" + script + "}";
    }
}
