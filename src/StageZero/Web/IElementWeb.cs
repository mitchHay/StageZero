using System;
using System.Threading.Tasks;

namespace StageZero.Web;

public interface IElementWeb : IElement
{
    public Task Type(string text);

    public Task PressKeys(Keys keys);

    public Task Click();

    public Task RightClick();

    public Task DoubleClick();

    public Task ClickAndHold(TimeSpan duration);
}
