using System.Threading.Tasks;

namespace StageZero;

public interface IElement 
{
    /// <summary>
    /// Type the provided text into the current <see cref="IElement"/>
    /// </summary>
    /// <param name="text">The text to type</param>
    public Task Type(string text);
}