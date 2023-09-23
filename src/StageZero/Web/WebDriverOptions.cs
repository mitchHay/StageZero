namespace StageZero.Web;

public class WebDriverOptions : DriverOptions
{
    public bool Headless { get; set; }

    public Browser Browser { get; set; }
}