using System.Threading.Tasks;

namespace StageZero.Web;

public interface IDriverWeb : IDriver 
{
    public string Title { get; }

    public string Url { get; }

    Task<IElementWeb> GetElement(string cssSelector);

    public Task GoTo(string url);

    public Task Terminate();
}
