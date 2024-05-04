using StageZero.Web;
using System;
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
    public string Message 
    {
        get
        {
            try
            {
                return _seleniumAlert.Text;
            }
            catch
            {
                Console.WriteLine("Something went wrong interacting with the requested alert element. Perhaps it's already closed?");
                return string.Empty;
            }
        }
    }

    /// <inheritdoc/>
    public Task Confirm()
    {
        return Task.Run(() => {
            try
            {
                _seleniumAlert.Accept();
            } 
            catch
            {
                Console.WriteLine("Something went wrong interacting with the requested alert element. Perhaps it's already closed?");
            }
        });
    }

    /// <inheritdoc/>
    public Task Dismiss()
    {
        return Task.Run(() => {
            try
            {
                _seleniumAlert.Dismiss();
            }
            catch
            {
                Console.WriteLine("Something went wrong interacting with the requested alert element. Perhaps it's already closed?");
            }
        });
    }
}
