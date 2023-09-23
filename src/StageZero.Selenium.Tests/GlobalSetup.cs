namespace StageZero.Selenium.Tests;

[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void BeforeEverything()
    {
        DriverBuilder.Register<WebDriverBuilder>();
    }
}
