using System.Collections;
using System.Collections.Generic;
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
    /// Gets n amount of elements given the provided <see cref="cssSelector"/>
    /// </summary>
    /// <param name="cssSelector">The css selector used to find the elements</param>
    /// <returns>An <see cref="IEnumerable"/> of <see cref="IElementWeb"/> instances.</returns>
    Task<IEnumerable<IElementWeb>> GetElements(string cssSelector);

    /// <summary>
    /// Invoke a browser level navigation event
    /// </summary>
    /// <returns>A new <see cref="INavigate"/> instance.</returns>
    public INavigate Navigate();

    /// <summary>
    /// Refresh the current web page
    /// </summary>
    public Task Refresh();

    /// <summary>
    /// The browser window 
    /// </summary>
    /// <returns>A new <see cref="IWindow"/> instance.</returns>
    public IWindow Window();

    /// <summary>
    /// The browser document
    /// </summary>
    /// <returns>A new <see cref="IDocument"/> instance.</returns>
    public IDocument Document();

    /// <summary>
    /// Terminate the current <see cref="IDriverWeb"/> instance.
    /// </summary>
    public Task Terminate();

    /// <summary>
    /// The <see cref="OnAlert"/> delegate handler.
    /// </summary>
    public delegate void HandleAlert(object sender, IAlert alert);

    /// <summary>
    /// An event handler used to subscribe to alert open events in the browser.
    /// </summary>
    public event HandleAlert OnAlert;
}
