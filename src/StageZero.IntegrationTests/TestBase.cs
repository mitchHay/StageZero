using System.Runtime.InteropServices;

namespace StageZero.IntegrationTests;

[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class TestBase
{
    public IDriverWeb Driver { get; private set; }

    public bool ShouldNavigateToTestSite { get; set; }

    public string? TestSitePath { get; private set; }

    [SetUp]
    public void BeforeEachTest()
    {
        Driver = DriverBuilder.Create(new WebDriverOptions
        {
            Headless = true
        });

        var rootDirectory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("src"));
        TestSitePath = Path.Join(rootDirectory, "test", "demo-site", "index.html");

        // On linux, opening files is a lil' different
        // Prepend the site path with file:// so that chrome can open it
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            TestSitePath = $"file://{TestSitePath}";
        }

        // In some test cases, we may not actually want to go to the test site
        if (ShouldNavigateToTestSite)
        {
            Driver.Navigate().ToUrl(TestSitePath);
        }
    }

    [TearDown]
    public async Task AfterEachTest()
    {
        await Driver.Terminate();
    }
}
