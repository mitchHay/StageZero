namespace StageZero.Web;

public class WebDriverOptions : DriverOptions
{
    /// <summary>
    /// Whether to run the browser in a "headless" state
    /// </summary>
    public bool Headless { get; set; }

    /// <summary>
    /// The target browser
    /// </summary>
    public Browser Browser { get; set; }
    
    /// <summary>
    /// The target emulated device (e.g. iPhone 12 Pro)
    /// </summary>
    public string EmulatedDeviceName { get; set; }
}
