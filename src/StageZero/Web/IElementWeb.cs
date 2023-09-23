using System;
using System.Threading.Tasks;

namespace StageZero.Web;

public interface IElementWeb : IElement
{
    /// <summary>
    /// Mimic a user "press" of the provided keys.
    /// </summary>
    /// <param name="keys">The keys to press</param>
    public Task PressKeys(Keys keys);

    /// <summary>
    /// Invoke a click event
    /// </summary>
    public Task Click();

    /// <summary>
    /// Invoke a right-click event
    /// </summary>
    public Task RightClick();

    /// <summary>
    /// Invoke a double-click event
    /// </summary>
    public Task DoubleClick();

    /// <summary>
    /// Click and hold the current element.
    /// </summary>
    /// <param name="duration">The duration to hold the click for</param>
    public Task ClickAndHold(TimeSpan duration);
}
