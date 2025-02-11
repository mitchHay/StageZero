using StageZero.Web;

namespace StageZero.Puppeteer;

public class WebDriverBuilder : IDriverBuilder
{
    public IDriver Create(DriverOptions options)
    {
        return new WebDriver((WebDriverOptions)options);
    }
}
