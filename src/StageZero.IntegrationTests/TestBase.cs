using System.Runtime.InteropServices;

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
            Headless = true
        });

        if (ShouldNavigateToTestSite)
        {
            var rootDirectory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("src"));
            var sitePath = Path.Join(rootDirectory, "test", "demo-site", "index.html");

            // On linux, opening files is a lil' different
            // Prepend the site path with file:// so that chrome can open it
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                sitePath = $"file://{sitePath}";
            }

            Driver.Navigate().ToUrl(sitePath);
        }
    }

    [TearDown]
    public async Task AfterEachTest()
    {
        await Driver.Terminate();
    }
}
