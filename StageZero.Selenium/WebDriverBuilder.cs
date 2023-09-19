using StageZero.Web;

namespace StageZero.Selenium;

public class WebDriverBuilder : IDriverBuilder
{
    IDriver IDriverBuilder.Create(DriverOptions options)
    {
        return new WebDriver((WebDriverOptions)options);
    }
}
