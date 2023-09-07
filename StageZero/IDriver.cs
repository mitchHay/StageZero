using System.Threading.Tasks;

namespace StageZero;

public interface IDriver
{
    Task<IElement> GetElement(string cssSelector);
}
