using System;
using System.Threading.Tasks;

namespace StageZero.Web;

public interface IElementWeb : IElement
{
    /// <summary>
    /// The elements class name
    /// </summary>
    public string ClassName { get; }

    /// <summary>
    /// The elements id
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The elements tag name
    /// </summary>
    public string Tag { get; }

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

    /// <summary>
    /// Get the value of a HTML attribute from its specified name
    /// </summary>
    /// <param name="attributeName">The HTML attribute name</param>
    /// <returns>The value of the HTML attribute</returns>
    public Task<string> GetAttributeValue(string attributeName);

    /// <summary>
    /// Scroll to and get the specified element
    /// </summary>
    /// <param name="cssSelector">The element to scroll to</param>
    /// <returns>The targeted <see cref="IElementWeb"/> instance.</returns>
    public Task<IElementWeb> ScrollTo(string cssSelector);
}
