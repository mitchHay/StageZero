using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace StageZero.Web;

public interface IWindow
{
    /// <summary>
    /// The current window handle
    /// </summary>
    public string CurrentHandle { get; }

    /// <summary>
    /// Get all available window handles
    /// </summary>
    public IEnumerable<string> Handles { get; }

    /// <summary>
    /// The size of the current <see cref="IWindow"/>
    /// </summary>
    public Size Size { get; }

    /// <summary>
    /// Set the size of the current <see cref="IWindow"/>
    /// </summary>
    /// <param name="width">The width of the window in pixels</param>
    /// <param name="height">The height of the window in pixels</param>
    public Task SetSize(int width, int height);

    /// <summary>
    /// Fullscreens the current <see cref="IWindow"/>
    /// </summary>
    public Task Fullscreen();
}
