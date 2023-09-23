using System.Threading.Tasks;

namespace StageZero.Web;

public interface IDriverWeb : IDriver 
{
    /// <summary>
    /// The current page title
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// The current browser URL
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Get the specified element from the HTML DOM
    /// </summary>
    /// <param name="cssSelector">The css selector used to find the element</param>
    /// <returns>The targeted <see cref="IElementWeb"/> instance.</returns>
    Task<IElementWeb> GetElement(string cssSelector);

    /// <summary>
    /// Scroll to and get the specified element
    /// </summary>
    /// <param name="cssSelector">The element to scroll to</param>
    /// <returns>The targeted <see cref="IElementWeb"/> instance.</returns>
    Task<IElementWeb> ScrollToElement(string cssSelector);

    /// <summary>
    /// Invoke a browser level navigation event
    /// </summary>
    /// <returns>A new <see cref="INavigate"/> instance.</returns>
    public INavigate Navigate();

    /// <summary>
    /// Terminate the current <see cref="IDriverWeb"/> instance.
    /// </summary>
    public Task Terminate();
}
