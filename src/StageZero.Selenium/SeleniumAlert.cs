using StageZero.Web;
using System.Threading.Tasks;

namespace StageZero.Selenium;

public class SeleniumAlert : IAlert
{
    private readonly OpenQA.Selenium.IAlert _seleniumAlert;

    public SeleniumAlert(OpenQA.Selenium.IAlert seleniumAlert)
    {
        _seleniumAlert = seleniumAlert;
    }

    /// <inheritdoc/>
    public string Message => _seleniumAlert.Text;

    /// <inheritdoc/>
    public Task Confirm()
    {
        return Task.Run(() => _seleniumAlert.Accept());
    }

    /// <inheritdoc/>
    public Task Dismiss()
    {
        return Task.Run(() => _seleniumAlert.Dismiss());
    }
}
