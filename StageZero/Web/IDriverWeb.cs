namespace StageZero.Web;

public interface IDriverWeb : IDriver 
{
    public string Title { get; }

    public string Url { get; }
}
