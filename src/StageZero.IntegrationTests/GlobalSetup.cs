namespace StageZero.IntegrationTests;

[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void BeforeEverything()
    {
        DriverBuilder.Register<WebDriverBuilder>();
    }
}
