using StageZero.IntegrationTests.Helpers;

namespace StageZero.IntegrationTests;

// Parallelisation
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
// Fixtures
// [TestFixture(typeof(Playwright.WebDriverBuilder))]
// [TestFixture(typeof(Selenium.WebDriverBuilder))]
[TestFixture(typeof(Puppeteer.WebDriverBuilder))]
public class TestBase
{
    public IDriverWeb Driver { get; private set; }

    public bool ShouldNavigateToTestSite { get; set; }

    public string? TestSitePath { get; private set; }

    public bool Headless => AppSettings.Get<bool>("Headless");

    private readonly Type _driverBuilderType;

    public TestBase(Type driverBuilderType)
    {
        _driverBuilderType = driverBuilderType;
    }

    [SetUp]
    public void BeforeEachTest()
    {
        // Register the driver builder
        var registerMethod = typeof(DriverBuilder).GetMethod(nameof(DriverBuilder.Register));
        if (registerMethod == null)
        {
            Assert.Fail($"Failed to retrieve the {nameof(DriverBuilder.Register)} method from {nameof(DriverBuilder)}");
            return;
        }

        var registerGeneric = registerMethod.MakeGenericMethod(_driverBuilderType);
        registerGeneric.Invoke(this, null);

        // Create the driver
        Driver = DriverBuilder.Create(new WebDriverOptions
        {
            Headless = Headless
        });

        var rootDirectory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("src"));
        TestSitePath = $"file://{Path.Join(rootDirectory, "test", "demo-site", "index.html")}";

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
