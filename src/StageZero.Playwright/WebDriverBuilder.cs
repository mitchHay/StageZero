using StageZero.Web;

namespace StageZero.Playwright;

public class WebDriverBuilder : IDriverBuilder
{
    public IDriver Create(DriverOptions options)
    {
        return new WebDriver((WebDriverOptions)options);
    }
}
