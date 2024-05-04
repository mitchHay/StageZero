using System.Threading.Tasks;

namespace StageZero.Web;

public interface INavigateContext 
{
    /// <summary>
    /// A browser based alert 
    /// </summary>
    /// <returns>An <see cref="IAlert"/> instance</returns>
    IAlert Alert();
}
