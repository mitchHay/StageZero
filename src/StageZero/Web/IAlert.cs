using System.Threading.Tasks;

namespace StageZero.Web;

public interface IAlert
{
    /// <summary>
    /// The message content of the browser alert
    /// </summary>
    string Message { get; }

    /// <summary>
    /// Click the confirmation action of the browser alert
    /// </summary>
    Task Confirm();

    /// <summary>
    /// Click the dismiss action of the browser alert
    /// </summary>
    Task Dismiss();
}