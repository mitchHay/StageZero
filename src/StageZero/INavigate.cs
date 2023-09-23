using System;
using System.Threading.Tasks;

namespace StageZero;

public interface INavigate
{
    /// <summary>
    /// Navigate to a URL
    /// </summary>
    /// <param name="url">The url to navigate to (e.g. https://google.com/)</param>
    Task ToUrl(string url);

    /// <summary>
    /// Navigate to a URL
    /// </summary>
    /// <param name="uri">The uri to navigate to e.g.<code>new Uri("https://google.com/")</code></param>
    Task ToUrl(Uri uri);

    /// <summary>
    /// Invoke a browser back event
    /// </summary>
    Task Back();

    /// <summary>
    /// Invoke a browser forward event
    /// </summary>
    Task Forward();
}
