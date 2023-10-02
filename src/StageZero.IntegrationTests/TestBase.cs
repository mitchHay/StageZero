namespace StageZero.IntegrationTests;

[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class TestBase
{
    public IDriverWeb Driver { get; private set; }

    public bool ShouldNavigateToTestSite { get; set; }

    [SetUp]
    public void BeforeEachTest()
    {
        Driver = DriverBuilder.Create(new WebDriverOptions
        {
            Headless = false
        });

        if (ShouldNavigateToTestSite)
        {
            var rootDirectory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("src"));
            var sitePath = Path.Join(rootDirectory, "test", "demo-site", "index.html");

            Driver.Navigate().ToUrl(sitePath);
        }
    }

    [TearDown]
    public async Task AfterEachTest()
    {
        await Driver.Terminate();
    }
}
