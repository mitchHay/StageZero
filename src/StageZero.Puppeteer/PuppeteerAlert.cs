using System.Threading.Tasks;
using StageZero.Web;

namespace StageZero.Puppeteer;

public class PuppeteerAlert(PuppeteerSharp.Dialog dialog) : IAlert
{
    public string Message => dialog.Message;

    public Task Confirm() => dialog.Accept();

    public Task Dismiss() => dialog.Dismiss();
}