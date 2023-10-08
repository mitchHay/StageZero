using System.Threading.Tasks;

namespace StageZero.Web;

public interface IDocument
{
    /// <summary>
    /// The documents fullscreen element, if defined the browser is currently fullscreened
    /// </summary>
    /// <returns>The current documents fullscreen <see cref="IElementWeb"/></returns>
    public Task<IElementWeb> FullscreenElement();

    /// <summary>
    /// Execute JavaScript in the current window
    /// </summary>
    /// <param name="script">The JavaScript to execute</param>
    public Task ExecuteJavaScript(string script);

    /// <summary>
    /// Execute JavaScript in the current window
    /// </summary>
    /// <typeparam name="TResult">The type of object being returned from the provided JavaScript</typeparam>
    /// <param name="script">The JavaScript to execute</param>
    /// <returns>The desired object</returns>
    public Task<TResult> ExecuteJavaScript<TResult>(string script);
}
