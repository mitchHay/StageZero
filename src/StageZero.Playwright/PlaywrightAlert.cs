using Microsoft.Playwright;
using StageZero.Web;
using System.Threading.Tasks;

namespace StageZero.Playwright;

public class PlaywrightAlert : IAlert
{
    private readonly IDialog _dialog;

    public PlaywrightAlert(IDialog dialog)
    {
        _dialog = dialog;
    }

    /// <inheritdoc/>
    public string Message => _dialog.Message;

    /// <inheritdoc/>
    public Task Confirm()
    {
        return _dialog.AcceptAsync();
    }

    /// <inheritdoc/>
    public Task Dismiss()
    {
        return _dialog.DismissAsync();
    }
}